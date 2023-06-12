using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using WorkFlowX.Models;
using System.Data;
using System.Data.SqlClient;

namespace WorkFlowX.Data
{
    public class DBUser
    {
        private static string connectionString = "Server=(local); Database=WorkFlowX; Trusted_Connection=True; TrustServerCertificate=True;";
        public static bool Register(DtoUser user) {
            bool result = false;
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    string insertQuery = "INSERT INTO Users(UserName, UserMail, UserPassword, NeedRestore, HasConfirmation, Token, ComId)";
                    insertQuery += " values(@userName, @userMail, @userPassword, @needRestore, @hasConfirmation, @token, @comId)";

                    SqlCommand cmd = new SqlCommand(insertQuery, conn);
                    cmd.Parameters.AddWithValue("@userName", user.UserName);
                    cmd.Parameters.AddWithValue("@userMail", user.UserMail);
                    cmd.Parameters.AddWithValue("@userPassword", user.UserPassword);
                    cmd.Parameters.AddWithValue("@needRestore", user.Restore);
                    cmd.Parameters.AddWithValue("@hasConfirmation", user.Confirmation);
                    cmd.Parameters.AddWithValue("@token", user.Token);
                    cmd.Parameters.AddWithValue("@comId", user.ComId);
                    cmd.CommandType = CommandType.Text;

                    conn.Open();

                    int AfectedLines = cmd.ExecuteNonQuery();
                    if(AfectedLines > 0) result = true;
                }
                return result;
            }catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DtoUser Validate(string userMail, string userPassword)
        {
            DtoUser user = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string insertQuery = "SELECT UserName, NeedRestore, HasConfirmation from Users";
                    insertQuery += " WHERE UserMail = @userMail and UserPassword = @userPassword";

                    SqlCommand cmd = new SqlCommand(insertQuery, conn);
                    cmd.Parameters.AddWithValue("@userMail", userMail);
                    cmd.Parameters.AddWithValue("@userPassword", userPassword);
                    cmd.CommandType = CommandType.Text;

                    conn.Open();
                    
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            user = new DtoUser()
                            {
                                UserName = reader["UserName"].ToString(),
                                Restore = (bool)reader["NeedRestore"],
                                Confirmation = (bool)reader["HasConfirmation"]
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return user;
        }

        public static DtoUser Find(string userMail)
        {
            DtoUser user = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string insertQuery = "SELECT UserName, UserPassword, NeedRestore, HasConfirmation, Token, ComId from Users";
                    insertQuery += " WHERE UserMail = @userMail";

                    SqlCommand cmd = new SqlCommand(insertQuery, conn);
                    cmd.Parameters.AddWithValue("@userMail", userMail);
                    cmd.CommandType = CommandType.Text;

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new DtoUser()
                            {
                                UserName = reader["UserName"].ToString(),
                                UserPassword = reader["UserPassword"].ToString(),
                                Restore = (bool)reader["NeedRestore"],
                                Confirmation = (bool)reader["HasConfirmation"],
                                Token = reader["Token"].ToString(),
                                ComId = (int)reader["ComId"]
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return user;
        }

        public static bool Restore(int restore, string userPassword, string token)
        {
            bool result = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string insertQuery = "UPDATE Users SET";
                    insertQuery += " NeedRestore = @needRestore, UserPassword = @userPassword " +
                        "WHERE Token = @token";

                    SqlCommand cmd = new SqlCommand(insertQuery, conn);
                    cmd.Parameters.AddWithValue("@needRestore", restore);
                    cmd.Parameters.AddWithValue("@userPassword", userPassword);
                    cmd.Parameters.AddWithValue("@token", token);
                    cmd.CommandType = CommandType.Text;

                    conn.Open();

                    int AfectedLines = cmd.ExecuteNonQuery();
                    if (AfectedLines > 0) result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool Confirm(string token)
        {
            bool result = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string insertQuery = "UPDATE Users SET";
                    insertQuery += " HasConfirmation = 1" +
                        " WHERE Token = @token";

                    SqlCommand cmd = new SqlCommand(insertQuery, conn);
                    cmd.Parameters.AddWithValue("@token", token);
                    cmd.CommandType = CommandType.Text;

                    conn.Open();

                    int AfectedLines = cmd.ExecuteNonQuery();
                    if (AfectedLines > 0) result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}