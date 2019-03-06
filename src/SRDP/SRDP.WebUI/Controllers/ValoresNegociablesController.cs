using AutoMapper;
using SRDP.Application.UseCases;
using SRDP.Application.UseCases.GetValoresNegociables;
using SRDP.Application.UseCases.SaveValoresNegociables;
using SRDP.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SRDP.WebUI.Controllers
{
    public class ValoresNegociablesController : Controller
    {
        private readonly IGetValoresNegociablesUserCase _getValoresNegociablesUserCase;
        private readonly ISaveValoresNegociablesUserCase _saveValoresNegociablesUserCase;

        public ValoresNegociablesController(IGetValoresNegociablesUserCase getValoresNegociablesUserCase, ISaveValoresNegociablesUserCase saveValoresNegociablesUserCase)
        {
            _getValoresNegociablesUserCase = getValoresNegociablesUserCase;
            _saveValoresNegociablesUserCase = saveValoresNegociablesUserCase;

        }

        // GET: ValoresNegociables
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> GetData(Guid id)
        {
            var result = await _getValoresNegociablesUserCase.ExecuteList(id);
            var modelView = Mapper.Map<ICollection<ValorNegociableOutput>, List<ValorNegociableModel>>(result);
            var serializedModel = Json(new { Data = modelView }, JsonRequestBehavior.AllowGet);
            return serializedModel;
        }

        [HttpGet]
        public async Task<ActionResult> Save(Guid id, Guid declaracionID)
        {
            if (id == null) return View(new ValorNegociableModel());

            var valorNegociable = await _getValoresNegociablesUserCase.Execute(id);

            if (valorNegociable == null) return View(new ValorNegociableModel
            {
                ID = Guid.Empty,
                DeclaracionID = declaracionID,
                Descripcion = String.Empty,
                ValorAproximado = 0
            }
            );

            var modelView = Mapper.Map<ValorNegociableOutput, ValorNegociableModel>(valorNegociable);
            return PartialView(modelView);
        }

        [HttpPost]
        public async Task<ActionResult> Save(ValorNegociableModel valorNegociable)
        {

            var valorNegociableOutput = await _saveValoresNegociablesUserCase.Execute(valorNegociable.ID, valorNegociable.DeclaracionID, valorNegociable.Descripcion, valorNegociable.TipoValor, valorNegociable.ValorAproximado);
            return Json(new { success = true, message = "Actualizado correctamente" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            try
            {
                _saveValoresNegociablesUserCase.DeleteValorNegociable(id);
                return Json(new { success = true, message = "Eliminado Correctamente" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}