namespace WorkChronicle.Utilities.Mapper
{
    using System;

    using CodeFirst.Models.Entities;

    using Models.Tasks;

    using ViewModels;
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
                Name = model.TaskName,
                Description = model.Description,
                Created = DateTime.Now,
                Estimate = model.Estimate,
                TicketStatusId = model.TicketStatusId,
                Completeness = model.Completeness,
                ProjectId = model.ProjectId,
                TypeId = model.TicketTypeId,
                PriorityId = model.PriorityId,
                AssigneeId = model.AssigneeId
            };
        }
    }
}
