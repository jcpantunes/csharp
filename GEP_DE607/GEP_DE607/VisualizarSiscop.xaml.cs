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
using System.Data;
using GEP_DE607.Componente;
using GEP_DE607.Util;
using GEP_DE607.Dominio;
using GEP_DE607.Dominio.Modelo;
using GEP_DE607.Negocio;
using GEP_DE607.Persistencia;

namespace GEP_DE607
{
    /// <summary>
    /// Interaction logic for VisualizarSiscop.xaml
    /// </summary>
    public partial class VisualizarSiscop : Window
    {

        private BaseWindow baseWindow;

        public VisualizarSiscop()
        {
            InitializeComponent();
            baseWindow = new BaseWindow();

            preencherCombos();
        }

        private void preencherCombos()
        {
            baseWindow.preencherComboLotacao(cmbLotacao, lstFuncionario, 6);

            baseWindow.preencherComboAno(cmbAno);

            baseWindow.preencherComboMes(cmbMes);
        }

        private void cmbLotacao_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)cmbLotacao.SelectedItem;
            string lotacao = Convert.ToString(item.Content);
            baseWindow.preencherListBoxFuncionario(lstFuncionario, lotacao);
        }

        private void btnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            executarAcao(tblPontoBatido, OpcaoIndicadorPonto.PONTO_BATIDO, true);
        }

        private void lstFuncionario_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private bool validarExibicaoTabela()
        {
            if (cmbLotacao.SelectedIndex >= 0 && cmbAno.SelectedIndex >=0 && cmbMes.SelectedIndex >= 0 &&
                lstFuncionario.Items.Count > 0 && lstFuncionario.SelectedItems.Count == 1)
            {
                return true;
            }
            else
            {
                Alerta alerta = new Alerta("Favor preencher todos os campos ou selecione apenas 1 funcionario.");
                alerta.Show();
            }
            return false;
        }

        private void executarAcao(DataGrid grid, int opcao, bool inteiro)
        {
            if (validarExibicaoTabela())
            {
                int ano = Convert.ToInt32(((ComboBoxItem)cmbAno.SelectedItem).Content);
                int mes = Convert.ToInt32(((ComboBoxItem)cmbMes.SelectedItem).Content);
                DateTime dtInicio = new DateTime(ano, mes, 1);
                DateTime dtFim = new DateTime(ano, mes, DataHoraUtil.recuperarDiaFinalMes(ano, mes));

                FuncionarioDAO fDAO = new FuncionarioDAO();
                List<Funcionario> listaFuncionario = new List<Funcionario>();

                foreach (ListBoxItem item in lstFuncionario.SelectedItems)
                {
                    int codigo = Convert.ToInt32(item.Tag);
                    listaFuncionario.Add(fDAO.recuperar(codigo));
                }

                SiscopBO tDAO = new SiscopBO();
                ApropriacaoBO apropBO = new ApropriacaoBO();

                List<Siscop> listaSiscop = new List<Siscop>();
                Dictionary<DateTime, decimal> apropriacaoPorDia = new Dictionary<DateTime, decimal>();

                foreach (Funcionario func in listaFuncionario)
                {
                    listaSiscop.AddRange(tDAO.recuperarSiscopPorResponsavel(func.Codigo, dtInicio, dtFim));
                    apropriacaoPorDia = apropBO.recuperarApropriacaoPorResponsavelPorDia(func.Nome, dtInicio, dtFim);
                }

                DataTable tabela = new DataTable();
                if (opcao == OpcaoIndicadorPonto.PONTO_BATIDO)
                {
                    object[] listaColunas = { "Data", "Apropriado", "Total", "Manha", "Almoco", "Tarde", "Extra", "Entrada 1", "Saida 1", "Entrada 2", "Saida 2", "Extra 1", "Extra 2" };
                    foreach (string str in listaColunas)
                    {
                        tabela.Columns.Add(Convert.ToString(str));
                    }

                    string file = @"C:\workspace-vs\DE607\csharp\GEP_DE607\GEP_DE607\Csv\siscopPadrao.csv";
                    string[] linhas = System.IO.File.ReadAllLines(file);

                    decimal totalHorarioApropriado = 0;
                    decimal totalPontoMes = 0;

                    foreach (Siscop item in listaSiscop)
                    {
                        object[] linha = new object[listaColunas.Count()];

                        linha[0] = item.Data.ToShortDateString();

                        decimal horarioApropriado = recuperarHorarioApropriado(apropriacaoPorDia, item.Data);
                        linha[1] = horarioApropriado;
                        totalHorarioApropriado += horarioApropriado;

                        string diffManha = recuperarDiferencaHoras(item.Entrada1.Trim(), 1, item.Saida1.Trim(), 2, linhas, item.Responsavel.Nome);
                        linha[3] = diffManha;
                        string diffAlmoco = recuperarDiferencaHoras(item.Saida1.Trim(), 2, item.Entrada2.Trim(), 3, linhas, item.Responsavel.Nome);
                        linha[4] = diffAlmoco;
                        string diffTarde = recuperarDiferencaHoras(item.Entrada2.Trim(), 3, item.Saida2.Trim(), 4, linhas, item.Responsavel.Nome);
                        linha[5] = diffTarde;

                        //07:45
                        string totalDia = DataHoraUtil.calcularTotalDia(diffManha, diffTarde);
                        if (totalDia.Length > 0)
                        {
                            Decimal total = Convert.ToDecimal(totalDia.Substring(0, 2)) + Convert.ToDecimal(totalDia.Substring(3, 2)) / 60;
                            totalPontoMes += total;
                            linha[2] = total.ToString("#.#");
                        }
                        else
                        {
                            linha[2] = totalDia;
                        }

                        linha[6] = "";
                        linha[7] = item.Entrada1;
                        linha[8] = item.Saida1;
                        linha[9] = item.Entrada2;
                        linha[10] = item.Saida2;
                        linha[11] = item.Extra1;
                        linha[12] = item.Extra2;
                        tabela.Rows.Add(linha);
                    }
                    lblTotalApropriadoMes.Content = totalHorarioApropriado.ToString("#.#");
                    lblTotalPontoMes.Content = totalPontoMes.ToString("#.#");
                }
                else if (opcao == OpcaoIndicadorPonto.CODIGO_PONTO)
                {
                    object[] listaColunas = { "Codigo 68/13/14/18", "Codigo 21", "Codgo #/99", "> 10", "<7 ou >21", "Flex", "T <3 >5", "T >6", "Int <1", "Int >2" };
                    foreach (string str in listaColunas)
                    {
                        tabela.Columns.Add(Convert.ToString(str));
                    }

                    string file = @"C:\workspace-vs\DE607\csharp\GEP_DE607\GEP_DE607\Csv\siscopPadrao.csv";
                    string[] linhas = System.IO.File.ReadAllLines(file);

                    CodigoPonto codigoPonto = new CodigoPonto();

                    calcularCodigos(listaSiscop, codigoPonto, linhas);
                    
                    object[] linha = new object[listaColunas.Count()];
                    linha[0] = codigoPonto.Codigo68;
                    linha[1] = codigoPonto.Codigo21;
                    linha[2] = codigoPonto.Codigo99;
                    linha[3] = codigoPonto.Maior10;
                    linha[4] = codigoPonto.Maior21;
                    linha[5] = codigoPonto.Flex;
                    linha[6] = codigoPonto.Turno35;
                    linha[7] = codigoPonto.Turno6;
                    linha[8] = codigoPonto.Intervalo1;
                    linha[9] = codigoPonto.Intervalo2;
                    tabela.Rows.Add(linha);
                }
                baseWindow.preencherGrid(grid, tabela, 80);
            }
        }

        private decimal recuperarHorarioApropriado(Dictionary<DateTime, decimal> apropriacaoPorDia, DateTime data)
        {
            foreach (DateTime key in apropriacaoPorDia.Keys)
            {
                if (key.Equals(data))
                {
                    return apropriacaoPorDia[key];
                }
            }
            return 0;
        }

        private void calcularCodigos(List<Siscop> listaSiscop, CodigoPonto codigoPonto, string[] linhas)
        {
            foreach (Siscop item in listaSiscop)
            {
                TimeSpan time1 = recuperarHorarioCodigo(item.Entrada1, codigoPonto);
                TimeSpan time2 = recuperarHorarioCodigo(item.Saida1, codigoPonto);
                TimeSpan time3 = recuperarHorarioCodigo(item.Entrada2, codigoPonto);
                TimeSpan time4 = recuperarHorarioCodigo(item.Saida2, codigoPonto);

                string diffManha = recuperarDiferencaHoras(item.Entrada1.Trim(), 1, item.Saida1.Trim(), 2, linhas, item.Responsavel.Nome);
                string diffTarde = recuperarDiferencaHoras(item.Entrada2.Trim(), 3, item.Saida2.Trim(), 4, linhas, item.Responsavel.Nome);
                string totalDia = DataHoraUtil.calcularTotalDia(diffManha, diffTarde);

                if (totalDia.Length > 2 && Convert.ToInt32(totalDia.Substring(0,2)) > 10)
                {
                    codigoPonto.Maior10 += 1;
                }

                if ((time1.Hours > 0 && time1.Hours < 7) || time4.Hours > 20)
                {
                    codigoPonto.Maior21 += 1;
                }
                
                codigoPonto.Flex = 0;

                if (diffManha.Length >= 5 && (Convert.ToInt32(diffManha.Substring(0,2)) < 3 || (Convert.ToInt32(diffManha.Substring(0,2)) == 5 && Convert.ToInt32(diffManha.Substring(3,2)) > 0)))
                {
                    codigoPonto.Turno35 += 1;
                }

                if (diffTarde.Length >= 5 && (Convert.ToInt32(diffTarde.Substring(0,2)) < 3 || (Convert.ToInt32(diffTarde.Substring(0,2)) == 5 && Convert.ToInt32(diffTarde.Substring(3,2)) > 0)))
                {
                    codigoPonto.Turno35 += 1;
                }

                if (diffManha.Length > 2 && Convert.ToInt32(diffManha.Substring(0, 2)) > 5  && Convert.ToInt32(diffTarde.Substring(3,2)) > 0)
                {
                    codigoPonto.Turno6 += 1;
                }

                if (diffTarde.Length > 2 && Convert.ToInt32(diffTarde.Substring(0, 2)) > 5 && Convert.ToInt32(diffTarde.Substring(3, 2)) > 0)
                {
                    codigoPonto.Turno6 += 1;
                }

                string diffAlmoco = recuperarDiferencaHoras(item.Saida1.Trim(), 2, item.Entrada2.Trim(), 3, linhas, item.Responsavel.Nome);

                if (diffAlmoco.Length > 2 && Convert.ToInt32(diffAlmoco.Substring(0, 2)) < 1)
                {
                    codigoPonto.Intervalo1 += 1;
                }

                if (diffAlmoco.Length > 2 && Convert.ToInt32(diffAlmoco.Substring(0, 2)) > 1)
                {
                    codigoPonto.Intervalo2 += 1;
                }
            }
        }

        private TimeSpan recuperarHorarioCodigo(string horario, CodigoPonto codigoPonto)
        {
            int codigo = 0;
            TimeSpan time = new TimeSpan(0, 0, 0);
            if (horario.Length == 2)
            {
                codigo = Convert.ToInt32(horario);
            }
            else if (horario.Length > 2)
            {
                time = DataHoraUtil.recuperarHorarioFormatado(horario, ref codigo);
            }
            if (codigo == 13 || codigo == 14 || codigo == 18 || codigo == 68)
            {
                codigoPonto.Codigo68 += 1;
            }
            else if (codigo == 21)
            {
                codigoPonto.Codigo21 += + 1;
            }
            else if (horario.StartsWith("#") || codigo == 99)
            {
                codigoPonto.Codigo99 += + 1;
            }
            return time;
        }

        private string recuperarDiferencaHoras(string horario1, int opcao1, string horario2, int opcao2, string[] linhas, string nome)
        {
            if (horario1.Length == 0 || horario2.Length == 0
                || (horario1.Length == 2 && !horario1.Equals("21"))
                || (horario2.Length == 2 && !horario2.Equals("21")))
            {
                return "";
            }
            TimeSpan time1 = DataHoraUtil.recuperarHora(horario1, linhas, nome, opcao1);
            TimeSpan time2 = DataHoraUtil.recuperarHora(horario2, linhas, nome, opcao2);
            return time2.Subtract(time1).ToString().Substring(0, 5);
        }

        private void pontoRealizado_Expanded(object sender, RoutedEventArgs e)
        {
            executarAcao(tblPontoBatido, OpcaoIndicadorPonto.PONTO_BATIDO, true);
        }

        private void codigoPonto_Expanded(object sender, RoutedEventArgs e)
        {
            executarAcao(tblCodigoPonto, OpcaoIndicadorPonto.CODIGO_PONTO, true);
        }

    }

    class OpcaoIndicadorPonto
    {
        public const int PONTO_BATIDO = 1;

        public const int CODIGO_PONTO = 2;

    }

}
