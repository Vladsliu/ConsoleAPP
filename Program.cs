using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace Students_APP
{
    class Program
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["StudentsDB"].ConnectionString;
        private static SqlConnection sqlConnection = null;
        static void Main(string[] args)
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            Console.WriteLine("StudentApp");

            SqlDataReader sqlDataReader = null;
            string command = string.Empty;

            while (true)
            {
                try
                {
                    Console.Write("> ");
                    command = Console.ReadLine();
                    if (command.ToLower().Equals("exit"))
                    {
                        if (sqlConnection.State == ConnectionState.Open)
                        {
                            sqlConnection.Close();
                        }
                        if (sqlDataReader != null)
                        {
                            sqlDataReader.Close();
                        }
                        break;
                    }
                    SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
                    switch (command.Split(' ')[0].ToLower())
                    {
                        case "select":
                            sqlDataReader = sqlCommand.ExecuteReader();
                            while (sqlDataReader.Read())
                            {
                                Console.WriteLine($"{sqlDataReader["Id"]} {sqlDataReader["FIO"]}" +
                                    $"{sqlDataReader["Birthday"]} {sqlDataReader["University"]}" +
                                    $"{sqlDataReader["Group_number"]} {sqlDataReader["Course"]} {sqlDataReader["Average_score"]}");
                                Console.WriteLine(new string('-', 32));
                            }
                            if (sqlDataReader != null)
                            {
                                sqlDataReader.Close();
                            }
                            break;
                        case "insert":
                            Console.WriteLine($"Inserted: {sqlCommand.ExecuteNonQuery()} line (s)");
                            break;
                        case "update":
                            break;
                            Console.WriteLine($"Update: {sqlCommand.ExecuteNonQuery()} line (s)");
                        case "delete":
                            Console.WriteLine($"Deleted: {sqlCommand.ExecuteNonQuery()} line (s)");
                            break;
                        default:
                            Console.WriteLine($"Command {command} not correct");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message }");
                }

                Console.WriteLine("Press anu cay for continue...");
                Console.ReadKey();
            }
        }
    }
}
