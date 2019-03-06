using Dapper;
using SRDP.Application.Repositories;
using SRDP.Domain.Funcionarios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Persitence.DapperDataAccess.Repositories
{
    public class FuncionarioRepository : IFuncionarioReadOnlyRepository
    {

        private readonly string connectionString;

        public FuncionarioRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Task<Funcionario> Get(Guid funcionarioID)
        {
            throw new NotImplementedException();
        }
        public async Task<ICollection<Funcionario>> GetAll()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {

                string funcionarioSQL = "SELECT CODIGO, NOMBRE, APELLIDO, CEDULA, FECHA_NAC, COD_ESTADO FROM Funcionarios";
                var funcionarios = await db.QueryAsync(funcionarioSQL);

                var outputList = new List<Funcionario>();

                if (funcionarios == null) return outputList;

                foreach (var funcionarioItem in funcionarios)
                {
                    Funcionario funcionario = new Funcionario(
                    funcionarioItem.CODIGO,
                    new Domain.ValueObjects.NombreCompleto(funcionarioItem.NOMBRE, funcionarioItem.APELLIDO),
                    new Domain.ValueObjects.Cedula(funcionarioItem.CEDULA),
                    Convert.ToDateTime(funcionarioItem.FECHA_NAC),
                    funcionarioItem.COD_ESTADO == "CAS" ? Domain.Enumerations.EstadoCivil.Casado : Domain.Enumerations.EstadoCivil.Soltero
                    );
                    outputList.Add(funcionario);
                }

                return outputList;
            }
        }
        public async Task<Funcionario> GetByCodigo(int codigo)
        {
            
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                
                string funcionarioSQL = "SELECT CODIGO, NOMBRE, APELLIDO, CEDULA, FECHA_NAC, COD_ESTADO FROM Funcionarios WHERE Codigo = @codigo";
                var funcionarioItem = await db.QueryFirstOrDefaultAsync(funcionarioSQL, new { codigo });

                if (funcionarioItem == null) return null;

                Funcionario funcionario = new Funcionario(
                    funcionarioItem.CODIGO,
                    new Domain.ValueObjects.NombreCompleto(funcionarioItem.NOMBRE, funcionarioItem.APELLIDO),
                    new Domain.ValueObjects.Cedula(funcionarioItem.CEDULA),
                    Convert.ToDateTime(funcionarioItem.FECHA_NAC),
                    funcionarioItem.COD_ESTADO == "CAS" ? Domain.Enumerations.EstadoCivil.Casado : Domain.Enumerations.EstadoCivil.Soltero
                    );

                return funcionario;
            }
        }

        public async Task<int> GetFuncionarioID(string usuario)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "Exec FuncionarioUsuario_Seleccionar @usuario";
                var queryResult = await db.QueryFirstOrDefaultAsync<Entities.FuncionarioUsuario>(sqlCommand, new { usuario});

                if (queryResult == null) return 0;

                return queryResult.FuncionarioID;
            }
        }
    }
}
