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
    public partial class Delivery : Form
    {
        private SqlConnection connection;
        string connectionString;
        public Delivery()
        {
            InitializeComponent();
        }

        private void Delivery_Load(object sender, EventArgs e)
        {
            connectionString = @"Data Source=SUSU\SQLEXPRESS;Initial Catalog=pizza_dilivery;Integrated Security=True";
            connection = new SqlConnection(connectionString);
            connection.Open();
            QueryOrder();

        }
        void QueryOrder()
        {
            //nested join 
            string instruction = "SELECT op.order_id, o.order_date, c.firstName, c.lastName, c.phone, c.addre, p.name AS pizza, pt.pizza_size, t.name AS topping, s.name AS side, d.name AS drink, op.order_status, op.total_price ";
            instruction += " FROM pizza p ";
            instruction += " LEFT JOIN (pizza_topping pt LEFT JOIN ingredient t ON (t.id = pt.topping_id or t.id is null and pt.topping_id is null)) ";
            instruction += " ON p.id = pt.pizza_id ";
            instruction += " LEFT JOIN order_pizza op ";
            instruction += " ON pt.pizza_topping_id = op.pizza_topping_id ";
            instruction += " LEFT JOIN (order_side os LEFT JOIN side s ON (s.id = os.side_id or s.id is null and os.side_id is null)) ";
            instruction += " ON op.order_id = os.order_id ";
            instruction += " LEFT JOIN (order_drink od LEFT JOIN drink d ON (d.id = od.drink_id or d.id is null and od.drink_id is null)) ";
            instruction += " ON os.order_id = od.order_id ";
            instruction += " LEFT JOIN orders o ";
            instruction += " ON o.order_id = od.order_id ";
            instruction += " LEFT JOIN customer c ";
            instruction += " ON o.customer_id = c.id ";
            instruction += " WHERE op.order_status = 'cooked' OR op.order_status = 'delivering' ORDER BY op.order_id ";

            SqlCommand cmd = new SqlCommand(instruction, connection);
            SqlDataAdapter mcmd = new SqlDataAdapter();
            mcmd.SelectCommand = cmd;
            DataSet ds = new DataSet();
            mcmd.Fill(ds);
            DorderView.DataSource = ds.Tables[0];

        }

        private void btnDU_Click(object sender, EventArgs e)
        {
            string statusChoice;
            int selectedRowCount = DorderView.Rows.GetRowCount(DataGridViewElementStates.Selected);

            if (selectedRowCount > 0)
            {
                if (statusCombo2.SelectedIndex > -1)
                {
                    statusChoice = statusCombo2.SelectedItem.ToString();
                    string instruction = "UPDATE order_pizza SET order_status = @statusChoice ";
                    instruction += " WHERE order_id = @order_id ";
                    SqlCommand cmd = new SqlCommand(instruction, connection);
                    cmd.Parameters.Add("@order_id", SqlDbType.Int);
                    cmd.Parameters.AddWithValue("@statusChoice", statusChoice);
                    cmd.Parameters["@order_id"].Value = int.Parse(DorderView.SelectedRows[0].Cells[0].Value.ToString());
                    cmd.ExecuteNonQuery();
                    QueryOrder();

                }
                else
                    MessageBox.Show("please choose the status to update!");

            }
            else
                MessageBox.Show("please select a row first to update!");
        }
    }
}
