using AutoMapper;
using SRDP.Application.UseCases.GetDeclaracion;
using SRDP.Application.UseCases.GetDeclaracionResumen;
using SRDP.Application.UseCases.GetGestiones;
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
        private readonly IGetDeclaracionResumenUserCase _getDeclaracionResumenUserCase;
        private readonly IGetProfileUserCase _getProfileUserCase;
        private readonly IGetGestionesUserCase _getGestionesUserCase;

        public HomeController(IGetDeclaracionResumenUserCase getDeclaracionResumenUserCase, IGetProfileUserCase getProfileUserCase, IGetGestionesUserCase getGestionesUserCase)
        {
            _getDeclaracionResumenUserCase = getDeclaracionResumenUserCase;
            _getProfileUserCase = getProfileUserCase;
            _getGestionesUserCase = getGestionesUserCase;
        }

        public async Task<ActionResult> Index()
        {
            var profile = _getProfileUserCase.Execute(User.Identity.Name);
            if (profile == null)
                throw new Exception("El Perfil del Usuario '" + User.Identity.Name + "' no fue encontrado.");
            var gestionActual = await _getGestionesUserCase.GestionVigente();
            if (gestionActual == null)
                throw new Exception("No hay ninguna gestión Vigente");
            var declaracion = await _getDeclaracionResumenUserCase.Execute(gestionActual.Anio, profile.FuncionarioID);
            ViewBag.NombreCompleto = profile.NombreCompleto;
            var viewModel = Mapper.Map<DeclaracionResumenModel>(declaracion);
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