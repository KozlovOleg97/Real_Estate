using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Real_Estate.Core.Application.DTOs.Account;
using Real_Estate.Core.Application.Features.Accounts.Commands.RegisterAdminUser;
using Real_Estate.Core.Application.Features.Accounts.Commands.RegisterDeveloperUser;
using Real_Estate.Core.Application.Features.Accounts.Queries.Authenticate;
using Real_Estate.Core.Application.Features.Improvements.Commands.CreateImprovements;
using Real_Estate.Core.Application.Features.Improvements.Commands.UpdateImprovements;
using Real_Estate.Core.Application.Features.TypeOfProperties.Commands.CreateTypeOfProperties;
using Real_Estate.Core.Application.Features.TypeOfProperties.Commands.UpdateTypeOfProperties;
using Real_Estate.Core.Application.Features.TypeOfSales.Commands.CreateTypeOfSales;
using Real_Estate.Core.Application.Features.TypeOfSales.Commands.UpdateTypeOfSales;
using Real_Estate.Core.Application.ViewModels.Improvements;
using Real_Estate.Core.Application.ViewModels.Properties;
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

            #region Properties

            CreateMap<Properties, SavePropertiesViewModel>()
                .ForMember(x => x.Improvements, opt => opt.Ignore())
                .ForMember(x => x.TypeOfProperties, opt => opt.Ignore())
                .ForMember(x => x.TypeOfSales, opt => opt.Ignore())
                .ForMember(x => x.Improvements, opt => opt.Ignore())
                .ForMember(x => x.ImageFileOne, opt => opt.Ignore())
                .ForMember(x => x.ImageFileTwo, opt => opt.Ignore())
                .ForMember(x => x.ImageFileThree, opt => opt.Ignore())
                .ForMember(x => x.ImageFileFour, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.Improvements, opt => opt.Ignore())
                .ForMember(x => x.TypeOfProperty, opt => opt.Ignore())
                .ForMember(x => x.TypeOfSale, opt => opt.Ignore());

            CreateMap<UpdateAgentUserRequest, SaveAgentProfileViewModel>()
                .ReverseMap()
                .ForMember(x => x.UserName, opt => opt.Ignore());

            CreateMap<SaveAgentProfileViewModel, UpdateAgentUserResponse>()
                .ReverseMap()
                .ForMember(x => x.File, opt => opt.Ignore());

            CreateMap<Properties, PropertiesViewModel>()
                .ForMember(x => x.Improvements, opt => opt.Ignore())
                .ForMember(x => x.TypeOfProperty, opt => opt.Ignore())
                .ForMember(x => x.TypeOfSale, opt => opt.Ignore())
                .ForMember(x => x.Improvements, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.Improvements, opt => opt.Ignore())
                .ForMember(x => x.TypeOfProperty, opt => opt.Ignore())
                .ForMember(x => x.TypeOfSale, opt => opt.Ignore());

            #endregion

            #region CQRS

            CreateMap<CreateImprovementsCommand, Improvements>()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<CreateTypeOfPropertiesCommand, TypeOfProperties>()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<CreateTypeOfSalesCommand, TypeOfSales>()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UpdateImprovementsCommand, Improvements>()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UpdateImprovementsResponse, Improvements>()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UpdateTypeOfPropertiesCommand, TypeOfProperties>()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UpdateTypeOfPropertiesResponse, TypeOfProperties>()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<TypeOfSalesUpdateCommand, TypeOfSales>()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<TypeOfSalesUpdateResponse, TypeOfSales>()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<RegisterAdminUserCommand, RegisterRequest>()
                .ReverseMap();

            CreateMap<RegisterDeveloperUserCommand, RegisterRequest>()
                .ReverseMap();

            CreateMap<AuthenticateUserQuery, AuthenticationRequest>()
                .ReverseMap();

            #endregion
        }
    }
}
