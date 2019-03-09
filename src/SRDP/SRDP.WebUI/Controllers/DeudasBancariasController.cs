using AutoMapper;
using SRDP.Application.UseCases;
using SRDP.Application.UseCases.GetDeudasBancarias;
using SRDP.Application.UseCases.SaveDeudasBancarias;
using SRDP.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SRDP.WebUI.Controllers
{
    public class DeudasBancariasController : Controller
    {
        private readonly IGetDeudasBancariasUserCase _getDeudasBancariasUserCase;
        private readonly ISaveDeudasBancariasUserCase _saveDeudasBancariasUserCase;

        public DeudasBancariasController(IGetDeudasBancariasUserCase getDeudasBancariasUserCase, ISaveDeudasBancariasUserCase saveDeudasBancariasUserCase)
        {
            _getDeudasBancariasUserCase = getDeudasBancariasUserCase;
            _saveDeudasBancariasUserCase = saveDeudasBancariasUserCase;
        }
        // GET: DeudasBancarias
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> GetData(Guid id)
        {
            var result = await _getDeudasBancariasUserCase.ExecuteList(id);
            var modelView = Mapper.Map<ICollection<DeudaBancariaOutput>, List<DeudaBancariaModel>>(result);
            var serializedModel = Json(new { Data = modelView }, JsonRequestBehavior.AllowGet);
            return serializedModel;
        }

        [HttpGet]
        public async Task<ActionResult> Save(Guid id, Guid declaracionID)
        {
            if (id == null) return View(new DeudaBancariaModel());

            var deuda = await _getDeudasBancariasUserCase.Execute(id);

            if (deuda == null) return View(new DeudaBancariaModel
            {
                ID = Guid.Empty,
                DeclaracionID = declaracionID,
                InstitucionFinanciera = String.Empty,
                Tipo = String.Empty,
                TiposDeDeuda = DeudaBancariaModel.GetTiposDeuda(),
                Monto = 0
            }
            );

            var modelView = Mapper.Map<DeudaBancariaOutput, DeudaBancariaModel>(deuda);
            return PartialView(modelView);
        }

        [HttpPost]
        public async Task<ActionResult> Save(DeudaBancariaModel deuda)
        {

            var deudaOutput = await _saveDeudasBancariasUserCase.Execute(deuda.ID, deuda.DeclaracionID, deuda.InstitucionFinanciera, deuda.Monto, deuda.Tipo);
            return Json(new { success = true, message = "Actualizado correctamente" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            try
            {
                _saveDeudasBancariasUserCase.DeleteDeuda(id);
                return Json(new { success = true, message = "Eliminado Correctamente" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}