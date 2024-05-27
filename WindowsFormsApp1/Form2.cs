using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        private List<TextBox> dynamicTextBoxes = new List<TextBox>();

        public Form2()
        {
            InitializeComponent();
            InitializeDataGridView();
        }

        public void AdjustForReadOnlyUser()
        {
            // Gombok és mezők elrejtése
            updateButton.Visible = false;
            deleteButton.Visible = false;
        }

        private void InitializeDataGridView()
        {
            dataGridView1 = new DataGridView
            {
                Location = new System.Drawing.Point(50, 150),
                Size = new System.Drawing.Size(800, 200),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            this.Controls.Add(dataGridView1);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearTextBoxes();

            string selectedValue = comboBox1.SelectedItem.ToString();
            int posY = comboBox1.Location.Y;

            switch (selectedValue)
            {
                case "Autok":
                    AddTextBox(1, posY);
                    break;
                case "Alkatreszek":
                    AddTextBox(1, posY);
                    break;
                case "Alkalmazottak":
                    AddTextBox(2, posY);
                    break;
                case "Ugyfelek":
                    AddTextBox(2, posY);
                    break;
                default:
                    break;
            }
        }

        private void selectbutton_Click(object sender, EventArgs e)
        {
            string selectedValue = comboBox1.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedValue))
            {
                MessageBox.Show("Please select a table from the combo box.");
                return;
            }

            string connectionString = "User Id=C##Info6;Password=Sapi12345;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=217.73.170.84)(PORT=44678))(CONNECT_DATA=(SID=oRCL)))";

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    if (selectedValue == "Autok")
                    {
                        if (dynamicTextBoxes.Count > 0)
                        {
                            string autoTipus = dynamicTextBoxes[0].Text;

                            string query1 = "SELECT EvLekeredezes(:autoTipus) FROM DUAL";
                            int ev = ExecuteScalarQuery(query1, connection, new OracleParameter("autoTipus", autoTipus));

                            string query2 = "SELECT TeljesitmenyLekeredezes(:autoTipus) FROM DUAL";
                            int teljesitmeny = ExecuteScalarQuery(query2, connection, new OracleParameter("autoTipus", autoTipus));

                            string query3 = "SELECT ArLekeredezes(:autoTipus) FROM DUAL";
                            int ar = ExecuteScalarQuery(query3, connection, new OracleParameter("autoTipus", autoTipus));

                            string query4 = "SELECT UzemanyagLekeredezes(:autoTipus) FROM DUAL";
                            string uzemanyag = ExecuteScalarQueryString(query4, connection, new OracleParameter("autoTipus", autoTipus));

                            if (ev == -1 || teljesitmeny == -1 || ar == -1 || uzemanyag == "-1")
                            {
                                MessageBox.Show("No such car type exists.");
                                return;
                            }

                            DataTable dataTable = new DataTable();
                            dataTable.Columns.Add("AutoTipus", typeof(string));
                            dataTable.Columns.Add("Ev", typeof(int));
                            dataTable.Columns.Add("Teljesitmeny", typeof(int));
                            dataTable.Columns.Add("Ar", typeof(int));
                            dataTable.Columns.Add("Uzemanyag", typeof(string));

                            DataRow row = dataTable.NewRow();
                            row["AutoTipus"] = autoTipus;
                            row["Ev"] = ev;
                            row["Teljesitmeny"] = teljesitmeny;
                            row["Ar"] = ar;
                            row["Uzemanyag"] = uzemanyag;
                            dataTable.Rows.Add(row);

                            dataGridView1.DataSource = dataTable;
                        }
                        else
                        {
                            MessageBox.Show("Please enter the auto type.");
                            return;
                        }
                    }
                    else if (selectedValue == "Alkatreszek")
                    {
                        if (dynamicTextBoxes.Count > 0)
                        {
                            string alkatreszTipus = dynamicTextBoxes[0].Text;

                            string query1 = "SELECT ArAlkatresz(:alkatreszTipus) FROM DUAL";
                            int ar = ExecuteScalarQuery(query1, connection, new OracleParameter("alkatreszTipus", alkatreszTipus));

                            string query2 = "SELECT MarkaAlkatresz(:alkatreszTipus) FROM DUAL";
                            string marka = ExecuteScalarQueryString(query2, connection, new OracleParameter("alkatreszTipus", alkatreszTipus));

                            if (ar == -1 || marka == "-1")
                            {
                                MessageBox.Show("No such car part type exists.");
                                return;
                            }

                            DataTable dataTable = new DataTable();
                            dataTable.Columns.Add("AlkatreszTipus", typeof(string));
                            dataTable.Columns.Add("Ar", typeof(int));
                            dataTable.Columns.Add("Marka", typeof(string));

                            DataRow row = dataTable.NewRow();
                            row["AlkatreszTipus"] = alkatreszTipus;
                            row["Ar"] = ar;
                            row["Marka"] = marka;
                            dataTable.Rows.Add(row);

                            dataGridView1.DataSource = dataTable;
                        }
                        else
                        {
                            MessageBox.Show("Please enter the auto part type.");
                            return;
                        }
                    }
                    else if (selectedValue == "Alkalmazottak")
                    {
                        if (dynamicTextBoxes.Count > 1)
                        {
                            string vezeteknev = dynamicTextBoxes[0].Text;
                            string keresztnev = dynamicTextBoxes[1].Text;

                            string query1 = "SELECT alkalmazottlekerdezes(:vezeteknev, :keresztnev) FROM DUAL";
                            string alkalmazott = ExecuteScalarQueryString(query1, connection, new OracleParameter("vezeteknev", vezeteknev), new OracleParameter("keresztnev", keresztnev));

                            if (alkalmazott == "-1")
                            {
                                MessageBox.Show("No such employee exists.");
                                return;
                            }

                            DataTable dataTable = new DataTable();
                            dataTable.Columns.Add("Alkalmazott", typeof(string));

                            DataRow row = dataTable.NewRow();
                            row["Alkalmazott"] = alkalmazott;
                            dataTable.Rows.Add(row);

                            dataGridView1.DataSource = dataTable;
                        }
                        else
                        {
                            MessageBox.Show("Please enter both first and last names.");
                            return;
                        }
                    }
                    else if (selectedValue == "Ugyfelek")
                    {
                        if (dynamicTextBoxes.Count > 1)
                        {
                            string vezeteknev = dynamicTextBoxes[0].Text;
                            string keresztnev = dynamicTextBoxes[1].Text;

                            string query1 = "SELECT ugyfellekerdezes(:vezeteknev, :keresztnev) FROM DUAL";
                            string ugyfel = ExecuteScalarQueryString(query1, connection, new OracleParameter("vezeteknev", vezeteknev), new OracleParameter("keresztnev", keresztnev));

                            if (ugyfel == "-1")
                            {
                                MessageBox.Show("No such customer exists.");
                                return;
                            }

                            DataTable dataTable = new DataTable();
                            dataTable.Columns.Add("Ugyfel", typeof(string));

                            DataRow row = dataTable.NewRow();
                            row["Ugyfel"] = ugyfel;
                            dataTable.Rows.Add(row);

                            dataGridView1.DataSource = dataTable;
                        }
                        else
                        {
                            MessageBox.Show("Please enter both first and last names.");
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }


        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearTextBoxes();

            string selectedValue = comboBox2.SelectedItem.ToString();
            int posY = comboBox2.Location.Y;

            ClearTextBoxes();
            switch (selectedValue)
            {
                case "UjRendeles":
                    AddTextBox(7, posY);
                    dynamicTextBoxes[0].Text = "Rendeles neve";
                    dynamicTextBoxes[1].Text = "Darab Szam";
                    dynamicTextBoxes[2].Text = "Alkalmazott V.Nev";
                    dynamicTextBoxes[3].Text = "Alkalmazott K.Nev";
                    dynamicTextBoxes[4].Text = "Ugyfel V.Nev";
                    dynamicTextBoxes[5].Text = "Ugyfel K.Nev";
                    dynamicTextBoxes[6].Text = "Termek Nev";
                    break;
                case "UjAlkalmazott":
                    AddTextBox(5, posY);
                    dynamicTextBoxes[0].Text = "Alkalmazott V.Nev";
                    dynamicTextBoxes[1].Text = "Alkalmazott K.Nev";
                    dynamicTextBoxes[2].Text = "Fizetes";
                    dynamicTextBoxes[3].Text = "Telefonszam";
                    dynamicTextBoxes[4].Text = "Osztaly neve";
                    break;
                case "UjUgyfel":
                    AddTextBox(7, posY);
                    dynamicTextBoxes[0].Text = "Ugyfel V.Nev";
                    dynamicTextBoxes[1].Text = "Ugyfel K.Nev";
                    dynamicTextBoxes[2].Text = "Telefonszam";
                    dynamicTextBoxes[3].Text = "Email";
                    dynamicTextBoxes[4].Text = "Orszag";
                    dynamicTextBoxes[5].Text = "Telepules";
                    dynamicTextBoxes[6].Text = "Lakcim";
                    break;
                default:
                    break;
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem.ToString() == "UjRendeles")
            {
                string rendelesNev = dynamicTextBoxes[0].Text;
                long darabszam;
                if (!long.TryParse(dynamicTextBoxes[1].Text, out darabszam))
                {
                    MessageBox.Show("Please enter a valid quentity.");
                    return;
                }
                string alkalmazottVezeteknev = dynamicTextBoxes[2].Text;
                string alkalmazottKeresztnev = dynamicTextBoxes[3].Text;
                string ugyfelVezeteknev = dynamicTextBoxes[4].Text;
                string ugyfelKeresztnev = dynamicTextBoxes[5].Text;
                string termek = dynamicTextBoxes[6].Text;

                string connectionString = "User Id=C##Info6;Password=Sapi12345;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=217.73.170.84)(PORT=44678))(CONNECT_DATA=(SID=oRCL)))";

                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        string plsql = @"
                DECLARE
                    v_result VARCHAR2(4000);
                BEGIN
                    v_result := ujRendeles(:rendelesNev, :darabszam, :alkalmazottVezeteknev, :alkalmazottKeresztnev, :ugyfelVezeteknev, :ugyfelKeresztnev, :termek);
                    :result := v_result;
                END;";

                        using (OracleCommand command = new OracleCommand(plsql, connection))
                        {
                            command.Parameters.Add(new OracleParameter("rendelesNev", OracleDbType.Varchar2)).Value = rendelesNev;
                            command.Parameters.Add(new OracleParameter("darabszam", OracleDbType.Long)).Value = darabszam;
                            command.Parameters.Add(new OracleParameter("alkalmazottVezeteknev", OracleDbType.Varchar2)).Value = alkalmazottVezeteknev;
                            command.Parameters.Add(new OracleParameter("alkalmazottKeresztnev", OracleDbType.Varchar2)).Value = alkalmazottKeresztnev;
                            command.Parameters.Add(new OracleParameter("ugyfelVezeteknev", OracleDbType.Varchar2)).Value = ugyfelVezeteknev;
                            command.Parameters.Add(new OracleParameter("ugyfelKeresztnev", OracleDbType.Varchar2)).Value = ugyfelKeresztnev;
                            command.Parameters.Add(new OracleParameter("termek", OracleDbType.Varchar2)).Value = termek;

                            var resultParam = new OracleParameter("result", OracleDbType.Varchar2, 4000)
                            {
                                Direction = ParameterDirection.Output
                            };
                            command.Parameters.Add(resultParam);

                            command.ExecuteNonQuery();

                            string result = resultParam.Value.ToString();
                            MessageBox.Show(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message);
                    }
                }
            }
            else if (comboBox2.SelectedItem.ToString() == "UjAlkalmazott")
            {
                string alkalmazottVezeteknev = dynamicTextBoxes[0].Text;
                string alkalmazottKeresztnev = dynamicTextBoxes[1].Text;
                long fizetes;
                if (!long.TryParse(dynamicTextBoxes[2].Text, out fizetes))
                {
                    MessageBox.Show("Please enter a valid salary.");
                    return;
                }
                string telefonszam = dynamicTextBoxes[3].Text;
                string osztaly = dynamicTextBoxes[4].Text;

                AddNewEmployee(alkalmazottVezeteknev, alkalmazottKeresztnev, fizetes, telefonszam, osztaly);
            }
            else if (comboBox2.SelectedItem.ToString() == "UjUgyfel")
            {
                string vezeteknev = dynamicTextBoxes[0].Text;
                string keresztnev = dynamicTextBoxes[1].Text;
                string telefonszam = dynamicTextBoxes[2].Text;
                string email = dynamicTextBoxes[3].Text;
                string orszag = dynamicTextBoxes[4].Text;
                string telepules = dynamicTextBoxes[5].Text;
                string lakcim = dynamicTextBoxes[6].Text;

                AddNewCustomer(vezeteknev, keresztnev, telefonszam, email, orszag, telepules, lakcim);
            }
        }

        private void AddNewEmployee(string vezeteknev, string keresztnev, long fizetes, string telefonszam, string osztaly)
        {
            string connectionString = "User Id=C##Info6;Password=Sapi12345;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=217.73.170.84)(PORT=44678))(CONNECT_DATA=(SID=oRCL)))";

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string plsql = @"
            DECLARE
                v_result VARCHAR2(4000);
            BEGIN
                v_result := ujAlkalmazott(:vezeteknev, :keresztnev, :fizetes, :telefonszam, :osztaly);
                :result := v_result;
            END;";

                    using (OracleCommand command = new OracleCommand(plsql, connection))
                    {
                        command.Parameters.Add(new OracleParameter("vezeteknev", OracleDbType.Varchar2)).Value = vezeteknev;
                        command.Parameters.Add(new OracleParameter("keresztnev", OracleDbType.Varchar2)).Value = keresztnev;
                        command.Parameters.Add(new OracleParameter("fizetes", OracleDbType.Long)).Value = fizetes;
                        command.Parameters.Add(new OracleParameter("telefonszam", OracleDbType.Varchar2)).Value = telefonszam;
                        command.Parameters.Add(new OracleParameter("osztaly", OracleDbType.Varchar2)).Value = osztaly;

                        var resultParam = new OracleParameter("result", OracleDbType.Varchar2, 4000)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(resultParam);

                        command.ExecuteNonQuery();

                        string result = resultParam.Value.ToString();
                        MessageBox.Show(result);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void AddNewCustomer(string vezeteknev, string keresztnev, string telefonszam, string email, string orszag, string telepules, string lakcim)
        {
            string connectionString = "User Id=C##Info6;Password=Sapi12345;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=217.73.170.84)(PORT=44678))(CONNECT_DATA=(SID=oRCL)))";

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string plsql = @"
            DECLARE
                v_result VARCHAR2(4000);
            BEGIN
                v_result := ujUgyfel(:vezeteknev, :keresztnev, :telefonszam, :email, :orszag, :telepules, :lakcim);
                :result := v_result;
            END;";

                    using (OracleCommand command = new OracleCommand(plsql, connection))
                    {
                        command.Parameters.Add(new OracleParameter("vezeteknev", OracleDbType.Varchar2)).Value = vezeteknev;
                        command.Parameters.Add(new OracleParameter("keresztnev", OracleDbType.Varchar2)).Value = keresztnev;
                        command.Parameters.Add(new OracleParameter("telefonszam", OracleDbType.Varchar2)).Value = telefonszam;
                        command.Parameters.Add(new OracleParameter("email", OracleDbType.Varchar2)).Value = email;
                        command.Parameters.Add(new OracleParameter("orszag", OracleDbType.Varchar2)).Value = orszag;
                        command.Parameters.Add(new OracleParameter("telepules", OracleDbType.Varchar2)).Value = telepules;
                        command.Parameters.Add(new OracleParameter("lakcim", OracleDbType.Varchar2)).Value = lakcim;

                        var resultParam = new OracleParameter("result", OracleDbType.Varchar2, 4000)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(resultParam);

                        command.ExecuteNonQuery();

                        string result = resultParam.Value.ToString();
                        MessageBox.Show(result);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }


        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void deleteButton_Click(object sender, EventArgs e)
        {

        }

        private void AddTextBox(int count, int posY)
        {
            const int textBoxWidth = 100;
            const int textBoxHeight = 25;
            const int spacing = 10;
            const int margin = 30;

            int currentX = 200;
            dynamicTextBoxes.Clear();
            ClearTextBoxes();

            for (int i = 0; i < count; i++)
            {
                TextBox textBox = new TextBox
                {
                    Name = "textBox" + (i + 1),
                    Location = new System.Drawing.Point(currentX, posY),
                    Size = new System.Drawing.Size(textBoxWidth, textBoxHeight)
                };
                textBox.KeyDown += new KeyEventHandler(OnKeyDownHandler);
                this.Controls.Add(textBox);
                dynamicTextBoxes.Add(textBox);

                currentX += textBoxWidth + spacing;
            }
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                selectbutton_Click(sender, e);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void ClearTextBoxes()
        {
            foreach (Control control in this.Controls)
            {
                if (control is TextBox)
                {
                    this.Controls.Remove(control);
                    control.Dispose();

                }
            }
            dynamicTextBoxes.Clear();
        }

        private int ExecuteScalarQuery(string query, OracleConnection connection, params OracleParameter[] parameters)
        {
            using (OracleCommand command = new OracleCommand(query, connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                object result = command.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : -1;
            }
        }

        private string ExecuteScalarQueryString(string query, OracleConnection connection, params OracleParameter[] parameters)
        {
            using (OracleCommand command = new OracleCommand(query, connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                object result = command.ExecuteScalar();
                return result != null ? result.ToString() : "-1";
            }
        }

    }
}
