using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GEP_DE607.Componente
{
    /// <summary>
    /// Interaction logic for Alerta.xaml
    /// </summary>
    public partial class Alerta : Window
    {
        public Alerta()
        {
            InitializeComponent();
        }

        public Alerta(string msg)
        {
            InitializeComponent();
            preencherMensagem(msg);
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            //this.DialogResult = true;
            this.Close();
        }

        public void preencherMensagem(string mensagem)
        {
            int tam = mensagem.Length;
            int indice = (tam / 46);
            string label = "";
            int i = 0;
            while (i < indice)
            {
                label += mensagem.Substring(i * 46, 46) + '\n';
                i++;
            }
            label += mensagem.Substring(i * 46, (mensagem.Length - (i * 46)));
            this.lblMensagem.Content = label;
        }
    }
}
