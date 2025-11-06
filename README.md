using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;
using System.Xml.Serialization;
using static Project02.Form1;

namespace Project02
{
    public partial class Form1 : Form
    {
        private List<MenuItems> menuItems;  //list for menuitems
        private List<OrderSystem> allOrders;   //list for the ordersystem

        public Form1()
        {
            InitializeComponent();
            InitializeMenuItems();
        }
        private void button1_Click(object sender, EventArgs e)
        {

            btnPlaceOrder_Click(sender, e);
        }

        private void InitializeMenuItems()
        {
            menuItems = new List<MenuItems>();
            allOrders = new List<OrderSystem>();

            //  these are my Appetizers and prices
            menuItems.Add(new MenuItems("Beef Gyoza", 8.99m, "Appetizer"));
            menuItems.Add(new MenuItems("Chicken Kebab", 7.49m, "Appetizer"));
            menuItems.Add(new MenuItems("Chicken Wings", 9.99m, "Appetizer"));
            menuItems.Add(new MenuItems("Sushi", 12.99m, "Appetizer"));
            menuItems.Add(new MenuItems("Spring Roll", 6.49m, "Appetizer"));

            // these are my Entrees and prices
            menuItems.Add(new MenuItems("Crab Fried Rice", 15.99m, "Entree"));
            menuItems.Add(new MenuItems("Beef Pho", 13.49m, "Entree"));
            menuItems.Add(new MenuItems("Banh Mi", 10.99m, "Entree"));
            menuItems.Add(new MenuItems("Butter Chicken and Naan", 16.99m, "Entree"));
            menuItems.Add(new MenuItems("Burger and Fries", 12.99m, "Entree"));

            // these are my Desserts and prices
            menuItems.Add(new MenuItems("Mochi Ice Cream (Matcha)", 6.99m, "Dessert"));
            menuItems.Add(new MenuItems("Gulab Jamun", 5.49m, "Dessert"));
            menuItems.Add(new MenuItems("Mango Sticky Rice", 7.99m, "Dessert"));
            menuItems.Add(new MenuItems("Bingsu", 8.99m, "Dessert"));
            menuItems.Add(new MenuItems("Chocolate Cake", 6.49m, "Dessert"));
        }


