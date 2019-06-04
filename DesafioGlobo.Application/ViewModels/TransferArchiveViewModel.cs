using AutoMapper.Configuration.Annotations;
using FluentValidation;
using FluentValidation.Results;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace DesafioGlobo.Application.ViewModels
{
    [DataContract]
    public class TransferArchiveViewModel
    {
        [DataMember]
        [JsonProperty("fullName")]
        public string FullName { get; set; }

        [DataMember]
        [JsonProperty("pathTarget")]
        public string PathTarget { get; set; }

        [IgnoreDataMember]
        public ValidationResult ValidationResult { get; set; }

        [IgnoreDataMember]
        public CascadeMode CascadeMode { get; set; }
    }
}
