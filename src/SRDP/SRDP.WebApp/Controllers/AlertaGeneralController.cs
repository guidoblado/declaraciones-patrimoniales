using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SRDP.Application.UseCases;
using SRDP.Application.UseCases.GetAlertaGeneral;
using SRDP.WebApp.Models;
using SRDP.WebApp.ModelViews;

namespace SRDP.WebApp.Controllers
{
    public class AlertaGeneralController : Controller
    {
        private readonly IGetAlertaGeneralUserCase _getAlertaGeneralUserCase;
        private readonly IMapper _mapper;

        public AlertaGeneralController(IGetAlertaGeneralUserCase getAlertaGeneralUserCase, IMapper mapper)
        {
            _getAlertaGeneralUserCase = getAlertaGeneralUserCase;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int? gestion, AlertaParametersModel alertaParameters)
        {
            var outputList = await _getAlertaGeneralUserCase.ExecuteList(gestion == null ? 2018 : gestion.Value, alertaParameters.Monto, alertaParameters.Operador, alertaParameters.Porcentaje);

            var viewModel = new AlertaGeneralModelView
            {
                Data = _mapper.Map<ICollection<AlertaGeneralOutput>, List<AlertaGeneralModel>>(outputList),
                AlertaParameters = alertaParameters,
            };

            return View(viewModel);
        }
    }
}