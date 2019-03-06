using SRDP.Domain.Reportes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases
{
    public class AlertaGeneralOutput
    {
        public Guid DeclaracionID { get; private set; }
        public int FuncionarioID { get; private set; }
        public string NombreCompleto { get; private set; }
        public string CodCargo { get; private set; }
        public string Cargo { get; private set; }
        public string CodArea { get; private set; }
        public string Area { get; private set; }
        public string CodUbicacionGeografica { get; private set; }
        public string UbicacionGeografica { get; private set; }
        public string CodCentroDeCosto { get; private set; }
        public string CentroDeCosto { get; private set; }
        public int TipoRol { get; private set; }
        public string Rol { get; private set; }
        public int EstadoDeclaracion { get; private set; }
        public decimal PatrimonioActual { get; private set; }
        public decimal PatrimonioGestionAnterior { get; private set; }
        public decimal DiferenciaPatrimonio { get; private set; }
        public decimal VariacionPorcentual { get; private set; }
        public Guid DeclaracionAnteriorID { get; private set; }

        private AlertaGeneralOutput() { }

        public AlertaGeneralOutput(Guid declaracionID, int funcionarioID, string nombreCompleto, string codCargo, string cargo, string codArea, string area,
            string codUbicacionGeografica, string ubicacionGeografica, string codCentroCosto, string centroDeCosto, int tipoRol, string rol, int estadoDeclaracion,
            Guid declaracionAnteriorID)
        {
            DeclaracionID = declaracionID;
            FuncionarioID = funcionarioID;
            NombreCompleto = nombreCompleto;
            CodCargo = codCargo;
            Cargo = cargo;
            CodArea = codArea;
            Area = area;
            CodUbicacionGeografica = codUbicacionGeografica;
            UbicacionGeografica = ubicacionGeografica;
            CodCentroDeCosto = codCentroCosto;
            CentroDeCosto = centroDeCosto;
            TipoRol = tipoRol;
            Rol = rol;
            EstadoDeclaracion = estadoDeclaracion;
            DeclaracionAnteriorID = declaracionAnteriorID;
        }

        public AlertaGeneralOutput(Guid declaracionID, Guid declaracionAnteriorID, int estadoDeclaracion, Colaborador funcionario)
        {
            DeclaracionID = declaracionID;
            FuncionarioID = funcionario.FuncionarioID;
            NombreCompleto = funcionario.NombreCompleto.ToString();
            CodCargo = funcionario.CodCargo;
            Cargo = funcionario.Cargo;
            CodArea = funcionario.CodArea;
            Area = funcionario.Area;
            CodUbicacionGeografica = funcionario.CodUbicacionGeografica;
            UbicacionGeografica = funcionario.UbicacionGeografica;
            CodCentroDeCosto = funcionario.CodCentroDeCosto;
            CentroDeCosto = funcionario.CentroDeCosto;
            TipoRol = funcionario.TipoRol;
            Rol = funcionario.Rol;
            EstadoDeclaracion = estadoDeclaracion;
            DeclaracionAnteriorID = declaracionAnteriorID;
        }

        public void SetMontosPatrimonio(decimal patrimonioActual, decimal patrimonioGestionAnterior,
            decimal diferenciaPatrimonio, decimal variacionPorcentual)
        {
            PatrimonioActual = patrimonioActual;
            PatrimonioGestionAnterior = patrimonioGestionAnterior;
            DiferenciaPatrimonio = diferenciaPatrimonio;
            VariacionPorcentual = variacionPorcentual;
        }
    }
}
