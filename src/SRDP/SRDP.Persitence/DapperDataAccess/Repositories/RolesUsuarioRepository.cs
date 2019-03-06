using Dapper;
using SRDP.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Persitence.DapperDataAccess.Repositories
{
    public class RolesUsuarioRepository : IRolesUsuarioReadOnlyRepository
    {
        private readonly string connectionString;

        public RolesUsuarioRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<List<string>> Get(string username)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "SELECT * FROM UsuariosAdministrador WHERE NombreUsuario = @username";

                var usuarioAdmin = await db.QueryFirstOrDefaultAsync<Entities.UsuarioAdministrador>(sqlCommand, new { username });

                var outputList = new List<string>();
                outputList.Add("Usuario");

                if (usuarioAdmin != null)
                {
                    outputList.Add("Administrador");
                }

                return outputList;
            }
        }
    }
}
