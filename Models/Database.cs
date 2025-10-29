using Microsoft.Data.SqlClient;
using Dapper;
using System;

public static class Database
{
    public int RegistrarUsuario(string nombre, string apellido, string contrasena, string email)
    {
        bool Existe;
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            Existe = connection.QuerySingleOrDefault<int>("RegistrarUsuario", new { Nombre = nombre, Apellido = apellido, Contrasena = constrasena, Email = email }, commandType: System.Data.CommandType.StoredProcedure);
        }
        return Existe;
    }

    public int IniciarSesion(string email, string contrasena)
    {
        bool Existe;
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            Existe = connection.QuerySingleOrDefault<int>("IniciarSesion", new { Email = email, Contrasena = constrasena }, commandType: System.Data.CommandType.StoredProcedure);
        }
        return Existe;
    }

    public int CrearPerfilPersonalizado(int idUsuario, string alergias, string intolerancias, string enfermedades, string cultura, string estiloDeVida, string dieta)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.QuerySingleOrDefault<int>("CrearPerfilPersonalizado", new { IDUsuario = idUsuario, Intolerancias = intolerancias, Enfermedades = enfermedades, Cultura = cultura, EstiloDeVida = estiloDeVida, Dieta = dieta }, commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}