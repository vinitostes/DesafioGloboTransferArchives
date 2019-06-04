using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioGlobo.Domain.Entities.Response
{
    public class FindArchiveResponse : Entity<FindArchiveResponse>
    {
        public string FullName { get; private set; }

        public string DirectoryName { get; private set; }

        public string Name { get; private set; }

        public DateTime DateCreate { get; private set; }

        public FindArchiveResponse(string fullName, string directoryName, string name, DateTime dateCreate)
        {
            FullName = fullName;
            DirectoryName = directoryName;
            Name = name;
            DateCreate = dateCreate;
        }

        public override bool EsValid()
        {
            Validate();
            return ValidationResult.IsValid;
        }

        private void Validate()
        {
            ValidateFullName();
            ValidateDirectoryName();
            ValidateName();
            ValidateDateCreate();
            ValidationResult = Validate(this);
        }

        private void ValidateFullName()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("The FullName can't be empty.");
        }

        private void ValidateDirectoryName()
        {
            RuleFor(x => x.DirectoryName)
                .NotEmpty().WithMessage("The DirectoryName can't be empty.");
        }

        private void ValidateName()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("The Name can't be empty.");
        }

        private void ValidateDateCreate()
        {
            RuleFor(x => x.DateCreate)
                .NotEmpty().WithMessage("The DateCreate can't be empty.");
        }
    }
}
