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
using GEP_DE611.persistencia;
using GEP_DE611.dominio;
using GEP_DE611.componente;

namespace GEP_DE611.visao
{
    /// <summary>
    /// Interaction logic for Burndown.xaml
    /// </summary>
    public partial class VisualizarBurndown : Window
    {
        public VisualizarBurndown()
        {
            InitializeComponent();

            preencherCombos();

            gerarBurndown();
        }

        private void preencherCombos()
        {
            ProjetoDAO pDAO = new ProjetoDAO();
            List<Projeto> lista = pDAO.recuperar();
            if (lista.Count > 0)
            {
                preencherComboProjeto(lista);
                cmbProjeto.SelectedIndex = 0;

                preencherComboSprint(lista[0].Codigo);
            }
        }

        private void preencherComboProjeto(List<Projeto> lista)
        {
            foreach (Projeto p in lista)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = p.Nome;
                item.Tag = p.Codigo;
                cmbProjeto.Items.Add(item);
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

        private void cmbProjeto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem) cmbProjeto.SelectedItem;
            int codigoProjeto = Convert.ToInt32(item.Tag);
            preencherComboSprint(codigoProjeto);
        }

        private void btnGerarBurndown_Click_1(object sender, RoutedEventArgs e)
        {
            gerarBurndown();
        }

        private void gerarBurndown()
        {
            if (cmbSprint.SelectedIndex >= 0)
            {
                ComboBoxItem item = (ComboBoxItem) cmbSprint.SelectedItem;
                int codigo = Convert.ToInt32(item.Tag);

                SprintDAO sDAO = new SprintDAO();
                List<Sprint> lista = sDAO.recuperar(Sprint.criarListaParametros(codigo));
                if (lista.Count > 0)
                {
                    Sprint sprint = lista[0];
                    int numDias = sprint.DtFinal.Subtract(sprint.DtInicio).Days + 1;

                    //Eixo X
                    List<string> listaX = gerarEixoX(sprint);

                    //Eixo Y
                    TarefaHistoricoDAO thDAO = new TarefaHistoricoDAO();
                    decimal estimativaTotal = thDAO.recuperarEstimativaTotalPorSprint(sprint.Nome);

                    if (estimativaTotal > 0)
                    {
                        gerarGrafico(sprint.Nome, numDias, estimativaTotal, listaX);
                    }
                    else
                    {
                        Alerta alerta = new Alerta("Não Existe Tarefas com estimativas.");
                        alerta.Show();
                    }
                }
            }
        }

        private List<string> gerarEixoX(Sprint sprint)
        {
            //Eixo X
            List<string> listaX = new List<string>();
            DateTime data = sprint.DtInicio;
            while (data <= sprint.DtFinal)
            {
                listaX.Add(data.ToShortDateString());
                data = data.AddDays(1);
            }
            return listaX;
        }

        private void gerarGrafico(string planejadoPara, int numDias, decimal estimativaTotal, List<string> listaX)
        {
            //LINHA IDEAL
            List<KeyValuePair<string, int>> lnIdeal = gerarLinhaIdeal(numDias, estimativaTotal, listaX);

            //LINHA PROGRESSO
            List<KeyValuePair<string, int>> lnProgresso = gerarLinhaProgresso(planejadoPara, numDias, estimativaTotal, listaX);

            var dataSourceList = new List<List<KeyValuePair<string, int>>>();
            dataSourceList.Add(lnIdeal);
            dataSourceList.Add(lnProgresso);

            //Setting data for line chart
            lineChart.DataContext = dataSourceList;
        }

        private List<KeyValuePair<string, int>> gerarLinhaIdeal(int numDias, decimal estimativaTotal, List<string> listaX)
        {
            // =SOMA(Dados.$G$2:Dados.$G$201)-((SOMA(Dados.$G$2:Dados.$G$201)/$A$2)*($A$2-$A3))
            List<KeyValuePair<string, int>> lnIdeal = new List<KeyValuePair<string, int>>();
            for (int i = 0; i < numDias && i < listaX.Count; i++)
            {
                decimal dado = estimativaTotal - ((estimativaTotal / numDias) * (i + 1));
                lnIdeal.Add(new KeyValuePair<string, int>(listaX[i], Convert.ToInt32(dado)));
            }
            return lnIdeal;
        }

        private List<KeyValuePair<string, int>> gerarLinhaProgresso(string planejadoPara, int numDias, decimal estimativaTotal, List<string> listaX)
        {
            // =$C$2-SOMA(Dados.J$2:J$201)
            List<KeyValuePair<string, int>> lnProgresso = new List<KeyValuePair<string, int>>();

            TarefaHistoricoDAO tDAO = new TarefaHistoricoDAO();
            List<KeyValuePair<string, decimal>> listaProgresso = tDAO.recuperarTempoGastoTotalPorData(planejadoPara);

            int dia = 0;
            int diaProgresso = 0;
            
            if ((listaProgresso.Count == 0) || (Convert.ToDateTime(listaX[dia]).CompareTo(Convert.ToDateTime(listaProgresso[diaProgresso].Key)) < 0))
            {
                while (Convert.ToDateTime(listaX[dia]).CompareTo(Convert.ToDateTime(listaProgresso[diaProgresso].Key)) < 0 && dia < listaX.Count)
                {
                    lnProgresso.Add(new KeyValuePair<string, int>(listaX[dia], Convert.ToInt32(estimativaTotal)));
                    dia++;
                }
            }

            decimal tempoGasto = estimativaTotal;
            while (dia < numDias && dia < listaX.Count)
            {
                if (Convert.ToDateTime(listaX[dia]).Equals(Convert.ToDateTime(listaProgresso[diaProgresso].Key)))
                {
                    tempoGasto = estimativaTotal - listaProgresso[diaProgresso].Value;
                    lnProgresso.Add(new KeyValuePair<string, int>(listaX[dia], Convert.ToInt32(tempoGasto)));
                    diaProgresso++;
                }
                else
                {
                    lnProgresso.Add(new KeyValuePair<string, int>(listaX[dia], Convert.ToInt32(tempoGasto)));
                }
                if (diaProgresso == listaProgresso.Count) { break; }
                dia++;
            }
            return lnProgresso;
        }
    }
}
