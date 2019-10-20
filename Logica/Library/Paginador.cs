using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logica.Library
{
    public class Paginador<T>
    {
        private List<T> _dataList;
        private Label _label;
        private static int maxReg, _reg_por_pag, pageCount, numPag;
        public Paginador(List<T> dataList, Label label, int reg_por_pag)
        {
            _dataList = dataList;
            _label = label;
            _reg_por_pag = reg_por_pag;
            cargarDatos();
        }
        private void cargarDatos()
        {
            numPag = 1;
            maxReg = _dataList.Count;
            pageCount = (maxReg / _reg_por_pag);

            if ((maxReg % _reg_por_pag) > 0)
            {
                pageCount += 1;
            }
            _label.Text = $"Páginas 1/{pageCount}";
        }
        public int first()
        {
            numPag = 1;
            _label.Text = $"Páginas {numPag}/{pageCount}";
            return numPag;
        }
        public int previous()
        {
            if (numPag > 1)
            {
                numPag -= 1;
                _label.Text = $"Páginas {numPag}/{pageCount}";
            }
            return numPag;
        }
        public int next()
        {
            if (numPag == pageCount)
            {
                numPag -= 1;
            }
            if (numPag < pageCount)
            {
                numPag += 1;
                _label.Text = $"Páginas {numPag}/{pageCount}";
            }
            return numPag;
        }
        public int last()
        {
            numPag = pageCount;
            _label.Text = $"Páginas {numPag}/{pageCount}";
            return numPag;
        }
    }
}
