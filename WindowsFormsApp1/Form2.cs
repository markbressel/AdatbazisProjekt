using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client; // For Oracle database

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

        private void InitializeDataGridView()
        {
            dataGridView1 = new DataGridView
            {
                Location = new System.Drawing.Point(50, 150),
                Size = new System.Drawing.Size(500, 200),
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

            string query = "";
            switch (selectedValue)
            {
                case "Autok":
                    if (dynamicTextBoxes.Count > 0)
                    {
                        string autoTipus = dynamicTextBoxes[0].Text;
                        query = $"SELECT TeljesitmenyLekeredezes('{autoTipus}') FROM DUAL";
                    }
                    else
                    {
                        MessageBox.Show("Please enter the auto type.");
                        return;
                    }
                    break;
                case "Alkatreszek":
                    query = "SELECT * FROM ALKATRESZEK";
                    break;
                case "Alkalmazottak":
                    query = "SELECT * FROM ALKALMAZOTTAK";
                    break;
                case "Ugyfelek":
                    query = "SELECT * FROM UGYFELEK";
                    break;
                default:
                    MessageBox.Show("Invalid selection.");
                    return;
            }

            string connectionString = "User Id=C##Info6;Password=Sapi12345;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=217.73.170.84)(PORT=44678))(CONNECT_DATA=(SID=oRCL)))";

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    OracleDataAdapter adapter = new OracleDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
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
                this.Controls.Add(textBox);
                dynamicTextBoxes.Add(textBox);

                currentX += textBoxWidth + spacing;
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
        }
    }
}
