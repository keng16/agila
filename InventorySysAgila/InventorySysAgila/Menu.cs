using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace InventorySysAgila
{
    public partial class Menu : Form
    {
        string title = "";
        string username="";
        MySqlConnection conn = new MySqlConnection("SERVER=" + "localhost" + ";" + "DATABASE=" + "agiladb" + ";" + "UID=" + "root" + ";" + "PASSWORD=" + "" + ";");
        public Menu(string useName,string Title)
        {
            username = useName;
            title = Title;
            InitializeComponent();
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            Account acc = new Account(username);
            acc.TopLevel = false;
            acc.Location = new Point(10, 75);
            panel1.Controls.Add(acc);
            acc.Show();
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            Inventory inv = new Inventory(username);
            inv.TopLevel = false;
            panel1.Controls.Add(inv);
            inv.Show();
        }
        private void btnLogout_Click(object sender, EventArgs e)
        {
            btnLogout.BackColor = Color.DarkSeaGreen;
            panel1.Controls.Clear();
            using (conn)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to LogOut? ", "LogOut", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    //ken
                    //update the table to input logout time and date
                    conn.Open();
                    MySqlCommand dbcom = new MySqlCommand("UPDATE log SET LogOutDate = '" + DateTime.Now.ToString("MM/dd/yyyy") + "',LogOutTime= '" + DateTime.Now.ToString("hh:mm") + "' WHERE userName = '" + username + "' and (LogOutDate ='-' and LogOutTime = '-');", conn);
                    dbcom.ExecuteNonQuery();
                    conn.Close();
                    //end
                    Application.Exit();
                }
            }
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            //log insert
            conn.Open();
            MySqlCommand dbins = new MySqlCommand("insert into log(userName,LogInDate,LogInTime,LogOutDate,LogOutTime,userTitle) values('" + username + "','" + DateTime.Now.ToString("MM/dd/yyyy") + "','" + DateTime.Now.ToString("hh:mm") + "','-','-','" + title + "');", conn);
            dbins.ExecuteNonQuery();
            conn.Close();
            //end
        }

        private void btnReservation_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            Reservation res = new Reservation(username);
            res.TopLevel = false;
            panel1.Controls.Add(res);
            res.Show();
        }
    }
}

