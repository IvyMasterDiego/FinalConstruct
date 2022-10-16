using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectConstruct.Class.Model
{
    public class PersonInformation
    {
        private static string myConn = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        public int Id { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Celular { get; set; }
        public string Direccion { get; set; }

        private const string selectQuery = "SELECT * FROM Person";
        private const string deleteQuery = "DELETE FROM Persona WHERE Cedula=@Cedula";
        private const string updateQuery = "UPDATE Person SET id=@Id, Cedula=@Cedula, Nombre=@Nombre, Apellido=@Apellido, Celular=@Celular, Direccion=@Direccion";
        private const string insertQuery = "INSERT INTO Person( id, Cedula, Nombre, Apellido, Celular, Direccion) VALUES ( @id, @Cedula, @Nombre, @Apellido, @Celular, @Direccion)";
       
        public bool InsertPerson(PersonInformation person)
        {
            int rows;
            using (SqlConnection con = new SqlConnection(myConn))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand(insertQuery, con))
                {
                    com.Parameters.AddWithValue("@id", person.Id);
                    com.Parameters.AddWithValue("@Cedula", person.Cedula);
                    com.Parameters.AddWithValue("@Nombre", person.Nombre);
                    com.Parameters.AddWithValue("@Apellido", person.Apellido);
                    com.Parameters.AddWithValue("@Celular", person.Celular);
                    com.Parameters.AddWithValue("@Direccion", person.Direccion);
                    rows = com.ExecuteNonQuery();
                }
            }
            return (rows > 0) ? true : false;
        }

        public bool UpdatePerson(PersonInformation person)
        {
            int rows;
            using (SqlConnection con = new SqlConnection(myConn))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand(updateQuery, con))
                {
                    com.Parameters.AddWithValue("@id", person.Id);
                    com.Parameters.AddWithValue("@Cedula", person.Cedula);
                    com.Parameters.AddWithValue("@Nombre", person.Nombre);
                    com.Parameters.AddWithValue("@Apellido", person.Apellido);
                    com.Parameters.AddWithValue("@Celular", person.Celular);
                    com.Parameters.AddWithValue("@Direccion", person.Direccion);
                    rows = com.ExecuteNonQuery();
                }
            }
            return (rows > 0) ? true : false;
        }

        public bool DeletePerson(PersonInformation person)
        {
            int rows;
            using (SqlConnection con = new SqlConnection(myConn))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand(deleteQuery, con))
                {
                    com.Parameters.AddWithValue("@Cedula", person.Id);
                    rows = com.ExecuteNonQuery();
                }
            }
            return (rows > 0) ? true : false;
        }

        public DataTable GetPersons()
        {
            var datatable = new DataTable();
            using (SqlConnection con = new SqlConnection(myConn))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand(selectQuery, con))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(com))
                    {
                        adapter.Fill(datatable);
                    }
                }
            }
            return datatable;
        }
    }
}
