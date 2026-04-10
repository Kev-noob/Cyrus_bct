using Cyrus_bct.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cyrus_bct.INS
{
    internal class DBHelper
    {
        //DATABASE CONNECTION--->
        private static MySqlConnection GetConnection()
        {
            string connStr = "server=localhost;" +
                             "port=3306;" +
                             "database=BCT_db;" +
                             "user=root;" +
                             "password=;";
            return new MySqlConnection(connStr);
        }//<----

        public static bool PendingINS(Instructor instructor)
        {
            try
            {
                using (MySqlConnection conn = GetConnection())
                {
                    string query = @"INSERT INTO PendingInstructor (FullName, Email, Phone, Department, Password) 
                             VALUES (@name, @email, @phone, @department, @password)";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", instructor.FullName);
                    cmd.Parameters.AddWithValue("@email", instructor.Email);
                    cmd.Parameters.AddWithValue("@phone", instructor.Phone);
                    cmd.Parameters.AddWithValue("@department", instructor.Department);
                    cmd.Parameters.AddWithValue("@password", instructor.Password);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Registration submitted! Awaiting admin approval.", "Success",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return true;
                }
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062)
                    MessageBox.Show("That email is already registered.", "Duplicate",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    MessageBox.Show("Database error: " + ex.Message);

                return false;
            }
        }
    }
}
