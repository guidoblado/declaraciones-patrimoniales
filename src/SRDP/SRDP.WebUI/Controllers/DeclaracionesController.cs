using AutoMapper;
using SRDP.Application.UseCases;
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

    public class DeclaracionesController : Controller
    {
        private readonly IGetDeclaracionUserCase _getDeclaracionUserCase;
        private readonly IGetProfileUserCase _getProfileUserCase;

        public DeclaracionesController(IGetDeclaracionUserCase getDeclaracionUserCase, IGetProfileUserCase getProfileUserCase)
        {
            _getDeclaracionUserCase = getDeclaracionUserCase;
            _getProfileUserCase = getProfileUserCase;
        }
        // GET: Declaraciones
        public async Task<ActionResult> Index()
        {
            var profile = _getProfileUserCase.Execute(User.Identity.Name);
            var outputList = await _getDeclaracionUserCase.ExecuteList(profile.GestionActual);
            var viewModel = Mapper.Map<ICollection<DeclaracionOutput>, List<DeclaracionModel>> (outputList);
            return View(viewModel);
        }

        public async Task<ActionResult> Details(Guid id)
        {
            var output = await _getDeclaracionUserCase.Execute(id);
            var modelView = Mapper.Map<DeclaracionModel>(output);
            return View(modelView);
        }

        public async Task<ActionResult> GeneratePdf(Guid id)
        {
            var output = await _getDeclaracionUserCase.Execute(id);
            var modelView = Mapper.Map<DeclaracionModel>(output);
            var report = new Rotativa.PartialViewAsPdf("DetailsPdf", modelView);
            return report;
        }
    }
}
