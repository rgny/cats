using Cats.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Cats.Data
{
    public static class DataAccess
    {
        static string _connectionString;

        static DataAccess()=>_connectionString = ConfigurationManager.ConnectionStrings["CatsDbCon"].ConnectionString;
        
        public static (bool,List<Cat>) GetCats()
        {

            List<Cat> cats = new List<Cat>();

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var sql = "SELECT Id,Name,Breed,Age FROM dbo.Cat WHERE Is_Active=1";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        using (var adapter = new SqlDataAdapter(cmd))
                        {
                            var dt = new DataTable();
                            adapter.Fill(dt);

                            foreach (DataRow row in dt.Rows)
                            {
                                cats.Add(new Cat
                                {
                                    Id = Int32.Parse(row["Id"].ToString()),
                                    Name = row["Name"].ToString(),
                                    Breed = row["Breed"].ToString(),
                                    Age = Byte.Parse(row["Age"].ToString())

                                });
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //log ex here - TBD

                return (false, cats);
                
            }

            return (true, cats);

        }

        public static bool AddCat(Cat record)
        {

            using (var conn = new SqlConnection(_connectionString))
            {
                var sql = "INSERT INTO dbo.Cat(Name,Breed,Age,Is_Active,Created_Dt) VALUES (@Name,@Breed,@Age,1,GETDATE())";

                using (var cmd = new SqlCommand(sql, conn))
                {

                    cmd.Parameters.AddWithValue("Name", record.Name);
                    cmd.Parameters.AddWithValue("Breed", record.Breed);
                    cmd.Parameters.AddWithValue("Age", record.Age);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        //log ex here - TBD

                        return false;
                    }
                    finally
                    {
                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                    }

                }
            }

            return true;
        }

        public static bool UpdateCat(Cat record)
        {

            using (var conn = new SqlConnection(_connectionString))
            {
                var sql = "UPDATE dbo.Cat SET Name=@Name," +
                    "                         Breed=@Breed," +
                    "                         Age=@Age," +
                    "                         Updated_Dt=GETDATE()" +
                    "      WHERE Id=@Id";

                using (var cmd = new SqlCommand(sql, conn))
                {

                    cmd.Parameters.AddWithValue("Name", record.Name);
                    cmd.Parameters.AddWithValue("Breed", record.Breed);
                    cmd.Parameters.AddWithValue("Age", record.Age);
                    cmd.Parameters.AddWithValue("Id", record.Id);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        //log ex here - TBD

                        return false;
                    }
                    finally
                    {
                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                    }

                }
            }

            return true;

        }

        public static bool DeleteCat(int Id)
        {

            using (var conn = new SqlConnection(_connectionString))
            {
                //logical delete
                var sql = "UPDATE dbo.Cat SET Is_Active=0," +
                    "                         Updated_Dt=GETDATE()" +
                    "      WHERE Id=@Id";

                using (var cmd = new SqlCommand(sql, conn))
                {

                    cmd.Parameters.AddWithValue("Id", Id);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        //log ex here - TBD

                        return false;
                    }
                    finally
                    {
                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                    }

                }
            }

            return true;
        }

    }
}