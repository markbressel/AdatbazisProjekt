using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            string username = usernameBox.Text;
            string password = PasswordBox.Text;

            string connectionString = "User Id=C##Info6;Password=Sapi12345;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=217.73.170.84)(PORT=44678))(CONNECT_DATA=(SID=oRCL)))";

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT FELHASZNALONEV, JELSZO, SZEREP_ID FROM FELHASZNALOK WHERE FELHASZNALONEV = :username AND JELSZO = :password";
                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add(new OracleParameter("username", username));
                        command.Parameters.Add(new OracleParameter("password", password));

                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int szerepId = Convert.ToInt32(reader["SZEREP_ID"]);
                                MessageBox.Show("Sikeres bejelentkezes");

                                Form2 mainForm = new Form2();

                                switch (szerepId)
                                {
                                    case 1:
                                        // Admin user belépés
                                        MessageBox.Show("Admin felhasznalo");
                                        break;
                                    case 2:
                                        // Csak olvasó user belépés
                                        MessageBox.Show("Csak olvaso felhasznalo");
                                        mainForm.AdjustForReadOnlyUser();
                                        break;
                                    case 3:
                                        MessageBox.Show("Korlatozott felhasznalo");
                                        // Korlátozott user belépés 
                                        mainForm.AdjustForLimitedUser();
                                        break;
                                    default:
                                        MessageBox.Show("Ismeretlen szerep azonosító.");
                                        return;
                                }

                                mainForm.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Hibas felhasznalonev vagy jelszo");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void PasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                loginButton_Click(sender, e);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
    }
}
