using System;
using System.Collections.Generic;
using System.Linq;
using DAO.Models;
using DAO.Repositories;

namespace DAO.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly ICommentsRepository _commentsRepository;

        public CommentsService(ICommentsRepository commentsRepository)
        {
            _commentsRepository = commentsRepository;
        }

        public IEnumerable<Comment> GetAll()
        {
            return _commentsRepository.GetAll().ToList();
        }

        public IEnumerable<Comment> GetAllByPostId(int id)
        {
            return _commentsRepository.GetAllByPostId(id).ToList();
        }

        public Comment GetById(int id)
        {
            return _commentsRepository.GetById(id);
        }

        public void Create(Comment comment)
        {
            comment.DateTime = DateTimeOffset.UtcNow;
            _commentsRepository.Create(comment);
            _commentsRepository.Save();
        }

        public void Update(Comment comment)
        {
            _commentsRepository.Update(comment);
            _commentsRepository.Save();
        }

        public void Delete(int id)
        {
            _commentsRepository.Delete(id);
            _commentsRepository.Save();
        }
    }
}
