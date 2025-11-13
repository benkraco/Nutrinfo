using Microsoft.Data.SqlClient;
using Dapper;
using System;

namespace Nutrinfo.Models;

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

    public static Usuarios traerUsuarioRegistro(string nombre, string apellido, string contrasena)
    {
        Usuarios usuario = new Usuarios();
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Usuarios WHERE Nombre = @pNombre AND Apellido = @pApellido AND Contrasena = @pContrasena";
            usuario = connection.QueryFirstOrDefault<Usuarios>(query, new { pNombre = nombre, pApellido = apellido, pContrasena = contrasena });
        }
        return usuario;
    }

    public static Usuarios traerUsuarioLogin(string email, string contrasena)
    {
        Usuarios usuario = new Usuarios();
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Usuarios WHERE Email = @pEmail AND Contrasena = @pContrasena";
            usuario = connection.QueryFirstOrDefault<Usuarios>(query, new { pEmail = email, pContrasena = contrasena });
        }
        return usuario;
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

    public static List<Productos> ListarProductos()
    {
        List<Productos> listaProductos = new List<Productos>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            listaProductos = connection.Query<Productos>("SELECT * FROM Productos").ToList();
        }

        return listaProductos;
    }

    public static List<Productos> BuscarProductos(string busqueda)
    {
        List<Productos> listaProductos = new List<Productos>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Query<Productos>("SELECT * FROM Productos WHERE Nombre LIKE @pBusqueda", new { pBusqueda = busqueda }).ToList();
        }

        return listaProductos;
    }
}