using AutoMapper;
using FluentValidation;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Interfaces;

namespace Boilerplate.Service.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        protected readonly IBaseRepository<TEntity> _baseRepository;
        protected readonly IMapper _mapper;

        public BaseService(IBaseRepository<TEntity> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public virtual async Task<TOutputModel> Add<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
            where TValidator : AbstractValidator<TEntity>
            where TInputModel : class
            where TOutputModel : class
        {
            TEntity entity = _mapper.Map<TEntity>(inputModel);

            Validate(entity, Activator.CreateInstance<TValidator>());
            await _baseRepository.Insert(entity);

            TOutputModel outputModel = _mapper.Map<TOutputModel>(entity);

            return outputModel;
        }

        public virtual async Task<List<TOutputModel>> GetAll<TOutputModel>() where TOutputModel : class
        {
            var entities = await _baseRepository.Select();

            var outputModels = entities.Select(s => _mapper.Map<TOutputModel>(s)).ToList();

            return outputModels;
        }

        public virtual async Task<TOutputModel> GetById<TOutputModel>(int id) where TOutputModel : class
        {
            var entity = await _baseRepository.Select(id);

            var outputModel = _mapper.Map<TOutputModel>(entity);

            return outputModel;
        }

        public virtual async Task<TOutputModel> Update<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
            where TInputModel : class
            where TOutputModel : class
            where TValidator : AbstractValidator<TEntity>
        {
            TEntity entity = _mapper.Map<TEntity>(inputModel);

            Validate(entity, Activator.CreateInstance<TValidator>());
            await _baseRepository.Update(entity);

            TOutputModel outputModel = _mapper.Map<TOutputModel>(entity);

            return outputModel;
        }

        public virtual async Task<bool> Delete(int id) => await _baseRepository.Delete(id);

        protected void Validate(TEntity obj, AbstractValidator<TEntity> validator)
        {
            if (obj == null)
                throw new Exception("Registry not detected!");

            validator.ValidateAndThrow(obj);
        }
    }
}
