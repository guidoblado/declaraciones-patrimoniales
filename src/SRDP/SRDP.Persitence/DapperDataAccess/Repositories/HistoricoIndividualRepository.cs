using SRDP.Application.Repositories;
using SRDP.Domain.Enumerations;
using SRDP.Domain.Reportes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SRDP.Persitence.Entities;
using SRDP.Application;

namespace SRDP.Persitence.DapperDataAccess.Repositories
{
    public class HistoricoIndividualRepository : IHistoricoIndividualReadOnlyRepository
    {
        private readonly string connectionString;

        public HistoricoIndividualRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Task<ICollection<HistoricoIndividual>> GetFromGestion(int gestion)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<HistoricoIndividualItemOutput>> GetHistoricoItems(int gestionInicial)
        {
            var result = new List<HistoricoIndividualItemOutput>();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "Exec dbo.HistoricoFuncionario_Seleccionar";
                var queryResult = await db.QueryAsync<HistoricoFuncionarioSchema>(sqlCommand);

                foreach (var item in queryResult)
                {
                    result.Add(new HistoricoIndividualItemOutput(item.FuncionarioID, item.NombreCompleto, item.CodCargo, item.Cargo, item.CodArea, item.Area, 
                        item.CodGeografico, item.UbicacionGeografica, item.CodCentroCosto, item.CentroCosto, item.TipoRol, item.Rol));
                }

                sqlCommand = sqlCommand = "Exec dbo.HistoricoDepositos_Pivot";
                var queryDepositos = await db.QueryAsync<HistoricoPivotSchema>(sqlCommand);

                foreach (var item in queryDepositos)
                {
                    var historico =  result.Find(c => c.FuncionarioID == item.FuncionarioID);
                    if (historico != null)
                        historico.SetDepositos(item.Gestion2016, item.Gestion2017, item.Gestion2018, item.Gestion2019, item.Gestion2020);
                }

                sqlCommand = sqlCommand = "Exec dbo.HistoricoDeudasBancarias_Pivot";
                var queryDeudas = await db.QueryAsync<HistoricoPivotSchema>(sqlCommand);

                foreach (var item in queryDeudas)
                {
                    var historico = result.Find(c => c.FuncionarioID == item.FuncionarioID);
                    if (historico != null)
                        historico.SetDeudasBancarias(item.Gestion2016, item.Gestion2017, item.Gestion2018, item.Gestion2019, item.Gestion2020);
                }

                sqlCommand = sqlCommand = "Exec dbo.HistoricoInmuebles_Pivot";
                var queryInmuebles = await db.QueryAsync<HistoricoPivotSchema>(sqlCommand);

                foreach (var item in queryInmuebles)
                {
                    var historico = result.Find(c => c.FuncionarioID == item.FuncionarioID);
                    if (historico != null)
                        historico.SetInmuebles(item.Gestion2016, item.Gestion2017, item.Gestion2018, item.Gestion2019, item.Gestion2020);
                }

                sqlCommand = sqlCommand = "Exec dbo.HistoricoOtrosIngresos_Pivot";
                var queryOtrosIngresos = await db.QueryAsync<HistoricoPivotSchema>(sqlCommand);

                foreach (var item in queryOtrosIngresos)
                {
                    var historico = result.Find(c => c.FuncionarioID == item.FuncionarioID);
                    if (historico != null)
                        historico.SetOtrosIngresos(item.Gestion2016, item.Gestion2017, item.Gestion2018, item.Gestion2019, item.Gestion2020);
                }

                sqlCommand = sqlCommand = "Exec dbo.HistoricoValoresNegociables_Pivot";
                var queryValoresNegociables = await db.QueryAsync<HistoricoPivotSchema>(sqlCommand);

                foreach (var item in queryValoresNegociables)
                {
                    var historico = result.Find(c => c.FuncionarioID == item.FuncionarioID);
                    if (historico != null)
                        historico.SetValoresNegociables(item.Gestion2016, item.Gestion2017, item.Gestion2018, item.Gestion2019, item.Gestion2020);
                }

                sqlCommand = sqlCommand = "Exec dbo.HistoricoVehiculos_Pivot";
                var queryVehiculos = await db.QueryAsync<HistoricoPivotSchema>(sqlCommand);

                foreach (var item in queryVehiculos)
                {
                    var historico = result.Find(c => c.FuncionarioID == item.FuncionarioID);
                    if (historico != null)
                        historico.SetVehiculos(item.Gestion2016, item.Gestion2017, item.Gestion2018, item.Gestion2019, item.Gestion2020);
                }
            }

            return result;
        }

