using AutoMapper;
using SRDP.Application.UseCases;
using SRDP.Application.UseCases.GetGestiones;
using SRDP.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SRDP.WebUI.Controllers
{
    public class GestionesController : Controller
    {
        private readonly IGetGestionesUserCase _getGestionesUserCase;

        public GestionesController(IGetGestionesUserCase getGestionesUserCase)
        {
            _getGestionesUserCase = getGestionesUserCase;
        }
        // GET: Gestiones
        public async Task<ActionResult> Index()
        {
            var outputList = await _getGestionesUserCase.ExecuteList();
            var modelView = Mapper.Map<ICollection<GestionOutput>, List<GestionModel>>(outputList);
            return View(modelView);
        }
    }
}