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
using GEP_DE611.visao;
using GEP_DE611.persistencia;

namespace GEP_DE611.componente
{
    /// <summary>
    /// Interaction logic for AlertaUpload.xaml
    /// </summary>
    public partial class AlertaUpload : Window
    {
        UploadArquivoTarefa uploadTela = null;

        string file = "";

        string planejadoPara = "";

        string data = "";

        public AlertaUpload(UploadArquivoTarefa uploadTela, String file, String planejadoPara, string data)
        {
            InitializeComponent();

            this.uploadTela = uploadTela;

            this.file = file;

            this.planejadoPara = planejadoPara;

            this.data = data;
        }

        private void btnSim_Click(object sender, RoutedEventArgs e)
        {
            TarefaDAO tDAO = new TarefaDAO();
            tDAO.excluirPorSprintPorData(this.planejadoPara, this.data);

            this.uploadTela.realizarUpload(this.file);

            this.Close();
        }

        private void btnNao_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
