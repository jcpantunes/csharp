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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using GEP_DE611.dominio;
using GEP_DE611.dominio.util;
using GEP_DE611.persistencia;
using GEP_DE611.componente;

namespace GEP_DE611.visao
{
    /// <summary>
    /// Interaction logic for UploadArquivoTarefa.xaml
    /// </summary>
    public partial class CadastrarTarefa : Window
    {
        public CadastrarTarefa()
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
                TarefaDAO tDAO = new TarefaDAO();
                tblTarefa.ItemsSource = tDAO.recuperar(param);
            }
            catch (Exception ex)
            {
                Alerta alerta = new Alerta("Problema ao tentar acessar o banco de dados: \n" + ex.Message);
                alerta.Show();

                this.Close();
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
            if (cmbProjeto.SelectedIndex < 0 || cmbSprint.SelectedIndex < 0 || 
                txtData.Text.Length == 0 || txtUpload.Text.Length == 0)
            {
                Alerta alerta = new Alerta("Favor preencher todos os campos");
                alerta.Show();
            }
            else
            {
                TarefaDAO tDAO = new TarefaDAO();
                List<DateTime> listaData = tDAO.recuperarListaDatasPorString(recuperarSprint());

                bool upload = true;
                foreach (DateTime data in listaData)
                {
                    if (Convert.ToDateTime(txtData.Text).Equals(data))
                    {
                        upload = false;
                        break;
                    }
                }

                if (upload == true)
                {
                    realizarUpload(txtUpload.Text);
                }
                else
                {
                    AlertaUpload alerta = new AlertaUpload(this, txtUpload.Text, recuperarSprint(), txtData.Text);
                    alerta.Show();
                }
            }
        }

        public void realizarUpload(String file)
        {
            string[] lines = System.IO.File.ReadAllLines(file);

            if (Util.validarArquivoTarefa(lines[0]) == true)
            {
                List<Tarefa> listaTarefaIncluir = new List<Tarefa>();

                List<Tarefa> listaTarefaAtualizar = new List<Tarefa>();

                List<Tarefa> listaTarefaHistorico = new List<Tarefa>();

                for (int i = 1; i < lines.Length; i++)
                {
                    string[] linha = lines[i].Replace("\"", "").Split('\t');

                    Tarefa t = new Tarefa();
                    // t.Codigo = reader.GetInt32(0);
                    t.Tipo = linha[0];
                    t.Id = Convert.ToInt32(linha[1]);
                    t.Titulo = linha[2];
                    t.Status = linha[4];
                    t.PlanejadoPara = linha[5];
                    t.Estimativa = DataHoraUtil.formatarHora(linha[6]);
                    t.EstimativaCorrigida = DataHoraUtil.formatarHora(linha[7]);
                    t.TempoGasto = DataHoraUtil.formatarHora(linha[8]);
                    t.Pai = linha[9].Replace("#", "");
                    t.DataColeta = Convert.ToDateTime(txtData.Text);

                    FuncionarioDAO fDAO = new FuncionarioDAO();
                    String resposanvel = linha[3];
                    Funcionario f = fDAO.recuperar(resposanvel);
                    if (f == null)
                    {
                        f = new Funcionario(0, "DEBHE/DE611", resposanvel);
                        fDAO.incluir(f.encapsularLista());
                        f = fDAO.recuperar(resposanvel);
                    }
                    t.Responsavel = f;

                    if (!existeTarefa(t))
                    {
                        listaTarefaIncluir.Add(t);
                    }
                    else
                    {
                        listaTarefaAtualizar.Add(t);
                    }
                    listaTarefaHistorico.Add(t);
                }

                TarefaDAO tDAO = new TarefaDAO();
                if (listaTarefaIncluir.Count > 0)
                {
                    tDAO.incluir(listaTarefaIncluir);
                }
                if (listaTarefaAtualizar.Count > 0)
                {
                    tDAO.atualizar(listaTarefaAtualizar);
                }
                if (listaTarefaHistorico.Count > 0)
                {
                    TarefaHistoricoDAO thDAO = new TarefaHistoricoDAO();
                    thDAO.incluir(listaTarefaHistorico);
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

        private bool existeTarefa(Tarefa item)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add(Tarefa.ID, Convert.ToString(item.Id));

            TarefaDAO tDAO = new TarefaDAO();
            List<Tarefa> listaItem = tDAO.recuperar(param);
            if (listaItem.Count > 0)
            {
                return true;
            }
            return false;
        }

        private void preencherCombos()
        {
            preencherComboProjeto();

            preencherComboFuncionario();

            preencherComboStatus();
        }

        private void preencherComboProjeto()
        {
            ComboBoxItem itemTodos = new ComboBoxItem();
            itemTodos.Content = "Todos";
            itemTodos.Tag = 0;
            cmbFiltroProjeto.Items.Add(itemTodos);
            cmbFiltroProjeto.SelectedIndex = 0;

            ProjetoDAO pDAO = new ProjetoDAO();
            List<Projeto> lista = pDAO.recuperar();
            if (lista.Count > 0)
            {
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
                preencherComboSprint(lista[0].Codigo);
            }
        }

        private void preencherComboSprint(int codigoProjeto)
        {
            cmbSprint.Items.Clear();
            SprintDAO sDAO = new SprintDAO();
            List<Sprint> lista = sDAO.recuperar(Sprint.criarListaParametrosPesquisaPorProjeto(codigoProjeto));
            if (lista.Count > 0)
            {
                foreach (Sprint s in lista)
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = s.Nome;
                    item.Tag = s.Codigo;
                    cmbSprint.Items.Add(item);
                }
                cmbSprint.SelectedIndex = 0;
            }
        }

        private void preencherComboFiltroSprint(int codigoProjeto)
        {
            cmbFiltroSprint.Items.Clear();
            SprintDAO sDAO = new SprintDAO();
            List<Sprint> lista = sDAO.recuperar(Sprint.criarListaParametrosPesquisaPorProjeto(codigoProjeto));
            if (lista.Count > 0)
            {
                foreach (Sprint s in lista)
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = s.Nome;
                    item.Tag = s.Codigo;
                    cmbFiltroSprint.Items.Add(item);
                }
                cmbFiltroSprint.SelectedIndex = 0;
            }
        }

