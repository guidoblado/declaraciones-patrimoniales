using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Domain.Reportes
{
    public class Catalogos
    {

        private Dictionary<string, string> _Areas;
        private Dictionary<string, string> _Cargos;
        private Dictionary<string, string> _UbicacionGeografica;
        private Dictionary<string, string> _CentroCosto;
        private Dictionary<string, string> _Roles;

        public IReadOnlyDictionary<string, string> Areas { get { return new ReadOnlyDictionary<string, string>(_Areas); } }
        public IReadOnlyDictionary<string, string> Cargos { get { return new ReadOnlyDictionary<string, string>(_Cargos); } }
        public IReadOnlyDictionary<string, string> UbicacionGeografica { get { return new ReadOnlyDictionary<string, string>(_UbicacionGeografica); } }
        public IReadOnlyDictionary<string, string> CentroCosto { get { return new ReadOnlyDictionary<string, string>(_CentroCosto); } }
        public IReadOnlyDictionary<string, string> Roles { get { return new ReadOnlyDictionary<string, string>(_Roles); } }

        public Catalogos()
        {
            _Areas = new Dictionary<string, string>();
            _Cargos = new Dictionary<string, string>();
            _UbicacionGeografica = new Dictionary<string, string>();
            _CentroCosto = new Dictionary<string, string>();
            _Roles = new Dictionary<string, string>();
        }

        public void AppendToAreas(string valor, string descripcion)
        {
            if (_Areas.ContainsKey(valor))
                _Areas[valor] = descripcion;
            else
                _Areas.Add(valor, descripcion);
        }

        public void AppendToCargos(string valor, string descripcion)
        {
            if (_Cargos.ContainsKey(valor))
                _Cargos[valor] = descripcion;
            else
                _Cargos.Add(valor, descripcion);
        }

        public void AppendToUbicacionGeografica(string valor, string descripcion)
        {
            if (_UbicacionGeografica.ContainsKey(valor))
                _UbicacionGeografica[valor] = descripcion;
            else
                _UbicacionGeografica.Add(valor, descripcion);
        }

        public void AppendToCentroCosto(string valor, string descripcion)
        {
            if (_CentroCosto.ContainsKey(valor))
                _CentroCosto[valor] = descripcion;
            else
                _CentroCosto.Add(valor, descripcion);
        }

        public void AppendToRoles(string valor, string descripcion)
        {
            if (_Roles.ContainsKey(valor))
                _Roles[valor] = descripcion;
            else
                _Roles.Add(valor, descripcion);
        }

    }
}
