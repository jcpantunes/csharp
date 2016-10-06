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
using System.Data;
using GEP_DE607.Componente;
using GEP_DE607.Dominio;
using GEP_DE607.Negocio;
using GEP_DE607.Persistencia;

namespace GEP_DE607
{
    /// <summary>
    /// Interaction logic for ConsultarDados.xaml
    /// </summary>
    public partial class ConsultarDados : Window
    {

        BaseWindow baseWindow = new BaseWindow();

        public ConsultarDados()
        {
            InitializeComponent();
        }

        public void preencherTabela(string titulo, DataTable tabela, int[] listaTamColunas, object[] listaColunas, List<object[]> listaLinhas)
        {
            this.lblTitulo.Content = titulo;

            foreach (string str in listaColunas)
            {
                tabela.Columns.Add(Convert.ToString(str));
            }
            foreach (object[] linha in listaLinhas)
            {
                tabela.Rows.Add(linha);
            }
            baseWindow.preencherGrid(tblDados, tabela, listaTamColunas);
        }
    }
}
