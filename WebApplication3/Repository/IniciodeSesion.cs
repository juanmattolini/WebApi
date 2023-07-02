//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Data.SqlClient;
//using System.Data;
//using WebApi.Models;




//namespace WebApi.Repository
//{
//    public static Usuario IniciarSesion(string nombreUsuario, string contrasena)

//    {
//        string connectionString = @"Server=swdmdzbaspi02;Database=SistemaGestion;Trusted_Connection=True;";
//        using (var connection = new SqlConnection(connectionString))

//        {

//            connection.Open();

//            const string query = @"SELECT Id, Nombre, Apellido, NombreUsuario, Contraseña, Mail FROM Usuario WHERE NombreUsuario = @nombreUsuario AND Contraseña = @contrasena";
//            //escribimos la query que queremos ejecutar
//            using (var command = new SqlCommand(query, connection))

//            {

//                command.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);

//                command.Parameters.AddWithValue("@contrasena", contrasena);



//                //iteramos cada fila que nos devuelve el select
//                using (var reader = command.ExecuteReader())

//                {

//                    if (reader.Read())

//                    {

//                        return new Usuario

//                        {

//                            Id = reader.GetInt64(0),

//                            Nombre = reader.GetString(1),

//                            Apellido = reader.GetString(2),

//                            NombreUsuario = reader.GetString(3),

//                            Contraseña = reader.GetString(4),

//                            Mail = reader.GetString(5)

//                        };

//                    }

//                    else

//                    {

//                        return null;

//                    }

//                }

//            }

//        }

//    }
//}

