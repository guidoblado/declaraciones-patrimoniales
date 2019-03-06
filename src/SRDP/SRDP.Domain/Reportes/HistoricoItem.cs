using SRDP.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Domain.Reportes
{
    public class HistoricoItem
    {
        private readonly IList<HistoricoPatrimonioItem> _Patrimonios;

        public RubroDeclaracion Rubro { get; private set; }
        public IReadOnlyList<HistoricoPatrimonioItem> Patrimonios
        {
            get
            {
                return new ReadOnlyCollection<HistoricoPatrimonioItem>(_Patrimonios.OrderBy(c => c.Gestion).ToList());
            }
        }

        #region Constructors
        private HistoricoItem()
        {
            _Patrimonios = new List<HistoricoPatrimonioItem>();
        }

        public  HistoricoItem(RubroDeclaracion rubro)
        {
            Rubro = Rubro;
            _Patrimonios = new List<HistoricoPatrimonioItem>();

        }
        #endregion

        public void AddPatrimonioItem(HistoricoPatrimonioItem item)
        {
            _Patrimonios.Add(item);
        }
    }
}
