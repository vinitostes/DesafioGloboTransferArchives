using AutoMapper;
using DesafioGlobo.Application.ViewModels;
using DesafioGlobo.Domain.Entities;
using DesafioGlobo.Domain.Entities.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioGlobo.Application.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(ps =>
            {
                ps.ShouldMapProperty = p => false;
                ps.ShouldMapField = p => false;

                ps.AddProfile(new DomaintToViewModelMappingProfile());
                ps.AddProfile(new ViewModelToDomainMappingProfile());

                //ps.ValidateInlineMaps = false;
            });
        }

        public class DomaintToViewModelMappingProfile : Profile
        {
            public DomaintToViewModelMappingProfile()
            {
                CreateMap<TransferArchive, TransferArchiveViewModel>();
                CreateMap<TransferArchiveFtp, TransferArchiveFtpViewModel>();
                CreateMap<FindArchive, FindArchiveViewModel>();
                CreateMap<TransferArchiveResponse, TransferArchiveResponseViewModel>();
            }
        }

        public class ViewModelToDomainMappingProfile : Profile
        {
            public ViewModelToDomainMappingProfile()
            {
                CreateMap<TransferArchiveViewModel, TransferArchive>()
                    .ConstructUsing(c => new TransferArchive(c.FullName, c.PathTarget))
                    .ForAllOtherMembers(oth => oth.Ignore());

                CreateMap<TransferArchiveFtpViewModel, TransferArchiveFtp>()
                    .ConstructUsing(c => new TransferArchiveFtp(c.User, c.Password, c.Url, c.FullNameArchive))
                    //.ForMember(dest => dest.CascadeMode, opt => opt.Ignore())
                    //.ForMember(dest => dest.ValidationResult, opt => opt.Ignore());
                    .ForAllOtherMembers(oth => oth.Ignore());

                CreateMap<FindArchiveViewModel, FindArchive>()
                   .ConstructUsing(c => new FindArchive(c.PartialName, c.PathSource))
                   .ForAllOtherMembers(oth => oth.Ignore());

                CreateMap<TransferArchiveResponseViewModel, TransferArchiveResponse>()
                   .ConstructUsing(c => new TransferArchiveResponse(c.CheckSum))
                   .ForAllOtherMembers(oth => oth.Ignore());
            }
        }
    }
}
