
using Microsoft.Data.SqlClient;

namespace surtidoraSmart
{
    public static class ConexionBD
    {
        private static readonly string cadenaConexion =
            @"Server=.\SQLEXPRESS;Database=SurtidoraSmart;Integrated Security=True;TrustServerCertificate=True;";

        public static SqlConnection ObtenerConexion()
        {
            return new SqlConnection(cadenaConexion);
        }
    }
}
