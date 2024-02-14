using Application.Files.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Contracts;
using Entities.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WebFramework.Api
{
    [ApiVersion("1")]
    public class CrudController<TDto, TSelectDto, TEntity, TKey> : BaseController
        where TDto : BaseDto<TDto, TEntity, TKey>, new()
        where TSelectDto : BaseDto<TSelectDto, TEntity, TKey>, new()
        where TEntity : class, IEntity<TKey>, new()
    {
        protected readonly IRepository<TEntity> Repository;
        protected readonly IMapper Mapper;

        public CrudController(IRepository<TEntity> repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }
        /// <summary>
        /// دریافت تمامی اطلاعات
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        public virtual async Task<ActionResult<List<TSelectDto>>> Get(CancellationToken cancellationToken)
        {
            var list = await Repository.TableNoTracking.ProjectTo<TSelectDto>(Mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return Ok(list);
        }
        /// <summary>
        /// ریافت اطلاعات با آیدی
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public virtual async Task<ApiResult<TSelectDto>> Get(TKey id, CancellationToken cancellationToken)
        {
            var dto = await Repository.TableNoTracking.ProjectTo<TSelectDto>(Mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(p => p.Id.Equals(id), cancellationToken);

            if (dto == null)
                return NotFound();

            return dto;
        }
        /// <summary>
        /// دریافت اطلاعات با بجینیشن
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public virtual async Task<ApiResult<GlobalGridResult<TSelectDto>>> GetByPage([FromQuery] GlobalGrid input)
        {
            var skip = (input.PageNumber!.Value - 1) * input.Count!.Value;

            var data = await Repository.TableNoTracking.ProjectTo<TSelectDto>(Mapper.ConfigurationProvider)
                .Skip(skip)
                .Take(input.Count.Value)
                .ToListAsync();

            var totalCount = await Repository.Table.CountAsync();
            return Ok(new GlobalGridResult<TSelectDto>
            {
                TotalCount = totalCount,
                Data = data
            });
        }
        /// <summary>
        /// ایجاد یک انتیتی جدید
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<ApiResult<TSelectDto>> Create(TDto dto, CancellationToken cancellationToken)
        {
            var model = dto.ToEntity(Mapper);

            await Repository.AddAsync(model, cancellationToken);

            var resultDto = await Repository.TableNoTracking.ProjectTo<TSelectDto>(Mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(p => p.Id.Equals(model.Id), cancellationToken);

            return resultDto;
        }

        /// <summary>
        /// ویرایش
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut]
        public virtual async Task<ApiResult<TSelectDto>> Update(TKey id, TDto dto, CancellationToken cancellationToken)
        {
            var model = await Repository.GetByIdAsync(cancellationToken, id);

            model = dto.ToEntity(Mapper, model);

            await Repository.UpdateAsync(model, cancellationToken);

            var resultDto = await Repository.TableNoTracking.ProjectTo<TSelectDto>(Mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(p => p.Id.Equals(model.Id), cancellationToken);

            return resultDto;
        }

        /// <summary>
        /// حذف
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public virtual async Task<ApiResult> Delete(TKey id, CancellationToken cancellationToken)
        {
            var model = await Repository.GetByIdAsync(cancellationToken, id);

            await Repository.SoftDeleteAsync(model, cancellationToken);

            return Ok();
        }
    }

    public class CrudController<TDto, TSelectDto, TEntity> : CrudController<TDto, TSelectDto, TEntity, int>
        where TDto : BaseDto<TDto, TEntity, int>, new()
        where TSelectDto : BaseDto<TSelectDto, TEntity, int>, new()
        where TEntity : class, IEntity<int>, new()
    {
        public CrudController(IRepository<TEntity> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }

    public class CrudController<TDto, TEntity> : CrudController<TDto, TDto, TEntity, int>
        where TDto : BaseDto<TDto, TEntity, int>, new()
        where TEntity : class, IEntity<int>, new()
    {
        public CrudController(IRepository<TEntity> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
