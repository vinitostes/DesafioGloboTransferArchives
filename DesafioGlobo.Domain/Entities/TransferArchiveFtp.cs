using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioGlobo.Domain.Entities
{
    public class TransferArchiveFtp : Entity<TransferArchiveFtp>
    {
        public string User { get; private set; }

        public string Password { get; private set; }

        public string Url { get; private set; }

        public string FullNameArchive { get; private set; }

        public TransferArchiveFtp(string user, string password, string url, string fullName)
        {
            User = user;
            Password = password;
            Url = url;
            FullNameArchive = fullName;
        }

        public override bool EsValid()
        {
            Validate();
            return ValidationResult.IsValid;
        }

        private void Validate()
        {
            ValidateUser();
            ValidatePassword();
            ValidateUrl();
            ValidateFullName();
            ValidationResult = Validate(this);
        }

        private void ValidateUser()
        {
            RuleFor(x => x.User)
                .NotEmpty().WithMessage("The User can't be empty.");
        }

        private void ValidatePassword()
        {
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("The Password can't be empty.");
        }

        private void ValidateUrl()
        {
            RuleFor(x => x.Url)
                .NotEmpty().WithMessage("The Url can't be empty.");
        }

        private void ValidateFullName()
        {
            RuleFor(x => x.FullNameArchive)
                .NotEmpty().WithMessage("The FullNameArchive can't be empty.");
        }
    }
}
