using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace pizza_delivery
{

    public partial class Ingredient_Management : Form
    {
        private SqlConnection connection;
        string connectionString;

        public Ingredient_Management()
        {
            InitializeComponent();
        }

        private void Ingredient_Management_Load(object sender, EventArgs e)
        {
            //try to connect with database
            this.WindowState = FormWindowState.Maximized;
            connectionString = @"Data Source=SUSU\SQLEXPRESS;Initial Catalog=pizza_dilivery;Integrated Security=True";
            connection = new SqlConnection(connectionString);
            if (connection != null)
            {
                connection.Open();
                query();
                connection.Close();
            }
            
        }

        private void btnUpdateI_Click(object sender, EventArgs e)
        {
            int selectedRowCount = ingreData.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if(connection != null)
            {
                connection.Open();
                if (txtAmout.Text != String.Empty)
                {
                    int amount = int.Parse(txtAmout.Text);
                    if (selectedRowCount > 0)
                    {

                        string instruction = "UPDATE ingredient SET amount = @amount WHERE id = @id ";
                        SqlCommand cmd = new SqlCommand(instruction, connection);
                        cmd.Parameters.Add("@id", SqlDbType.Int);
                        cmd.Parameters["@id"].Value = int.Parse(ingreData.SelectedRows[0].Cells[0].Value.ToString());
                        cmd.Parameters.AddWithValue("@amount", amount);
                        cmd.ExecuteNonQuery();
                        query();


                    }
                    else
                        MessageBox.Show("please select a row first to update!");

                }
                connection.Close();
            }
            
            
        }//finish method

        //query method
        void query()
        {
            string query = "SELECT * FROM ingredient";
            SqlDataAdapter mcmd = new SqlDataAdapter(query, connection);
            DataSet ds = new DataSet();
            mcmd.Fill(ds);
            ingreData.DataSource = ds.Tables[0];
            
        }
    }
}
