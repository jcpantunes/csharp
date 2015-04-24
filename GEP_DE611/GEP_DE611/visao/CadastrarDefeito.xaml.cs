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
using System.Windows.Forms;
using GEP_DE611.dominio;
using GEP_DE611.dominio.util;
using GEP_DE611.persistencia;
using GEP_DE611.componente;

namespace GEP_DE611.visao
{
    /// <summary>
    /// Interaction logic for CadastrarDefeito.xaml
    /// </summary>
    public partial class CadastrarDefeito : Window
    {
        private BaseWindow baseWindow;

        public CadastrarDefeito()
        {
            InitializeComponent();

            txtData.Text = DateTime.Now.ToShortDateString();

            baseWindow = new BaseWindow();

            preencherCombos();

        }

        private void preencherLista(Dictionary<string, string> param)
        {
            DefeitoDAO tDAO = new DefeitoDAO();
            tblDefeito.ItemsSource = tDAO.recuperar(param);
        }

        private void preencherCombos()
        {
            baseWindow.preencherComboProjeto(cmbProjeto, false);
            
            baseWindow.preencherComboProjeto(cmbFiltroProjeto, true);

            baseWindow.preencherComboStatus(cmbFiltroStatus, StatusUtil.recuperarListaStatusDefeito(), true);
        }

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = Util.retornarOpenDialogBox();
            if (openFileDialog.ShowDialog().ToString().Equals("OK"))
            {
                txtUpload.Text = openFileDialog.FileName;
            }
        }

