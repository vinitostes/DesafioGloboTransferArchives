using FluentValidation;
using FluentValidation.Results;
using System;

namespace DesafioGlobo.Domain
{
    public abstract class Entity<T> : AbstractValidator<T> where T : Entity<T>
    {
        public ValidationResult ValidationResult { get; protected set; }

        public abstract bool EsValid();

        protected Entity()
        {
            ValidationResult = new ValidationResult();
        }
    }
}
