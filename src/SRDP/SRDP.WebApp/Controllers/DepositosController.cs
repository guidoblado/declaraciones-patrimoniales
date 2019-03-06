using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SRDP.Application.UseCases;
using SRDP.Application.UseCases.GetDepositos;
using SRDP.Application.UseCases.SaveDeposito;
using SRDP.WebApp.Models;

namespace SRDP.WebApp.Controllers
{
    public class DepositosController : Controller
    {
        private readonly IGetDepositosUserCase _getDepositosUserCase;
        private readonly ISaveDepositoUserCase _saveDepositoUserCase;
        private readonly IMapper _mapper;

        public DepositosController(IGetDepositosUserCase getDepositosUserCase, ISaveDepositoUserCase saveDepositoUserCase, IMapper mapper)
        {
            _getDepositosUserCase = getDepositosUserCase;
            _saveDepositoUserCase = saveDepositoUserCase;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetDepositos(Guid id)
        {
            var result = await _getDepositosUserCase.ExecuteList(id);
            var modelView = _mapper.Map<ICollection<DepositoOutput>, List<DepositoModel>> (result);
            var serializedModel = Json(new { Data = modelView });
            return serializedModel;
        }
        
        [HttpGet]
        public async Task<IActionResult> Save(Guid id, Guid declaracionID)
        {
            if (id == null) return View(new DepositoModel());

            var deposito = await _getDepositosUserCase.Execute(id);

            if (deposito == null) return View(new DepositoModel
                {
                  ID = Guid.Empty,
                  DeclaracionID = declaracionID,
                  TipoDeCuenta = String.Empty,
                  Saldo = 0
                }
            );

            var modelView = _mapper.Map<DepositoOutput, DepositoModel>(deposito);
            return PartialView(modelView);
        }

        [HttpPost]
        public async Task<IActionResult> Save(DepositoModel deposito)
        {
            
            var depositoOutput = await _saveDepositoUserCase.Execute(deposito.ID, deposito.DeclaracionID, deposito.Institucion, deposito.TipoDeCuenta, deposito.Saldo);
            return RedirectToAction("Actualizar", "Declaracion", new { ID = deposito.DeclaracionID });
            //return Json(new { success = true, message = "Actualizado correctamente" });
            //return View(_mapper.Map<DepositoOutput, DepositoModel>(depositoOutput));
        }
    }
}