using AutoMapper;
using Product.API.Models;
using Product.Entities.Posts;
using Product.Infrastructure.Contracts;
using Product.WebFramework.Api;
using System;

namespace Product.API.Controllers.v1
{
    /// <summary>
    /// کنترلر پست ها
    /// </summary>
    public class PostsController : CrudController<PostDto, PostSelectDto, Post, Guid>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public PostsController(IRepository<Post> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