        private void btnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            if (cmbProjeto.SelectedIndex < 0 ||
                txtData.Text.Length == 0 || txtUpload.Text.Length == 0)
            {
                Alerta alerta = new Alerta("Favor preencher todos os campos");
                alerta.Show();
            }
            else
            {
                realizarUpload(txtUpload.Text);
            }
        }

        public void realizarUpload(String file)
        {
            string[] lines = System.IO.File.ReadAllLines(file);

            if (lines.Length > 1 && Util.validarArquivoDefeito(lines[0]) == true)
            {
                List<Defeito> listaIncluir = new List<Defeito>();
                List<Defeito> listaAtualizar = new List<Defeito>();
                List<Funcionario> listaFuncionario = new List<Funcionario>();
                List<Projeto> listaProjeto = new List<Projeto>();

                for (int i = 1; i < lines.Length; i++)
                {
                    string[] linha = lines[i].Replace("\"", "").Split('\t');
                    Defeito item = new Defeito();
                    item.Tipo = linha[0];
                    item.Id = Convert.ToInt32(linha[1]);
                    item.Titulo = linha[2];
                    item.Responsavel = baseWindow.recuperarFuncionarioInCache(listaFuncionario, Convert.ToString(linha[3]));
                    item.Status = linha[4];
                    item.PlanejadoPara = linha[5];
                    item.DataColeta = Convert.ToDateTime(txtData.Text);
                    item.EncontradoProjeto = Convert.ToString(linha[6]);
                    item.TipoRelato = Convert.ToString(linha[7]);
                    item.Resolucao = Convert.ToString(linha[8]);
                    item.Pai = linha[9].Replace("#", "");
                    
                    int codigo = Convert.ToInt32(((ComboBoxItem)cmbProjeto.SelectedItem).Tag);
                    string nome = Convert.ToString(((ComboBoxItem)cmbProjeto.SelectedItem).Content);
                    Projeto p = baseWindow.recuperarProjetoInCache(listaProjeto, Convert.ToInt32(linha[10]), codigo, nome);
                    item.Projeto = p.Codigo;

                    if (!existeDefeito(item))
                    {
                        listaIncluir.Add(item);
                    }
                    else
                    {
                        listaAtualizar.Add(item);
                    }
                }
                DefeitoDAO tDAO = new DefeitoDAO();
                if (listaIncluir.Count > 0)
                {
                    tDAO.incluir(listaIncluir);
                }
                if (listaAtualizar.Count > 0)
                {
                    tDAO.atualizarPorId(listaAtualizar);
                }

                Alerta alerta = new Alerta("Arquivo incluido com sucesso!");
                alerta.Show();

                preencherLista(new Dictionary<string, string>());
            }
            else
            {
                Alerta alerta = new Alerta("Arquivo invalido");
                alerta.Show();
            }
        }

        private bool existeDefeito(Defeito item)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add(ItemTrabalho.ID, Convert.ToString(item.Id));

            DefeitoDAO iDAO = new DefeitoDAO();
            List<Defeito> listaItem = iDAO.recuperar(param);
            if (listaItem.Count > 0)
            {
                return true;
            }
            return false;
        }

        private void cmbFiltroProjeto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)cmbFiltroProjeto.SelectedItem;
            if (item != null)
            {
                int codigoProjeto = Convert.ToInt32(item.Tag);
                baseWindow.preencherComboSprint(codigoProjeto, cmbFiltroSprint, true);
            }
        }

        private void iniciarCamposFiltro()
        {
            txtFiltroTitulo.Text = "";
            txtFiltroId.Text = "";
            txtFiltroDtInicio.Text = "";
            txtFiltroDtFinal.Text = "";

            cmbFiltroProjeto.SelectedIndex = 0;
            cmbFiltroSprint.SelectedIndex = 0;
            cmbFiltroStatus.SelectedIndex = 0;
        }

        private void btnFiltroLimpar_Click(object sender, RoutedEventArgs e)
        {
            iniciarCamposFiltro();
            preencherLista(new Dictionary<string, string>());
        }

        private void btnPesquisar_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();

            if (txtFiltroTitulo.Text.Length > 0)
            {
                param.Add(ItemTrabalho.TITULO, txtFiltroTitulo.Text);
            }
            if (txtFiltroId.Text.Length > 0)
            {
                param.Add(ItemTrabalho.ID, txtFiltroId.Text);
            }
            if (txtFiltroDtInicio.Text.Length > 0)
            {
                param.Add(ItemTrabalho.DTINICIO, txtFiltroDtInicio.Text);
            }
            if (txtFiltroDtFinal.Text.Length > 0)
            {
                param.Add(ItemTrabalho.DTFINAL, txtFiltroDtFinal.Text);
            }
            if (cmbFiltroProjeto.SelectedIndex > 0)
            {
                int codigo = Convert.ToInt32(((ComboBoxItem)cmbFiltroProjeto.SelectedItem).Tag);
                param.Add(ItemTrabalho.PROJETO, Convert.ToString(codigo));
            }
            if (cmbFiltroSprint.SelectedIndex > 0)
            {
                string sprint = Convert.ToString(((ComboBoxItem)cmbFiltroSprint.SelectedItem).Content);
                param.Add(ItemTrabalho.PLANEJADO_PARA, sprint);
            }
            if (cmbFiltroStatus.SelectedIndex > 0)
            {
                string sprint = Convert.ToString(((ComboBoxItem)cmbFiltroStatus.SelectedItem).Content);
                param.Add(ItemTrabalho.STATUS, sprint);
            }
            preencherLista(param);
        }

        private void tblDefeito_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            // ... Get the TextBox that was edited.
            var element = e.EditingElement as System.Windows.Controls.TextBox;
            var text = element.Text;

            Defeito item = (Defeito)e.Row.Item;

            var coluna = e.Column.DisplayIndex;

            // ... See if the text edit should be canceled.
            //     We cancel if the user typed a question mark.
            if (text.Length == 0)
            {
                e.Cancel = true;
            }
            else
            {
                DefeitoDAO iDAO = new DefeitoDAO();
                if (coluna < 7)
                {
                    Alerta alerta = new Alerta("Somente as colunas Resolucao e Pai podem ser alteradas");
                    alerta.Show();
                    e.Cancel = true;
                }
                else
                {
                    if (coluna == 9)
                    {
                        item.Resolucao = Convert.ToString(text);
                    }
                    else if (coluna == 10)
                    {
                        item.Pai = Convert.ToString(text);
                    }
                    iDAO.atualizar(item.encapsularLista());
                }
            }
        }
    }
}
