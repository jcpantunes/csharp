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
using GEP_DE607.Dominio;
using GEP_DE607.Negocio;
using GEP_DE607.Componente;
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

            prepararTela();

            preencherLista(new Dictionary<string, string>());
        }

        private void prepararTela()
        {
            txtCodigo.Text = "0";
            txtSS.Text = "";
            preencherCombo(cmbTipo, Constantes.recuperarDominioTipo(), "");
            this.txtNome.Text = "";
            this.txtId.Text = "";
            preencherCombo(cmbSistema, Constantes.recuperarDominioSistema(), "");
            preencherCombo(cmbLinguagem, Constantes.recuperarDominioLinguagem(), "");
            preencherCombo(cmbProcesso, Constantes.recuperarDominioProcesso(), "");
            preencherCombo(cmbTipoProjeto, Constantes.recuperarDominioTipoProjeto(), "");
            preencherCombo(cmbSituacao, Constantes.recuperarDominioSituacao(), "");
            preencherCombo(cmbConclusividade, Constantes.recuperarDominioConclusividade(), "");
            this.txtPFPrev.Text = "";
            this.txtPFReal.Text = "";
            this.txtApropriacao.Text = "";
            this.txtDtInicio. Text = DateTime.Now.ToShortDateString();
            this.txtDtEntrega.Text = DateTime.Now.ToShortDateString();
            this.txtDtFinal.Text = DateTime.Now.ToShortDateString();
        }

        private void preencherCombo(ComboBox combo, List<string> listaDominio, string itemNome)
        {
            combo.Items.Clear();
            int i = 0, selectedItem = 0;
            foreach (string str in listaDominio)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = str;
                combo.Items.Add(item);
                if (str.Equals(itemNome))
                {
                    selectedItem = i;
                }
                i++;
            }
            combo.SelectedIndex = selectedItem;
        }

        private void btnLimpar_Click(object sender, RoutedEventArgs e)
        {
            prepararTela();
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            if (txtNome.Text.Length == 0 || txtId.Text.Length == 0 ||
                    txtDtInicio.Text.Length == 0 || txtDtFinal.Text.Length == 0)
            {
                Alerta alerta = new Alerta("Favor preencher todos os campos");
                alerta.Show();
            }
            else if (!Util.Util.verificarStringNumero(txtId.Text))
            {
                Alerta alerta = new Alerta("Preencha o campo Id com numero");
                alerta.Show();
            }
            else
            {// int codigo, int ss, string tipo, int id, string nome, string sistema, string linguagem, string processo, string tipoProjeto, 
                // string situacao, int conclusividade, decimal pfprev, decimal pfreal, decimal apropriacao, DateTime dtInicio, DateTime dtEntrega, DateTime dtFinal

                Projeto p = preencherProjeto();
                ProjetoBO projBO = new ProjetoBO();
                projBO.salvar(p);

                Alerta alerta = new Alerta("Salvo com sucesso.");
                alerta.Show();

                prepararTela();
                preencherLista(new Dictionary<string, string>());

            }
        }

        private Projeto preencherProjeto()
        {
            Projeto p = new Projeto();
            p.Codigo = Convert.ToInt32(txtCodigo.Text);
            p.Ss = Convert.ToInt32(txtSS.Text);
            p.Tipo = this.cmbTipo.Text;
            p.Id = Convert.ToInt32(this.txtId.Text);
            p.Nome = this.txtNome.Text;
            p.Sistema = cmbSistema.Text;
            p.Linguagem = cmbLinguagem.Text;
            p.Processo = cmbProcesso.Text;
            p.TipoProjeto = cmbTipoProjeto.Text;
            p.Situacao = cmbSituacao.Text;
            p.Conclusividade = Convert.ToInt32(cmbConclusividade.Text);
            p.Pfprev = Convert.ToDecimal(this.txtPFPrev.Text);
            p.Pfreal = Convert.ToDecimal(this.txtPFReal.Text);
            p.Apropriacao = Convert.ToDecimal(this.txtApropriacao.Text);
            p.DtInicio = Convert.ToDateTime(this.txtDtInicio.Text);
            p.DtEntrega = Convert.ToDateTime(this.txtDtEntrega.Text);
            p.DtFinal = Convert.ToDateTime(this.txtDtFinal.Text);
            return p;
        }

        private void preencherTela(Projeto p)
        {
            txtCodigo.Text = Convert.ToString(p.Codigo);
            txtSS.Text = Convert.ToString(p.Ss);
            preencherCombo(cmbTipo, Constantes.recuperarDominioSistema(), p.Tipo);
            this.txtNome.Text = p.Nome;
            this.txtId.Text = Convert.ToString(p.Id);
            preencherCombo(cmbSistema, Constantes.recuperarDominioSistema(), p.Sistema);
            preencherCombo(cmbLinguagem, Constantes.recuperarDominioLinguagem(), p.Linguagem);
            preencherCombo(cmbProcesso, Constantes.recuperarDominioProcesso(), p.Processo);
            preencherCombo(cmbTipoProjeto, Constantes.recuperarDominioTipoProjeto(), p.TipoProjeto);
            preencherCombo(cmbSituacao, Constantes.recuperarDominioSituacao(), p.Situacao);
            preencherCombo(cmbConclusividade, Constantes.recuperarDominioConclusividade(), Convert.ToString(p.Conclusividade));
            this.txtPFPrev.Text = Convert.ToString(p.Pfprev);
            this.txtPFReal.Text = Convert.ToString(p.Pfreal);;
            this.txtApropriacao.Text = Convert.ToString(p.Apropriacao);;
            this.txtDtInicio.Text = Convert.ToDateTime(p.DtInicio).ToShortDateString();
            this.txtDtEntrega.Text = Convert.ToDateTime(p.DtEntrega).ToShortDateString();
            this.txtDtFinal.Text = Convert.ToDateTime(p.DtFinal).ToShortDateString();
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            int codigo = Convert.ToInt32(txtCodigo.Text);
            if (codigo > 0)
            {
                ProjetoBO projBO = new ProjetoBO();
                Projeto p = preencherProjeto();
                projBO.excluir(p);

                Alerta alerta = new Alerta("Excluido com sucesso.");
                alerta.Show();
            }

            prepararTela();
            preencherLista(new Dictionary<string, string>());
        }

        private void prepararTelaFiltro()
        {
            txtFiltroNome.Text = "";
            txtFiltroId.Text = "";
            txtFiltroDtInicio.Text = "";
            txtFiltroDtFinal.Text = "";
        }

        private void btnFiltroLimpar_Click(object sender, RoutedEventArgs e)
        {
            prepararTelaFiltro();
            preencherLista(new Dictionary<string, string>());
        }

        private void preencherLista(Dictionary<string, string> param)
        {
            try
            {
                ProjetoBO projBO = new ProjetoBO();
                tblProjeto.ItemsSource = projBO.recuperar(param);
            }
            catch (Exception ex)
            {
                Alerta alerta = new Alerta();
                alerta.preencherMensagem("Problema ao tentar acessar o banco de dados: \n" + ex.Message);
                alerta.Show();
                this.Close();
            }
        }

        private void btnPesquisar_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();

            if (txtFiltroNome.Text.Length > 0)
            {
                param.Add(Projeto.NOME, txtFiltroNome.Text);
            }
            if (txtFiltroId.Text.Length > 0)
            {
                param.Add(Projeto.ID, txtFiltroId.Text);
            }
            if (txtFiltroDtInicio.Text.Length > 0)
            {
                param.Add(Projeto.DTINICIO, txtFiltroDtInicio.Text);
            }
            if (txtFiltroDtFinal.Text.Length > 0)
            {
                param.Add(Projeto.DTFINAL, txtFiltroDtFinal.Text);
            }

            preencherLista(param);
        }

        private void tblProjeto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int linha = tblProjeto.SelectedIndex;
            if (linha >= 0 && tblProjeto.SelectedItem is Projeto)
            {
                Projeto p = (Projeto)tblProjeto.SelectedItem;
                preencherTela(p);
            }
        }
    }
}
