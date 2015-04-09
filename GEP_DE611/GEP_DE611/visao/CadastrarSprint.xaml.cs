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
using GEP_DE611.dominio;
using GEP_DE611.dominio.constante;
using GEP_DE611.persistencia;
using GEP_DE611.componente;

namespace GEP_DE611.visao
{
    /// <summary>
    /// Interaction logic for CadastrarSprint.xaml
    /// </summary>
    public partial class CadastrarSprint : Window
    {
        public CadastrarSprint()
        {
            InitializeComponent();

            preencherLista();

            preencherComboProjeto();

            iniciarCampos();
        }

        private void iniciarCampos()
        {
            txtCodigo.Text = "0";
            txtNome.Text = "";
            cmbProjeto.SelectedIndex = 0;
            txtDtInicio.Text = DateTime.Now.ToShortDateString();
            txtDtFinal.Text = DateTime.Now.ToShortDateString();
        }

        private void preencherCampos(Sprint s)
        {
            txtCodigo.Text = Convert.ToString(s.Codigo);
            txtNome.Text = s.Nome;

            foreach (ComboBoxItem item in cmbProjeto.Items)
            {
                if (Convert.ToInt32(item.Tag) == s.Projeto.Codigo)
                {
                    cmbProjeto.SelectedItem = item;
                    break;
                }
            }

            txtDtInicio.Text = s.DtInicio.ToShortDateString();
            txtDtFinal.Text = s.DtFinal.ToShortDateString();
        }

        private void preencherLista()
        {
            try
            {
                SprintDAO pDAO = new SprintDAO();
                tblSprint.ItemsSource = pDAO.recuperar();
            }
            catch (Exception ex)
            {
                Alerta alerta = new Alerta("Problema ao tentar acessar o banco de dados: \n" + ex.Message);
                alerta.Show();

                this.Close();
            }
        }

        private void preencherComboProjeto()
        {
            ComboBoxItem itemTodos = new ComboBoxItem();
            itemTodos.Content = "Todos";
            itemTodos.Tag = 0;
            cmbFiltroProjeto.Items.Add(itemTodos);
            
            ProjetoDAO pDAO = new ProjetoDAO();
            List<Projeto> lista = pDAO.recuperar();
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
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            if (txtNome.Text.Length == 0 || cmbProjeto.SelectedIndex < 0 ||
                    txtDtInicio.Text.Length == 0 || txtDtFinal.Text.Length == 0)
            {
                Alerta alerta = new Alerta("Favor preencher todos os campos");
                alerta.Show();
            }
            else
            {
                Projeto p = recuperarProjeto();
                if (p != null)
                {
                    Sprint s = new Sprint(Convert.ToInt32(txtCodigo.Text), txtNome.Text, Convert.ToDateTime(txtDtInicio.Text),
                        Convert.ToDateTime(txtDtFinal.Text), p);

                    SprintDAO sDAO = new SprintDAO();
                    if (s.Codigo == 0)
                    {
                        sDAO.incluir(s.encapsularLista());
                    }
                    else
                    {
                        sDAO.atualizar(s.encapsularLista());
                    }

                    Alerta alerta = new Alerta("Salvo com sucesso.");
                    alerta.Show();

                    iniciarCampos();

                    preencherLista();
                }
            }
        }

        private Projeto recuperarProjeto()
        {
            ProjetoDAO pDAO = new ProjetoDAO();
            int codigo = Convert.ToInt32(((ComboBoxItem)cmbProjeto.SelectedItem).Tag);
            List<Projeto> lista = pDAO.recuperar(Projeto.criarListaParametros(codigo));
            if (lista.Count > 0)
            {
                return lista[0];
            }
            return null;
        }

        private void tblSprint_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int linha = tblSprint.SelectedIndex;
            if (linha >= 0 && tblSprint.SelectedItem is Sprint)
            {
                Sprint s = (Sprint)tblSprint.SelectedItem;
                preencherCampos(s);
            }
        }

        private void btnLimpar_Click(object sender, RoutedEventArgs e)
        {
            iniciarCampos();
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            Projeto p = recuperarProjeto();
            if ((p != null) && (Convert.ToInt32(txtCodigo.Text) > 0) && (txtNome.Text.Length != 0 && cmbProjeto.SelectedIndex >= 0 &&
                    txtDtInicio.Text.Length != 0 && txtDtFinal.Text.Length != 0))
            {
                Sprint s = new Sprint(Convert.ToInt32(txtCodigo.Text), txtNome.Text, Convert.ToDateTime(txtDtInicio.Text),
                    Convert.ToDateTime(txtDtFinal.Text), p);

                if (p.Codigo > 0)
                {
                    SprintDAO sDAO = new SprintDAO();
                    sDAO.excluir(s.encapsularLista());

                    Alerta alerta = new Alerta("Excluido com sucesso.");
                    alerta.Show();
                }
            }
            else
            {
                Alerta alerta = new Alerta("Projeto não existente ou os dados do projeto foram alterados. Favor selecionar o projeto novamente.");
                alerta.Show();
            }

            iniciarCampos();
            preencherLista();
        }


        private void iniciarCamposFiltro()
        {
            txtFiltroNome.Text = "";
            cmbFiltroProjeto.SelectedIndex = 0;
            txtFiltroDtInicio.Text = "";
            txtFiltroDtFinal.Text = "";
        }

        private void btnFiltroLimpar_Click(object sender, RoutedEventArgs e)
        {
            iniciarCamposFiltro();

            preencherLista();
        }

        private void btnPesquisar_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();

            if (txtFiltroNome.Text.Length > 0)
            {
                param.Add(Sprint.NOME, txtFiltroNome.Text);
            }
            if (cmbFiltroProjeto.SelectedIndex > 0)
            {
                int codigo = Convert.ToInt32(((ComboBoxItem)cmbFiltroProjeto.SelectedItem).Tag);
                param.Add(Sprint.PROJETO, Convert.ToString(codigo));
            }
            if (txtFiltroDtInicio.Text.Length > 0)
            {
                param.Add(Sprint.DTINICIO, txtFiltroDtInicio.Text);
            }
            if (txtFiltroDtFinal.Text.Length > 0)
            {
                param.Add(Sprint.DTFINAL, txtFiltroDtFinal.Text);
            }

            if (param.Count > 0)
            {
                try
                {
                    SprintDAO pDAO = new SprintDAO();
                    tblSprint.ItemsSource = pDAO.recuperar(param);
                }
                catch (Exception ex)
                {
                    Alerta alerta = new Alerta();
                    alerta.preencherMensagem("Problema ao tentar acessar o banco de dados: \n" + ex.Message);
                    alerta.Show();

                    this.Close();
                }
            }
            else
            {
                preencherLista();
            }
        }
    }
}
