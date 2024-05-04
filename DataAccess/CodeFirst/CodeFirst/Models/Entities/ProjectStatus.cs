namespace CodeFirst.Models.Entities
{
    using System.Collections.Generic;

    public class ProjectStatus : IEntity<int>
    {
        public int Id { get; set; }

        public string Description { get; set; }

        #region Navigation properties

        public virtual List<Project> Projects { get; set; }

        #endregion
    }
}
