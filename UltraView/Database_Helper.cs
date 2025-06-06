using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace UltraView
{
    internal class Database_Helper
    {
        private static string database_file = "user.db";

        public static void InitializeDatabase()
        {
            if (!System.IO.File.Exists(database_file))
            {
                SQLiteConnection.CreateFile(database_file);
                using (var conn = new SQLiteConnection($"Data Source={database_file};Version=3;"))
                {
                    conn.Open();
                    string createTable = @"CREATE TABLE Users (
                                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                        Username TEXT NOT NULL UNIQUE,
                                        Password TEXT NOT NULL)";
                    SQLiteCommand cmd = new SQLiteCommand(createTable, conn);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
        public static bool CheckLogin(string username, string password)
        {
            using (var conn = new SQLiteConnection($"Data Source={database_file};Version=3;"))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Users WHERE Username=@user AND Password=@pass";
                var cmd = new SQLiteCommand(query, conn);
                cmd.Parameters.AddWithValue("@user", username);
                cmd.Parameters.AddWithValue("@pass", HashPassword(password));
                long count = (long)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        public static bool RegisterUser(string username, string password)
        {
            using (var conn = new SQLiteConnection($"Data Source={database_file};Version=3;"))
            {
                conn.Open();
                string query = "INSERT INTO Users (Username, Password) VALUES (@user, @pass)";
                var cmd = new SQLiteCommand(query, conn);
                cmd.Parameters.AddWithValue("@user", username);
                cmd.Parameters.AddWithValue("@pass", HashPassword(password));
                try
                {
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
