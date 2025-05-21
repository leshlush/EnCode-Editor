using System.Collections.Generic;
using SnapSaves.Models;

namespace SnapSaves.Models.ViewModels
{
    public class TemplateDetailsViewModel
    {
        public Template Template { get; set; }
        public List<Project> UserProjects { get; set; } = new();
        public int CourseId { get; set; }
    }
}
