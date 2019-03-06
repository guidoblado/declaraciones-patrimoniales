using SRDP.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Domain.Reportes
{
    public class EstadoGeneral
    {
        public Guid DeclaracionID { get; private set; }
        public int FuncionarioID { get; private set; }
        public Colaborador Funcionario { get; private set; }
        public EstadoDeclaracion Estado { get; private set; }

        #region Constructors
        private EstadoGeneral()
        {
        }

        public EstadoGeneral(Guid declaracionID, int funcionarioID, Colaborador funcionario, EstadoDeclaracion estado)
        {
            DeclaracionID = declaracionID;
            FuncionarioID = funcionarioID;
            Funcionario = funcionario;
            Estado = estado;
        }
        #endregion

        public static EstadoGeneral Load(Guid declaracionID, int funcionarioID, Colaborador funcionario, EstadoDeclaracion estado)
        {
            return new EstadoGeneral
            {
                DeclaracionID = declaracionID,
                FuncionarioID = funcionarioID,
                Funcionario = funcionario,
                Estado = estado
            };
        }
    }

}
