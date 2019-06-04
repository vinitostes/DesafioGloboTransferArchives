using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioGlobo.Domain.Entities
{
    public class TransferArchiveControl : Entity<TransferArchiveControl>
    {
        public Guid IdTransferArchiveControl { get; private set; }

        public int TypeAction { get; private set; }

        public string Request { get; private set; }

        public DateTime CreateDate { get; private set; }

        public Guid? IdResponse { get; private set; }

        public string CheckSum { get; private set; }

        public DateTime? DateSend { get; private set; }

        public TransferArchiveControl(int typeAction, string request)
        {
            IdTransferArchiveControl = Guid.NewGuid();
            TypeAction = typeAction;
            Request = request;
            CreateDate = DateTime.Now;
        }

        public void UpdateData(Guid? idResponse, string checkSum, DateTime? dateSend)
        {
            IdResponse = idResponse;
            CheckSum = checkSum;
            DateSend = dateSend;
        }

        public override bool EsValid()
        {
            Validate();
            return ValidationResult.IsValid;
        }

        private void Validate()
        {
            ValidateId();
            ValidateTypeAction();
            ValidateRequest();
            ValidateCreateDate();
            ValidationResult = Validate(this);
        }

        private void ValidateId()
        {
            RuleFor(x => x.IdTransferArchiveControl)
                .NotEmpty().WithMessage("The Id can't be empty.");
        }

        private void ValidateTypeAction()
        {
            RuleFor(x => x.TypeAction)
                .NotEmpty().WithMessage("The TypeAction can't be empty.");
        }

        private void ValidateRequest()
        {
            RuleFor(x => x.Request)
                .NotEmpty().WithMessage("The Request can't be empty.");
        }

        private void ValidateCreateDate()
        {
            RuleFor(x => x.CreateDate)
                .NotEmpty().WithMessage("The CreateDate can't be empty.");
        }
    }
}
