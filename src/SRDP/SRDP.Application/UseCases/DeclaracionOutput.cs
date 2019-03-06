﻿using SRDP.Domain.Declaraciones;
using SRDP.Domain.Depositos;
using SRDP.Domain.Funcionarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases
{
    public class DeclaracionOutput
    {
        public Guid DeclaracionID { get; }
        public int Gestion { get; }
        public string NombreCompleto { get; }
        public string CedulaIdentidad { get;  }
        public DateTime FechaNacimiento { get;  }
        public string EstadoCivil { get;  }
        public DateTime FechaLlenado { get; }
        public IReadOnlyList <DepositoOutput> Depositos { get; }
        public IReadOnlyList <DeudaBancariaOutput> DeudasBancarias { get; }
        public IReadOnlyList <InmuebleOutput> Inmuebles { get; }
        public IReadOnlyList <OtroIngresoOutput> OtrosIngresos { get; }
        public IReadOnlyList <ValorNegociableOutput> ValoresNegociables { get; }
        public IReadOnlyList <VehiculoOutput> Vehiculos { get; }
        public decimal PatrimonioNeto { get; }

        private DeclaracionOutput()
        {
            Depositos = new List<DepositoOutput>();
            DeudasBancarias = new List<DeudaBancariaOutput>();
            Inmuebles = new List<InmuebleOutput>();
            OtrosIngresos = new List<OtroIngresoOutput>();
            ValoresNegociables = new List<ValorNegociableOutput>();
            Vehiculos = new List<VehiculoOutput>();
        }
        public DeclaracionOutput(Declaracion declaracion, Funcionario funcionario, List<DepositoOutput> depositos,
            List<DeudaBancariaOutput> deudasBancarias, List<InmuebleOutput> inmuebles, List<OtroIngresoOutput> otrosIngresos,
            List<ValorNegociableOutput> valoresNegociables, List<VehiculoOutput> vehiculos, decimal patrimonioNeto)
        {
            DeclaracionID = declaracion.ID;
            Gestion = declaracion.Gestion;
            NombreCompleto = funcionario.NombreFuncionario.ToString();
            CedulaIdentidad = funcionario.CI.ToString();
            FechaNacimiento = funcionario.FechaNacimiento;
            EstadoCivil = funcionario.EstadoCivil.ToString();
            FechaLlenado = declaracion.FechaLlenado;
            Depositos = depositos;
            DeudasBancarias = deudasBancarias;
            Inmuebles = inmuebles;
            OtrosIngresos = otrosIngresos;
            ValoresNegociables = valoresNegociables;
            Vehiculos = vehiculos;
            PatrimonioNeto = patrimonioNeto;
        }
    }
}
