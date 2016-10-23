using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace InventorySysAgila
{
    public partial class Login : Form
    {
        
        MySqlConnection conn = new MySqlConnection("SERVER=" + "localhost" + ";" + "DATABASE=" + "agiladb" + ";" + "UID=" + "root" + ";" + "PASSWORD=" + "" + ";");
        public Login()
        {
            InitializeComponent();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            
            conn.Open();

            string cmdString = "select userName,userPass,userTitle from Users where userName='" + txtUsername.Text + "'and userPass ='" + txtPassword.Text + "'";
            MySqlCommand cmd = new MySqlCommand(cmdString, conn);
            cmd.ExecuteNonQuery();

            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                if (txtUsername.Text == reader["userName"].ToString() && txtPassword.Text == reader["userPass"].ToString())
                {

                    string title = reader["userTitle"].ToString();
                    string uname = reader["userName"].ToString();
                    this.Hide();
                    Menu men = new Menu(uname,title);
                    men.ShowDialog();
                    reader.Close();
                    conn.Close();
                }
            }
            else
            {
                MessageBox.Show("Invalid Username / Password Combination");
                conn.Close();
            }

            txtUsername.Clear();
            txtPassword.Clear();
            txtUsername.Focus();
        }
        
      

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}