using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioGlobo.Domain.Entities
{
    public class FindArchive : Entity<FindArchive>
    {
        public string PartialName { get; private set; }

        public string PathSource { get; private set; }

        public FindArchive(string partialName, string pathSource)
        {
            PartialName = partialName;
            PathSource = pathSource;
        }

        public override bool EsValid()
        {
            Validate();
            return ValidationResult.IsValid;
        }

        private void Validate()
        {
            ValidadePartialName();
            ValidadePathSource();
            ValidationResult = Validate(this);
        }

        private void ValidadePartialName()
        {
            RuleFor(x => x.PartialName)
                .NotEmpty().WithMessage("The PartialName can't be empty.");
        }

        private void ValidadePathSource()
        {
            RuleFor(x => x.PathSource)
                .NotEmpty().WithMessage("The PathSource can't be empty.");
        }
    }
}
