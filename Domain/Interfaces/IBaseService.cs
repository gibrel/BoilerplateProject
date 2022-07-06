using FluentValidation;
using Boilerplate.Domain.Entities;

namespace Boilerplate.Domain.Interfaces
{
    public interface IBaseService<TEntity> where TEntity : BaseEntity
    {
        public abstract Task<TOutputModel> Add<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
            where TValidator : AbstractValidator<TEntity>
            where TInputModel : class
            where TOutputModel : class;
        public abstract Task<List<TOutputModel>> GetAll<TOutputModel>() where TOutputModel : class;
        public abstract Task<TOutputModel> GetById<TOutputModel>(int id) where TOutputModel : class;
        public abstract Task<TOutputModel> Update<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
            where TValidator : AbstractValidator<TEntity>
            where TInputModel : class
            where TOutputModel : class;
        public abstract Task<bool> Delete(int id);
    }
}
