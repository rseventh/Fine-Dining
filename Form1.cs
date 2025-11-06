using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Program2
{
    public partial class Form1 : Form
    {                                  // Prices of all the books
        double checkBox1Price = 12.99; // This is Book 1: The Great Adventure
        double checkBox2Price = 15.99; // This is Book 2: Journey to the Stars
        double checkBox3Price = 19.99; // This is Book 3: History of Art
        double checkBox4Price = 10.99; // This is Book 4: The Fantasy Realm
        double checkBox5Price = 8.99;  // This is Book 5: Cooking 101

                                           // Prices of the types of books
        double radioButton1Price = 5.00;   // Hardcover
        double radioButton2Price = 0.00;   // Paperback
        double radioButton3Price = -2.00;  // E-book, -2.00 since it's $2 less than the Paperback price
        double radioButton4Price = 7.00;   // Audiobook

                                       // Prices of the accessories
        double checkBox6Price = 1.50;  // Bookmarks
        double checkBox7Price = 7.99;  // Reading Light
        double checkBox8Price = 3.99;  // Personalized Bookplates
        double checkBox9Price = 10.00; // Custom Book Cover (Hardcover)
        double checkBox10Price = 5.99; // Notebook

                                          // Prices of the types of shipping
        double radioButton5Price = 5.00;  // Standard Shipping
        double radioButton6Price = 15.00; // Express Shipping
        double radioButton7Price = 0.00;  // Free Shipping (applied if total > $50)

        // This is the discount code
        string promoCode = "BOOK10";
        double Discount = 0.10; 
        public Form1()
        {
            InitializeComponent();

        }
        private double SubmitOrder()
        {
            // totals up the buttons selected once submit order button is selected.
            double total = 0;

            if (checkBox1.Checked) total += checkBox1Price;
            if (checkBox2.Checked) total += checkBox2Price;
            if (checkBox3.Checked) total += checkBox3Price;
            if (checkBox4.Checked) total += checkBox4Price;
            if (checkBox5.Checked) total += checkBox5Price;

            if (radioButton1.Checked) total += radioButton1Price;
            if (radioButton2.Checked) total += radioButton2Price;
            if (radioButton3.Checked) total += radioButton3Price;
            if (radioButton4.Checked) total += radioButton4Price;

            if (checkBox6.Checked) total += checkBox6Price;
            if (checkBox7.Checked) total += checkBox7Price;
            if (checkBox8.Checked) total += checkBox8Price;
            if (checkBox9.Checked) total += checkBox9Price;
            if (checkBox10.Checked) total += checkBox10Price;

            if (radioButton5.Checked) total += radioButton5Price;
            if (radioButton6.Checked) total += radioButton6Price;
            if (radioButton7.Checked && total > 50) total += radioButton7Price;
            if (textBox1.Text == promoCode)
            {
                total *= (1 - Discount); // will take the total minus the discount
            }

            return total; 
        }

            
        

        private void ResetForm() // this will reset everything to the default
        {
            checkBox1.Checked = true; // default book selected
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;

            radioButton1.Checked = false;
            radioButton2.Checked = true; // default book style selected, which is the paperback
            radioButton3.Checked = false;
            radioButton4.Checked = false;

            checkBox6.Checked = false;
            checkBox7.Checked = false;
            checkBox8.Checked = false;
            checkBox9.Checked = false;
            checkBox10.Checked = false;

            radioButton5.Checked = true; // default shipping is selected, which is standard
            radioButton6.Checked = false;
            radioButton7.Checked = false;

            textBox1.Text = "";
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Our bookstore offers a wide variety of books across genres. We offer free returns on all purchases within 30 days. Our books are carefully selected to provide the best reading experience");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            double total = SubmitOrder();
            DialogResult result = MessageBox.Show(
       "Your total is $"  + total.ToString() + ". Is this your final order?", "Order Summary",
       MessageBoxButtons.YesNo
   );

            if (result == DialogResult.Yes)
            {
                
                MessageBox.Show("Thank you for your order!");
                ResetForm();
            }
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}