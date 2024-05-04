namespace CodeFirst
{
    using System;
    using System.Data.Entity;
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    using Enums;

    using Extensions;

    using Interfaces;
    using Interfaces.WorkChronicle;

    using Repositories;
    using Repositories.WorkChronicle;

    using Utilities;
    using Utilities.Interfaces;

    public class UnitOfWork : IUnitOfWork
    {
        #region PrivateFields

        private ITestTableRepository testTableRepository;

        private IUserRepository userRepository;

        private IRoleRepository roleRepository;

        private IUserRoleRepository userRoleRepository;

        private IPlaceRepository placeRepository;

        private IPositionRepository positionRepository;

        private IProjectRepository projectRepository;

        private IPriorityRepository priorityRepository;

        private IProjectStatusRepository projectStatusRepository;

        private ITicketRepository ticketRepository;

        private ITicketStatusRepository ticketStatusRepository;

        private ITicketTypeRepository ticketTypeRepository;

        private ITimeTrackRepository timeTrackRepository;

        private IUserToProjectRepository userToProjectRepository;

        #endregion

        private readonly int retryAttempts;

        private readonly TimeSpan retryDelay;

        private ApplicationDbContext context;

        private IBeginTransactionUtility beginTransactionUtility;

        private bool isDisposed;

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            isDisposed = false;
            retryAttempts = 5;
            retryDelay = TimeSpan.FromSeconds(5);
            beginTransactionUtility = new BeginTransactionUtility();
        }

        public UnitOfWork()
            : this(new ApplicationDbContext())
        {
        }

        #region Repositories

        public ITestTableRepository TestTableRepository => testTableRepository ??= new TestTableRepository(context);

        public IUserRepository UserRepository => userRepository ??= new UserRepository(context);

        public IRoleRepository RoleRepository => roleRepository ??= new RoleRepository(context);

        public IUserRoleRepository UserRoleRepository => userRoleRepository ??= new UserRoleRepository(context);

        public IPlaceRepository PlaceRepository => placeRepository ??= new PlaceRepository(context);

        public IPriorityRepository PriorityRepository => priorityRepository ??= new PriorityRepository(context);

        public IPositionRepository PositionRepository => positionRepository ??= new PositionRepository(context);

        public IProjectRepository ProjectRepository => projectRepository ??= new ProjectRepository(context);

        public IProjectStatusRepository ProjectStatusRepository => projectStatusRepository ??= new ProjectStatusRepository(context);

        public ITicketRepository TicketRepository => ticketRepository ??= new TicketRepository(context);

        public ITicketStatusRepository TicketStatusRepository => ticketStatusRepository ??= new TicketStatusRepository(context);

        public ITicketTypeRepository TicketTypeRepository => ticketTypeRepository ??= new TicketTypeRepository(context);

        public ITimeTrackRepository TimeTrackRepository => timeTrackRepository ??= new TimeTrackRepository(context);

        public IUserToProjectRepository UserToProjectRepository => userToProjectRepository ??= new UserToProjectRepository(context);

        #endregion

        public void Save(bool retryOnException = false)
        {
            var retryAttemptsCount = 0;

            do
            {
                try
                {
                    retryAttemptsCount++;

                    context.SaveChanges();
                    break;
                }
                catch (Exception exception)
                {
                    var originalException = exception.GetLastException() as SqlException;

                    if (originalException != null && originalException.Number == 1205)
                    {
                        // Deadlock
                        if (retryOnException && retryAttemptsCount < retryAttempts)
                        {
                            Task.Delay(retryDelay).Wait();
                            continue;
                        }
                    }

                    throw;
                }
            }
            while (true);
        }

        public async Task SaveAsync(bool retryOnException = false)
        {
            var retryAttemptsCount = 0;

            do
            {
                try
                {
                    retryAttemptsCount++;

                    await context.SaveChangesAsync();
                    break;
                }
                catch (Exception exception)
                {
                    var originalException = exception.GetLastException() as SqlException;

                    if (originalException != null && originalException.Number == 1205)
                    {
                        // Deadlock
                        if (retryOnException && retryAttemptsCount < retryAttempts)
                        {
                            Task.Delay(retryDelay).Wait();
                            continue;
                        }
                    }

                    throw;
                }
            }
            while (true);
        }

        public ITransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            return beginTransactionUtility.BeginTransaction(context, isolationLevel);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void DetachAllEntities()
        {
            foreach (var entry in context.ChangeTracker.Entries())
            {
                context.Entry(entry.Entity).State = EntityState.Detached;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    context.Dispose();
                    context = null;
                }
            }

            isDisposed = true;
        }
    }
}
