using System.Collections.Generic;
using System.Linq;
using DAO.Models;
using DAO.Repositories;
using DAO.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace DAO.Tests.ServicesTests
{
    public class PostsServiceTests : IClassFixture<DIFixture>
    {
        private readonly DIFixture _dIFixture;

        public PostsServiceTests(DIFixture fixture)
        {
            _dIFixture = fixture;
        }

        [Fact]
        public void GetAll()
        {
            var expected = new List<Post>
            {
                new Post
                {
                    Id = 1,
                    UserId = 1,
                    Content = "content",
                    Title = "title",
                    Comments = null,
                },
                new Post
                {
                    Id = 2,
                    UserId = 2,
                    Content = "content2",
                    Title = "title2",
                    Comments = null,
                }
            };
            var mockRepository = new Mock<IPostsRepository>();
            mockRepository.Setup(m => m.GetAll()).Returns(() => expected.AsQueryable());

            _dIFixture.DIContainer.Configure(c => c.ExportInstance(mockRepository.Object).As<IPostsRepository>());

            var service = _dIFixture.DIContainer.Locate<PostsService>();
            service.GetAll().ShouldAllBeEquivalentTo(expected);
        }
    }
}
