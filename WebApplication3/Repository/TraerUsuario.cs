using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using WebApi.Models;





namespace WebApi.Repository

{



    public static class TraerUsuario
    {
    
        //public static List<Usuario> ListarUsuarios()
        public static List<Usuario> GetUsuarios(DataTable table)

        {
            string connectionString = @"Server=swdmdzbaspi02;Database=SistemaGestion;Trusted_Connection=True;";
            var query = "SELECT Id,Nombre,Apellido,NombreUsuario,Contraseña,Mail FROM Usuario";
            var listaUsuarios = new List<Usuario>();

            using (SqlConnection conect = new SqlConnection(connectionString))
            {
                conect.Open();
                using (SqlCommand comando = new SqlCommand(query, conect))
                {
                    using (SqlDataReader dr = comando.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            var usuario = new Usuario();
                            usuario.Id = dr.GetInt64("Id");
                            usuario.Nombre = dr.GetString("Nombre");
                            usuario.Apellido = dr.GetString("Apellido");
                            usuario.NombreUsuario = dr.GetString("NombreUsuario");
                            usuario.Contraseña = dr.GetString("Contraseña");
                            usuario.Mail = dr.GetString("Mail");
                            listaUsuarios.Add(usuario);
                        }

                    }

                }
            }
            return listaUsuarios;
        }
        public static Usuario GetUsuarioByPassword(string nombre, string contraseña)
        {

            Usuario usuario = new Usuario();
            string connectionString = @"Server=swdmdzbaspi02;Database=SistemaGestion;Trusted_Connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.Connection.Open();

                    command.CommandText = @" SELECT * 
                                FROM Usuario 
                                WHERE NombreUsuario = @nombre
                                AND   Contraseña = @contraseña;";

                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@contraseña", contraseña);

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    if (table.Rows.Count < 1)
                    {
                        return new Usuario();
                    }


                    List<Usuario> usuarios = GetUsuarios(table);
                    usuario = usuarios[0];

                    command.Connection.Close();
                }
            }
            return usuario;
        }

        public static bool InsertUsuario(Usuario usuario)
        {
            bool alta = false;
            Usuario usuarioRepetido = GetUsuarioByUserName(usuario.NombreUsuario);
            string connectionString = @"Server=swdmdzbaspi02;Database=SistemaGestion;Trusted_Connection=True;";

            if (usuario.NombreUsuario == null ||
                usuario.NombreUsuario.Trim() == "" ||
                usuario.Contraseña == null ||
                usuario.Contraseña.Trim() == "" ||
                usuario.Nombre == null ||
                usuario.Nombre.Trim() == "" ||
                usuario.Apellido == null ||
                usuario.Apellido.Trim() == "")
            {
                return alta;
                throw new Exception("Faltan datos obligatorios");
            }
            else if (usuarioRepetido.Id != 0)
            {
                return alta;
                throw new Exception("El nombre de usuario ya existe");
            }
            else
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.Connection.Open();
                sqlCommand.CommandText = @"INSERT INTO Usuario
                                    ([Nombre]
                                    ,[Apellido]
                                    ,[NombreUsuario]
									,[Contraseña]
									,[Mail] )
                                    VALUES
                                    (@Nombre,
                                        @Apellido,
                                        @NombreUsuario,
										@Contraseña,
										@Mail)";

                sqlCommand.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                sqlCommand.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                sqlCommand.Parameters.AddWithValue("@NombreUsuario", usuario.NombreUsuario);
                sqlCommand.Parameters.AddWithValue("@Contraseña", usuario.Contraseña);
                sqlCommand.Parameters.AddWithValue("@Mail", usuario.Mail);

                sqlCommand.ExecuteNonQuery(); //Se ejecuta realmente el INSERT INTO
                usuario.Id = GetId.Get(sqlCommand);

                alta = usuario.Id != 0 ? true : false;
                sqlCommand.Connection.Close();
                return alta;

            }
        }

        public static bool UpdateUsuario(Usuario usuario)
        {
            bool modificado = false;
            string connectionString = @"Server=swdmdzbaspi02;Database=SistemaGestion;Trusted_Connection=True;";

            if (usuario.NombreUsuario == null ||
                usuario.NombreUsuario.Trim() == "" ||
                usuario.Contraseña == null ||
                usuario.Contraseña.Trim() == "" ||
                usuario.Nombre == null ||
                usuario.Nombre.Trim() == "" ||
                usuario.Apellido == null ||
                usuario.Apellido.Trim() == "")
            {
                return modificado;
                throw new Exception("Faltan datos obligatorios");
            }
            else
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.Connection.Open();
                        sqlCommand.CommandText = @" UPDATE Usuario
                                                SET 
                                                   Nombre = @Nombre,
                                                   Apellido = @Apellido,
                                                   NombreUsuario = @NombreUsuario,
										           Contraseña = @Contraseña,
										           Mail = @Mail
                                                WHERE id = @ID";

                        sqlCommand.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                        sqlCommand.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                        sqlCommand.Parameters.AddWithValue("@NombreUsuario", usuario.NombreUsuario);
                        sqlCommand.Parameters.AddWithValue("@Contraseña", usuario.Contraseña);
                        sqlCommand.Parameters.AddWithValue("@Mail", usuario.Mail);
                        sqlCommand.Parameters.AddWithValue("@ID", usuario.Id);


                        int recordsAffected = sqlCommand.ExecuteNonQuery(); //Se ejecuta realmente UPDATE

                        sqlCommand.Connection.Close();
                        if (recordsAffected == 0)
                        {
                            return modificado;
                            throw new Exception("El registro a modificar no existe.");
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
        }

        public static Usuario GetUsuarioByUserName(string nombre)
        {
            Usuario usuario = new Usuario();
            string connectionString = @"Server=swdmdzbaspi02;Database=SistemaGestion;Trusted_Connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.Connection.Open();

                    command.CommandText = @"SELECT * 
                                FROM Usuario 
                                WHERE NombreUsuario = @nombre;";

                    command.Parameters.AddWithValue("@nombre", nombre);

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    if (table.Rows.Count < 1)
                    {
                        return new Usuario();
                    }


                    List<Usuario> usuarios = GetUsuarios(table);
                    usuario = usuarios[0];

                    command.Connection.Close();
                }
            }
            return usuario;
        }



    }
}

