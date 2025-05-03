using MongoDB.Bson;
using MongoDB.Driver;
using SnapSaves.Data;
using SnapSaves.Models;

namespace SnapSaves.Helpers
{
    public class TemplateHelper
    {
        private readonly MongoDbContext _mongoDbContext;
        private readonly AppIdentityDbContext _dbContext;

        public TemplateHelper(MongoDbContext mongoDbContext, AppIdentityDbContext dbContext)
        {
            _mongoDbContext = mongoDbContext;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Creates a new project in MongoDB, retrieves its MongoId, and creates a template from it.
        /// </summary>
        /// <param name="project">The project object to insert into MongoDB.</param>
        /// <param name="courseId">The ID of the course to assign the template to.</param>
        /// <returns>A boolean indicating success or failure, and an error message if applicable.</returns>
        public async Task<(bool Success, string ErrorMessage)> CreateNewProjectTemplateAsync(Project project, int courseId)
        {
            try
            {
                // Step 1: Insert the provided project into MongoDB
                await _mongoDbContext.Projects.InsertOneAsync(project);

                // Step 2: Retrieve the MongoId of the newly created project
                var mongoId = project.Id;
                if (string.IsNullOrEmpty(mongoId))
                {
                    return (false, "Failed to retrieve MongoId for the new project.");
                }

                // Step 3: Create a template from the newly created project
                return await CreateTemplateFromProjectAsync(mongoId, courseId);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }


        /// <summary>
        /// Creates a template from an existing MongoDB project, assigns it to a course, and updates the MySQL database.
        /// </summary>
        /// <param name="projectId">The MongoDB ID of the project to use as a template.</param>
        /// <param name="courseId">The ID of the course to assign the template to.</param>
        /// <returns>A boolean indicating success or failure, and an error message if applicable.</returns>
        public async Task<(bool Success, string ErrorMessage)> CreateTemplateFromProjectAsync(string projectId, int courseId)
        {
            using var session = await _mongoDbContext.StartSessionAsync();

            session.StartTransaction();

            try
            {
                // Step 1: Fetch the project from the Projects collection
                var project = await _mongoDbContext.Projects
                    .Find(p => p.Id == projectId)
                    .FirstOrDefaultAsync();

                if (project == null)
                {
                    return (false, "Project not found in MongoDB.");
                }

                // Step 2: Insert the project into the TemplateProjects collection
                await _mongoDbContext.TemplateProjects.InsertOneAsync(session, project);

                // Step 3: Remove the project from the Projects collection
                await _mongoDbContext.Projects.DeleteOneAsync(session, p => p.Id == projectId);

                // Step 4: Create a new template in MySQL
                var template = new Template
                {
                    MongoId = project.Id,
                    Name = project.Name,
                    Description = "Template created from project: " + project.Name
                };

                _dbContext.Templates.Add(template);
                await _dbContext.SaveChangesAsync();

                // Step 5: Assign the template to the course
                var course = await _dbContext.Courses.FindAsync(courseId);
                if (course == null)
                {
                    return (false, "Course not found in MySQL.");
                }

                var courseTemplate = new CourseTemplate
                {
                    CourseId = course.Id,
                    TemplateId = template.Id
                };

                _dbContext.CourseTemplates.Add(courseTemplate);
                await _dbContext.SaveChangesAsync();

                // Commit the MongoDB transaction
                await session.CommitTransactionAsync();

                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                // Abort the MongoDB transaction in case of an error
                await session.AbortTransactionAsync();
                return (false, ex.Message);
            }
        }

    }
}
