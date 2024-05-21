using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            string username = usernameBox.Text;
            string password = PasswordBox.Text;

            string connectionString = "User Id=C##Info6;Password=Sapi12345;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=217.73.170.84)(PORT=44678))(CONNECT_DATA=(SID=oRCL)))";

            int r = 0;

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT * FROM FELHASZNALOK";
                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        using(OracleDataReader reader = command.ExecuteReader())
                        {
                            while(reader.Read())
                            {

                                if (reader["FELHASZNALONEV"].ToString() == username) {
                                    if (reader["JELSZO"].ToString() == password)
                                    {
                                        MessageBox.Show("Sikeres bejelentkezes");
                                        r = 1;
                                    }
                                }
                            }
                            reader.Close();
                            if(r == 0)MessageBox.Show("Hibas felhasznalonev vagy jelszo");
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void UsernameLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
