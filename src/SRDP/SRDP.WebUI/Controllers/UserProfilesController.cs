using AutoMapper;
using SRDP.Application.UseCases;
using SRDP.Application.UseCases.GetProfile;
using SRDP.Application.UseCases.UpdateProfile;
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
        private readonly IUpdateProfileUserCase _updateProfileUserCase;
        public UserProfilesController(IGetProfileUserCase getProfileUserCase, IUpdateProfileUserCase updateProfileUserCase)
        {
            _getProfileUserCase = getProfileUserCase;
            _updateProfileUserCase = updateProfileUserCase;
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

        public async Task<ActionResult> AddToAdmin(bool soloAdmin, string userName, int funcionarioID)
        {
            try
            {
                await _updateProfileUserCase.AddToAdmin(userName, funcionarioID);
                TempData["UserMessage"] = new MessageModel { CssClassName = "alert-success", Title = "Adicionado correctamente", Message = "" };
                return RedirectToAction("Index", soloAdmin);
            }
            catch (Exception ex)
            {
                TempData["UserMessage"] = new MessageModel { CssClassName = "alert-error", Title = "No se pudo adicionar el usuario '" + userName + "'", Message = ex.Message };
                return RedirectToAction("Index", soloAdmin);
            }
        }

        public async Task<ActionResult> DeleteFromAdmin(bool soloAdmin, string userName)
        {
            try
            {
                await _updateProfileUserCase.DeleteFromAdmin(userName);
                TempData["UserMessage"] = new MessageModel { CssClassName = "alert-success", Title = "Eliminado correctamente", Message = "" };
                return RedirectToAction("Index", soloAdmin);
            }
            catch (Exception ex)
            {
                TempData["UserMessage"] = new MessageModel { CssClassName = "alert-danger", Title = "No se pudo eliminar el usuario '" + userName + "'", Message = ex.Message };
                return RedirectToAction("Index", soloAdmin);
            }
        }
    }
}