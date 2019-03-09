using AutoMapper;
using SRDP.Application.UseCases;
using SRDP.Application.UseCases.GetProfile;
using SRDP.WebUI.App_Start;
using SRDP.WebUI.Models;
using SRDP.WebUI.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SRDP.WebUI.Controllers
{
    [RoleAuthorize(Roles.Administrador)]
    public class UserProfilesController : Controller
    {
        private readonly IGetProfileUserCase _getProfileUserCase;

        public UserProfilesController(IGetProfileUserCase getProfileUserCase)
        {
            _getProfileUserCase = getProfileUserCase;
        }
        // GET: ProfileUser
        public async Task<ActionResult> Index(bool ? soloAdmin)
        {
            var showAdmin = soloAdmin.HasValue ? soloAdmin.Value : false;
            var outputList = await _getProfileUserCase.ExecuteList(showAdmin);

            var userProfiles = Mapper.Map<ICollection<UserProfileOutput>, List<UserProfileModel>>(outputList);
            var modelView = new UserProfileModelView
            {
                Data = userProfiles,
                SoloAdmin = showAdmin,
            };

            return View(modelView);
        }
    }
}