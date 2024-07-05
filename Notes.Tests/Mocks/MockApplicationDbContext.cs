using Microsoft.EntityFrameworkCore;
using Notes.Infrastructure;
using Moq;
using Notes.Core.Entities;


namespace Notes.Tests.Mocks;

public class MockApplicationDbContext
{
    public Mock<ApplicationDbContext> MockedContext = new();


    public MockApplicationDbContext SetEntities<TEntity>(List<TEntity> startData) where TEntity : BaseEntity
    {
        var mockSet = new Mock<DbSet<TEntity>>();
        mockSet.As<IQueryable<Tag>>().Setup(m => m.Provider).Returns(startData.AsQueryable().Provider);
        mockSet.As<IQueryable<Tag>>().Setup(m => m.Expression).Returns(startData.AsQueryable().Expression);
        mockSet.As<IQueryable<Tag>>().Setup(m => m.ElementType).Returns(startData.AsQueryable().ElementType);
        mockSet.As<IQueryable<Tag>>().Setup(m => m.GetEnumerator())
            .Returns(startData.AsQueryable().GetEnumerator() as IEnumerator<Tag>);

        mockSet.Setup(m => m.AsQueryable()).Returns(startData.AsQueryable());
        MockedContext.Setup(m => m.Set<TEntity>()).Returns(mockSet.Object);
        return this;
    }

}