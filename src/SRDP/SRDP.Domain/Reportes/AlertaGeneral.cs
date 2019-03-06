using SRDP.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Domain.Reportes
{
    public class AlertaGeneral
    {
        public Guid DeclaracionID { get; private set; }
        public int FuncionarioID { get; private set; }
        public Colaborador Funcionario { get; private set; }
        public EstadoDeclaracion Estado { get; private set; }
        public decimal PatrimonioActual { get; private set; }
        public decimal PatrimonioGestionAnterior { get; private set; }
        public decimal DiferenciaPatrimonio { get; private set; }
        public decimal VariacionPorcentual { get; private set; }

        #region Constructors
        private AlertaGeneral()
        {
        }

        public AlertaGeneral(Guid declaracionID, int funcionarioID, Colaborador funcionario, EstadoDeclaracion estado,
            decimal patrimonioActual, decimal patrimonioGestionAnterior, decimal diferenciaPatrimonio, decimal variacionPorcentual)
        {
            DeclaracionID = declaracionID;
            FuncionarioID = funcionarioID;
            Funcionario = funcionario;
            Estado = estado;
            PatrimonioActual = patrimonioActual;
            PatrimonioGestionAnterior = patrimonioGestionAnterior;
            DiferenciaPatrimonio = diferenciaPatrimonio;
            VariacionPorcentual = variacionPorcentual;
        }
        #endregion

        public static AlertaGeneral Load(Guid declaracionID, int funcionarioID, Colaborador funcionario, EstadoDeclaracion estado,
            decimal patrimonioActual, decimal patrimonioGestionAnterior, decimal diferenciaPatrimonio, decimal variacionPorcentual)
        {
            return new AlertaGeneral
            {
                DeclaracionID = declaracionID,
                FuncionarioID = funcionarioID,
                Funcionario = funcionario,
                Estado = estado,
                PatrimonioActual = patrimonioActual,
                PatrimonioGestionAnterior = patrimonioGestionAnterior,
                DiferenciaPatrimonio = diferenciaPatrimonio,
                VariacionPorcentual = variacionPorcentual,
            };
        }
    }
}
