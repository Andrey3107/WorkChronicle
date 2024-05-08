namespace WorkChronicle.ViewModels
{
    using System.Collections.Generic;

    using CodeFirst.Models.Entities;

    public class ProjectListViewModel
    {
        public List<CodeFirst.Models.Entities.Project> Projects { get; set; }

        public List<string> Tasks { get; set; }
    }
}
