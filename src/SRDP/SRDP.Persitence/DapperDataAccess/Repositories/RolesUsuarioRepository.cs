﻿using Dapper;
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
    public class RolesUsuarioRepository : IRolesUsuarioReadOnlyRepository, IRolesUsuarioWriteOnlyRepository
    {
        private readonly string connectionString;

        public RolesUsuarioRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task Add(string userName, int funcionarioID)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "INSERT INTO UsuariosAdministrador (NombreUsuario, FuncionarioID) VALUES (@NombreUsuario, @FuncionarioID)";
                DynamicParameters usuarioAdminParameters = new DynamicParameters();
                usuarioAdminParameters.Add("@NombreUsuario", userName, DbType.AnsiString);
                usuarioAdminParameters.Add("@FuncionarioID", funcionarioID);

                int rows = await db.ExecuteAsync(sqlCommand, usuarioAdminParameters);
            }
        }

        public async Task Delete(string username)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "DELETE UsuariosAdministrador WHERE NombreUsuario = @username";

                DynamicParameters usuarioAdminParameters = new DynamicParameters();
                usuarioAdminParameters.Add("@username", username);

                int rows = await db.ExecuteAsync(sqlCommand, usuarioAdminParameters);
            }
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

        public async Task<List<string>> GetAdminUsers()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "SELECT * FROM UsuariosAdministrador";

                var usuariosAdmin = await db.QueryAsync<Entities.UsuarioAdministrador>(sqlCommand);

                var outputList = new List<string>();

                if (usuariosAdmin == null) return outputList;
                foreach (var usuario in usuariosAdmin.ToList())
                {
                    outputList.Add(usuario.NombreUsuario);
                }

                return outputList;
            }
        }

       
    }
}
