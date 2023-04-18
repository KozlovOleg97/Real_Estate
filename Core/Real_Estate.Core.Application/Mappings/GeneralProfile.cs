using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Real_Estate.Core.Application.DTOs.Account;
using Real_Estate.Core.Application.ViewModels.Improvements;
using Real_Estate.Core.Application.ViewModels.TypeOfProperties;
using Real_Estate.Core.Application.ViewModels.TypeOfSales;
using Real_Estate.Core.Application.ViewModels.Users;
using Real_Estate.Core.Domain.Entities;

namespace Real_Estate.Core.Application.Mappings
{
	public class GeneralProfile : Profile
	{
		public GeneralProfile()
		{
			#region UserProfile

			CreateMap<AuthenticationRequest, LoginViewModel>()
				.ForMember(userProfile => userProfile.HasError,
					option => option.Ignore())
				.ForMember(userProfile => userProfile.Error,
					option => option.Ignore())
				.ReverseMap();

			CreateMap<RegisterRequest, SaveUserViewModel>()
				.ForMember(userProfile => userProfile.File,
					option => option.Ignore())
				.ForMember(userProfile => userProfile.HasError,
					option => option.Ignore())
				.ForMember(userProfile => userProfile.Error,
					option => option.Ignore())
				.ReverseMap();

			CreateMap<ForgotPasswordRequest, ForgotPasswordViewModel>()
				.ForMember(userProfile => userProfile.HasError,
					option => option.Ignore())
				.ForMember(userProfile => userProfile.Error,
					option => option.Ignore())
				.ReverseMap();

			CreateMap<ResetPasswordRequest, ResetPasswordViewModel>()
				.ForMember(userProfile => userProfile.HasError,
					option => option.Ignore())
				.ForMember(userProfile => userProfile.Error,
					option => option.Ignore())
				.ReverseMap();

            #endregion

            #region ImprovementsProfile
            CreateMap<Improvements, ImprovementsViewModel>()
                .ReverseMap()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            CreateMap<Improvements, SaveImprovementsViewModel>()
                .ReverseMap()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            #endregion

            #region TypeOfPropertiesProfile

            CreateMap<TypeOfProperties, TypeOfPropertiesViewModel>()
                .ReverseMap()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            CreateMap<TypeOfProperties, SaveTypeOfPropertiesViewModel>()
                .ReverseMap()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            #endregion

            #region TypeOfSalesProfile

            CreateMap<TypeOfSales, TypeOfSalesViewModel>()
                .ReverseMap()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            CreateMap<TypeOfSales, SaveTypeOfSalesViewModel>()
                .ReverseMap()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            #endregion
        }
    }
}
