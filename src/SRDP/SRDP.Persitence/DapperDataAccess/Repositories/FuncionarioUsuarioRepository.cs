using Dapper;
using SRDP.Application.Repositories;
using SRDP.Domain.Funcionarios;
using SRDP.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Persitence.DapperDataAccess.Repositories
{
    public class FuncionarioUsuarioRepository : IFuncionarioUsuarioReadOnlyRepository
    {
        private readonly string connectionString;

        public FuncionarioUsuarioRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<FuncionarioUsuario> Get(string usuario)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "Exec dbo.FuncionarioUsuario_Seleccionar @usuario";
                var funcionario = await db.QueryFirstOrDefaultAsync<Entities.FuncionarioUsuario>(sqlCommand, new { usuario });

                if (funcionario == null) return null;

                return FuncionarioUsuario.Load(funcionario.FuncionarioID, funcionario.NombreUsuario, 
                    new NombreCompleto( funcionario.Nombre, funcionario.Apellido), funcionario.EstadoID, funcionario.Estado, funcionario.Email);
            }
        }

        public async Task<ICollection<FuncionarioUsuario>> GetAll()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "Exec dbo.FuncionarioUsuario_Seleccionar";
                var funcionarios = await db.QueryAsync<Entities.FuncionarioUsuario>(sqlCommand);

                var outputResult = new List<FuncionarioUsuario>();
                if (funcionarios == null) return outputResult;

                foreach (var funcionario in funcionarios)
                {
                    outputResult.Add(FuncionarioUsuario.Load(funcionario.FuncionarioID, funcionario.NombreUsuario,
                    new NombreCompleto(funcionario.Nombre, funcionario.Apellido), funcionario.EstadoID, funcionario.Estado, funcionario.Email));
                }
                return outputResult;
            }
        }
    }
}
