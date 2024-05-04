namespace CodeFirst.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using Enums;

    using WorkChronicle;

    public interface IUnitOfWork : IDisposable
    {
        ITestTableRepository TestTableRepository { get; }

        IUserRepository UserRepository { get; }

        IRoleRepository RoleRepository { get; }

        IUserRoleRepository UserRoleRepository { get; }

        IPlaceRepository PlaceRepository { get; }

        IPositionRepository PositionRepository { get; }

        IPriorityRepository PriorityRepository { get; }

        IProjectRepository ProjectRepository { get; }

        IProjectStatusRepository ProjectStatusRepository { get; }

        ITicketRepository TicketRepository { get; }

        ITicketStatusRepository TicketStatusRepository { get; }

        ITicketTypeRepository TicketTypeRepository { get; }

        ITimeTrackRepository TimeTrackRepository { get; }

        IUserToProjectRepository UserToProjectRepository { get; }

        void Save(bool retryOnException = false);

        Task SaveAsync(bool retryOnException = false);

        ITransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

        void DetachAllEntities();
    }
}
