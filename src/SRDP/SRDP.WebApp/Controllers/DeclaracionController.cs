using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SRDP.Application.UseCases.GetDeclaracion;
using SRDP.Application.UseCases.GetGestiones;
using SRDP.WebApp.Models;

namespace SRDP.WebApp.Controllers
{
    [Authorize]
    public class DeclaracionController : Controller
    {
        private readonly IGetDeclaracionUserCase _getDeclaracionUserCase;
        private readonly IGetGestionesUserCase _getGestionesUserCase;
        private readonly IMapper _mapper;

        public DeclaracionController(IGetDeclaracionUserCase getDeclaracionUserCase, IGetGestionesUserCase getGestionesUserCase, IMapper mapper)
        {
            _getDeclaracionUserCase = getDeclaracionUserCase;
            _getGestionesUserCase = getGestionesUserCase;
            _mapper = mapper;
        }
        
        // GET: Declaracion
        public async Task<ActionResult> Index(int ? gestion)
        {
            var gestionVigente = await _getGestionesUserCase.GestionVigente();
            var gestionParam = gestion.HasValue ? gestion.Value : gestionVigente.Anio;
            var declaraciones = await _getDeclaracionUserCase.ExecuteList(gestionParam, false);

            return View(declaraciones);
        }

        // GET: Declaracion/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var output = await _getDeclaracionUserCase.Execute(id);
            var viewModel = _mapper.Map<DeclaracionModel>(output);
            return View(viewModel);
        }

        public async Task<IActionResult> Actualizar(Guid id)
        {
            var output = await _getDeclaracionUserCase.Execute(id);
            var viewModel = _mapper.Map<DeclaracionModel>(output);
            return View(viewModel);
        }
        // GET: Declaracion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Declaracion/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Pdf(Guid id)
        {
            return new Rotativa.NetCore.ActionAsPdf("Declaracion/Details/" + id.ToString());
        }

        // GET: Declaracion/Edit/5
        public ActionResult Edit(Guid id)
        {
            return View();
        }

        // POST: Declaracion/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        
    }
}