using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

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

                            string query5 = "SELECT dbLekeredezes(:autoTipus) FROM DUAL";
                            int db = ExecuteScalarQuery(query5, connection, new OracleParameter("db", autoTipus));

                            if (ev == -1 || teljesitmeny == -1 || ar == -1 || uzemanyag == "-1" || db == -1)
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
                            dataTable.Columns.Add("Db", typeof(int));

                            DataRow row = dataTable.NewRow();
                            row["AutoTipus"] = autoTipus;
                            row["Ev"] = ev;
                            row["Teljesitmeny"] = teljesitmeny;
                            row["Ar"] = ar;
                            row["Uzemanyag"] = uzemanyag;
                            row["Db"] = db;
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

                            string query3 = "SELECT DbAlkatresz(:alkatreszTipus) FROM DUAL";
                            int db = ExecuteScalarQuery(query3, connection, new OracleParameter("db", alkatreszTipus));

                            if (ar == -1 || marka == "-1" || db == -1)
                            {
                                MessageBox.Show("No such car part type exists.");
                                return;
                            }

                            DataTable dataTable = new DataTable();
                            dataTable.Columns.Add("AlkatreszTipus", typeof(string));
                            dataTable.Columns.Add("Ar", typeof(int));
                            dataTable.Columns.Add("Marka", typeof(string));
                            dataTable.Columns.Add("Db", typeof(int));

                            DataRow row = dataTable.NewRow();
                            row["AlkatreszTipus"] = alkatreszTipus;
                            row["Ar"] = ar;
                            row["Marka"] = marka;
                            row["Db"] = db;
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
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearTextBoxes();

            string selectedValue = comboBox1.SelectedItem.ToString();

            switch (selectedValue)
            {
                case "Autok":
                    AddTextBox(1);
                    break;
                case "Alkatreszek":
                    AddTextBox(1);
                    break;
                case "Alkalmazottak":
                    AddTextBox(2);
                    break;
                case "Ugyfelek":
                    AddTextBox(2);
                    break;
                default:
                    break;
            }
        }

        private void AddTextBox(int count)
        {
            const int textBoxWidth = 150;
            const int textBoxHeight = 25;
            const int spacing = 10;
            const int margin = 30;

            int currentX = 200;

            for (int i = 0; i < count; i++)
            {
                TextBox textBox = new TextBox
                {
                    Name = "textBox" + (i + 1),
                    Location = new System.Drawing.Point(currentX, margin),
                    Size = new System.Drawing.Size(textBoxWidth, textBoxHeight)
                };
                textBox.KeyDown += new KeyEventHandler(OnKeyDownHandler); // Attach the KeyDown event handler
                this.Controls.Add(textBox);
                dynamicTextBoxes.Add(textBox);

                currentX += textBoxWidth + spacing;
            }
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Handle Enter key press
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
    }
}
