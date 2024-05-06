namespace WorkChronicle.Utilities.Mapper
{
    using CodeFirst.Models.Entities;

    using ViewModels;

    public class Mapper : IMapper
    {
        public Project Map(EditProjectViewModel model)
        {
            return new Project { Id = model.Id, Name = model.ProjectName };
        }
    }
}
