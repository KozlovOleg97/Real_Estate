using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Real_Estate.Core.Application.Enums;
using Real_Estate.Core.Application.Interfaces.Services;
using Real_Estate.Core.Application.ViewModels.Admin;
using Real_Estate.Core.Application.ViewModels.Properties;

namespace Real_Estate.Presentation.WebApp.Controllers
{
    //[Authorize(Roles = "Client")]
    public class ClientController : Controller
    {
        private readonly IPropertiesService _propertiesService;
        private readonly IAccountService _accountService;
        private readonly ITypeOfPropertiesService _typeOfPropertiesService;
        private readonly IUserService _userService;


        public ClientController(IPropertiesService propertiesService, IAccountService accountService, ITypeOfPropertiesService typeOfPropertiesService, IUserService userService)
        {
            _propertiesService = propertiesService;
            _accountService = accountService;
            _typeOfPropertiesService = typeOfPropertiesService;
            _userService = userService;
        }



        public async Task<IActionResult> Index()
        {
            var properties = await _propertiesService.GetAllWithInclude();
            ViewBag.TypeOfPropertiesList = await _typeOfPropertiesService.GetAllViewModel();
            return View(properties);
        }

        public async Task<IActionResult> Agents()
        {
            var usersList = await _userService.GetAllUsersViewModels();
            List<UserViewModel> AgentsList = usersList.Where(user => user.Role == Roles.Agent.ToString()).ToList();

            List<PropertiesViewModel> propertiesList = await _propertiesService.GetAll();

            foreach (UserViewModel agent in AgentsList)
            {
                agent.PropertiesQuantity = propertiesList.Where(property => property.AgentId == agent.Id).Count();
            }

            return View(AgentsList.OrderBy(x => x.FirstName).ToList());
        }


        public async Task<IActionResult> MyProperties()
        {
            var properties = await _propertiesService.GetAllWithInclude();
            ViewBag.TypeOfPropertiesList = await _typeOfPropertiesService.GetAllViewModel();
            return View(properties);
        }
    }
}
