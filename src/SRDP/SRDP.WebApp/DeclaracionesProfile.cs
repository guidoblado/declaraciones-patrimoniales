using AutoMapper;
using SRDP.Application;
using SRDP.Application.UseCases;
using SRDP.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRDP.WebApp
{
    public class DeclaracionesProfile : Profile
    {
        public DeclaracionesProfile()
        {
            CreateMap<DeclaracionOutput, DeclaracionModel>();
            CreateMap<DepositoOutput, DepositoModel>();
            CreateMap<DeudaBancariaOutput, DeudaBancariaModel>();
            CreateMap<InmuebleOutput, InmuebleModel>();
            CreateMap<OtroIngresoOutput, OtroIngresoModel>();
            CreateMap<ValorNegociableOutput, ValorNegociableModel>();
            CreateMap<VehiculoOutput, VehiculoModel>();
            CreateMap<EstadoGeneralOutput,EstadoGeneralModel>();
            CreateMap<HistoricoIndividualOutput,HistoricoIndividualModel>();
            CreateMap<AlertaGeneralOutput,AlertaGeneralModel>();
        }
    }
}
