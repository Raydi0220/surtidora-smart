
using System;
using Microsoft.Data.SqlClient;

namespace surtidoraSmart
{
    public class PruebaConexion
    {
        public static void Probar()
        {
            try
            {
                using (SqlConnection conexion = ConexionBD.ObtenerConexion())
                {
                    conexion.Open();
                    System.Windows.Forms.MessageBox.Show(
                        "¡Conexión exitosa con SurtidoraSmart!",
                        "Surtidora Smart");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(
                    "Error de conexión: " + ex.Message,
                    "Surtidora Smart");
            }
        }
    }
}
