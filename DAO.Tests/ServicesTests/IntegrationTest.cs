using DAO.Contexts;
using DAO.Services;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using DAO.DTO;
using Xunit;

namespace DAO.Tests.ServicesTests
{
    public class XIntegrationTest : IClassFixture<DIFixture>, IDisposable
    {
        private readonly DIFixture _dIFixture;
        private Stack<Action> _actions;


        public XIntegrationTest(DIFixture fixture)
        {
            _dIFixture = fixture;
            _actions = new Stack<Action>();
        }

        public void Dispose()
        {
            foreach (var action in _actions)
            {
                action();
            }
        }

        [Fact]
        public void TestCreatePost()
        {
            var dbcontext = _dIFixture.DIContainer.Locate<BlogContext>();

            var user = dbcontext.Users.First();
            var post = new PostDto
            {
                Title = Guid.NewGuid().ToString(),
                Content = Guid.NewGuid().ToString(),
                UserId = user.Id
            };

            _actions.Push(
                () =>
                {
                    var findPost = dbcontext.Posts.FirstOrDefault(p => p.Title == post.Title);
                    if (findPost != null)
                    {
                        dbcontext.Posts.Remove(findPost);
                        dbcontext.SaveChanges();
                    }
                });

            var postService = _dIFixture.DIContainer.Locate<PostsService>();
            postService.Create(post);

            dbcontext.Posts.Should().Contain(x => x.Title == post.Title);
        }
    }
}
