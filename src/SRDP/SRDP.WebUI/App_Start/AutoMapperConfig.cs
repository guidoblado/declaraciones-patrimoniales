using AutoMapper;
using SRDP.Application.UseCases;
using SRDP.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SRDP.WebUI.App_Start
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
           {
               x.CreateMap<DepositoOutput, DepositoModel>()
                .ForMember(destination => destination.TiposDeCuenta, source => source.MapFrom( list => DepositoModel.GetTipoDeCuenta()));
               x.CreateMap<InmuebleOutput, InmuebleModel>()
               .ForMember(destination => destination.TiposDeInmuebles, source => source.MapFrom(list => InmuebleModel.GetTiposDeInmuebles()));
               x.CreateMap<DeudaBancariaOutput, DeudaBancariaModel>()
               .ForMember(destination => destination.TiposDeDeuda, source => source.MapFrom(list => DeudaBancariaModel.GetTiposDeuda()));
           });
        }
    }
}