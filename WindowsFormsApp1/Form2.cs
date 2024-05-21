using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void selectbutton_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearTextBoxes();

            string selectedValue = comboBox1.SelectedItem.ToString();

            switch(selectedValue)
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
                TextBox textBox = new TextBox();
                textBox.Name = "textBox" + (i + 1);
                textBox.Location = new System.Drawing.Point(currentX, margin);
                textBox.Size = new System.Drawing.Size(textBoxWidth, textBoxHeight);
                this.Controls.Add(textBox);

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
