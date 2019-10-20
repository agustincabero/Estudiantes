using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logica.Library
{
    public class TextBoxEvent
    {
        public void textKeyPress(KeyPressEventArgs e)
        {
            //Verifica que se ingrese datos de tipo string
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = false;
            }
            //Impide generar salto de linea cuando se apreta ENTER
            else if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                e.Handled = true;
            }
            //Permite utilizar la tecla Backspace
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            //Permite utilizar espacio
            else if (char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        public void numberKeyPress(KeyPressEventArgs e)
        {
            //Verifica que se ingrese datos de tipo numerico
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            //Impide generar salto de linea cuando se apreta ENTER
            else if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                e.Handled = true;
            }
            //Impide utilizar datos de tipo string
            else if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
            //Permite utilizar la tecla Backspace
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            //Permite utilizar espacio
            else if (char.IsSeparator(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = true;
            }
        }

        public bool emailValidation(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }
        
    }
}
