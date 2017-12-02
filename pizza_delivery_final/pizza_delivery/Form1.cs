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
    public partial class Form1 : Form
    {
        private SqlConnection connection;
        string connectionString;
        decimal total_price = 0m;
        Rewarding_Management reward;
        decimal reward_small;
        decimal reward_medium;
        decimal reward_large;
        int order_id;
        decimal tax_rate = 0.05m;
        //use list to store the selected pizza and topping for later to reverse the amount when cancel the order
        List<int> PizzaIDC = new List<int>();
        List<int> ToppingIDC = new List<int>();

        public Form1()
        {
            InitializeComponent();
        }

       
        private void Form1_Load(object sender, EventArgs e)
        {
            //this.TopMost = true;
            //this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            connectionString = @"Data Source=SUSU\SQLEXPRESS;Initial Catalog=pizza_dilivery;Integrated Security=True";
            connection = new SqlConnection(connectionString);
            
        }

        private void btnReward_Click(object sender, EventArgs e)
        {
            reward = new Rewarding_Management();
            reward.Show();
 
        }

        private void btnIngredient_Click(object sender, EventArgs e)
        {
            Ingredient_Management ingredient = new Ingredient_Management();
            ingredient.Show();
        }

        private void btnAddC_Click(object sender, EventArgs e)
        {
            string instruction = "INSERT INTO customer(firstName,lastName,phone,addre) output INSERTED.id ";
            instruction += "values(@a,@b,@c,@d) "; // note there is no space within the quotation mark

            //create command and assign the query and connection from the constructor
            if (connection != null)
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(instruction, connection);

                cmd.Parameters.AddWithValue("@a", txtFname.Text);
                cmd.Parameters.AddWithValue("@b", txtLname.Text);
                cmd.Parameters.AddWithValue("@c", txtPhone.Text);
                cmd.Parameters.AddWithValue("@d", txtAddre.Text);
                int lastID = (int)cmd.ExecuteScalar();
                txtID.Text = lastID.ToString();
                txtReward.Text = "0.00";
                connection.Close();
                
            }
        }
        //search customer through user name input and autofill the textbox
        private void button4_Click(object sender, EventArgs e)
        {
            if (connection != null)
            {
                connection.Open();
                if (txtFname.Text.Length > 0 && txtLname.Text.Length > 0)
                {
                    string query = "SELECT id,phone,addre,reward FROM customer ";
                    query += " WHERE firstName = '" + txtFname.Text + "'";
                    query += " AND lastName = '" + txtLname.Text + "'";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        txtID.Text = dataReader["id"].ToString();
                        txtPhone.Text = dataReader["phone"].ToString();
                        txtAddre.Text = dataReader["addre"].ToString();
                        txtReward.Text = dataReader["reward"].ToString();

                    }
                    dataReader.Close();
                    RewardResult();
                    //disable the search once the first search is finished
                    
                }
                else
                {
                    MessageBox.Show("please input both the first and last name!");
                }
                connection.Close();

            }
        }
        //update user information if any data has changed
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (connection != null)
            {
                connection.Open();
                string instruction = "Update customer set phone = @a, addre = @b WHERE id = @c ";
                SqlCommand cmd = new SqlCommand(instruction, connection);
                cmd.Parameters.AddWithValue("@a", txtPhone.Text);
                cmd.Parameters.AddWithValue("@b", txtAddre.Text);
                cmd.Parameters.AddWithValue("@c", int.Parse(txtID.Text));
                cmd.ExecuteNonQuery();
                connection.Close();
            }

        }
        //generate new order and fill the orderID textbox
        private void btnNewOrder_Click(object sender, EventArgs e)
        {
            //disable three customer query buttons
            btnAddC.Enabled = false;
            btnSearch.Enabled = false;
            btnUpdate.Enabled = false;

            if (connection != null)
            {
                connection.Open();
                string instruction = "INSERT INTO orders(customer_id) output INSERTED.order_id VALUES(@a) ";
                SqlCommand cmd = new SqlCommand(instruction, connection);
                cmd.Parameters.AddWithValue("@a", int.Parse(txtID.Text));
                order_id = (int)cmd.ExecuteScalar();
                txtOrderID.Text = order_id.ToString();
                //RewardResult();
                connection.Close();
                btnNewOrder.Enabled = false;
            }
          

        }
        //show redeem pizza size available
        void RewardResult()
        {
            //reward = new Rewarding_Management();
            if (txtReward.Text != String.Empty)
            {
                decimal rewards = Convert.ToDecimal(txtReward.Text);
                string instruction = "SELECT reward_small from reward ";
                SqlCommand cmd = new SqlCommand(instruction, connection);
                reward_small = Convert.ToDecimal(cmd.ExecuteScalar());
                reward_medium = reward_small + 5;
                reward_large = reward_small + 10;
                if (rewards >= reward_small && rewards < reward_medium)
                {
                    txtR.Text = "small";
                    
                }
                else if (rewards >= reward_medium && rewards < reward_large)
                {
                    txtR.Text = "medium";
                   
                }
                else if (rewards >= reward_large)
                {
                    txtR.Text = "large";
                    
                }
                else
                {
                    txtR.Text = null;
                }
                
            }
        }

        
        private void pizzaCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sPizza;
            if (pizzaCombo.SelectedIndex != -1)
                sPizza = pizzaCombo.SelectedItem.ToString();
        }

        //add pizza to order
        private void btnAddP_Click(object sender, EventArgs e)
        {
            if (connection != null)
            {
                connection.Open();
                string pizza_size, pizza_name, topping_name;
                int pizza_id, topping_id, pizza_topping_id, topping_amount;
                pizza_size = "small";
                bool check = false;
                List<int> IngreAmount = new List<int>();
                bool pizzaAvai = true;


                if (btnSmall.Checked || btnMedium.Checked || btnLarge.Checked)
                {
                    //size selection
                    if (btnSmall.Checked)
                        pizza_size = btnSmall.Text;
                    else if (btnMedium.Checked)
                        pizza_size = btnMedium.Text;
                    else if (btnLarge.Checked)
                        pizza_size = btnLarge.Text;
                    //pizza selection
                    if (pizzaCombo.SelectedIndex > -1)
                    {
                        //get pizza_id
                        pizza_name = pizzaCombo.SelectedItem.ToString();
                        pizza_id = PizzaID(pizza_name);
                        IngreAmount = CheckPizza(pizza_id);
                        foreach (int ingredient_amount in IngreAmount)
                        {
                            if(ingredient_amount == 0)
                            {
                                pizzaAvai = false;
                            }

                        }
                        if (pizzaAvai)
                        {
                            //update amout of related ingredients
                            UpdateIngre(pizza_id);

                            //get pizza_price
                            total_price += PizzaPrice(pizza_id, pizza_size);

                            PizzaIDC.Add(pizza_id);


                            //topping selection
                            if (chkCheese.Checked)
                            {
                                check = true;
                                topping_name = chkCheese.Text;
                                topping_id = ToppingID(topping_name);
                                topping_amount = CheckTopping(topping_id);
                                if (topping_amount > 0)
                                {
                                    UpdateIngre2(topping_id);
                                    ToppingIDC.Add(topping_id);
                                    total_price += ToppingPrice(topping_id);
                                    pizza_topping_id = insertPizza_topping(pizza_id, pizza_size, topping_id);
                                    insertOrder_pizza(order_id, pizza_topping_id);
                                }
                                else
                                    MessageBox.Show("cheese is not available right now!");
                                
                            }
                            if (chkOnion.Checked)
                            {
                                check = true;
                                topping_name = chkOnion.Text;
                                topping_id = ToppingID(topping_name);
                                topping_amount = CheckTopping(topping_id);
                                if (topping_amount > 0)
                                {
                                    UpdateIngre2(topping_id);
                                    ToppingIDC.Add(topping_id);
                                    total_price += ToppingPrice(topping_id);
                                    pizza_topping_id = insertPizza_topping(pizza_id, pizza_size, topping_id);
                                    insertOrder_pizza(order_id, pizza_topping_id);

                                }
                                else
                                    MessageBox.Show("onion is not available right now!");

                            }
                            if (chkBlack.Checked)
                            {
                                check = true;
                                topping_name = chkBlack.Text;
                                topping_id = ToppingID(topping_name);
                                topping_amount = CheckTopping(topping_id);
                                if (topping_amount > 0)
                                {
                                    UpdateIngre2(topping_id);
                                    ToppingIDC.Add(topping_id);
                                    total_price += ToppingPrice(topping_id);
                                    pizza_topping_id = insertPizza_topping(pizza_id, pizza_size, topping_id);
                                    insertOrder_pizza(order_id, pizza_topping_id);

                                }
                                else
                                    MessageBox.Show("black olive is not available right now!");
                            }
                            if (chkTomato.Checked)
                            {
                                check = true;
                                topping_name = chkTomato.Text;
                                topping_id = ToppingID(topping_name);
                                topping_amount = CheckTopping(topping_id);
                                if (topping_amount > 0)
                                {
                                    UpdateIngre2(topping_id);
                                    ToppingIDC.Add(topping_id);
                                    total_price += ToppingPrice(topping_id);
                                    pizza_topping_id = insertPizza_topping(pizza_id, pizza_size, topping_id);
                                    insertOrder_pizza(order_id, pizza_topping_id);
                                }
                                else
                                    MessageBox.Show("tomato is not available right now!");
                               
                            }
                            if (chkMush.Checked)
                            {
                                check = true;
                                topping_name = chkMush.Text;
                                topping_id = ToppingID(topping_name);
                                topping_amount = CheckTopping(topping_id);
                                if (topping_amount > 0)
                                {
                                    UpdateIngre2(topping_id);
                                    ToppingIDC.Add(topping_id);
                                    total_price += ToppingPrice(topping_id);
                                    pizza_topping_id = insertPizza_topping(pizza_id, pizza_size, topping_id);
                                    insertOrder_pizza(order_id, pizza_topping_id);
                                }
                                else
                                    MessageBox.Show("mushroom is not available right now!");
                                
                            }
                            if (chkPepper.Checked)
                            {
                                check = true;
                                topping_name = chkPepper.Text;
                                topping_id = ToppingID(topping_name);
                                topping_amount = CheckTopping(topping_id);
                                if (topping_amount > 0)
                                {
                                    UpdateIngre2(topping_id);
                                    ToppingIDC.Add(topping_id);
                                    total_price += ToppingPrice(topping_id);
                                    pizza_topping_id = insertPizza_topping(pizza_id, pizza_size, topping_id);
                                    insertOrder_pizza(order_id, pizza_topping_id);
                                }
                                else
                                    MessageBox.Show("hot pepper is not available right now!");
                               
                            }
                            if (chkSpinach.Checked)
                            {
                                check = true;
                                topping_name = chkSpinach.Text;
                                topping_id = ToppingID(topping_name);
                                topping_amount = CheckTopping(topping_id);
                                if (topping_amount > 0)
                                {
                                    UpdateIngre2(topping_id);
                                    ToppingIDC.Add(topping_id);
                                    total_price += ToppingPrice(topping_id);
                                    pizza_topping_id = insertPizza_topping(pizza_id, pizza_size, topping_id);
                                    insertOrder_pizza(order_id, pizza_topping_id);

                                }
                                else
                                    MessageBox.Show("spinach is not available right now!");
                               

                            }
                            if (chkPine.Checked)
                            {
                                check = true;
                                topping_name = chkPine.Text;
                                topping_id = ToppingID(topping_name);
                                topping_amount = CheckTopping(topping_id);
                                if (topping_amount > 0)
                                {
                                    UpdateIngre2(topping_id);
                                    ToppingIDC.Add(topping_id);
                                    total_price += ToppingPrice(topping_id);
                                    pizza_topping_id = insertPizza_topping(pizza_id, pizza_size, topping_id);
                                    insertOrder_pizza(order_id, pizza_topping_id);
                                }
                                else
                                    MessageBox.Show("pineapple is not available right now!");
                                   
                               
                            }
                            if (chkBacon.Checked)
                            {
                                check = true;
                                topping_name = chkBacon.Text;
                                topping_id = ToppingID(topping_name);
                                topping_amount = CheckTopping(topping_id);
                                if (topping_amount > 0)
                                {
                                    UpdateIngre2(topping_id);
                                    ToppingIDC.Add(topping_id);
                                    total_price += ToppingPrice(topping_id);
                                    pizza_topping_id = insertPizza_topping(pizza_id, pizza_size, topping_id);
                                    insertOrder_pizza(order_id, pizza_topping_id);
                                }
                                else
                                    MessageBox.Show("bacon is not available right now!");
                                
                            }
                            if (chkRoni.Checked)
                            {
                                check = true;
                                topping_name = chkRoni.Text;
                                topping_id = ToppingID(topping_name);
                                topping_amount = CheckTopping(topping_id);
                                if (topping_amount > 0)
                                {
                                    UpdateIngre2(topping_id);
                                    ToppingIDC.Add(topping_id);
                                    total_price += ToppingPrice(topping_id);
                                    pizza_topping_id = insertPizza_topping(pizza_id, pizza_size, topping_id);
                                    insertOrder_pizza(order_id, pizza_topping_id);
                                }
                                else
                                    MessageBox.Show("pepperoni is not available right now!");
                                

                            }
                            if (chkChicken.Checked)
                            {
                                check = true;
                                topping_name = chkChicken.Text;
                                topping_id = ToppingID(topping_name);
                                topping_amount = CheckTopping(topping_id);
                                if (topping_amount > 0)
                                {
                                    UpdateIngre2(topping_id);
                                    ToppingIDC.Add(topping_id);
                                    total_price += ToppingPrice(topping_id);
                                    pizza_topping_id = insertPizza_topping(pizza_id, pizza_size, topping_id);
                                    insertOrder_pizza(order_id, pizza_topping_id);

                                }
                                else
                                    MessageBox.Show("chicken sausage is not available right now!");
                                

                            }
                            //not select any topping 
                            if (!check)
                            {
                                pizza_topping_id = insertPizza_topping2(pizza_id, pizza_size);
                                insertOrder_pizza(order_id, pizza_topping_id);

                            }


                        }//finish if pizza is not available 
                        else
                            MessageBox.Show("this pizza is not available right now!");
                           
                    }
                    else
                        MessageBox.Show("Please select a pizza!");
                }
                else
                    MessageBox.Show("Please choose a size for your pizza!");
                connection.Close();
            }//finish if connection != null
        }//finish add method
        
        //get pizza_id accroding to pizza_name
        int PizzaID (string pizza_name)
        {
            string instruction = "select id from pizza where name = @pizza_name ";
            SqlCommand cmd = new SqlCommand(instruction, connection);
            cmd.Parameters.AddWithValue("@pizza_name", pizza_name);
            return (Int32)cmd.ExecuteScalar();
        }

        //update ingredient amount when pizza is selected
        void UpdateIngre(int pizza_id)
        {
            string instruction = "UPDATE ingredient ";
                   instruction += " SET amount = amount - 1 ";
                   instruction += " WHERE id IN ( ";
                   instruction += " SELECT ingredient_id from pizza_ingredient ";
                   instruction += " WHERE pizza_id = @pizza_id ) ";
            SqlCommand cmd = new SqlCommand(instruction, connection);
            cmd.Parameters.AddWithValue("@pizza_id", pizza_id);
            cmd.ExecuteNonQuery();

        }
        //check pizza is available 
        List<int> CheckPizza(int pizza_id)
        {
            List<int> amountList = new List<int>();
            string instruction = "SELECT amount ";
                   instruction += " from ingredient ";
                   instruction += " WHERE id IN ( ";
                   instruction += " SELECT ingredient_id from pizza_ingredient ";
                   instruction += " WHERE pizza_id = @pizza_id ) ";
            SqlCommand cmd = new SqlCommand(instruction, connection);
            cmd.Parameters.AddWithValue("@pizza_id", pizza_id);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                amountList.Add((int)reader["amount"]);
            }
            reader.Close();
            return amountList;

        }
        //check topping is available 
        int CheckTopping(int topping_id)
        {
            string instruction = "SELECT amount ";
                   instruction += " from ingredient ";
                   instruction += " WHERE id = @topping_id ";
            SqlCommand cmd = new SqlCommand(instruction, connection);
            cmd.Parameters.AddWithValue("@topping_id", topping_id);
            return (Int32)cmd.ExecuteScalar();
        }
        //update ingredient when topping is selected
        void UpdateIngre2(int topping_id)
        {
            string instruction = "UPDATE ingredient ";
                   instruction += " SET amount = amount - 1 ";
                   instruction += " WHERE id = @topping_id ";
            SqlCommand cmd = new SqlCommand(instruction, connection);
            cmd.Parameters.AddWithValue("@topping_id", topping_id);
            cmd.ExecuteNonQuery();

        }

        //get pizza_price 
        decimal PizzaPrice (int pizza_id, string pizza_size)
        {
            string instruction = "select price from pizza_price where pizza_id = @pizza_id ";
            instruction += " AND size = @pizza_size";
            SqlCommand cmd = new SqlCommand(instruction, connection);
            cmd.Parameters.AddWithValue("@pizza_id", pizza_id);
            cmd.Parameters.AddWithValue("@pizza_size", pizza_size);
            return Convert.ToDecimal(cmd.ExecuteScalar());

        }
        //return the topping id according to the name
        int ToppingID (string topping_name)
        {
            string instruction = "select id from ingredient where name = @topping_name ";
            SqlCommand cmd = new SqlCommand(instruction, connection);
            cmd.Parameters.AddWithValue("@topping_name", topping_name);
            return (Int32)cmd.ExecuteScalar();
        }
        //return the topping price 
        decimal ToppingPrice(int topping_id)
        {
            string instruction = "select price from ingredient where id = @topping_id ";
            SqlCommand cmd = new SqlCommand(instruction, connection);
            cmd.Parameters.AddWithValue("@topping_id", topping_id);
            return Convert.ToDecimal(cmd.ExecuteScalar());
        }
        //insert pizza_id, pizza_size, pizza_topping into pizza_topping table 
        int insertPizza_topping (int pizza_id, string pizza_size, int topping_id)
        {
            string instruction = "INSERT INTO pizza_topping(pizza_id, pizza_size, topping_id) output INSERTED.pizza_topping_id ";
                   instruction += "values(@a,@b,@c) "; // note there is no space within the quotation mark
            SqlCommand cmd = new SqlCommand(instruction, connection);
            cmd.Parameters.AddWithValue("@a", pizza_id);
            cmd.Parameters.AddWithValue("@b", pizza_size);
            cmd.Parameters.AddWithValue("@c", topping_id);
            return (Int32)cmd.ExecuteScalar();
           // txtPizza_topping.Text = pizza_topping_id.ToString();
            
            
        }
        //insert pizza_id, pizza_size whithout topping_id 
        int insertPizza_topping2(int pizza_id, string pizza_size)
        {
            string instruction = "INSERT INTO pizza_topping(pizza_id, pizza_size) output INSERTED.pizza_topping_id ";
            instruction += "values(@a,@b) "; // note there is no space within the quotation mark
            SqlCommand cmd = new SqlCommand(instruction, connection);
            cmd.Parameters.AddWithValue("@a", pizza_id);
            cmd.Parameters.AddWithValue("@b", pizza_size);
            return (Int32)cmd.ExecuteScalar();
            // txtPizza_topping.Text = pizza_topping_id.ToString();


        }

        //insert order_match table
        void insertOrder_pizza (int order_id, int pizza_topping_id)
        {
            string instruction = "INSERT INTO order_pizza(order_id, pizza_topping_id) ";
            instruction += "values(@order_id,@pizza_topping_id) ";
            SqlCommand cmd = new SqlCommand(instruction, connection);
            cmd.Parameters.AddWithValue("@order_id", order_id);
            cmd.Parameters.AddWithValue("@pizza_topping_id", pizza_topping_id);
            cmd.ExecuteNonQuery();
        }

        private void btnNewp_Click(object sender, EventArgs e)
        {
            btnSmall.Checked = false;
            btnMedium.Checked = false;
            btnLarge.Checked = false;
            pizzaCombo.SelectedIndex = -1;
            chkBacon.Checked = false;
            chkBlack.Checked = false;
            chkCheese.Checked = false;
            chkChicken.Checked = false;
            chkMush.Checked = false;
            chkOnion.Checked = false;
            chkPepper.Checked = false;
            chkPine.Checked = false;
            chkRoni.Checked = false;
            chkSpinach.Checked = false;
            chkTomato.Checked = false;
            

        }
        //add side to order
        private void btnAddS_Click(object sender, EventArgs e)
        {
            int side_id;
            string side_name;
            if (connection != null)
            {
                connection.Open();
                if (chkGK.Checked)
                {
                    side_name = chkGK.Text;
                    side_id = SideID(side_name);
                    total_price += SidePrice(side_id);
                    insertOrder_side(order_id, side_id);

                }
                if (chkBW.Checked)
                {
                    side_name = chkBW.Text;
                    side_id = SideID(side_name);
                    total_price += SidePrice(side_id);
                    insertOrder_side(order_id, side_id);

                }
                if (chkBC.Checked)
                {
                    side_name = chkBC.Text;
                    side_id = SideID(side_name);
                    total_price += SidePrice(side_id);
                    insertOrder_side(order_id, side_id);

                }
                if (chkCP.Checked)
                {
                    side_name = chkCP.Text;
                    side_id = SideID(side_name);
                    total_price += SidePrice(side_id);
                    insertOrder_side(order_id, side_id);

                }
                //txtTprice.Text = total_price.ToString();
                connection.Close();
            }//finish connection != null condition
        }//fnish method
        
        //find the side_id according to the name
        int SideID(string side_name)
        {
            string instruction = "select id from side where name = @side_name ";
            SqlCommand cmd = new SqlCommand(instruction, connection);
            cmd.Parameters.AddWithValue("@side_name", side_name);
            return (Int32)cmd.ExecuteScalar();
        }
        //get the side_price accoridng to the side_id
        decimal SidePrice(int side_id)
        {
            string instruction = "select price from side where id = @side_id ";
            SqlCommand cmd = new SqlCommand(instruction, connection);
            cmd.Parameters.AddWithValue("@side_id", side_id);
            return Convert.ToDecimal(cmd.ExecuteScalar());
        }
        //insert into order_match(order_id, side_id)
        void insertOrder_side (int order_id, int side_id)
        {
            string instruction = "insert into order_side(order_id, side_id) ";
            instruction += "values(@order_id, @side_id) ";
            SqlCommand cmd = new SqlCommand(instruction, connection);
            cmd.Parameters.AddWithValue("@order_id", order_id);
            cmd.Parameters.AddWithValue("@side_id", side_id);
            cmd.ExecuteNonQuery();
        }

        //add drink to the order
        private void btnAddD_Click(object sender, EventArgs e)
        {
            int drink_id;
            string drink_name;
            if (connection != null)
            {
                connection.Open();
                if (chkP.Checked)
                {
                    drink_name = chkP.Text;
                    drink_id = DrinkID(drink_name);
                    total_price += DrinkPrice(drink_id);
                    insertOrder_drink(order_id, drink_id);

                }
                if (chkO.Checked)
                {
                    drink_name = chkO.Text;
                    drink_id = DrinkID(drink_name);
                    total_price += DrinkPrice(drink_id);
                    insertOrder_drink(order_id, drink_id);


                }
                if (chkS.Checked)
                {
                    drink_name = chkS.Text;
                    drink_id = DrinkID(drink_name);
                    total_price += DrinkPrice(drink_id);
                    insertOrder_drink(order_id, drink_id);


                }
                if (chkL.Checked)
                {
                    drink_name = chkL.Text;
                    drink_id = DrinkID(drink_name);
                    total_price += DrinkPrice(drink_id);
                    insertOrder_drink(order_id, drink_id);

                }
                
                connection.Close();
            }//finish connection != null condition

        }

       

        //get drink_id according to drink_name
        int DrinkID(string drink_name)
        {

            string instruction = "select id from drink where name = @drink_name ";
            SqlCommand cmd = new SqlCommand(instruction, connection);
            cmd.Parameters.AddWithValue("@drink_name", drink_name);
            return (Int32)cmd.ExecuteScalar();

        }
        //get drink_price according to drink_id
        decimal DrinkPrice(int drink_id)
        {
            string instruction = "select price from drink where id = @drink_id ";
            SqlCommand cmd = new SqlCommand(instruction, connection);
            cmd.Parameters.AddWithValue("@drink_id", drink_id);
            return Convert.ToDecimal(cmd.ExecuteScalar());


        }
        //insert order_drink table
        private void insertOrder_drink(int order_id, int drink_id)
        {
            string instruction = "insert into order_drink(order_id, drink_id) ";
            instruction += "values(@order_id, @drink_id) ";
            SqlCommand cmd = new SqlCommand(instruction, connection);
            cmd.Parameters.AddWithValue("@order_id", order_id);
            cmd.Parameters.AddWithValue("@drink_id", drink_id);
            cmd.ExecuteNonQuery();

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (connection != null)
            {
                connection.Open();
                txtTprice.Text = total_price.ToString();
                txtTax.Text = (total_price * tax_rate).ToString();
                UpdateTprice();
                plus_Reward();
                txtAT.Text = total_price.ToString();
                QueryOrder();
                connection.Close();
            }


        }
        //update total_price into order_pizza table
        void UpdateTprice()
        {
            total_price = Math.Round(total_price + total_price * tax_rate, 2);
            string instruction = "UPDATE order_pizza SET total_price = @total_price ";
            instruction += " WHERE order_id = @order_id ";
            SqlCommand cmd = new SqlCommand(instruction, connection);
            cmd.Parameters.AddWithValue("@order_id", order_id);
            cmd.Parameters.AddWithValue("@total_price", total_price);
            cmd.ExecuteNonQuery();
        }

        //plus reward when new order submitted
        void plus_Reward()
        {
            string instruction;
            /*
            if(txtReward.Text != String.Empty)
            {
                instruction = "UPDATE customer SET reward = reward + @rewards WHERE id = @CusID ";

            }
            else
            {
                instruction = "UPDATE customer SET reward = @rewards WHERE id = @CusID ";
            }
            */
            instruction = "UPDATE customer SET reward = reward + @rewards WHERE id = @CusID ";
            decimal rewards;
            rewards = total_price / 10;
            int CusID = int.Parse(txtID.Text);
            SqlCommand cmd = new SqlCommand(instruction, connection);
            cmd.Parameters.AddWithValue("@rewards", rewards);
            cmd.Parameters.AddWithValue("@CusID", CusID);
            cmd.ExecuteNonQuery();
        }

        //reverse reward after order cancelled
        void ReverseReward()
        {
    
            decimal rewards = Convert.ToDecimal(txtReward.Text); 
            int CusID = int.Parse(txtID.Text);
            string instruction = "UPDATE customer SET reward = @rewards ";
            instruction += " WHERE id = @CusID ";
            SqlCommand cmd = new SqlCommand(instruction, connection);
            cmd.Parameters.AddWithValue("@CusID", CusID);
            cmd.Parameters.AddWithValue("@rewards", rewards);
            cmd.ExecuteNonQuery();
        }

        //query order information from order_pizza, order_side, order_drink tables
        void QueryOrder()
        {
            //nested join 
            string instruction = "SELECT op.order_id, p.name AS pizza, pt.pizza_size, t.name AS topping, s.name AS side, d.name AS drink, op.order_status ";
            instruction += " FROM pizza p ";
            instruction += " LEFT JOIN (pizza_topping pt LEFT JOIN ingredient t ON (t.id = pt.topping_id or t.id is null and pt.topping_id is null)) ";
            instruction += " ON p.id = pt.pizza_id ";
            instruction += " LEFT JOIN order_pizza op ";
            instruction += " ON pt.pizza_topping_id = op.pizza_topping_id ";
            instruction += " LEFT JOIN (order_side os LEFT JOIN side s ON (s.id = os.side_id or s.id is null and os.side_id is null)) ";
            instruction += " ON op.order_id = os.order_id ";
            instruction += " LEFT JOIN (order_drink od LEFT JOIN drink d ON (d.id = od.drink_id or d.id is null and od.drink_id is null)) ";
            instruction += " ON os.order_id = od.order_id ";
            instruction += " WHERE op.order_id = @order_id ";

            SqlCommand cmd = new SqlCommand(instruction, connection);
            cmd.Parameters.AddWithValue("@order_id", order_id);
            SqlDataAdapter mcmd = new SqlDataAdapter();
            mcmd.SelectCommand = cmd;
            DataSet ds = new DataSet();
            mcmd.Fill(ds);
            orderView.DataSource = ds.Tables[0];

        }

        //cancel or delete the five tables in orders (order_drink, order_side, order_pizza, pizza_topping, orders)
        private void btnCancel_Click(object sender, EventArgs e)
        {
            List<int> PizzaToppingID;
            if(connection != null)
            {
                connection.Open();
                DeOrderDrink();
                DeOrderSide();
                PizzaToppingID = getPizzaToppingID();
                DeOrderPizza();
                DePizzaTopping(PizzaToppingID);
                DeOrders();
                reverseAmount(PizzaIDC);
                reverseAmount2(ToppingIDC);
                txtTprice.Clear();
                txtTax.Clear();
                txtAT.Clear();
                orderView.DataSource = null;
                orderView.Rows.Clear();
                ReverseReward();
                btnReset_Click(sender, e);
                connection.Close();
                
            }
        }
        //delete order_drink row of the order_id
        void DeOrderDrink()
        {
            string instruction = "DELETE from order_drink Where order_id = @order_id ";
            SqlCommand cmd = new SqlCommand(instruction, connection);
            cmd.Parameters.AddWithValue("@order_id", order_id);
            cmd.ExecuteNonQuery();

        }
        //delete order_side row of the order_id
        void DeOrderSide()
        {
            string instruction = "DELETE from order_side Where order_id = @order_id ";
            SqlCommand cmd = new SqlCommand(instruction, connection);
            cmd.Parameters.AddWithValue("@order_id", order_id);
            cmd.ExecuteNonQuery();
        }
        
        
        
        //find pizza_topping_id and store it in a list to delete order_pizza first
        List<int> getPizzaToppingID()
        {
            List<int> PizzaToppingID = new List<int>();
            string instruction = " SELECT pizza_topping_id from order_pizza ";
                   instruction += " WHERE order_id = @order_id ";
            SqlCommand cmd = new SqlCommand(instruction, connection);
            cmd.Parameters.AddWithValue("@order_id", order_id);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                PizzaToppingID.Add((int)reader["pizza_topping_id"]);
            }
            reader.Close();
            return PizzaToppingID;
            
        }
        //delete pizza_topping according the PizzaToppingID list above
        void DePizzaTopping(List<int> PizzaToppingID)
        {
            
            
            foreach(int pizza_topping_id in PizzaToppingID)
            {
                string instruction = "DELETE from pizza_topping WHERE pizza_topping_id = @pizza_topping_id ";
                SqlCommand cmd = new SqlCommand(instruction, connection);
                cmd.Parameters.AddWithValue("@pizza_topping_id", pizza_topping_id);
                cmd.ExecuteNonQuery();
            }

        }
        //note the squence should be delete order_pizza after deleting pizza_topping
        void DeOrderPizza()
        {
            string instruction = "DELETE from order_pizza Where order_id = @order_id ";
            SqlCommand cmd = new SqlCommand(instruction, connection);
            cmd.Parameters.AddWithValue("@order_id", order_id);
            cmd.ExecuteNonQuery();

        }
        //delete order finally according to order_id
        void DeOrders()
        {
            string instruction = "DELETE from orders Where order_id = @order_id ";
            SqlCommand cmd = new SqlCommand(instruction, connection);
            cmd.Parameters.AddWithValue("@order_id", order_id);
            cmd.ExecuteNonQuery();
        }
        //reverse amount of ingredient according to pizzaList
        void reverseAmount(List<int> pizzaIDC)
        {
            foreach(int pizza_id in pizzaIDC)
            {
                string instruction = "UPDATE ingredient ";
                instruction += " SET amount = amount + 1 ";
                instruction += " WHERE id IN ( ";
                instruction += " SELECT ingredient_id from pizza_ingredient ";
                instruction += " WHERE pizza_id = @pizza_id ) ";
                SqlCommand cmd = new SqlCommand(instruction, connection);
                cmd.Parameters.AddWithValue("@pizza_id", pizza_id);
                cmd.ExecuteNonQuery();

            }
        }
        //reverse amount of ingredient according to toppingList
        void reverseAmount2(List<int> toppingIDC)
        {
            foreach(int topping_id in toppingIDC)
            {
                string instruction = "UPDATE ingredient ";
                instruction += " SET amount = amount + 1 ";
                instruction += " WHERE id = @topping_id ";
                SqlCommand cmd = new SqlCommand(instruction, connection);
                cmd.Parameters.AddWithValue("@topping_id", topping_id);
                cmd.ExecuteNonQuery();

            }
             
        }
            

        private void btnReset_Click(object sender, EventArgs e)
        {
            //unchecked all the checkbox 
            txtID.Clear();
            txtPhone.Clear();
            txtAddre.Clear();
            txtFname.Clear();
            txtLname.Clear();
            txtReward.Clear();
            txtTprice.Clear();
            txtTax.Clear();
            txtAT.Clear();
            txtOrderID.Clear();
            orderView.DataSource = null;
            pizzaCombo.SelectedIndex = -1;
            btnNewp_Click(sender, e);
            chkGK.Checked = false;
            chkBC.Checked = false;
            chkBW.Checked = false;
            chkCP.Checked = false;
            chkP.Checked = false;
            chkO.Checked = false;
            chkS.Checked = false;
            chkL.Checked = false;
            //every time new order clear the PizzaIDC and ToppingIDC
            PizzaIDC.Clear();
            ToppingIDC.Clear();
            txtR.Clear();
            total_price = 0m;
            //restore all the function buttons when reset
            btnSearch.Enabled = true;
            btnAddC.Enabled = true;
            btnUpdate.Enabled = true;
            btnNewOrder.Enabled = true;
            btnRedeem.Enabled = true;
            

        }

        private void btnKitchen_Click(object sender, EventArgs e)
        {
            Kitchen kitchen = new Kitchen();
            kitchen.Show();
        }

        private void btnDelivery_Click(object sender, EventArgs e)
        {
            Delivery delivery = new Delivery();
            delivery.Show();
        }

        private void btnRedeem_Click(object sender, EventArgs e)
        {
            
            int pizza_id;
            string pizza_name;
            string pizza_size;
            int pizza_topping_id;
            bool pizzaAvai = true;
            List<int> IngreAmount = new List<int>();
            if(connection != null)
            {
                connection.Open();
                if (txtR.Text != String.Empty)
                {
                    pizza_size = txtR.Text;
                    if (pizzaCombo.SelectedIndex > -1)
                    {
                        pizza_name = pizzaCombo.SelectedItem.ToString();
                        pizza_id = PizzaID(pizza_name);
                        IngreAmount = CheckPizza(pizza_id);
                        foreach (int ingredient_amount in IngreAmount)
                        {
                            if (ingredient_amount == 0)
                            {
                                pizzaAvai = false;
                            }

                        }
                        if (pizzaAvai)
                        {
                            pizza_topping_id = insertPizza_topping2(pizza_id, pizza_size);
                            insertOrder_pizza(order_id, pizza_topping_id);
                            //update ingredient amount
                            UpdateIngre(pizza_id);
                            //later to reverse when it is canceled
                            PizzaIDC.Add(pizza_id);
                            QueryOrder();
                            minus_Reward();
                            btnRedeem.Enabled = false;

                        }
                        else
                        {
                            MessageBox.Show("please choose a differnt pizza to redeem!");
                        }
                        
                    }
                    else
                        MessageBox.Show("please select a pizza to redeem!");
                }
                else
                    MessageBox.Show("no pizza available to redeem");
                connection.Close();

            }
        }
        //update reward after redeem
        void minus_Reward()
        {
            string redeemSize = txtR.Text;
            decimal redeemPoint = 0m;
            int CusID = int.Parse(txtID.Text);
            if (redeemSize == "small")
            {
                redeemPoint = reward_small;
            }
            else if (redeemSize == "medium")
            {
                redeemPoint = reward_medium;
            }
            else if (redeemSize == "large")
            {
                redeemPoint = reward_large;
            }
            string instruction = "Update customer SET reward = reward - @redeemPoint ";
            instruction += " WHERE id = @CusID ";
            SqlCommand cmd = new SqlCommand(instruction, connection);
            cmd.Parameters.AddWithValue("@redeemPoint", redeemPoint);
            cmd.Parameters.AddWithValue("@CusID", CusID);

            cmd.ExecuteNonQuery();
                
            
       }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            customer_management customer = new customer_management();
            customer.Show();
        }

        
    }//form
}//namespace

