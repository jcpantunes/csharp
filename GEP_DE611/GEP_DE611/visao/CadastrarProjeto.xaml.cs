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
    /// Interaction logic for Projeto.xaml
    /// </summary>
    public partial class CadastrarProjeto : Window
    {
        public CadastrarProjeto()
        {
            InitializeComponent();

            iniciarCampos();

            preencherLista();
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            int i;
            if (txtNome.Text.Length == 0 || txtId.Text.Length == 0 ||
                    txtDtInicio.Text.Length == 0 || txtDtFinal.Text.Length == 0)
            {
                Alerta alerta = new Alerta("Favor preencher todos os campos");
                alerta.Show();
            }
            else if (!Util.verificarStringNumero(txtId.Text))
            {
                Alerta alerta = new Alerta("Preencha o campo Id com numero");
                alerta.Show();
            }
            else
            {
                Projeto p = new Projeto(Convert.ToInt32(txtCodigo.Text), txtNome.Text, Convert.ToInt32(txtId.Text), Convert.ToDateTime(txtDtInicio.Text),
                    Convert.ToDateTime(txtDtFinal.Text));

                ProjetoDAO pDAO = new ProjetoDAO();
                if (p.Codigo == 0)
                {
                    pDAO.incluir(p.encapsularLista());
                }
                else
                {
                    pDAO.atualizar(p.encapsularLista());
                }

                Alerta alerta = new Alerta("Salvo com sucesso.");
                alerta.Show();

                iniciarCampos();
                preencherLista();
            }

        }

        private void iniciarCampos()
        {
            txtCodigo.Text = "0";
            txtNome.Text = "";
            txtId.Text = "";
            txtDtInicio.Text = DateTime.Now.ToShortDateString();
            txtDtFinal.Text = DateTime.Now.ToShortDateString();
        }

        private void preencherCampos(Projeto p)
        {
            txtCodigo.Text = Convert.ToString(p.Codigo);
            txtNome.Text = p.Nome;
            txtId.Text = Convert.ToString(p.Id);
            txtDtInicio.Text = p.DtInicio.ToShortDateString();
            txtDtFinal.Text = p.DtFinal.ToShortDateString();
        }

        private void preencherLista()
        {
            try
            {
                ProjetoDAO pDAO = new ProjetoDAO();
                tblProjeto.ItemsSource = pDAO.recuperar();
            }
            catch (Exception ex)
            {
                Alerta alerta = new Alerta();
                alerta.preencherMensagem("Problema ao tentar acessar o banco de dados: \n" + ex.Message);
                alerta.Show();

                this.Close();
            }
        }

        private void tblProjeto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int linha = tblProjeto.SelectedIndex;
            if (linha >= 0)
            {
                Projeto p = (Projeto) tblProjeto.SelectedItem;
                preencherCampos(p);
            }
        }

        private void btnLimpar_Click(object sender, RoutedEventArgs e)
        {
            iniciarCampos();
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            Projeto p = new Projeto(Convert.ToInt32(txtCodigo.Text), txtNome.Text, Convert.ToInt32(txtId.Text), Convert.ToDateTime(txtDtInicio.Text),
                    Convert.ToDateTime(txtDtFinal.Text));

            if (p.Codigo > 0)
            {
                ProjetoDAO pDAO = new ProjetoDAO();
                pDAO.excluir(p.encapsularLista());
            }
            Alerta alerta = new Alerta("Excluido com sucesso.");
            alerta.Show();

            iniciarCampos();
            preencherLista();
        }


        private void iniciarCamposFiltro()
        {
            txtFiltroNome.Text = "";
            txtFiltroId.Text = "";
            txtFiltroDtInicio.Text = "";
            txtFiltroDtFinal.Text = "";
        }

        private void btnFiltroLimpar_Click(object sender, RoutedEventArgs e)
        {
            iniciarCamposFiltro();
        }

        private void btnPesquisar_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            
            if (txtFiltroNome.Text.Length > 0)
            {
                param.Add(Projeto.NOME, txtFiltroNome.Text);
            }
            else if (txtFiltroId.Text.Length > 0)
            {
                param.Add(Projeto.ID, txtFiltroId.Text);
            }
            else if (txtFiltroDtInicio.Text.Length > 0)
            {
                param.Add(Projeto.DTINICIO, txtFiltroDtInicio.Text);
            }
            else if (txtFiltroDtFinal.Text.Length > 0)
            {
                param.Add(Projeto.DTFINAL, txtFiltroDtFinal.Text);
            }

            if (param.Count > 0)
            {
                try
                {
                    ProjetoDAO pDAO = new ProjetoDAO();
                    tblProjeto.ItemsSource = pDAO.recuperar(param);
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
