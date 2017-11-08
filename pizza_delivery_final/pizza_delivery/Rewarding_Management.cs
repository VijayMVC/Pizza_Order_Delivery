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
    public partial class Rewarding_Management : Form
    {
        public decimal reward_small;
        
        private SqlConnection connection;
        string connectionString;

        public Rewarding_Management()
        {
            InitializeComponent();
        }

        public void Rewarding_Management_Load(object sender, EventArgs e)
        {
            connectionString = @"Data Source=SUSU\SQLEXPRESS;Initial Catalog=pizza_dilivery;Integrated Security=True";
            connection = new SqlConnection(connectionString);
            connection.Open();
            reward_small = QueryReward();
            
        }

        public decimal QueryReward()
        {
            string instruction = "SELECT reward_small from reward ";
            SqlCommand cmd = new SqlCommand(instruction, connection);
            return Convert.ToDecimal(cmd.ExecuteScalar());
            
        }

        void UpdateReward()
        {
            string instruction = "UPDATE reward SET reward_small = @reward_small ";
            SqlCommand cmd = new SqlCommand(instruction, connection);
            
            cmd.Parameters.AddWithValue("@reward_small", reward_small);
            cmd.ExecuteNonQuery();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            reward_small = decimal.Parse(txtUnit.Text);
            UpdateReward();

            
        }
    }
}
