using SRDP.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Domain.Declaraciones
{
    public sealed class DeclaracionResumen : IEntity
    {
        #region Public Properties
        public Guid ID { get; private set; }
        public int Gestion { get; private set; }
        public int FuncionarioID { get; private set; }
        public EstadoDeclaracion Estado { get; private set; }

        #endregion

        #region Constructors
        public DeclaracionResumen(Guid id, int gestion, int funcionarioID, EstadoDeclaracion estado)
        {
            ID = id;
            Gestion = gestion;
            FuncionarioID = funcionarioID;
            Estado = estado;
        }
        #endregion

        public static DeclaracionResumen Load(Guid id, int gestion, int funcionarioID, EstadoDeclaracion estado)
        {
            return new DeclaracionResumen(id, gestion, funcionarioID, estado);
        }

    }
}
