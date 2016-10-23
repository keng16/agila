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
    public partial class Inventory : Form
    {
        string usernameonline = "",fin="";
        int q= 0;

        MySqlConnection conn = new MySqlConnection("SERVER=" + "localhost" + ";" + "DATABASE=" + "agiladb" + ";" + "UID=" + "root" + ";" + "PASSWORD=" + "" + ";");

        public Inventory(string userName)
        {
            usernameonline = userName;
            InitializeComponent();
        }

        private void Inventory_Load(object sender, EventArgs e)
        {
            
            finID.ResetText();
            finName.Clear();
            finDesc.Clear();
            finQty.ResetText();
            finPrice.ResetText();
            refresh();

            /* foreach (DataGridViewRow row in ProductGridView.Rows)
             {
                 if (Convert.ToInt32(row.Cells[2].Value) < Convert.ToInt32(row.Cells[3].Value))
                 {
                     row.DefaultCellStyle.BackColor = Color.Red;
                 }
             }*/
            conn.Close();

        }

        public void refresh()
        {//removed conn,open
            using (conn)
            {
               
                MySqlDataAdapter dbadapt = new MySqlDataAdapter("select fin_id as ProductID, fin_name as ProductName, Quantity as ProductQuantity, Price as ProductPrice from finished_product", conn);
                DataTable table = new DataTable();
                dbadapt.Fill(table);
                ProductGridView.DataSource = table;
                conn.Close();
                DataGridViewColumn column = ProductGridView.Columns[0]; column.Width = 70;
                DataGridViewColumn column1 = ProductGridView.Columns[1]; column1.Width = 200;
                DataGridViewColumn column2 = ProductGridView.Columns[2]; column2.Width = 100;
                DataGridViewColumn column3 = ProductGridView.Columns[3]; column3.Width = 100;
            }
            using (conn)
            {
                conn.Open();
                MySqlDataAdapter dbadapt = new MySqlDataAdapter("select * from finished_product", conn);
                DataTable table = new DataTable();
                dbadapt.Fill(table);
                conn.Close();
            }
            using (conn)
            {
                conn.Open();
                MySqlCommand dbadapt = new MySqlCommand("select fin_name, Quantity from finished_product", conn);
                MySqlDataReader dbread = dbadapt.ExecuteReader();

                if(dbread.HasRows)
                {


                    dbread.Read();
                    dbread.NextResult();
                    q = Convert.ToInt32(dbread["Quantity"].ToString());
                    fin = dbread["fin_name"].ToString();
                    if (q < 10)
                    {
                        timer1.Start();
                    }
                }
            }
        }

        public void clearinf()
        {
            finID.ResetText();
            finName.Clear();
            finDesc.Clear();
            finQty.ResetText();
            finPrice.ResetText();


        }

        string selectedProdID;

        private void ProductGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //removed counter i, chgange [i] to [0]
            using (conn)
            {
                DataGridViewSelectedRowCollection DGV = this.ProductGridView.SelectedRows;
                foreach (DataGridViewRow row in DGV)
                {
                    DataRow myRow = (row.DataBoundItem as DataRowView).Row;
                    selectedProdID = Convert.ToString(myRow[0]);
                    conn.Open();
                    MySqlCommand dbcom = new MySqlCommand("select * from finished_product where fin_id = '" + selectedProdID + "'", conn);
                    MySqlDataReader dbread = dbcom.ExecuteReader();
                    if (dbread.HasRows)
                    {
                        dbread.Read();
                        finID.Text = dbread["fin_id"].ToString();
                        finName.Text = dbread["fin_name"].ToString();
                        finDesc.Text = dbread["fin_desc"].ToString();
                        finQty.Text = dbread["Quantity"].ToString();
                        finPrice.Text = dbread["Price"].ToString();


                        dbread.Close();
                    }
                   
                    conn.Close();
                }

            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            clearinf();
            information.Enabled = true;
            add = true;
        }

        bool add = false;

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (finID.Text.Length == 6)
            {
                //put validations here
                double id = Convert.ToDouble(finID.Text);
                double qty = Convert.ToDouble(finQty.Text);
                double price = Convert.ToDouble(finPrice.Text);

                using (conn)
                {
                    conn.Open();
                    if (add)
                    {
                        if (finID.Text.Trim() == "" || finName.Text.Trim() == "" || finDesc.Text.Trim() == "" || finQty.Text.Trim() == "" || finPrice.Text.Trim() == "")
                        {
                            MessageBox.Show("Please fill up all the following informations!");
                        }
                        else
                        {
                            try
                            {
                                string icmdString = "Insert into finished_product values(" + id + ",'" + finName.Text + "','" + finDesc.Text + "'," + qty + "," + price + ");";
                                MySqlCommand icmd = new MySqlCommand(icmdString, conn);
                                icmd.ExecuteNonQuery();
                                //removed false visibility for btn save and cancel
                                information.Enabled = false;
     
   
                                clearinf();
                                groupBox1.Visible = true;

                                MessageBox.Show("Successfully Registered!");

                                conn.Close();

                                add = false;
                                refresh();
                                clearinf();
                            }
                            catch
                            {

                                MessageBox.Show("Product ID or the Same Product Already Exist");
                            }
                            finally
                            {
                                conn.Close();
                            }
                        }
                    }
                    else
                    {
                        //change icmdString to icmdString1, last value of icmdString1 to selectedProdID
                        string icmdString1 = "update finished_product set fin_id='"+finID.Text+"', fin_name='"+finName.Text+"', fin_desc = '"+finDesc.Text+"', Quantity = '"+qty+"', Price = '"+price+"' where fin_id = '"+selectedProdID+"'";
                        MySqlCommand icmd = new MySqlCommand(icmdString1, conn);
                        icmd.ExecuteNonQuery();
                        btnCancel.Visible = false;
                        btnSave.Visible = false;
                        information.Enabled = false;
                        add = false;
                        MessageBox.Show("Successfully Updated!");
                        clearinf();
                        refresh();
                        conn.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Product ID must have 6 numbers");
            }
            conn.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            using (conn)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete? " + finID.Text, "Delete", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    conn.Open();
                    MySqlCommand dbcom = new MySqlCommand("delete from finished_product where fin_id = '" + finID.Text+"'", conn);
                    dbcom.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("The Product Has Been Deleted");

                    finID.ResetText();
                    finName.ResetText();
                    finDesc.ResetText();
                    finQty.ResetText();
                    finPrice.ResetText();
                }
                else
                {
                    //Problem here
                }
                ProductGridView.Refresh();
                refresh();
                clearinf();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            add = false;
            clearinf();
            information.Enabled = true;
        }

        private void searchLikez_Click(object sender, EventArgs e)
        {
            using (conn)
            {
                MySqlDataAdapter dbAdapt = new MySqlDataAdapter("select fin_id as ProductID, fin_name as ProductName ,Quantity as Quantity from finished_product where fin_id like '%" + textBoxlike.Text + "%' or fin_name like '%" + textBoxlike.Text + "%' or Quantity like '%" + textBoxlike.Text + "%'", conn);
                DataTable table = new DataTable();
                dbAdapt.Fill(table);
                ProductGridView.DataSource = table;
                conn.Close();
                DataGridViewColumn column = ProductGridView.Columns[0]; column.Width = 70;
                DataGridViewColumn column1 = ProductGridView.Columns[1]; column1.Width = 70;
                DataGridViewColumn column2 = ProductGridView.Columns[2]; column2.Width = 70;

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void NotifBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {//timer for notifdisplay
            NotifBox.Text = fin+" "+q+ " stock is less than 10";
            NotifBox.Enabled = false;
            NotifBox.Visible = true;
            timer2.Start();
            timer1.Stop();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {//timer for notif close
            NotifBox.Visible = false;
            timer2.Stop();
        }


    }
}