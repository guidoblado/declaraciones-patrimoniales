using AutoMapper;
using SRDP.Application.UseCases;
using SRDP.Application.UseCases.GetOtrosIngresos;
using SRDP.Application.UseCases.SaveOtrosIngresos;
using SRDP.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SRDP.WebUI.Controllers
{
    public class OtrosIngresosController : Controller
    {
        private readonly IGetOtrosIngresosUserCase _getOtrosIngresosUserCase;
        private readonly ISaveOtrosIngresosUserCase _saveOtrosIngresosUserCase;
        public OtrosIngresosController(IGetOtrosIngresosUserCase getOtrosIngresosUserCase, ISaveOtrosIngresosUserCase saveOtrosIngresosUserCase)
        {
            _getOtrosIngresosUserCase = getOtrosIngresosUserCase;
            _saveOtrosIngresosUserCase = saveOtrosIngresosUserCase;

        }
        // GET: OtrosIngresos
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> GetData(Guid id)
        {
            var result = await _getOtrosIngresosUserCase.ExecuteList(id);
            var modelView = Mapper.Map<ICollection<OtroIngresoOutput>, List<OtroIngresoModel>>(result);
            var serializedModel = Json(new { Data = modelView }, JsonRequestBehavior.AllowGet);
            return serializedModel;
        }

        [HttpGet]
        public async Task<ActionResult> Save(Guid id, Guid declaracionID)
        {
            if (id == null) return View(new OtroIngresoModel());

            var otroIngreso = await _getOtrosIngresosUserCase.Execute(id);

            if (otroIngreso == null) return View(new OtroIngresoModel
            {
                ID = Guid.Empty,
                DeclaracionID = declaracionID,
                Concepto = String.Empty,
                IngresoMensual = 0
            }
            );

            var modelView = Mapper.Map<OtroIngresoOutput, OtroIngresoModel>(otroIngreso);
            return PartialView(modelView);
        }

        [HttpPost]
        public async Task<ActionResult> Save(OtroIngresoModel otroIngreso)
        {

            var otroIngresoOutput = await _saveOtrosIngresosUserCase.Execute(otroIngreso.ID, otroIngreso.DeclaracionID, otroIngreso.Concepto, otroIngreso.IngresoMensual);
            return Json(new { success = true, message = "Actualizado correctamente" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            try
            {
                _saveOtrosIngresosUserCase.DeleteOtroIngreso(id);
                return Json(new { success = true, message = "Eliminado Correctamente" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}