        private void preencherComboFuncionario()
        {
            ComboBoxItem itemTodos = new ComboBoxItem();
            itemTodos.Content = "Todos";
            itemTodos.Tag = 0;
            cmbFiltroFuncionario.Items.Add(itemTodos);
            cmbFiltroFuncionario.SelectedIndex = 0;

            FuncionarioDAO fDAO = new FuncionarioDAO();
            List<Funcionario> lista = fDAO.recuperar();
            if (lista.Count > 0)
            {
                foreach (Funcionario f in lista)
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = f.Nome;
                    item.Tag = f.Codigo;
                    cmbFiltroFuncionario.Items.Add(item);
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

        private void cmbProjeto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)cmbProjeto.SelectedItem;
            int codigoProjeto = Convert.ToInt32(item.Tag);
            preencherComboSprint(codigoProjeto);
        }

        private void cmbFiltroProjeto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)cmbFiltroProjeto.SelectedItem;
            int codigoProjeto = Convert.ToInt32(item.Tag);
            preencherComboFiltroSprint(codigoProjeto);
        }

        private string recuperarSprint()
        {
            if (cmbSprint.SelectedIndex >= 0)
            {
                ComboBoxItem item = (ComboBoxItem)cmbSprint.SelectedItem;
                return item.Content.ToString();
            }
            return "";
        }

        private void iniciarCamposFiltro()
        {
            txtFiltroTitulo.Text = "";
            txtFiltroId.Text = "";
            txtFiltroDtInicio.Text = "";
            txtFiltroDtFinal.Text = "";

            cmbFiltroProjeto.SelectedIndex = 0;
            cmbFiltroSprint.SelectedIndex = 0;
            cmbFiltroFuncionario.SelectedIndex = 0;
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
                param.Add(Tarefa.TITULO, txtFiltroTitulo.Text);
            }
            if (txtFiltroId.Text.Length > 0)
            {
                param.Add(Tarefa.ID, txtFiltroId.Text);
            }
            if (txtFiltroPai.Text.Length > 0)
            {
                param.Add(Tarefa.PAI, txtFiltroPai.Text);
            }
            if (txtFiltroDtInicio.Text.Length > 0)
            {
                param.Add(Tarefa.DTINICIO, txtFiltroDtInicio.Text);
            }
            if (txtFiltroDtFinal.Text.Length > 0)
            {
                param.Add(Tarefa.DTFINAL, txtFiltroDtFinal.Text);
            }
            if (cmbFiltroSprint.SelectedIndex >= 0)
            {
                string sprint = Convert.ToString(((ComboBoxItem)cmbFiltroSprint.SelectedItem).Content);
                param.Add(Tarefa.PLANEJADO_PARA, sprint);
            }
            if (cmbFiltroFuncionario.SelectedIndex > 0)
            {
                int codigo = Convert.ToInt32(((ComboBoxItem)cmbFiltroFuncionario.SelectedItem).Tag);
                param.Add(Tarefa.RESPONSAVEL, Convert.ToString(codigo));
            }
            if (cmbFiltroStatus.SelectedIndex > 0)
            {
                string sprint = Convert.ToString(((ComboBoxItem)cmbFiltroStatus.SelectedItem).Content);
                param.Add(Tarefa.STATUS, sprint);
            }
            preencherLista(param);
        }
    }
}
