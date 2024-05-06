namespace WorkChronicle.Utilities.Mapper
{
    using CodeFirst.Models.Entities;

    using Models.Tasks;

    using ViewModels;
    using ViewModels.Tasks;

    public interface IMapper
    {
        Project Map(EditProjectViewModel model);

        AssigneeModel Map(User user);

        Ticket Map(TaskViewModel model);
    }
}
