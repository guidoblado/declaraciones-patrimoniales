using System.Collections.ObjectModel;
using SRDP.Domain.Declaraciones;
using SRDP.Domain.Depositos;
using SRDP.Domain.Funcionarios;

namespace SRDP.Persitence.InMemoryDataAccess
{
    public class Context
    {
        public Collection<Declaracion> Declaraciones { get; set; }
        public Collection<Funcionario> Funcionarios { get; set; }
        public Collection<DepositoMayor10K> Depositos { get; set; }

        public Context()
        {
            Declaraciones = new Collection<Declaracion>();
            Funcionarios = new Collection<Funcionario>();
            Depositos = new Collection<DepositoMayor10K>();
        }
    }
}
