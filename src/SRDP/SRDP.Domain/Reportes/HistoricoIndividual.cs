using SRDP.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Domain.Reportes
{
    public class HistoricoIndividual
    {
        public int FuncionarioID { get; private set; }
        public Colaborador Funcionario { get; private set; }
        private Dictionary<RubroDeclaracion, HistoricoItem> _Historico;
        public IReadOnlyDictionary<RubroDeclaracion, HistoricoItem> Historico
        {
            get
            {
                return new ReadOnlyDictionary<RubroDeclaracion, HistoricoItem>(_Historico);
            }
        }

        #region Constructors
        private HistoricoIndividual()
        {
            _Historico = new Dictionary<RubroDeclaracion, HistoricoItem>();
        }

        public HistoricoIndividual(int funcionarioID, Colaborador funcionario)
        {
            FuncionarioID = funcionarioID;
            Funcionario = funcionario;
            _Historico = new Dictionary<RubroDeclaracion, HistoricoItem>();

        }
        #endregion

        public void AppendHistorico(RubroDeclaracion rubro, HistoricoItem item)
        {
            if (_Historico.ContainsKey(rubro))
            {
                _Historico[rubro] = item;
            }
            else
            {
                _Historico.Add(rubro, item);
            }
        }
    }
}
