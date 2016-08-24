using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GEP_DE607.Componente
{
    public class MyFilePopup
    {
        
        public static OpenFileDialog criarMyFilePopup()
        {
            OpenFileDialog popup = new OpenFileDialog();

            popup.InitialDirectory = @"C:\";
            popup.Title = "Browse Text Files";

            popup.CheckFileExists = true;
            popup.CheckPathExists = true;

            popup.DefaultExt = "txt";
            popup.Filter = "Text files (*.csv)|*.csv|All files (*.*)|*.*";
            popup.FilterIndex = 2;
            popup.RestoreDirectory = true;

            popup.ReadOnlyChecked = true;
            popup.ShowReadOnly = true;

            return popup;
        }

    }
}
