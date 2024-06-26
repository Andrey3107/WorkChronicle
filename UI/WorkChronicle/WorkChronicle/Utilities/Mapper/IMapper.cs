﻿namespace WorkChronicle.Utilities.Mapper
{
    using CodeFirst.Models.Entities;

    using Models.Tasks;

    using ViewModels;
    using ViewModels.AccessManagement;
    using ViewModels.Tasks;

    public interface IMapper
    {
        Project Map(EditProjectViewModel model);

        AssigneeModel Map(User user);

        Ticket Map(TaskViewModel model);

        UserViewModel MapUser(User model);

        TimeTrack Map(TimeTrackViewModel model, long assigneeId);
    }
}
