namespace WorkChronicle.Utilities.Mapper
{
    using System;

    using CodeFirst.Models.Entities;

    using Models.Tasks;

    using ViewModels;
    using ViewModels.AccessManagement;
    using ViewModels.Tasks;

    public class Mapper : IMapper
    {
        public Project Map(EditProjectViewModel model)
        {
            return new Project { Id = model.Id, Name = model.ProjectName };
        }

        public AssigneeModel Map(User user)
        {
            return new AssigneeModel { Id = user.Id, FullName = $"{user.FirstName} {user.LastName}" };
        }

        public Ticket Map(TaskViewModel model)
        {
            return new Ticket
            {
                Id = model.Id,
                Name = model.TaskName,
                Description = model.Description,
                Created = model.StartDate,
                DueDate = model.EndDate,
                Estimate = model.Estimate,
                TicketStatusId = model.TicketStatusId,
                Completeness = model.Completeness,
                ProjectId = model.ProjectId,
                TypeId = model.TicketTypeId,
                PriorityId = model.PriorityId,
                AssigneeId = model.AssigneeId
            };
        }

        public UserViewModel MapUser(User model)
        {
            return new UserViewModel
            {
                Id = model.Id,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
        }

        public TimeTrack Map(TimeTrackViewModel model, long assigneeId)
        {
            return new TimeTrack
            {
                Duration = model.Duration,
                Comment = model.Comment,
                TicketId = model.TicketId,
                PlaceId = model.PlaceId,
                AssigneeId = assigneeId
            };
        }
    }
}
