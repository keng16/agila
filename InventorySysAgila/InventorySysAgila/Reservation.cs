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
    public partial class Reservation : Form
    {
        string usernameonline = "";
        string selectedResvID = "";

        MySqlConnection conn = new MySqlConnection("SERVER=" + "localhost" + ";" + "DATABASE=" + "agiladb" + ";" + "UID=" + "root" + ";" + "PASSWORD=" + "" + ";");

        public Reservation(string username)
        {
            usernameonline = username;
            InitializeComponent();
        }

        private void Reservation_Load(object sender, EventArgs e)
        {
            txtDOR.Text = DateTime.Today.ToString();
            txtDOR.Value = DateTime.Today;
            txtEx.Text = txtDOR.Value.AddDays(15).ToString();
            txtEx.Value = txtDOR.Value.AddDays(15);


            PanelResv.Visible = false;
            refresh();

            string prName = "";
            string cmdstring = "select fin_name from finished_product";
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(cmdstring, conn);
            cmd.ExecuteNonQuery();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                prodDDown1.Items.Add(prName = reader["fin_name"].ToString());
                prodDDown2.Items.Add(prName = reader["fin_name"].ToString());
                prodDDown3.Items.Add(prName = reader["fin_name"].ToString());
            }

            reader.Close();
            conn.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            groupBox1.Enabled = true;
            groupBox2.Enabled = true;

            add = true;
        }

        private void refresh()
        {
            string sql = "select resv_id as Reservation_ID, cus_name as Customer_Name, prod_name as Product_Name, prod_name1 as Product_Name1, prod_name2 as Product_Name2 from reservations";
            conn.Open();
            MySqlDataAdapter adapt = new MySqlDataAdapter(sql, conn);
            DataTable table = new DataTable();
            adapt.Fill(table);
            ReservationGridView2.DataSource = table;


            //string prod = ReservationGridView2.Columns[2].ToString() + ReservationGridView2.Columns[3].ToString() + ReservationGridView2.Columns[4].ToString();
            
            DataGridViewColumn column = ReservationGridView2.Columns[0]; column.Width = 150;
            DataGridViewColumn column1 = ReservationGridView2.Columns[1]; column1.Width = 200;
            DataGridViewColumn column2 = ReservationGridView2.Columns[2] ; column2.Width = 170;
            DataGridViewColumn column3 = ReservationGridView2.Columns[2]; column2.Width = 170;
            DataGridViewColumn column4 = ReservationGridView2.Columns[2]; column2.Width = 170;
            conn.Close();
        }

        bool add = false;
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (add == true)
            {
                if (prodDDown1.Text != "Select Product" && prodQty1.Text != "0" && prodDDown2.Text != "Select Product" && prodQty2.Text != "0" && prodDDown3.Text != "Select Product" && prodQty3.Text != "0")
                {
                    if (txtName.Text == "")
                    {
                    }
                    else if (txtContact.Text == "")
                    {
                    }
                    else if (txtAddress.Text == "")
                    {
                    }
                    else
                    {
                        conn.Open();
                        int prodID = 0, prodID1 = 0, prodID2 = 0;

                        //Prod1
                        string cmdstring = "select fin_id from finished_product where fin_name='" + prodDDown1.Text + "' ";
                        MySqlCommand cmd1 = new MySqlCommand(cmdstring, conn);
                        cmd1.ExecuteNonQuery();
                        MySqlDataReader reder = cmd1.ExecuteReader();

                       
                        if (reder.HasRows)
                        {
                            reder.Read();
                            prodID = Convert.ToInt32(reder["fin_id"]);
                        }
                        reder.Close();

                        //Prod2
                        cmdstring = "select fin_id from finished_product where fin_name='" + prodDDown2.Text + "' ";
                        MySqlCommand cmd2 = new MySqlCommand(cmdstring, conn);
                        cmd2.ExecuteNonQuery();
                        MySqlDataReader reader2 = cmd2.ExecuteReader();


                        if (reader2.HasRows)
                        {
                            reader2.Read();
                            prodID1 = Convert.ToInt32(reader2["fin_id"]);
                        }
                        reader2.Close();

                        //Prod3
                        cmdstring = "select fin_id from finished_product where fin_name='" + prodDDown3.Text + "' ";
                        MySqlCommand cmd3 = new MySqlCommand(cmdstring, conn);
                        cmd3.ExecuteNonQuery();
                        MySqlDataReader reader3 = cmd3.ExecuteReader();


                        if (reader3.HasRows)
                        {
                            reader3.Read();
                            prodID2 = Convert.ToInt32(reader3["fin_id"]);
                        }
                        reader3.Close();

                        int prID = Convert.ToInt32(prodID);
                        int quantity = Convert.ToInt16(prodQty1.Text);
                        int prID1 = Convert.ToInt32(prodID1);
                        int quantity1 = Convert.ToInt16(prodQty2.Text);
                        int prID2 = Convert.ToInt32(prodID2);
                        int quantity2 = Convert.ToInt16(prodQty3.Text);

                        MySqlCommand comm = new MySqlCommand("insert into reservations (prod_id, prod_id1, prod_id2, prod_name, prod_name1, prod_name2, quantity, quantity1, quantity2, cus_name, cus_address, cus_number, cus_email, dateofreservation, dateofexpiry) values('" + prID + "','" + prID1 + "','" + prID2 + "','" + prodDDown1.SelectedItem + "','" + prodDDown2.SelectedItem + "','" + prodDDown3.SelectedItem + "','" + quantity + "', '" + quantity1 + "', '" + quantity2 + "','" + txtName.Text + "','" + txtAddress.Text + "','" + txtContact.Text + "','" + txtEmail.Text + "','" + txtDOR.Text + "','" + txtEx.Text + "')", conn);
                        comm.ExecuteNonQuery();
                        add = false;
                        MessageBox.Show("Successfully Registered!");
                        
                        conn.Close();
                        refresh();
                    }
                }
                else
                {
                    MessageBox.Show("Please Select Product ID & Quantity!");
                }
            }
            else
            {
                conn.Open();
                int prodID = 0, prodID1 = 0, prodID2 = 0;

                //Prod1
                string cmdstring = "select fin_id from finished_product where fin_name='" + prodDDown1.Text + "' ";
                MySqlCommand cmd1 = new MySqlCommand(cmdstring, conn);
                cmd1.ExecuteNonQuery();
                MySqlDataReader reder = cmd1.ExecuteReader();


                if (reder.HasRows)
                {
                    reder.Read();
                    prodID = Convert.ToInt32(reder["fin_id"]);
                }
                reder.Close();

                //Prod2
                cmdstring = "select fin_id from finished_product where fin_name='" + prodDDown2.Text + "' ";
                MySqlCommand cmd2 = new MySqlCommand(cmdstring, conn);
                cmd2.ExecuteNonQuery();
                MySqlDataReader reader2 = cmd2.ExecuteReader();


                if (reader2.HasRows)
                {
                    reader2.Read();
                    prodID1 = Convert.ToInt32(reader2["fin_id"]);
                }
                reader2.Close();

                //Prod3
                cmdstring = "select fin_id from finished_product where fin_name='" + prodDDown3.Text + "' ";
                MySqlCommand cmd3 = new MySqlCommand(cmdstring, conn);
                cmd3.ExecuteNonQuery();
                MySqlDataReader reader3 = cmd3.ExecuteReader();


                if (reader3.HasRows)
                {
                    reader3.Read();
                    prodID2 = Convert.ToInt32(reader3["fin_id"]);
                }
                reader3.Close();

                int prID = Convert.ToInt32(prodID);
                int quantity = Convert.ToInt16(prodQty1.Text);
                int prID1 = Convert.ToInt32(prodID1);
                int quantity1 = Convert.ToInt16(prodQty2.Text);
                int prID2 = Convert.ToInt32(prodID2);
                int quantity2 = Convert.ToInt16(prodQty3.Text);

                txtEx.Value = txtDOR.Value.AddDays(15);
                txtEx.Text = txtEx.Value.ToString();

                string icmdString1 = "update reservations set prod_id='" + prID + "', prod_id1='" + prID1 + "', prod_id2='" + prID2 + "', prod_name='" + prodDDown1.SelectedItem + "', prod_name1='" + prodDDown2.SelectedItem + "', prod_name2='" + prodDDown3.SelectedItem + "', quantity = '" + quantity + "', quantity1 = '" + quantity1 + "', quantity2 = '" + quantity2 + "', cus_name = '" + txtName.Text + "', cus_number = '" + txtContact.Text + "', cus_email = '" + txtEmail.Text + "', dateofreservation = '" + txtDOR.Text + "', dateofexpiry = '" + txtEx.Text + "' where resv_id = '" + selectedResvID + "'";
                MySqlCommand icmd = new MySqlCommand(icmdString1, conn);
                icmd.ExecuteNonQuery();
                add = false;
                MessageBox.Show("Successfully Updated!");
                conn.Close();
                refresh();
            }
        }

        private void ReservationGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            using (conn)
            {
                DataGridViewSelectedRowCollection DGV = this.ReservationGridView2.SelectedRows;
                foreach (DataGridViewRow row in DGV)
                {
                    DataRow myRow = (row.DataBoundItem as DataRowView).Row;
                    selectedResvID = Convert.ToString(myRow[0]);
                    conn.Open();
                    MySqlCommand dbcom = new MySqlCommand("select * from reservations where resv_id = '" + selectedResvID + "'", conn);
                    MySqlDataReader dbread = dbcom.ExecuteReader();
                    if (dbread.HasRows)
                    {
                        dbread.Read();
                        txtName.Text = dbread["cus_name"].ToString();
                        txtEmail.Text = dbread["cus_email"].ToString();
                        txtContact.Text = dbread["cus_number"].ToString();
                        txtAddress.Text = dbread["cus_address"].ToString();
                        txtDOR.Text = dbread["dateofreservation"].ToString();
                        txtEx.Text = dbread["dateofexpiry"].ToString();

                        System.DateTime resv = Convert.ToDateTime(txtDOR.Text);
                        System.DateTime ex = Convert.ToDateTime(txtEx.Text);

                        System.TimeSpan diff = ex.Subtract(DateTime.Today);// DateTime.Today.Subtract(ex);
                        txtResvEx.Text = diff.Days.ToString();


                        dbread.Close();
                    }

                    conn.Close();
                }

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            using (conn)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete? " + selectedResvID, "Delete", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    conn.Open();
                    MySqlCommand dbcom = new MySqlCommand("delete from reservations where resv_id = '" + selectedResvID + "'", conn);
                    dbcom.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("The reservation has been deleted");

                    txtName.ResetText();
                    txtContact.ResetText();
                    txtEmail.ResetText();
                    txtAddress.ResetText();

                }
                else
                {
                    //Problem here
                }
                ReservationGridView2.Refresh();
                refresh();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            add = false;
            groupBox1.Enabled = true;
            groupBox2.Enabled = true;

            txtDOR.Enabled = true;
            txtEx.Enabled = true;

            txtDOR.MinDate = txtDOR.Value;
            txtEx.MinDate = txtEx.Value;
        }

        string name = "";
        string prodName = "", prodName1 = "", prodName2 = "";
        int prodID = 0, prodID1 = 0, prodID2 = 0;
        int qty = 0, qty1 = 0, qty2 = 0;
        int qtyInv = 0, qtyInv1 = 0, qtyInv2 = 0;
        int prcInv = 0, prcInv1 = 0, prcInv2 = 0;
        int total = 0;

        private void btnPayment_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            groupBox1.Visible = false;
            groupBox2.Visible = false;

            PanelResv.Visible = true;


            //get the prod id from reservation

            conn.Open();


            string cmdstring = "select prod_id, prod_id1, prod_id2 from reservations where resv_id='" + selectedResvID + "' ";
            MySqlCommand cmd = new MySqlCommand(cmdstring, conn);
            cmd.ExecuteNonQuery();
            MySqlDataReader reader = cmd.ExecuteReader();


            if (reader.HasRows)
            {
                reader.Read();
                prodID = Convert.ToInt32(reader["prod_id"]);
                prodID1 = Convert.ToInt32(reader["prod_id1"]);
                prodID2 = Convert.ToInt32(reader["prod_id2"]);
            }
            reader.Close();

            //get the prod name from reservation


            string cmdstring5 = "select prod_name, prod_name1, prod_name2 from reservations where resv_id='" + selectedResvID + "' ";
            MySqlCommand cmd5 = new MySqlCommand(cmdstring5, conn);
            cmd.ExecuteNonQuery();
            MySqlDataReader reader5 = cmd5.ExecuteReader();


            if (reader5.HasRows)
            {
                reader5.Read();
                prodName = reader5["prod_name"].ToString();
                prodName1 = reader5["prod_name1"].ToString();
                prodName2 = reader5["prod_name2"].ToString();
            }
            reader5.Close();

            //get the quantity from reservation

            string cmdstring1 = "select quantity, quantity1, quantity2 from reservations where resv_id='" + selectedResvID + "' ";
            MySqlCommand cmd1 = new MySqlCommand(cmdstring1, conn);
            cmd1.ExecuteNonQuery();
            MySqlDataReader reader1 = cmd1.ExecuteReader();


            if (reader1.HasRows)
            {
                reader1.Read();
                qty = Convert.ToInt32(reader1["quantity"]);
                qty1 = Convert.ToInt32(reader1["quantity1"]);
                qty2 = Convert.ToInt32(reader1["quantity2"]);
            }
            reader1.Close();

            //get the quantity from inventory



            //prod1
            string cmdstring2 = "select quantity, price from finished_product where fin_id='" + prodID + "' ";
            MySqlCommand cmd2 = new MySqlCommand(cmdstring2, conn);
            cmd2.ExecuteNonQuery();
            MySqlDataReader reader2 = cmd2.ExecuteReader();


            if (reader2.HasRows)
            {
                reader2.Read();
                qtyInv = Convert.ToInt32(reader2["quantity"]);
                prcInv = Convert.ToInt32(reader2["price"]);
            }
            reader2.Close();

            //prod2
            string cmdstring3 = "select quantity, price from finished_product where fin_id='" + prodID + "' ";
            MySqlCommand cmd3 = new MySqlCommand(cmdstring3, conn);
            cmd3.ExecuteNonQuery();
            MySqlDataReader reader3 = cmd2.ExecuteReader();


            if (reader3.HasRows)
            {
                reader3.Read();
                qtyInv1 = Convert.ToInt32(reader3["quantity"]);
                prcInv1 = Convert.ToInt32(reader3["price"]);
            }
            reader3.Close();

            //prod1
            string cmdstring4 = "select quantity, price from finished_product where fin_id='" + prodID + "' ";
            MySqlCommand cmd4 = new MySqlCommand(cmdstring4, conn);
            cmd4.ExecuteNonQuery();
            MySqlDataReader reader4 = cmd4.ExecuteReader();


            if (reader4.HasRows)
            {
                reader4.Read();
                qtyInv2 = Convert.ToInt32(reader4["quantity"]);
                prcInv2 = Convert.ToInt32(reader4["price"]);
            }
            reader4.Close();

            //CustomerName

            string cmdstring6 = "select cus_name from reservations where resv_id='" + selectedResvID + "' ";
            MySqlCommand cmd6 = new MySqlCommand(cmdstring6, conn);
            cmd6.ExecuteNonQuery();
            MySqlDataReader reader6 = cmd6.ExecuteReader();


            if (reader6.HasRows)
            {
                reader6.Read();
                name = reader6["cus_name"].ToString() ;
            }
            reader6.Close();


            conn.Close();

            CustomerName.Text = name.ToString();

            ResvProdShow.Text = prodID.ToString();
            ResvProdShow1.Text = prodID1.ToString();
            ResvProdShow2.Text = prodID2.ToString();

            ResvQtyShow.Text = qty.ToString();
            ResvQtyShow1.Text = qty1.ToString();
            ResvQtyShow2.Text = qty2.ToString();

            ResvProdNShow.Text = prodName.ToString();
            ResvProdNShow1.Text = prodName1.ToString();
            ResvProdNShow2.Text = prodName2.ToString();

            ResvPriceShow.Text = prcInv.ToString();
            ResvPriceShow1.Text = prcInv1.ToString();
            ResvPriceShow2.Text = prcInv2.ToString();



            total = ((qty * prcInv) + (qty1 * prcInv1) + (qty2 * prcInv2));

            TotalPrice.Text = total.ToString();


        }

        //string name = "";
        //string prodName = "", prodName1 = "", prodName2 = "";
        //int prodID = 0, prodID1 = 0, prodID2 = 0;
        //int qty = 0, qty1 = 0, qty2 = 0;
        //int qtyInv = 0, qtyInv1 = 0, qtyInv2 = 0;
        //int prcInv = 0, prcInv1 = 0, prcInv2 = 0;
        //int total = 0;

        private void btnFinalize_Click(object sender, EventArgs e)
        {
            conn.Open();
            MySqlCommand comm = new MySqlCommand("insert into pointofsales (res_id, prod_id, prod_id1, prod_id2, prod_name, prod_name1, prod_name2, quantity, quantity1, quantity2, price, cus_name, DOR, DOP) values('" + selectedResvID + "','" + prodID + "','" + prodID1 + "','" + prodID2 + "','" + prodName.ToString() + "','" + prodName1.ToString() + "','" + prodName2.ToString() + "','" + qty + "', '" + qty1 + "', '" + qty2 + "','" + total + "','" + name.ToString() + "','" + "1" + "','" + "1" + "')", conn);
            comm.ExecuteNonQuery();
            conn.Close();
        }
    }
}