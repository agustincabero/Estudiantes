using Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Estudiantes
{
    public partial class Form1 : Form
    {
        private LEstudiantes estudiante;
        public Form1()
        {
            InitializeComponent();

            var listTextBox = new List<TextBox>();
            listTextBox.Add(textBoxNombre);
            listTextBox.Add(textBoxApellido);
            listTextBox.Add(textBoxId);
            listTextBox.Add(textBoxMail);
            var listLabel = new List<Label>();
            listLabel.Add(labelNombre);
            listLabel.Add(labelApellido);
            listLabel.Add(labelId);
            listLabel.Add(labelMail);
            listLabel.Add(labelPag);
            Object[] objetos = {
                pictureBox,
                Properties.Resources.baseline_add_a_photo_black_48dp,
                dataGridView1,
                numericUpDown1
            };

            estudiante = new LEstudiantes(listTextBox, listLabel, objetos);
        }

        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            estudiante.uploadImage.CargarImagen(pictureBox);
        }

        private void TextBoxNombre_TextChanged(object sender, EventArgs e)
        {
            if (textBoxNombre.Text.Equals(""))
            {
                labelNombre.ForeColor = Color.MediumPurple;
            }
            else
            {
                labelNombre.ForeColor = Color.Green;
                labelNombre.Text = "Nombre";
            }
        }

        private void TextBoxNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            estudiante.textBoxEvent.textKeyPress(e);
        }

        private void TextBoxApellido_TextChanged(object sender, EventArgs e)
        {
            if (textBoxApellido.Text.Equals(""))
            {
                labelApellido.ForeColor = Color.MediumPurple;
            }
            else
            {
                labelApellido.ForeColor = Color.Green;
                labelApellido.Text = "Apellido";
            }
        }

        private void TextBoxApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            estudiante.textBoxEvent.textKeyPress(e);
        }

        private void TextBoxId_TextChanged(object sender, EventArgs e)
        {
            if (textBoxId.Text.Equals(""))
            {
                labelId.ForeColor = Color.MediumPurple;
            }
            else
            {
                labelId.ForeColor = Color.Green;
                labelId.Text = "Id";
            }

        }

        private void TextBoxId_KeyPress(object sender, KeyPressEventArgs e)
        {
            estudiante.textBoxEvent.numberKeyPress(e);
        }

        private void TextBoxMail_TextChanged(object sender, EventArgs e)
        {
            if (textBoxMail.Text.Equals(""))
            {
                labelMail.ForeColor = Color.MediumPurple;
            }
            else
            {
                labelMail.ForeColor = Color.Green;
                labelMail.Text = "eMail";
            }
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            estudiante.Registrar();
        }

        private void TextBoxBuscar_TextChanged(object sender, EventArgs e)
        {
            estudiante.SearchEstudiante(textBoxBuscar.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            estudiante.Restablecer();
        }

        private void btnFirstPage_Click(object sender, EventArgs e)
        {
            estudiante.Paginador("First");
        }

        private void btnPreviousPage_Click(object sender, EventArgs e)
        {
            estudiante.Paginador("Previous");
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            estudiante.Paginador("Next");
        }

        private void btnLastPage_Click(object sender, EventArgs e)
        {
            estudiante.Paginador("Last");
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            //leer el valor del sleector y pasarlo para elegir los registros por pagina.
            estudiante.Registro_Paginas();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                estudiante.GetEstudiante();
            }
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                estudiante.GetEstudiante();
            }
        }
    }
}
