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
    /// Interaction logic for CadastrarSolicitacao.xaml
    /// </summary>
    public partial class CadastrarSolicitacao : Window
    {
        public CadastrarSolicitacao()
        {
            InitializeComponent();

            prepararTela();

            preencherLista(new Dictionary<string, string>());
        }

        private void prepararTela()
        {
            txtCodigo.Text = "0";
            this.txtId.Text = "";
            this.txtDemanda.Text = "";
            preencherCombo(cmbSistema, Constantes.recuperarDominioSistema(), "");
            preencherCombo(cmbTipoDemanda, Constantes.recuperarDominioSolicitacaoTipoDemanda(), "");
            FuncionarioBO funcBO = new FuncionarioBO();
            preencherCombo(cmbDestinatario, funcBO.recuperarNomes(), "");
            preencherCombo(cmbStatus, Constantes.recuperarDominioSolicitacaoStatus(), "");
            this.txtDtInicio.Text = DateTime.Now.ToShortDateString();
            this.txtDtEntrega.Text = DateTime.Now.ToShortDateString();
            this.txtDtFinal.Text = DateTime.Now.ToShortDateString();
            this.txtAssunto.Text = "";
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
            if (txtId.Text.Length == 0 || txtDemanda.Text.Length == 0 ||
                    txtAssunto.Text.Length == 0 || cmbSistema.Text.Length == 0 ||
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
            {// int codigo, int ss, string tipo, int id, string nome, string sistema, string linguagem, string processo, string tipoSolicitacao, 
                // string situacao, int conclusividade, decimal pfprev, decimal pfreal, decimal apropriacao, DateTime dtInicio, DateTime dtEntrega, DateTime dtFinal

                Solicitacao p = preencherSolicitacao();
                SolicitacaoBO projBO = new SolicitacaoBO();
                projBO.salvar(p);

                Alerta alerta = new Alerta("Salvo com sucesso.");
                alerta.Show();

                prepararTela();
                preencherLista(new Dictionary<string, string>());

            }
        }

        private Solicitacao preencherSolicitacao()
        {
            Solicitacao solicitacao = new Solicitacao();
            solicitacao.Codigo = Convert.ToInt32(txtCodigo.Text);
            solicitacao.Id = Convert.ToInt32(this.txtId.Text);
            solicitacao.Demanda = this.txtDemanda.Text;
            solicitacao.Sistema = cmbSistema.Text;
            solicitacao.TipoDemanda = cmbTipoDemanda.Text;
            solicitacao.Destinatario = cmbDestinatario.Text;
            solicitacao.Status = cmbStatus.Text;
            solicitacao.Assunto = txtAssunto.Text;
            solicitacao.DtInicio = Convert.ToDateTime(this.txtDtInicio.Text);
            solicitacao.DtEntrega = Convert.ToDateTime(this.txtDtEntrega.Text);
            solicitacao.DtFinal = Convert.ToDateTime(this.txtDtFinal.Text);
            solicitacao.Assunto = txtAssunto.Text;
            return solicitacao;
        }

        private void preencherTela(Solicitacao solicitacao)
        {
            txtCodigo.Text = Convert.ToString(solicitacao.Codigo);
            this.txtId.Text = Convert.ToString(solicitacao.Id);
            this.txtDemanda.Text = solicitacao.Demanda;
            preencherCombo(cmbSistema, Constantes.recuperarDominioSistema(), solicitacao.Sistema);
            
            preencherCombo(cmbTipoDemanda, Constantes.recuperarDominioSolicitacaoTipoDemanda(), solicitacao.TipoDemanda);
            FuncionarioBO funcBO = new FuncionarioBO();
            preencherCombo(cmbDestinatario, funcBO.recuperarNomes(), solicitacao.Destinatario);
            preencherCombo(cmbStatus, Constantes.recuperarDominioSolicitacaoStatus(), solicitacao.Status);

            this.txtDtInicio.Text = Convert.ToDateTime(solicitacao.DtInicio).ToShortDateString();
            this.txtDtEntrega.Text = Convert.ToDateTime(solicitacao.DtEntrega).ToShortDateString();
            this.txtDtFinal.Text = Convert.ToDateTime(solicitacao.DtFinal).ToShortDateString();

            txtAssunto.Text = solicitacao.Assunto;
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            int codigo = Convert.ToInt32(txtCodigo.Text);
            if (codigo > 0)
            {
                SolicitacaoBO solicBO = new SolicitacaoBO();
                Solicitacao solicitacao = preencherSolicitacao();
                solicBO.excluir(solicitacao);

                Alerta alerta = new Alerta("Excluido com sucesso.");
                alerta.Show();
            }

            prepararTela();
            preencherLista(new Dictionary<string, string>());
        }

        private void prepararTelaFiltro()
        {
            txtFiltroId.Text = "";
            txtFiltroAssunto.Text = "";
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
                SolicitacaoBO solicBO = new SolicitacaoBO();
                tblSolicitacao.ItemsSource = solicBO.recuperar(param);
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

            if (txtFiltroAssunto.Text.Length > 0)
            {
                param.Add(Solicitacao.ASSUNTO, txtFiltroAssunto.Text);
            }
            if (txtFiltroId.Text.Length > 0)
            {
                param.Add(Solicitacao.ID, txtFiltroId.Text);
            }
            if (txtFiltroDtInicio.Text.Length > 0)
            {
                param.Add(Solicitacao.DTINICIO, txtFiltroDtInicio.Text);
            }
            if (txtFiltroDtFinal.Text.Length > 0)
            {
                param.Add(Solicitacao.DTFINAL, txtFiltroDtFinal.Text);
            }

            preencherLista(param);
        }

        private void tblSolicitacao_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int linha = tblSolicitacao.SelectedIndex;
            if (linha >= 0 && tblSolicitacao.SelectedItem is Solicitacao)
            {
                Solicitacao p = (Solicitacao)tblSolicitacao.SelectedItem;
                preencherTela(p);
            }
        }
    }
}