        private void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            // asking for you to enter name
            if (string.IsNullOrWhiteSpace(customerText.Text))
            {
                MessageBox.Show("Please enter customer name.", "Missing Information",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // selects the option you pick for each meal section
            MenuItems selectedAppetizer = GetSelectedAppetizer();
            MenuItems selectedEntree = GetSelectedEntree();
            MenuItems selectedDessert = GetSelectedDessert();

            //makes sure you have selected an item in each category
              if (selectedAppetizer == null || selectedEntree == null || selectedDessert == null)
            {
                MessageBox.Show("Please select one item from each category (Appetizer, Entree, Dessert).",
                                "Incomplete Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // this creates the order.
            OrderSystem newOrder = new OrderSystem(customerText.Text);
            newOrder.AddMenuItem(selectedAppetizer);
            newOrder.AddMenuItem(selectedEntree);
            newOrder.AddMenuItem(selectedDessert);

            Random rnd = new Random();
            newOrder.Discount = (decimal)(rnd.NextDouble() * 5); // random discount
            // this calculates the total of your bill.
            newOrder.CalculateTotals();

            // displays the price of your items. f2 used for format.
            listBox1.Items.Clear();
            listBox1.Items.Add($"Appetizer: {selectedAppetizer.Name} - ${selectedAppetizer.Price:F2}");
            listBox1.Items.Add($"Entree: {selectedEntree.Name} - ${selectedEntree.Price:F2}");
            listBox1.Items.Add($"Dessert: {selectedDessert.Name} - ${selectedDessert.Price:F2}");


            label8.Text = $"${newOrder.Subtotal:F2}";      // This is the total before tax and discount
            label9.Text = $"${newOrder.Tax:F2}";           // This is the tax
            label10.Text = $"${newOrder.Discount:F2}";     // This is the discount
            label11.Text = $"${newOrder.GrandTotal:F2}";   // This is the grand total, accounting for discount and tax.

            // saves the order
            allOrders.Add(newOrder);

            // message box showing your order summary
            MessageBox.Show($"Order placed successfully for {newOrder.CustomerName}!\n\n" +
                           $"Subtotal: ${newOrder.Subtotal:F2}\n" +
                           $"Tax: ${newOrder.Tax:F2}\n" +
                           $"Grand Total: ${newOrder.GrandTotal:F2}",
                            "Order Confirmed", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void btnClearOrder_Click(object sender, EventArgs e) // this whole section is for clearing and resetting values and buttons.
        {

            foreach (Control control in groupBox1.Controls)
            {
                if (control is RadioButton rb)
                    rb.Checked = false;
            }

            foreach (Control control in groupBox2.Controls)
            {
                if (control is RadioButton rb)
                    rb.Checked = false;
            }

            foreach (Control control in groupBox3.Controls)
            {
                if (control is RadioButton rb)
                    rb.Checked = false;
            }

            customerText.Text = "";

            listBox1.Items.Clear();


            label8.Text = "$0.00";
            label9.Text = "$0.00";
            label10.Text = "$0.00";
            label11.Text = "$0.00";

            MessageBox.Show("Order cleared!", "Cleared", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private MenuItems GetSelectedAppetizer() //  radio button associated with your appetizer
        {
            if (App1.Checked)
            {
                foreach (var item in menuItems)
                {
                    if (item.Name == "Beef Gyoza")
                        return item;
                }
            }
            else if (App2.Checked)
            {
                foreach (var item in menuItems)
                {
                    if (item.Name == "Chicken Kebab")
                        return item;
                }
            }
            else if (App3.Checked)
            {
                foreach (var item in menuItems)
                {
                    if (item.Name == "Sushi")
                        return item;
                }
            }
            else if (App4.Checked)
            {
                foreach (var item in menuItems)
                {
                    if (item.Name == "Spring Roll")
                        return item;
                }
            }
            else if (App5.Checked)
            {
                foreach (var item in menuItems)
                {
                    if (item.Name == "Chicken Wings")
                        return item;
                }
            }

            return null;
        }

        private MenuItems GetSelectedEntree()  // radio button associated with your entree
        {
            if (Entree1.Checked)
            {
                foreach (var item in menuItems)
                {
                    if (item.Name == "Crab Fried Rice")
                        return item;
                }
            }
            else if (Entree2.Checked)
            {
                foreach (var item in menuItems)
                {
                    if (item.Name == "Beef Pho")
                        return item;
                }
            }
            else if (Entree3.Checked)
            {
                foreach (var item in menuItems)
                {
                    if (item.Name == "Butter Chicken and Naan")
                        return item;
                }
            }
            else if (Entree4.Checked)
            {
                foreach (var item in menuItems)
                {
                    if (item.Name == "Burger and Fries")
                        return item;
                }
            }
            else if (Entree5.Checked)
            {
                foreach (var item in menuItems)
                {
                    if (item.Name == "Banh Mi")
                        return item;
                }
            }

            return null;
        }

        private MenuItems GetSelectedDessert() // radio button associated with your dessert 
        {
            if (Dessert1.Checked)
            {
                foreach (var item in menuItems)
                {
                    if (item.Name == "Mochi Ice Cream (Matcha)")
                        return item;
                }
            }
            else if (Dessert2.Checked)
            {
                foreach (var item in menuItems)
                {
                    if (item.Name == "Gulab Jamun")
                        return item;
                }
            }
            else if (Dessert3.Checked)
            {
                foreach (var item in menuItems)
                {
                    if (item.Name == "Bingsu")
                        return item;
                }
            }
            else if (Dessert4.Checked)
            {
                foreach (var item in menuItems)
                {
                    if (item.Name == "Chocolate Cake")
                        return item;
                }
            }
            else if (Dessert5.Checked)
            {
                foreach (var item in menuItems)
                {
                    if (item.Name == "Mango Sticky Rice")
                        return item;
                }
            }

            return null;
        }



        private void Entree5_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void Dessert1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void App1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void App4_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            // Clear all radio buttons in Appetizer group
            foreach (Control control in groupBox1.Controls) 
            {
                if (control is RadioButton rb)
                    rb.Checked = false;
            }

            // Clear all radio buttons in Entree group
            foreach (Control control in groupBox2.Controls) 
            {
                if (control is RadioButton rb)
                    rb.Checked = false;
            }

            // Clear all radio buttons in Dessert group
            foreach (Control control in groupBox3.Controls) 
            {
                if (control is RadioButton rb)
                    rb.Checked = false;
            }

            
            customerText.Text = "";

           
            listBox1.Items.Clear();


            label8.Text = "$0.00"; 
            label9.Text = "$0.00";  
            label10.Text = "$0.00";
            label11.Text = "$0.00"; 

            
            MessageBox.Show("Order cleared!", "Cleared", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }


        public class MenuItems
        {
            public string Name { get; set; }
            public decimal Price { get; set; }
            public string Course { get; set; }

            public MenuItems(string name, decimal price, string course) // constructors OOP
            {
                Name = name;
                Price = price;
                Course = course;
            }

            public override string ToString()
            {
                return $"{Name} - ${Price:F2}";
            }
        }

        public class OrderSystem // getters and setters for lists // ENCAPSULATION OOP
        {
            public string CustomerName { get; set; }
            public List<MenuItems> Items { get; set; }
            public decimal Subtotal { get; private set; }
            public decimal Tax { get; private set; }
            public decimal Discount { get; set; }
            public decimal GrandTotal { get; private set; }

            public OrderSystem(string customerName)
            {
                CustomerName = customerName;
                Items = new List<MenuItems>();
                Discount = 0;
            }

            public void AddMenuItem(MenuItems item) // EXAMPLE OF METHODS OOP
            {
                Items.Add(item);
            }

            public void CalculateTotals(decimal taxRate = 0.08m)
            {
                Subtotal = 0;
                foreach (MenuItems item in Items)
                {
                    Subtotal += item.Price;
                }

                Tax = Subtotal * taxRate;
                GrandTotal = Subtotal + Tax - Discount;
            }

            public string GetOrderSummary() // receipt, information goes into list box and will display pries of your food.
            {
                string receipt = $"Customer: {CustomerName}\n\n";

                foreach (MenuItems item in Items)
                {
                    receipt += $"{item.Course}: {item.Name} - ${item.Price:F2}\n";
                }

                receipt += $"\nSubtotal: ${Subtotal:F2}";
                receipt += $"\nTax (8%): ${Tax:F2}";
                receipt += $"\nDiscount: -${Discount:F2}";
                receipt += $"\nGrand Total: ${GrandTotal:F2}";

                return receipt;
            }
        }
    }

