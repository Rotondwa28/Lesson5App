using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Lesson5App
{
    public partial class LoanForm : Form
    {
        public LoanForm()
        {
            InitializeComponent();
        }

        private void saveLoanButton_Click(object sender, EventArgs e)
        {
            string fullname = fullNameTextBox.Text;
            string idNumber = idNumberTextBox.Text;
            double amount = Convert.ToDouble(amountTextBox.Text);
            double interestRate = Convert.ToDouble(interestRateTextBox.Text);

            double calculatedLoan1 = 0;

            calculatedLoan1 = calculateLoan(amount, interestRate);
            message(calculatedLoan1.ToString());

            if (fullname.Trim() == "" && idNumber.Trim() == "" && amount == 0 && interestRate == 0)
            {
                MessageBox.Show("Empty Fields", "Error");
            }
            else
            {
                string query = "insert into loan(fullName,idNumber,amount,interestRate ) values(@fullName, @idNumber, @amount, @interestRate)";
                SQLiteConnection conn = new SQLiteConnection("Data Source=databaseFile.db; Version=3");
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                cmd.Parameters.AddWithValue("@fullName", fullname);
                cmd.Parameters.AddWithValue("@idNumber", idNumber);
                cmd.Parameters.AddWithValue("@amount", amount);
                cmd.Parameters.AddWithValue("@interestRate", interestRate);
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();

                if (i != 0)
                {
                    MessageBox.Show("Successfully saved");
                }

            }

            


        }

        private void message(string message)
        {
            MessageBox.Show(message);
        }

        private double calculateLoan(double amount, double interestRate)
        {
            double totalLoan = 0;

            totalLoan = amount + (amount * interestRate / 100);

            return totalLoan;
        }
    }
}
