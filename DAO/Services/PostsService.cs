using DAO.Models;
using DAO.Repositories;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DAO.DTO;

namespace DAO.Services
{
    public class PostsService : IPostsService
    {
        private readonly IPostsRepository _postRepository;
        private readonly IMapper _mapper;

        public PostsService(IPostsRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public IEnumerable<PostDto> GetAll()
        {
            return _postRepository.GetAll().AsEnumerable().Select(_mapper.Map<Post, PostDto>).ToList();
        }

        public IEnumerable<PostDto> GetAllByUserId(int id)
        {
            return _postRepository.GetAllByUserId(id).AsEnumerable().Select(_mapper.Map<Post, PostDto>).ToList();
        }

        public PostDto GetById(int id)
        {
            return _mapper.Map<Post, PostDto>(_postRepository.GetById(id));
        }

        public void Create(PostDto post)
        {
            _postRepository.Create(_mapper.Map<PostDto, Post>(post));
            _postRepository.Save();
        }

        public void Update(PostDto post)
        {
            _postRepository.Update(_mapper.Map<PostDto, Post>(post));
            _postRepository.Save();
        }

        public void Delete(int id)
        {
            _postRepository.Delete(id);
            _postRepository.Save();
        }
    }
}
