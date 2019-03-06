using AutoMapper;
using SRDP.Application.UseCases;
using SRDP.Application.UseCases.GetInmuebles;
using SRDP.Application.UseCases.SaveInmuebles;
using SRDP.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SRDP.WebUI.Controllers
{
    public class InmueblesController : Controller
    {
        private readonly IGetInmueblesUserCase _getInmueblesUserCase;
        private readonly ISaveInmueblesUserCase _saveInmueblesUserCase;

        public InmueblesController(IGetInmueblesUserCase getInmueblesUserCase, ISaveInmueblesUserCase saveInmueblesUserCase)
        {
            _getInmueblesUserCase = getInmueblesUserCase;
            _saveInmueblesUserCase = saveInmueblesUserCase;
        }
        // GET: Inmuebles
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> GetData(Guid id)
        {
            var result = await _getInmueblesUserCase.ExecuteList(id);
            var modelView = Mapper.Map<ICollection<InmuebleOutput>, List<InmuebleModel>>(result);
            var serializedModel = Json(new { Data = modelView }, JsonRequestBehavior.AllowGet);
            return serializedModel;
        }

        [HttpGet]
        public async Task<ActionResult> Save(Guid id, Guid declaracionID)
        {
            if (id == null) return View(new InmuebleModel());

            var inmueble = await _getInmueblesUserCase.Execute(id);

            if (inmueble == null) return View(new InmuebleModel
            {
                ID = Guid.Empty,
                DeclaracionID = declaracionID,
                Direccion = String.Empty,
                TiposDeInmuebles = InmuebleModel.GetTiposDeInmuebles(),
                SaldoHipoteca = 0
            }
            );

            var modelView = Mapper.Map<InmuebleOutput, InmuebleModel>(inmueble);
            return PartialView(modelView);
        }

        [HttpPost]
        public async Task<ActionResult> Save(InmuebleModel inmueble)
        {

            var inmuebleOutput = await _saveInmueblesUserCase.Execute(inmueble.ID, inmueble.DeclaracionID, inmueble.Direccion, inmueble.TipoDeInmueble, inmueble.PorcentajeParticipacion,
                inmueble.ValorComercial, inmueble.SaldoHipoteca, inmueble.Banco);
            return Json(new { success = true, message = "Actualizado correctamente" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            try
            {
                _saveInmueblesUserCase.DeleteInmueble(id);
                return Json(new { success = true, message = "Eliminado Correctamente" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}