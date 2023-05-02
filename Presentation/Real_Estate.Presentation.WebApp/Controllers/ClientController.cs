using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Real_Estate.Core.Application.DTOs.Account;
using Real_Estate.Core.Application.Enums;
using Real_Estate.Core.Application.Helpers;
using Real_Estate.Core.Application.Interfaces.Services;
using Real_Estate.Core.Application.ViewModels.Admin;
using Real_Estate.Core.Application.ViewModels.Properties;

namespace Real_Estate.Presentation.WebApp.Controllers
{
    [Authorize(Roles = "Client")]
    public class ClientController : Controller
    {
        private readonly IPropertiesService _propertiesService;
        private readonly IAccountService _accountService;
        private readonly ITypeOfPropertiesService _typeOfPropertiesService;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse userviewModel;


        public ClientController(IPropertiesService propertiesService, IAccountService accountService, 
            ITypeOfPropertiesService typeOfPropertiesService, IUserService userService, 
            IHttpContextAccessor httpContextAccessor)
        {
            _propertiesService = propertiesService;
            _accountService = accountService;
            _typeOfPropertiesService = typeOfPropertiesService;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            userviewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }



        public async Task<IActionResult> Index()
        {
            if (userviewModel.Roles.FirstOrDefault() == Roles.Admin.ToString())
            {
                return RedirectToRoute(new { controller = "Admin", action = "Index" });
            }

            else if (userviewModel.Roles.FirstOrDefault() == Roles.Agent.ToString())
            {
                return RedirectToRoute(new { controller = "Agent", action = "Index" });

            }

            else if (userviewModel.Roles.FirstOrDefault() == Roles.Developer.ToString())
            {
                return RedirectToRoute(new { controller = "Home", action = "AccessDenied" });

            }

            var properties = await _propertiesService.GetAllWithInclude();
            ViewBag.TypeOfPropertiesList = await _typeOfPropertiesService.GetAllViewModel();
            return View(properties);
        }

        public async Task<IActionResult> Agents()
        {
            if (userviewModel.Roles.FirstOrDefault() == Roles.Admin.ToString())
            {
                return RedirectToRoute(new { controller = "Admin", action = "Index" });
            }

            else if (userviewModel.Roles.FirstOrDefault() == Roles.Agent.ToString())
            {
                return RedirectToRoute(new { controller = "Agent", action = "Index" });

            }

            else if (userviewModel.Roles.FirstOrDefault() == Roles.Developer.ToString())
            {
                return RedirectToRoute(new { controller = "Home", action = "AccessDenied" });

            }

            var usersList = await _userService.GetAllUsersViewModels();
            List<UserViewModel> AgentsList = usersList.Where(
                user => user.Role == Roles.Agent.ToString()).ToList();

            List<PropertiesViewModel> propertiesList = await _propertiesService.GetAll();

            foreach (UserViewModel agent in AgentsList)
            {
                agent.PropertiesQuantity = propertiesList.Where(
                    property => property.AgentId == agent.Id).Count();
            }

            return View(AgentsList.OrderBy(x => x.FirstName).ToList());
        }


        public async Task<IActionResult> MyProperties()
        {
            if (userviewModel.Roles.FirstOrDefault() == Roles.Admin.ToString())
            {
                return RedirectToRoute(new { controller = "Admin", action = "Index" });
            }

            else if (userviewModel.Roles.FirstOrDefault() == Roles.Agent.ToString())
            {
                return RedirectToRoute(new { controller = "Agent", action = "Index" });

            }

            else if (userviewModel.Roles.FirstOrDefault() == Roles.Developer.ToString())
            {
                return RedirectToRoute(new { controller = "Home", action = "AccessDenied" });

            }

            var properties = await _propertiesService.GetAllWithInclude();
            ViewBag.TypeOfPropertiesList = await _typeOfPropertiesService.GetAllViewModel();
            return View(properties);
        }
    }
}
