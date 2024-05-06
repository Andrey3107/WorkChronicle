namespace WorkChronicle.Utilities.Mapper
{
    using CodeFirst.Models.Entities;

    using ViewModels;

    public interface IMapper
    {
        Project Map(EditProjectViewModel model);
    }
}
