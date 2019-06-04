using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DesafioGlobo.Application.ViewModels
{
    [DataContract]
    public class TransferArchiveFtpViewModel
    {
        [DataMember]
        public string User { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public string FullNameArchive { get; set; }

        [IgnoreDataMember]
        public ValidationResult ValidationResult { get; set; }

        [IgnoreDataMember]
        public CascadeMode CascadeMode { get; set; }
    }
}
