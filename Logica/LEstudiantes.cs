using Data;
using LinqToDB;
using Logica.Library;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logica
{
    public class LEstudiantes : Librarys
    {
        private List<TextBox> listTextBox;
        private List<Label> listLabel;
        private PictureBox image;
        private Bitmap _imageBitmap;
        private DataGridView _dataGridView;
        private NumericUpDown _numericUpDown;
        private Paginador<Estudiante> _paginador;
        private string _accion = "insert";

        public LEstudiantes(List<TextBox> listTextBox, List<Label> listLabel, object[] objetos)
        {
            this.listTextBox = listTextBox;
            this.listLabel = listLabel;
            image = (PictureBox)objetos[0];
            _imageBitmap = (Bitmap)objetos[1];
            _dataGridView = (DataGridView)objetos[2];
            _numericUpDown = (NumericUpDown)objetos[3];
            Restablecer();
        }

        public void Registrar()
        {
            if (listTextBox[2].Text.Equals(""))
            {
                listLabel[2].Text = "Id requerido";
                listLabel[2].ForeColor = Color.Red;
                listTextBox[2].Focus();
            }
            else
            {
                if (listTextBox[0].Text.Equals(""))
                {
                    listLabel[0].Text = "Nombre requerido";
                    listLabel[0].ForeColor = Color.Red;
                    listTextBox[0].Focus();
                }
                else
                {
                    if (listTextBox[1].Text.Equals(""))
                    {
                        listLabel[1].Text = "Apellido requerido";
                        listLabel[1].ForeColor = Color.Red;
                        listTextBox[1].Focus();
                    }
                    else
                    {
                        if (listTextBox[3].Text.Equals(""))
                        {
                            listLabel[3].Text = "Mail requerido";
                            listLabel[3].ForeColor = Color.Red;
                            listTextBox[3].Focus();
                        }
                        else
                        {
                            if (!textBoxEvent.emailValidation(listTextBox[3].Text))
                            {
                                listLabel[3].Text = "Ingrese un mail válido";
                                listLabel[3].ForeColor = Color.Red;
                                listTextBox[3].Focus();
                            }
                            else
                            {
                                //Valida si el mail existe ya en la base de datos
                                var user = _Estudiante.Where(u => u.email.Equals(listTextBox[3].Text)).ToList();
                                if (user.Count.Equals(0))
                                {
                                    Save();
                                }
                                else
                                {
                                    listLabel[3].Text = "El email ya resgistrado";
                                    listLabel[3].ForeColor = Color.Red;
                                    listTextBox[3].Focus();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Save()
        {
            //Inicia una transaccion y no guardará los datos en la DB hasta que la misma no sea "commiteada"
            BeginTransactionAsync();
            try
            {
                var imageArray = uploadImage.ImageToByte(image.Image);
                _Estudiante.Value(e => e.nid, listTextBox[2].Text)
                    .Value(e => e.nombre, listTextBox[0].Text)
                    .Value(e => e.apellido, listTextBox[1].Text)
                    .Value(e => e.email, listTextBox[3].Text)
                    .Value(e => e.imagen, imageArray)
                    .Insert();
                //Cierra la transaccion y guarda los datos en la DB
                CommitTransaction();
                Restablecer();
            }
            catch (Exception)
            {
                //Revierte los cambios almacenados en memoria y deja la db como estaba antes de inicar la transaccion.
                RollbackTransaction();
                //throw;
            }            
        }

        private int _reg_por_pagina = 5, _num_pagina = 1;
        public void SearchEstudiante(string campo)
        {
            List<Estudiante> query = new List<Estudiante>();
            int inicio = (_num_pagina - 1) * _reg_por_pagina;
            if (campo.Equals(""))
            {
                query = _Estudiante.ToList();
            }
            else
            {
                query = _Estudiante.Where(c => c.nid.StartsWith(campo) || c.nombre.StartsWith(campo) || c.apellido.StartsWith(campo)).ToList();
            }

            if (0 < query.Count())
            {
                _dataGridView.DataSource = query.Select(c => new {
                    c.id,
                    c.nid,
                    c.nombre,
                    c.apellido,
                    c.email,
                    c.imagen
                }).Skip(inicio).Take(_reg_por_pagina).ToList();
                _dataGridView.Columns[0].Visible = false;
                _dataGridView.Columns[5].Visible = false;
                _dataGridView.Columns[1].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                _dataGridView.Columns[3].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            }
            else
            {
                _dataGridView.DataSource = query.Select(c => new {
                    c.nid,
                    c.nombre,
                    c.apellido,
                    c.email,
                }).ToList();
            }
        }

        private int _idEstudiante = 0;
        public void GetEstudiante()
        {
            _accion = "update";
            _idEstudiante = Convert.ToInt16(_dataGridView.CurrentRow.Cells[0].Value);
            listTextBox[0].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[2].Value);
            listTextBox[1].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[3].Value);
            listTextBox[2].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[1].Value);
            listTextBox[3].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[4].Value);
            try
            {
                byte[] arrayImage = (byte[]) _dataGridView.CurrentRow.Cells[5].Value;
                image.Image = uploadImage.byteArrayToImage(arrayImage);
            }
            catch (Exception)
            {
                image.Image = _imageBitmap;
            }
        }

        private List<Estudiante> listEstudiante;
        public void Paginador(string btn)
        {
            switch (btn)
            {
                case "First":
                    _num_pagina = _paginador.first();
                    break;
                case "Previous":
                    _num_pagina = _paginador.previous();
                    break;
                case "Next":
                    _num_pagina = _paginador.next();
                    break;
                case "Last":
                    _num_pagina = _paginador.last();
                    break;
            }
            SearchEstudiante("");
        }

        public void Registro_Paginas()
        {
            _num_pagina = 1;
            _reg_por_pagina = (int)_numericUpDown.Value;
            var list = _Estudiante.ToList();
            if (0 < list.Count)
            {
                _paginador = new Paginador<Estudiante>(listEstudiante, listLabel[4], _reg_por_pagina);
                SearchEstudiante("");
            }
        }

        public void Restablecer()
        {
            listLabel[0].Text = "Nombre";
            listLabel[1].Text = "Apellido";
            listLabel[2].Text = "Id";
            listLabel[3].Text = "eMail";
            image.Image = _imageBitmap;
            listLabel[0].ForeColor = Color.MediumPurple;
            listLabel[1].ForeColor = Color.MediumPurple;
            listLabel[2].ForeColor = Color.MediumPurple;
            listLabel[3].ForeColor = Color.MediumPurple;
            listTextBox[0].Text = "";
            listTextBox[1].Text = "";
            listTextBox[2].Text = "";
            listTextBox[3].Text = "";
            listEstudiante = _Estudiante.ToList();
            if (0 < listEstudiante.Count)
            {
                _paginador = new Paginador<Estudiante>(listEstudiante, listLabel[4], _reg_por_pagina);
            }
            SearchEstudiante("");
        }
    }
}
