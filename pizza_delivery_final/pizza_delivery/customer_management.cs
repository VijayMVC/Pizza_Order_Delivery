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
    public partial class customer_management : Form
    {
        private SqlConnection connection;
        string connectionString;
        public customer_management()
        {
            InitializeComponent();
        }

        private void customer_management_Load(object sender, EventArgs e)
        {
            connectionString = @"Data Source=SUSU\SQLEXPRESS;Initial Catalog=pizza_dilivery;Integrated Security=True";
            connection = new SqlConnection(connectionString);
            if (connection != null)
            {
                connection.Open();
                query();
                connection.Close();
            }

        }

        void query()
        {
            string query = "SELECT * FROM customer";
            SqlDataAdapter mcmd = new SqlDataAdapter(query, connection);
            DataSet ds = new DataSet();
            mcmd.Fill(ds);
            CusData.DataSource = ds.Tables[0];

        }

        private void btnUP_Click(object sender, EventArgs e)
        {
            if (connection != null)
            {
                connection.Open();
                string instruction = "UPDATE customer SET reward = 0 ";
                SqlCommand cmd = new SqlCommand(instruction, connection);
                cmd.ExecuteNonQuery();
                query();
                connection.Close();

            }

        }


      
    }
}