        public async Task<ICollection<HistoricoIndividual>> GetTwoGestiones(int gestionActual, int gestionAnterior)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "Exec dbo.HistoricoIndividual_Seleccionar @gestionActual, @gestionAnterior";
                var queryResult = await db.QueryAsync<HistoricoIndividualSchema>(sqlCommand, new { gestionActual, gestionAnterior});

                var result = new List<HistoricoIndividual>();
                foreach (var item in queryResult)
                {
                    var funcionario = Colaborador.Load(
                        item.Codigo, //Codigo
                        new Domain.ValueObjects.NombreCompleto(item.Nombre, item.Apellido),
                        item.CodCargo, item.Cargo, item.CodArea, item.Area, item.CodGeografico, item.UbicacionGeografica, item.CodCentroCosto, item.CentroCosto,
                        item.TipoRol, item.Rol);

                    var historicoIndividual = new HistoricoIndividual(item.Codigo, funcionario);
                    //Deposito
                    var historicoItemDeposito = new HistoricoItem(RubroDeclaracion.Depositos);
                    historicoItemDeposito.AddPatrimonioItem(new HistoricoPatrimonioItem(item.DeclaracionID_1, item.Gestion_1, item.Depositos_1));
                    historicoItemDeposito.AddPatrimonioItem(new HistoricoPatrimonioItem(item.DeclaracionID_2, item.Gestion_2, item.Depositos_2));

                    //Vehiculo
                    var historicoItemVehiculo = new HistoricoItem(RubroDeclaracion.Vehiculos);
                    historicoItemVehiculo.AddPatrimonioItem(new HistoricoPatrimonioItem(item.DeclaracionID_1, item.Gestion_1, item.Vehiculos_1));
                    historicoItemVehiculo.AddPatrimonioItem(new HistoricoPatrimonioItem(item.DeclaracionID_2, item.Gestion_2, item.Vehiculos_2));

                    //Inmueble
                    var historicoItemInmueble = new HistoricoItem(RubroDeclaracion.Inmuebles);
                    historicoItemInmueble.AddPatrimonioItem(new HistoricoPatrimonioItem(item.DeclaracionID_1, item.Gestion_1, item.Inmueble_1));
                    historicoItemInmueble.AddPatrimonioItem(new HistoricoPatrimonioItem(item.DeclaracionID_2, item.Gestion_2, item.Inmueble_2));

                    //OtroIngreso
                    var historicoItemOtroIngreso = new HistoricoItem(RubroDeclaracion.OtrosIngresos);
                    historicoItemOtroIngreso.AddPatrimonioItem(new HistoricoPatrimonioItem(item.DeclaracionID_1, item.Gestion_1, item.OtrosIngresos_1));
                    historicoItemOtroIngreso.AddPatrimonioItem(new HistoricoPatrimonioItem(item.DeclaracionID_2, item.Gestion_2, item.OtrosIngresos_2));

                    //DeudaBanco
                    var historicoItemDeudaBanco = new HistoricoItem(RubroDeclaracion.DeudasBancarias);
                    historicoItemDeudaBanco.AddPatrimonioItem(new HistoricoPatrimonioItem(item.DeclaracionID_1, item.Gestion_1, item.DeudaBanco_1));
                    historicoItemDeudaBanco.AddPatrimonioItem(new HistoricoPatrimonioItem(item.DeclaracionID_2, item.Gestion_2, item.DeudaBanco_2));

                    //OtrosIngresos
                    var historicoItemOtrosIngresos = new HistoricoItem(RubroDeclaracion.OtrosIngresos);
                    historicoItemOtrosIngresos.AddPatrimonioItem(new HistoricoPatrimonioItem(item.DeclaracionID_1, item.Gestion_1, item.OtrosIngresos_1));
                    historicoItemOtrosIngresos.AddPatrimonioItem(new HistoricoPatrimonioItem(item.DeclaracionID_2, item.Gestion_2, item.OtrosIngresos_2));

                    historicoIndividual.AppendHistorico(RubroDeclaracion.Depositos, historicoItemDeposito);
                    historicoIndividual.AppendHistorico(RubroDeclaracion.Vehiculos, historicoItemVehiculo);
                    historicoIndividual.AppendHistorico(RubroDeclaracion.Inmuebles, historicoItemInmueble);
                    historicoIndividual.AppendHistorico(RubroDeclaracion.OtrosIngresos, historicoItemOtroIngreso);
                    historicoIndividual.AppendHistorico(RubroDeclaracion.DeudasBancarias, historicoItemDeudaBanco);

                    result.Add(historicoIndividual);
                }

                return result;
             }
        }
    }
}
