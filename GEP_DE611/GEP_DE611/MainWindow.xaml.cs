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
using GEP_DE611.chart;
using GEP_DE611.visao;
using GEP_DE611.dominio;
using GEP_DE611.dominio.util;
using GEP_DE611.persistencia;
using GEP_DE611.componente;

namespace GEP_DE611
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                FuncionarioDAO fDAO = new FuncionarioDAO();
                fDAO.recuperar(1);
            }
            catch (Exception ex)
            {
                Alerta alerta = new Alerta("Problema ao tentar acessar o banco de dados: \n" + ex.Message);
                alerta.Show();
            }

            // testarFuncionario();
            // testarTarefa();
            // testarProjeto();
            // testarSprint();
            // testarHora();

        }

        private void btnBurndown_Click(object sender, RoutedEventArgs e)
        {
            VisualizarBurndown burndown = new VisualizarBurndown();
            burndown.Show();
        }

        private void btnUploadArquivoItemBacklog_Click(object sender, RoutedEventArgs e)
        {
            CadastrarItemBacklog tela = new CadastrarItemBacklog();
            tela.Show();
        }

        private void btnUploadArquivoTarefa_Click(object sender, RoutedEventArgs e)
        {
            CadastrarTarefa upload = new CadastrarTarefa();
            upload.Show();
        }

        private void btnProjeto_Click(object sender, RoutedEventArgs e)
        {
            CadastrarProjeto tela = new CadastrarProjeto();
            tela.Show();
        }

        private void btnSprint_Click(object sender, RoutedEventArgs e)
        {
            CadastrarSprint tela = new CadastrarSprint();
            tela.Show();
        }


        private void btnFuncionario_Click(object sender, RoutedEventArgs e)
        {
            CadastrarFuncionario tela = new CadastrarFuncionario();
            tela.Show();
        }

        private void btnIndicador_Click(object sender, RoutedEventArgs e)
        {
            VisualizarIndicador tela = new VisualizarIndicador();
            tela.Show();
        }

        private void btnUploadArquivoDefeito_Click(object sender, RoutedEventArgs e)
        {
            CadastrarDefeito tela = new CadastrarDefeito();
            tela.Show();
        }
    }
}
