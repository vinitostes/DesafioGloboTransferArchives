using FluentValidation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioGlobo.Domain.Entities
{
    public class TransferArchive : Entity<TransferArchive>
    {
        [JsonProperty("fullName")]
        public string FullName { get; private set; }

        [JsonProperty("pathTarget")]
        public string PathTarget { get; private set; }


        public TransferArchive(string fullName, string pathTarget)
        {
            FullName = fullName;
            PathTarget = pathTarget;
        }

        public override bool EsValid()
        {
            Validate();
            return ValidationResult.IsValid;
        }

        private void Validate()
        {
            ValidadeFullName();
            ValidadePathDestination();            
            ValidationResult = Validate(this);
        }

        private void ValidadeFullName()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("The FullName can't be empty.");
        }

        private void ValidadePathDestination()
        {
            RuleFor(x => x.PathTarget)
                .NotEmpty().WithMessage("The PathDestination can't be empty.");
        }
    }
}
