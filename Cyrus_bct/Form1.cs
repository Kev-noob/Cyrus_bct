using Cyrus_bct.Models;
using Cyrus_bct.INS;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Cyrus_bct
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //DATABASE CONNECTION--->
        private MySqlConnection GetConnection()
        {
            string connStr = "server=localhost;" +
                             "port=3306;" +
                             "database=bct_db;" +
                             "user=root;" +
                             "password=;";
            return new MySqlConnection(connStr);
        }//<----

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void regBtn_switch_Click(object sender, EventArgs e)
        {
            panel1.BringToFront();
        }

        private void logins_switch_Click(object sender, EventArgs e)
        {
            panel2.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.BringToFront();
        }

        private void registerBtn_Click(object sender, EventArgs e)
        {
            string studentId = id_txtBox.Text.Trim();
            string fullName = name_txtBox.Text.Trim();
            string course = course_txtBox.Text.Trim();
            string password = password_txtBox.Text;
            string confirmPassword = confirmPass_txtBox.Text;

            //input & placeholder verification-->
            if (name_txtBox.Text == "" ||
                   id_txtBox.Text == "" ||
                   course_txtBox.Text == "" ||
                   yearlv_txtBox.Text == "" ||
                   password_txtBox.Text == "" ||
                   confirmPass_txtBox.Text == "")
            {
                MessageBox.Show("Please complete inputs.");
                return;
            }//<--


            //PASSWORD CHECKER-->
            if (password != confirmPassword)
            {
                MessageBox.Show("PasswordS does not match.");
                return;
            }//<---


            //YEAR LEVEL CHECKER--->
            int yearLevel;
            if (!int.TryParse(yearlv_txtBox.Text.Trim(), out yearLevel))
            {
                MessageBox.Show("Year level must be a number.");
                return;
            }

            if (yearLevel < 1 || yearLevel > 4)
            {
                MessageBox.Show("Year level must be between 1 and 4.");
                return;
            }//<---



            //INSERT TO DATABASE--->
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                //ID UNIQUENESS CHEKER--->
                string checkSql = "SELECT COUNT(*) FROM inputs WHERE student_id = @studentId";
                using (MySqlCommand checkCmd = new MySqlCommand(checkSql, conn))
                {
                    checkCmd.Parameters.AddWithValue("@studentId", studentId);
                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                    if (count > 0)
                    {
                        MessageBox.Show("Student ID already exists. Please choose a different one.");
                        return;
                    }
                }//<----


                string sql = @"INSERT INTO inputs (student_id, full_name, year_level, password)
                             VALUES(@student_id, @full_name, @year_level, @password)";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@student_id", studentId);
                    cmd.Parameters.AddWithValue("@full_name", fullName);
                    
                    cmd.Parameters.AddWithValue("@year_level", yearLevel);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.ExecuteNonQuery();
                }

            }//<----
            MessageBox.Show("Registered Succesfully.");
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            string studentId = login_id_txtBox.Text.Trim();
            string password = login_password_txtBox.Text;
            

            //CHECK FOR EMPTY TEXTBOX---->
            if (studentId == "" || password == "")
            {
                MessageBox.Show("Please complete inputs.");
                return;
            }//<----


            try
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT student_id, full_name, password FROM inputs WHERE student_id=@student_id AND password=@password";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@student_id", studentId);
                        cmd.Parameters.AddWithValue("@password", password);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string id = reader.GetString("student_id");
                                string name = reader.GetString("full_name");


                                //GO TO HOME SCREEN
                                Form2 home = new Form2();
                                home.Name = name;
                                this.Hide();
                                home.Show();
                            }
                            else
                            {
                                MessageBox.Show("Invalid Student ID or Password.");
                            }
                        }
                    }
                }

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Database error: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        //EYE HIDE SHOW --->
        private void eye_btn_Click(object sender, EventArgs e)
        {
            if (password_txtBox.PasswordChar == '•') 
            {
                password_txtBox.PasswordChar = '\0';
                RegEye_btn.Image = Properties.Resources.open_eye;

            }
            else
            {
                password_txtBox.PasswordChar = '•';
                RegEye_btn.Image = Properties.Resources.off_eye;

            }
        }
        private void RegConEye_btn_Click(object sender, EventArgs e)
        {
            if (confirmPass_txtBox.PasswordChar == '•')
            {
                confirmPass_txtBox.PasswordChar = '\0';
                RegConEye_btn.Image = Properties.Resources.open_eye;
            }
            else
            {
                confirmPass_txtBox.PasswordChar = '•';
                RegConEye_btn.Image = Properties.Resources.off_eye;
            }
        }
        private void Login_eye_Click(object sender, EventArgs e)
        {
            if (login_password_txtBox.PasswordChar == '•')
            {
                login_password_txtBox.PasswordChar = '\0';
                Login_eye.Image = Properties.Resources.open_eye;
            }
            else
            {
                login_password_txtBox.PasswordChar = '•';
                Login_eye.Image = Properties.Resources.off_eye;
            }
        }
        private void login_student_Click(object sender, EventArgs e)
        {
            string studentId = login_id_txtBox.Text.Trim();
            string password = login_password_txtBox.Text;

            //CHECK FOR EMPTY TEXTBOX---->
            if (studentId == "" || password == "")
            {
                MessageBox.Show("Please complete inputs.");
                return;
            }//<----


            try
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT student_id, full_name FROM inputs WHERE student_id=@student_id AND password=@password";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@student_id", studentId);
                        cmd.Parameters.AddWithValue("@password", password);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string id = reader.GetString("student_id");
                                string name = reader.GetString("full_name");


                                //GO TO HOME SCREEN
                                Form2 loginForm = new Form2();
                                this.Hide();
                                loginForm.Show();
                            }
                            else
                            {
                                MessageBox.Show("Invalid Student ID or Password.");
                            }
                        }
                    }
                }

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Database error: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void student_Click(object sender, EventArgs e)
        {
            reg_panel.BringToFront();
        }

        private void InsReg_btn_Click(object sender, EventArgs e)
        {
            INS_reg.BringToFront();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            student_login.BringToFront();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            instructor_login.BringToFront();
        }
        ///--------------------------------------------------------------------------------INS REG-------------------------------------------------------------------------------
        private void INS_FinReg_Click(object sender, EventArgs e)
        {
            string Ins_name = INS_name.Text.Trim();
            string Ins_email = INS_email.Text.Trim();
            string Ins_phone = INS_phone.Text.Trim();
            string Ins_department = INS_department.Text.Trim();

            string password = INS_pass.Text;
            string confirmPassword = INS_cnfrm_pass.Text;

            //input & placeholder verification-->
            if (INS_name.Text == "" ||
                   INS_email.Text == "" ||
                   INS_phone.Text == "" ||
                   INS_department.Text == "" ||

                   INS_pass.Text == "" ||
                   INS_cnfrm_pass.Text == "")
            {
                MessageBox.Show("Please complete inputs!");
                return;
            }//<--


            //PASSWORD CHECKER-->
            if (password != confirmPassword)
            {
                MessageBox.Show("PasswordS does not match!");
                return;
            }//<---



            Instructor instructor = new Instructor
            {
                FullName = INS_name.Text.Trim(),
                Email = INS_email.Text.Trim(),
                Phone = INS_phone.Text.Trim(),
                Department = INS_department.Text.Trim(),
                Password = INS_pass.Text.Trim()
            };

            DBHelper.PendingINS(instructor);


        }
        
    }
}
