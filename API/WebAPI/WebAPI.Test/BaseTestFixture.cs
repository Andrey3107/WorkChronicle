namespace WebAPI.Test
{
    using System.Data.Entity.Infrastructure;
    using System.Linq;

    using CodeFirst.Enums;
    using CodeFirst.Interfaces;

    using Moq;

    using TestConfig;

    public class BaseTestFixture
    {
        protected IQueryable<T> CreateGetAsQueryable<T>(IQueryable<T> values)
        {
            var queryableDbResult = new Mock<IQueryable<T>>();

            queryableDbResult.As<IDbAsyncEnumerable<T>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<T>(values.GetEnumerator()));
            queryableDbResult.Setup(x => x.Provider).Returns(() => new TestDbAsyncQueryProvider<T>(values.Provider));
            queryableDbResult.Setup(x => x.Expression).Returns(() => values.Expression);
            queryableDbResult.Setup(x => x.ElementType).Returns(() => values.ElementType);
            queryableDbResult.Setup(x => x.GetEnumerator()).Returns(values.GetEnumerator);

            return queryableDbResult.Object;
        }

        protected virtual void MockTransaction(Mock<IUnitOfWork> unitOfWork)
        {
            var transaction = new Mock<ITransaction>();
            unitOfWork.Setup(u => u.BeginTransaction(It.IsAny<IsolationLevel>())).Returns(() => transaction.Object);
        }
    }
}
