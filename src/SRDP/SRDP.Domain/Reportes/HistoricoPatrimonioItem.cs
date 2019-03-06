using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Domain.Reportes
{
    public class HistoricoPatrimonioItem
    {
        public Guid DeclaracionID { get; private set; }
        public int Gestion { get; private set; }
        public decimal Monto { get; private set; }

        #region Constructors
        private HistoricoPatrimonioItem() { }
        public HistoricoPatrimonioItem(Guid declaracionID, int gestion, decimal monto)
        {
            DeclaracionID = declaracionID;
            Gestion = gestion;
            Monto = monto;
        }
        #endregion

        public static HistoricoPatrimonioItem Load(Guid declaracionID, int gestion, decimal monto)
        {
            return new HistoricoPatrimonioItem
            {
                DeclaracionID = declaracionID,
                Gestion = gestion,
                Monto = monto,
            };
        }
    }
}
