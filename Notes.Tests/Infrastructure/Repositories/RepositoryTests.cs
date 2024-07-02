// using FluentAssertions;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
// using Moq;
// using Xunit;
// using Notes.Core.Entities;
// using Notes.Infrastructure;
// using Notes.Core.Interfaces.IRepositories;
// using Notes.Infrastructure.Repositories;
// using Notes.Tests.Infrastructure.Repositories.Mocks;
// namespace Notes.Tests.Infrastructure.Repositories;
//
// public class TagRepositoryTests
// {
//
//     public TagRepositoryTests()
//     {
//         
//     }
//
//     [Fact]
//     public async Task CreateTagsAsync()
//     {
//         var mockSet = new Mock<DbSet<Tag>>();
//         var tagsInDb = new List<Tag>  {
//             new Tag { Title = "tag1" },
//             new Tag { Title = "tag2" },
//             new Tag { Title = "tag3" },
//         };
//
//         mockSet.As<IQueryable<Tag>>().Setup(m => m.Provider).Returns(tagsInDb.AsQueryable().Provider);
//         mockSet.As<IQueryable<Tag>>().Setup(m => m.Expression).Returns(tagsInDb.AsQueryable().Expression);
//         mockSet.As<IQueryable<Tag>>().Setup(m => m.ElementType).Returns(tagsInDb.AsQueryable().ElementType);
//         mockSet.As<IQueryable<Tag>>().Setup(m => m.GetEnumerator()).Returns(tagsInDb.AsQueryable().GetEnumerator());
//         Mock<ApplicationDbContext> mockApplicationDbContext = new Mock<ApplicationDbContext>();
//         mockApplicationDbContext.Setup(c => c.Set<Tag>()).Returns(mockSet.Object);
//         
//         
//         TagRepository tagRepository = new TagRepository(mockApplicationDbContext.Object);
//         ICollection<string> tags = new List<string>
//         {
//             "tag1", "tag2", "tag3", "tag4"
//         };
//         IEnumerable<Tag> existing;
//         IEnumerable<Tag> nonExisting;
//         
//         (existing, nonExisting) = await tagRepository.CheckTagsTitlesAsync(tags);
//         nonExisting.Should().BeEquivalentTo(new List<Tag> { new Tag { Title = "tag4" } });
//     }
//     
//
// }