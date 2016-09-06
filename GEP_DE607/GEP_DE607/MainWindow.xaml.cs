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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GEP_DE607
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnCarga_Click(object sender, RoutedEventArgs e)
        {
            RealizarCarga tela = new RealizarCarga();
            tela.Show();
        }

        private void btnProjeto_Click(object sender, RoutedEventArgs e)
        {
            CadastrarProjeto tela = new CadastrarProjeto();
            tela.Show();
        }

        private void btnSolicitacao_Click(object sender, RoutedEventArgs e)
        {
            CadastrarSolicitacao tela = new CadastrarSolicitacao();
            tela.Show();
        }

        private void btnIndicadores_Click(object sender, RoutedEventArgs e)
        {
            VisualizarIndicador tela = new VisualizarIndicador();
            tela.Show();
        }
    }
}
