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
    /// Interaction logic for CadastrarItemBacklog.xaml
    /// </summary>
    public partial class CadastrarItemBacklog : Window
    {
        public CadastrarItemBacklog()
        {
            InitializeComponent();

            txtData.Text = DateTime.Now.ToShortDateString();

            preencherLista(new Dictionary<string, string>());

            preencherCombos();
        }

        private void preencherLista(Dictionary<string, string> param)
        {
            try
            {
                ItemBacklogDAO tDAO = new ItemBacklogDAO();
                tblItemBacklog.ItemsSource = tDAO.recuperar(param);
            }
            catch (Exception ex)
            {
                Alerta alerta = new Alerta("Problema ao tentar acessar o banco de dados: \n" + ex.Message);
                alerta.Show();
                this.Close();
            }
        }

        private void preencherCombos()
        {
            preencherComboProjeto();
            preencherComboStatus();
        }

        private void preencherComboProjeto()
        {
            ProjetoDAO pDAO = new ProjetoDAO();
            List<Projeto> lista = pDAO.recuperar();
            if (lista.Count > 0)
            {
                ComboBoxItem itemTodos = new ComboBoxItem();
                itemTodos.Content = "Todos";
                itemTodos.Tag = 0;
                cmbFiltroProjeto.Items.Add(itemTodos);
                cmbFiltroProjeto.SelectedIndex = 0;

                foreach (Projeto p in lista)
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = p.Nome;
                    item.Tag = p.Codigo;
                    cmbProjeto.Items.Add(item);

                    ComboBoxItem itemFiltro = new ComboBoxItem();
                    itemFiltro.Content = p.Nome;
                    itemFiltro.Tag = p.Codigo;
                    cmbFiltroProjeto.Items.Add(itemFiltro);
                }
                cmbProjeto.SelectedIndex = 0;
            }
        }

        private void preencherComboFiltroSprint(int codigoProjeto)
        {
            cmbFiltroSprint.Items.Clear();
            SprintDAO sDAO = new SprintDAO();
            List<Sprint> lista = sDAO.recuperar(Sprint.criarListaParametrosPesquisaPorProjeto(codigoProjeto));
            if (lista.Count > 0)
            {
                ComboBoxItem itemTodos = new ComboBoxItem();
                itemTodos.Content = "Todos";
                itemTodos.Tag = 0;
                cmbFiltroSprint.Items.Add(itemTodos);
                cmbFiltroSprint.SelectedIndex = 0;

                foreach (Sprint s in lista)
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = s.Nome;
                    item.Tag = s.Codigo;
                    cmbFiltroSprint.Items.Add(item);
                }
            }
        }

        private void preencherComboStatus()
        {
            ComboBoxItem itemTodos = new ComboBoxItem();
            itemTodos.Content = "Todos";
            itemTodos.Tag = 0;
            cmbFiltroStatus.Items.Add(itemTodos);
            cmbFiltroStatus.SelectedIndex = 0;

            List<string> lista = StatusUtil.recuperarListaStatus();
            if (lista.Count > 0)
            {
                foreach (string str in lista)
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = str;
                    cmbFiltroStatus.Items.Add(item);
                }
            }
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

            if (lines.Length > 1 && Util.validarArquivoItemBacklog(lines[0]) == true)
            {
                List<ItemBacklog> listaIncluir = new List<ItemBacklog>();
                List<ItemBacklog> listaAtualizar = new List<ItemBacklog>();

                List<Projeto> listaProjeto = new List<Projeto>();

                for (int i = 1; i < lines.Length; i++)
                {
                    string[] linha = lines[i].Replace("\"", "").Split('\t');
                    ItemBacklog item = new ItemBacklog();
                    item.Tipo = linha[0];
                    item.Id = Convert.ToInt32(linha[1]);
                    item.Titulo = linha[2];
                    item.Status = linha[3];
                    item.PlanejadoPara = linha[4];
                    item.DataColeta = Convert.ToDateTime(txtData.Text);
                    item.ValorNegocio = Convert.ToInt32(linha[5].Replace("pts", "").Replace("pt", ""));
                    item.Tamanho = Convert.ToInt32(linha[6]);
                    item.Complexidade = Convert.ToInt32(linha[8]);
                    item.Pf = Convert.ToDecimal(linha[9]);

                    Projeto p = recuperarProjeto(listaProjeto, Convert.ToInt32(linha[7]));
                    if (p != null)
                    {
                        item.Projeto = p.Codigo;
                    }
                    else
                    {
                        int codigo = Convert.ToInt32(((ComboBoxItem)cmbProjeto.SelectedItem).Tag);
                        item.Projeto = codigo;
                    }

                    if (!existeItemBacklog(item))
                    {
                        listaIncluir.Add(item);
                    }
                    else
                    {
                        listaAtualizar.Add(item);
                    }
                }
                ItemBacklogDAO tDAO = new ItemBacklogDAO();
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

        private Projeto recuperarProjeto(List<Projeto> listaProjeto, int idProjeto)
        {
            foreach (Projeto p in listaProjeto)
            {
                if (p.Id.Equals(idProjeto))
                {
                    return p;
                }
            }
            ProjetoDAO pDAO = new ProjetoDAO();
            List<Projeto> lista = pDAO.recuperar(Projeto.criarListaParametrosId(idProjeto));
            if (lista.Count > 0)
            {
                listaProjeto.Add(lista[0]);
                return lista[0];
            }
            return null;
        }

        private bool existeItemBacklog(ItemBacklog item)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add(ItemTrabalho.ID, Convert.ToString(item.Id));

            ItemBacklogDAO iDAO = new ItemBacklogDAO();
            List<ItemBacklog> listaItem = iDAO.recuperar(param);
            if (listaItem.Count > 0)
            {
                return true;
            }
            return false;
        }

        private void cmbFiltroProjeto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)cmbFiltroProjeto.SelectedItem;
            int codigoProjeto = Convert.ToInt32(item.Tag);
            preencherComboFiltroSprint(codigoProjeto);
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
                param.Add(ItemBacklog.PROJETO, Convert.ToString(codigo));
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

        private void tblItemBacklog_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            // ... Get the TextBox that was edited.
            var element = e.EditingElement as System.Windows.Controls.TextBox;
            var text = element.Text;

            ItemBacklog item = (ItemBacklog)e.Row.Item;

            var coluna = e.Column.DisplayIndex;

            // ... See if the text edit should be canceled.
            //     We cancel if the user typed a question mark.
            if (text.Length == 0)
            {
                e.Cancel = true;
            }
            else
            {
                ItemBacklogDAO iDAO = new ItemBacklogDAO();
                if (coluna < 7)
                {
                    Alerta alerta = new Alerta("Somente as colunas Valor Negocio, Tamanho, Complexidade e PF podem ser alteradas");
                    alerta.Show();
                    e.Cancel = true;
                }
                else
                {
                    if (coluna == 7)
                    {
                        item.ValorNegocio = Convert.ToInt32(text);
                    }
                    else if (coluna == 8)
                    {
                        item.Tamanho = Convert.ToInt32(text);
                    }
                    else if (coluna == 9)
                    {
                        item.Complexidade = Convert.ToInt32(text);
                    }
                    else if (coluna == 10)
                    {
                        item.Pf = Convert.ToDecimal(text);
                    }
                    iDAO.atualizar(item.encapsularLista());
                }
            }
        }
    }
}
