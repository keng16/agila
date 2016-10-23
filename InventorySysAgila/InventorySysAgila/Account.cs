using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace InventorySysAgila
{
    public partial class Account : Form
    {
        string usernameonline = "";
        string namePic = "";
        string title = "";

        MySqlConnection conn = new MySqlConnection("SERVER=" + "localhost" + ";" + "DATABASE=" + "agiladb" + ";" + "UID=" + "root" + ";" + "PASSWORD=" + "" + ";");

        public Account(string userName)
        {
            usernameonline = userName;
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            add = true;
            clearAll();
            pictureBox1.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\" + "profilePic.jpg");
            panel1.Visible = false;
            groupBox2.Location = new Point(248, 10);
            groupBox1.Enabled = true;
            groupBox2.Enabled = true;
            btnAdd.Visible = false;
            btnEdit.Visible = false;
            btnDelete.Visible = false;
            btnSave.Visible = true;
            btnCancel.Visible = true;
            txtUsername.Focus();
        }

        bool add = false;
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (add == true)
            {
                if (txtUsername.Text.Trim() != "" && txtPassword.Text.Trim() != "")
                {
                    //Still needs debugging
                    conn.Open();
                    string cmdstring = "select userName,userPass,userTitle from Users where userName='" + txtUsername.Text + "'and userPass ='" + txtPassword.Text + "'";

                    MySqlCommand cmd = new MySqlCommand(cmdstring, conn);

                    cmd.ExecuteNonQuery();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        MessageBox.Show("Username Already Exists");
                        reader.Close();
                    }

                    if (txtFirstName.Text.Trim() == "")
                    {
                        MessageBox.Show("First Name is needed");
                        txtFirstName.Focus();
                    }
                    else if (txtLastName.Text.Trim() == "")
                    {
                        MessageBox.Show("Last name is needed");
                        txtLastName.Focus();
                    }
                    else
                    {
                        if (txtPassword.Text != txtVerPass.Text)
                        {
                            MessageBox.Show("Password Does Not Match!");
                            txtVerPass.Focus();
                        }
                        else
                        {
                            if (btnClerk.Checked)
                            {
                                title = "Clerk";
                            }
                            if (btnManager.Checked)
                            {
                                title = "Manager";
                            }
                            if (btnAssistant.Checked)
                            {
                                title = "Assistant";
                            }
                            reader.Close();
                            MySqlCommand comm = new MySqlCommand("insert into users values('" + txtUsername.Text + "','" + txtPassword.Text + "','" + txtFirstName.Text + "','" + txtLastName.Text + "','" + txtContact.Text + "','" + txtBirthdate.Text + "','" + txtAddress.Text + "','" + title + "','" + namePic + "')", conn);
                            comm.ExecuteNonQuery();
                            add = false;
                            MessageBox.Show("Successfully Registered!");

                            conn.Close();
                            refresh();

                            clearAll();
                            btnAdd.Visible = true;
                            btnEdit.Visible = true;
                            txtUsername.ResetText();
                            btnDelete.Visible = true;
                            btnSave.Visible = false;
                            groupBox1.Enabled = false;
                            groupBox2.Enabled = false;
                            AccountGridView2.Select();
                            panel1.Visible = true;
                            groupBox2.Location = new Point(248, 359);
                        }
                    }
                }
            }
            else
            {
                if (txtFirstName.Text.Trim() == "")
                {
                    MessageBox.Show("First Name is needed");
                    txtFirstName.Focus();
                }
                else if (txtLastName.Text.Trim() == "")
                {
                    MessageBox.Show("Last name is needed");
                    txtLastName.Focus();
                }
                else
                {
                    if (txtPassword.Text != txtVerPass.Text)
                    {
                        MessageBox.Show("Password Does Not Match!");
                        txtVerPass.Focus();
                    }
                    else
                    {
                        if (btnClerk.Checked)
                        {
                            title = "Clerk";
                        }
                        if (btnManager.Checked)
                        {
                            title = "Manager";
                        }
                        if (btnAssistant.Checked)
                        {
                            title = "Assistant";
                        }
                        conn.Open();
                        string icmdString1 = "update users set userPass='" + txtPassword.Text + "',userFirstname='" + txtFirstName.Text + "',userLastname='" + txtLastName.Text + "',userContactnum='" + txtContact.Text + "',userBirthdate='" + txtBirthdate.Text + "',userAddress='" + txtAddress.Text + "',userTitle='" + title + "',userPicture='" + namePic + "'where userName='" + username + "'";
                        MySqlCommand icmd1 = new MySqlCommand(icmdString1, conn);
                        icmd1.ExecuteNonQuery();
                        //btnSave.Enabled = false;
                        add = false;
                        MessageBox.Show("Successfully Updated!");
                        conn.Close();
                        refresh();
                    }
                }
            }
        }



        private void refresh()
        {
            string sql = "select userTitle as Position, userLastname as FullName, userName as Username from users";
            conn.Open();
            MySqlDataAdapter adapt = new MySqlDataAdapter(sql, conn);
            DataTable table = new DataTable();
            adapt.Fill(table);
            AccountGridView2.DataSource = table;
            conn.Close();
            DataGridViewColumn column = AccountGridView2.Columns[0]; column.Width = 250;
            DataGridViewColumn column1 = AccountGridView2.Columns[1]; column1.Width = 350;
            DataGridViewColumn column2 = AccountGridView2.Columns[2]; column2.Width = 250;
        }

        private void clearAll()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtVerPass.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            txtAddress.Clear();
            txtContact.Clear();
            btnClerk.Select();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog nuPic = new OpenFileDialog();
            nuPic.ShowDialog();

            string currentDirect = Directory.GetCurrentDirectory();
            namePic = nuPic.SafeFileName.ToString();
            if (nuPic.SafeFileName.ToString() == "")
            {
                MessageBox.Show("No picture was selected");
            }
            else
            {
                if (File.Exists(currentDirect + @"\" + nuPic.SafeFileName.ToString()))
                {
                    pictureBox1.Image = Image.FromFile(currentDirect + @"\" + nuPic.SafeFileName.ToString());
                }
                else
                {
                    File.Copy(nuPic.FileName.ToString(), currentDirect + @"\" + nuPic.SafeFileName.ToString());
                    pictureBox1.Image = Image.FromFile(currentDirect + @"\" + nuPic.SafeFileName.ToString());
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (usernameonline == txtUsername.Text)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to Delete Your Account? ", "Delete", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    conn.Open();
                    MySqlCommand dbcom = new MySqlCommand("delete from Users where Username = '" + username + "'", conn);
                    dbcom.ExecuteNonQuery();
                    conn.Close();
                }
            }
            else
            {
                btnAdd.Visible = true;
                btnEdit.Visible = true;
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete? " + username, "Delete", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    conn.Open();
                    MySqlCommand dbcom = new MySqlCommand("delete from Users where Username = '" + username + "'", conn);
                    dbcom.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show(username + "'s Account has been deleted");
                }
                AccountGridView2.Refresh();
                refresh();
            }
        }

        string username;
        private void AccountGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btnAdd.Visible = true;
            btnEdit.Visible = true;
            btnSave.Visible = false;
            btnCancel.Visible = false;
            btnDelete.Visible = true;
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;

            DataGridViewSelectedRowCollection DGV = this.AccountGridView2.SelectedRows;
            foreach (DataGridViewRow row in DGV)
            {
                DataRow myRow = (row.DataBoundItem as DataRowView).Row;
                username = Convert.ToString(myRow[2]);
                conn.Open();
                MySqlCommand dbcom = new MySqlCommand("select * from users where Username = '" + username + "'", conn);
                MySqlDataReader dbread = dbcom.ExecuteReader();
                if (dbread.HasRows)
                {
                    dbread.Read();
                    txtUsername.Text = dbread["userName"].ToString();
                    txtPassword.Text = dbread["userPass"].ToString();
                    txtFirstName.Text = dbread["userFirstName"].ToString();
                    txtLastName.Text = dbread["userLastName"].ToString();
                    txtBirthdate.Text = dbread["userBirthDate"].ToString();
                    txtAddress.Text = dbread["userAddress"].ToString();
                    txtContact.Text = dbread["userContactNum"].ToString();
                    string directory = Directory.GetCurrentDirectory() + @"\" + dbread["userPicture"].ToString();
                    try { pictureBox1.Image = Image.FromFile(directory); }
                    catch { pictureBox1.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\" + "profilePic.jpg"); }
                    if (dbread["userTitle"].ToString() == "Clerk")
                    {
                        btnClerk.Checked = true;
                    }
                    if (dbread["userTitle"].ToString() == "Manager")
                    {
                        btnManager.Checked = true;
                    }
                    if (dbread["userTitle"].ToString() == "Assistant")
                    {
                        btnAssistant.Checked = true;
                    }
                    dbread.Close();
                }
                conn.Close();
            }
        }

        private void Account_Load(object sender, EventArgs e)
        {
            refresh();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            add = false;
            groupBox2.Location = new Point(248, 10);
            groupBox1.Enabled = true;
            groupBox2.Enabled = true;
            btnSave.Visible = true;
            btnAdd.Visible = false;
            btnEdit.Visible = false;
            btnDelete.Visible = false;
            btnCancel.Visible = true;
            txtUsername.Enabled = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            clearAll();
            btnAdd.Visible = true;
            btnEdit.Visible = false;
            btnDelete.Visible = false;
            btnSave.Visible = false;
            btnCancel.Visible = false;
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            AccountGridView2.Select();
            panel1.Visible = true;
            groupBox2.Location = new Point(248, 359);
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {

        }
    }
}