using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DesafioGlobo.Application.ViewModels
{
    [DataContract]
    public class FindArchiveResponseViewModel
    {
        [DataMember]
        public string FullName { get; set; }

        [DataMember]
        public string DirectoryName { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public DateTime DateCreate { get; set; }

        [IgnoreDataMember]
        public ValidationResult ValidationResult { get; set; }

        [IgnoreDataMember]
        public CascadeMode CascadeMode { get; set; }
    }
}
