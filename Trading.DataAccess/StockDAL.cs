using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Trading.Entities;

namespace Trading.DataAccess
{
    public class StockDAL
    {
        private string ObtenerCadenaConexion()
        {
            return ConfigurationManager.ConnectionStrings["SignalR"].ToString();
        }

        public IEnumerable<Stock> GetAllStocks()
        {
            using (IDbConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                conexion.Open();
                var coleccion = conexion.Query<Stock>("dbo.sp_stock_getAll", commandType: CommandType.StoredProcedure);
                return coleccion;
            }
        }

        public IEnumerable<Stock> GetLastStocks()
        {
            using (IDbConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                conexion.Open();
                var coleccion = conexion.Query<Stock>("dbo.sp_stock_getLastDate", commandType: CommandType.StoredProcedure);
                return coleccion;
            }
        }
    }
}
