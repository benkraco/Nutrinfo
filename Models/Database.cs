using Microsoft.Data.SqlClient;
using Dapper;
using System;

public static class Database
{
    private static string _connectionString = @"Server=localhost;DataBase=Nutrinfo;Integrated Security=True;TrustServerCertificate=True";

    public static int RegistrarUsuario(string nombre, string apellido, string contrasena, string email)
    {
        int Existe;
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            Existe = connection.QuerySingleOrDefault<int>("RegistrarUsuario", new { Nombre = nombre, Apellido = apellido, Contrasena = contrasena, Email = email }, commandType: System.Data.CommandType.StoredProcedure);
        }
        return Existe;
    }

    public static int IniciarSesion(string email, string contrasena)
    {
        int Existe;
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            Existe = connection.QuerySingleOrDefault<int>("IniciarSesion", new { Email = email, Contrasena = contrasena }, commandType: System.Data.CommandType.StoredProcedure);
        }
        return Existe;
    }

    public static void CrearPerfilPersonalizado(int idUsuario, string alergias, string intolerancias, string enfermedades, string cultura, string estiloDeVida, string dieta)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.QuerySingleOrDefault<int>("CrearPerfilPersonalizado", new { IDUsuario = idUsuario, Intolerancias = intolerancias, Enfermedades = enfermedades, Cultura = cultura, EstiloDeVida = estiloDeVida, Dieta = dieta }, commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}