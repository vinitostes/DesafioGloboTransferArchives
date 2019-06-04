using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DesafioGlobo.Application.ViewModels
{
    [DataContract]
    public class FindArchiveViewModel
    {
        [DataMember]
        public string PartialName { get; set; }

        [DataMember]
        public string PathSource { get; set; }

        [IgnoreDataMember]
        public ValidationResult ValidationResult { get; set; }

        [IgnoreDataMember]
        public CascadeMode CascadeMode { get; set; }
    }
}
