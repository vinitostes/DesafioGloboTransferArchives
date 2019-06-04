using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioGlobo.Domain.Entities.Response
{
    public class TransferArchiveResponse : Entity<TransferArchiveResponse>
    {
        public string CheckSum { get; private set; }

        public TransferArchiveResponse()
        {
        }

        public TransferArchiveResponse(string checksum)
        {
            CheckSum = checksum;
        }
        public override bool EsValid()
        {
            Validate();
            return ValidationResult.IsValid;
        }

        private void Validate()
        {
            ValidateCheckSum();
            ValidationResult = Validate(this);
        }

        private void ValidateCheckSum()
        {
            RuleFor(x => x.CheckSum)
                .NotEmpty().WithMessage("The CheckSum can't be empty.");
        }
    }
}
