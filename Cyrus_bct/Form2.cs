using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Cyrus_bct
{
    public partial class Form2 : Form
    {
        public string Name { get; set; }

        public Form2()
        {
            InitializeComponent();
           
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            welcomeLabel.Text = "Welcome student,\n" + Name;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //GO TO HOME SCREEN
            Form1 loginForm = new Form1();
            this.Hide();
            loginForm.Show();
        }

        //EXIT METHOD -->
        private void exit()
        {
           this.Close();
        }//<---
        private void button1_Click(object sender, EventArgs e)
        {
            exit();
        }
       private void button7_Click(object sender, EventArgs e)
        {
            exit();
        }
        private void button10_Click(object sender, EventArgs e)
        {
            exit();
        }
        private void button12_Click(object sender, EventArgs e)
        {
            exit();
        }
        private void button14_Click(object sender, EventArgs e)
        {
            exit();
        }


        //HOME BRING TO FRONT METHOD -->
        private void HOME()
        {
            home_panel.BringToFront();
        }//<---

        private void button8_Click(object sender, EventArgs e)
        {
            HOME();
        }
        private void button9_Click(object sender, EventArgs e)
        {
            HOME();
        }
        private void button11_Click(object sender, EventArgs e)
        {
            HOME();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            HOME();
        }

        //BUTTON TO FRONT METHOD -->
        private void button2_Click(object sender, EventArgs e)
        {
            profile_panel.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            enroll_panel.BringToFront();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            courses_panel.BringToFront();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            grades_panel.BringToFront();
        }









        private void welcomeLabel_Click(object sender, EventArgs e)
        {

        }

        
    }
}
