using Dapper;
using Entities;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class NewsDAL
    {
        private string ObtenerCadenaConexion()
        {
            return ConfigurationManager.ConnectionStrings["SignalR"].ToString();
        }

        public bool RegisterNews(News news)
        {
            using (IDbConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                conexion.Open();
                var parametros = new DynamicParameters();

                parametros.Add("Minute", news.Minute);
                parametros.Add("Message", news.Message);
                parametros.Add("EventTime", news.EventTime);

                var result = conexion.Execute("dbo.sp_news_register", param: parametros, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }

        public IEnumerable<News> GetNews()
        {
            using (IDbConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                conexion.Open();
                var coleccion = conexion.Query<News>("dbo.sp_news_getAll", commandType: CommandType.StoredProcedure);
                return coleccion;
            }
        }
    }
}
