using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Real_Estate.Core.Application.DTOs.Account;
using Real_Estate.Core.Application.Enums;
using Real_Estate.Core.Application.Helpers;
using Real_Estate.Core.Application.Interfaces.Services;
using Real_Estate.Core.Application.Services;
using Real_Estate.Core.Application.ViewModels.Admin;
using Real_Estate.Core.Application.ViewModels.Properties;

namespace Real_Estate.Presentation.WebApp.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IPropertiesService _propertiesService;
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse currentlyUser;
        private readonly IMapper _mapper;

        public AdminController(IUserService userService, IPropertiesService propertiesService, IAccountService accountService, 
            IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _propertiesService = propertiesService;
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
            currentlyUser = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>(
                "user");
            _mapper = mapper;
            _userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            HomeAdminViewModel homeAdminViewModel = new();
            homeAdminViewModel = await _userService.GetUsersQuantity();
            var propertiesVM = await _propertiesService.GetAllWithData();
            homeAdminViewModel.PropertiesQuantity = propertiesVM.Count();

            return View(homeAdminViewModel);
        }

        public async Task<IActionResult> AgentsList()
        {
            HomeAdminViewModel homeAdminViewModel = new();
            homeAdminViewModel = await _userService.GetUsersQuantity();
            var propertiesVM = await _propertiesService.GetAllWithData();
            homeAdminViewModel.PropertiesQuantity = propertiesVM.Count();

            return View(homeAdminViewModel);
        }
    }
}
