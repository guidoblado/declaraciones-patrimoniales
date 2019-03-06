using AutoMapper;
using SRDP.Application.UseCases.GetDeclaracion;
using SRDP.Application.UseCases.GetProfile;
using SRDP.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SRDP.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGetDeclaracionUserCase _getDeclaracionUserCase;
        private readonly IGetProfileUserCase _getProfileUserCase;

        public HomeController(IGetDeclaracionUserCase getDeclaracionUserCase, IGetProfileUserCase getProfileUserCase)
        {
            _getDeclaracionUserCase = getDeclaracionUserCase;
            _getProfileUserCase = getProfileUserCase;
        }
        public async Task<ActionResult> Index()
        {
            var profile = _getProfileUserCase.Execute(User.Identity.Name);
            var declaracion = await _getDeclaracionUserCase.Execute(profile.GestionActual, profile.FuncionarioID);
            var viewModel = Mapper.Map<DeclaracionModel>(declaracion);
            return View(viewModel);
        }

        public ActionResult Empty()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}