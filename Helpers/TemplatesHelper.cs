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

        public async Task<(bool Success, string ErrorMessage)> CreateNewProjectTemplateAsync(Project project, int courseId)
        {
            IClientSessionHandle? session = null; // Nullable session for safe handling
            bool isReplicaSet = false;

            try
            {
                // Check if the MongoDB server supports transactions
                isReplicaSet = _mongoDbContext.GetMongoClusterType() == MongoDB.Driver.Core.Clusters.ClusterType.ReplicaSet;

                if (isReplicaSet)
                {
                    session = await _mongoDbContext.StartSessionAsync();
                    session.StartTransaction();
                }

                // Step 1: Insert the provided project into MongoDB
                if (isReplicaSet && session != null)
                {
                    await _mongoDbContext.Projects.InsertOneAsync(session, project);
                }
                else
                {
                    await _mongoDbContext.Projects.InsertOneAsync(project);
                }

                // Step 2: Retrieve the MongoId of the newly created project
                var mongoId = project.Id;
                if (string.IsNullOrEmpty(mongoId))
                {
                    if (isReplicaSet && session != null)
                    {
                        await session.AbortTransactionAsync();
                    }
                    return (false, "Failed to retrieve MongoId for the new project.");
                }

                // Step 3: Create a template from the newly created project
                var result = await CreateTemplateFromProjectAsync(mongoId, courseId, session, isReplicaSet);

                if (isReplicaSet && session != null)
                {
                    if (result.Success)
                    {
                        await session.CommitTransactionAsync();
                    }
                    else
                    {
                        await session.AbortTransactionAsync();
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                if (isReplicaSet && session != null && session.IsInTransaction)
                {
                    await session.AbortTransactionAsync();
                }
                return (false, ex.Message);
            }
            finally
            {
                session?.Dispose();
            }
        }

        public async Task<(bool Success, string ErrorMessage)> CreateTemplateFromProjectAsync(
     string projectId,
     int courseId,
     IClientSessionHandle? session,
     bool isReplicaSet)
        {
            try
            {
                // Step 1: Fetch the project from the Projects collection
                var project = isReplicaSet && session != null
                    ? await _mongoDbContext.Projects.Find(session, p => p.Id == projectId).FirstOrDefaultAsync()
                    : await _mongoDbContext.Projects.Find(p => p.Id == projectId).FirstOrDefaultAsync();

                if (project == null)
                {
                    return (false, "Project not found in MongoDB.");
                }

                // Step 2: Insert the project into the TemplateProjects collection
                if (isReplicaSet && session != null)
                {
                    await _mongoDbContext.TemplateProjects.InsertOneAsync(session, project);
                }
                else
                {
                    await _mongoDbContext.TemplateProjects.InsertOneAsync(project);
                }

                // Step 3: Remove the project from the Projects collection
                if (isReplicaSet && session != null)
                {
                    await _mongoDbContext.Projects.DeleteOneAsync(session, p => p.Id == projectId);
                }
                else
                {
                    await _mongoDbContext.Projects.DeleteOneAsync(p => p.Id == projectId);
                }

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

                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }


        public async Task<(bool Success, string ErrorMessage)> CreateTemplateFromProjectAsync(string projectId, int courseId)
        {
            IClientSessionHandle? session = null; // Nullable session for safe handling
            bool isReplicaSet = false;

            try
            {
                // Check if the MongoDB server supports transactions
                isReplicaSet = _mongoDbContext.GetMongoClusterType() == MongoDB.Driver.Core.Clusters.ClusterType.ReplicaSet;

                if (isReplicaSet)
                {
                    session = await _mongoDbContext.StartSessionAsync();
                    session.StartTransaction();
                }

                // Step 1: Fetch the project from the Projects collection
                var project = isReplicaSet && session != null
                    ? await _mongoDbContext.Projects.Find(session, p => p.Id == projectId).FirstOrDefaultAsync()
                    : await _mongoDbContext.Projects.Find(p => p.Id == projectId).FirstOrDefaultAsync();

                if (project == null)
                {
                    if (isReplicaSet && session != null)
                    {
                        await session.AbortTransactionAsync();
                    }
                    return (false, "Project not found in MongoDB.");
                }

                // Step 2: Insert the project into the TemplateProjects collection
                if (isReplicaSet && session != null)
                {
                    await _mongoDbContext.TemplateProjects.InsertOneAsync(session, project);
                }
                else
                {
                    await _mongoDbContext.TemplateProjects.InsertOneAsync(project);
                }

                // Step 3: Remove the project from the Projects collection
                if (isReplicaSet && session != null)
                {
                    await _mongoDbContext.Projects.DeleteOneAsync(session, p => p.Id == projectId);
                }
                else
                {
                    await _mongoDbContext.Projects.DeleteOneAsync(p => p.Id == projectId);
                }

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
                    if (isReplicaSet && session != null)
                    {
                        await session.AbortTransactionAsync();
                    }
                    return (false, "Course not found in MySQL.");
                }

                var courseTemplate = new CourseTemplate
                {
                    CourseId = course.Id,
                    TemplateId = template.Id
                };

                _dbContext.CourseTemplates.Add(courseTemplate);
                await _dbContext.SaveChangesAsync();

                if (isReplicaSet && session != null)
                {
                    await session.CommitTransactionAsync();
                }

                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                if (isReplicaSet && session != null && session.IsInTransaction)
                {
                    await session.AbortTransactionAsync();
                }
                return (false, ex.Message);
            }
            finally
            {
                session?.Dispose();
            }
        }



    }
}
