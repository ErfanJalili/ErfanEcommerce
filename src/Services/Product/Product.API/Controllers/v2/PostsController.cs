﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Product.API.Models;
using Product.Entities.Posts;
using Product.Infrastructure.Contracts;
using Product.WebFramework.Api;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Product.API.Controllers.v2
{
    [ApiVersion("2")]
    public class PostsController : v1.PostsController
    {
        public PostsController(IRepository<Post> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }

        public override Task<ApiResult<PostSelectDto>> Create(PostDto dto, CancellationToken cancellationToken)
        {
            return base.Create(dto, cancellationToken);
        }

        [NonAction]
        public override Task<ApiResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            return base.Delete(id, cancellationToken);
        }

        public async override Task<ActionResult<List<PostSelectDto>>> Get(CancellationToken cancellationToken)
        {
            return await Task.FromResult(new List<PostSelectDto>
            {
                new PostSelectDto
                {
                     FullTitle = "FullTitle",
                     AuthorFullName =  "AuthorFullName",
                     CategoryName = "CategoryName",
                     Description = "Description",
                     Title = "Title",
                }
            });
        }

        public async override Task<ApiResult<PostSelectDto>> Get(Guid id, CancellationToken cancellationToken)
        {
            if (Guid.Empty == id)
                return NotFound();
            return await base.Get(id, cancellationToken);
        }

        [HttpGet("Test")]
        public ActionResult Test()
        {
            return Content("This is test");
        }

        public override Task<ApiResult<PostSelectDto>> Update(Guid id, PostDto dto, CancellationToken cancellationToken)
        {
            return base.Update(id, dto, cancellationToken);
        }
    }
}
