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
using GEP_DE607.Util;

namespace GEP_DE607
{
    /// <summary>
    /// Interaction logic for CadastrarProjeto.xaml
    /// </summary>
    public partial class CadastrarProjeto : Window
    {
        public CadastrarProjeto()
        {
            InitializeComponent();

            preparaTela();
        }

        private void preparaTela()
        {
            preencherCombo(cmbTipo, Constantes.recuperarDominioTipo());
            preencherCombo(cmbSistema, Constantes.recuperarDominioSistema());
            preencherCombo(cmbLinguagem, Constantes.recuperarDominioLinguagem());
            preencherCombo(cmbProcesso, Constantes.recuperarDominioProcesso());
            preencherCombo(cmbTipoProjeto, Constantes.recuperarDominioTipoProjeto());
            preencherCombo(cmbSituacao, Constantes.recuperarDominioSituacao());
            preencherCombo(cmbConclusividade, Constantes.recuperarDominioConclusividade());
        }

        private void preencherCombo(ComboBox combo, List<string> listaDominio)
        {
            foreach (string str in listaDominio)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = str;
                combo.Items.Add(item);
            }
            combo.SelectedIndex = 0;
        }

        private void btnLimpar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnFiltroLimpar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPesquisar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void tblProjeto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
