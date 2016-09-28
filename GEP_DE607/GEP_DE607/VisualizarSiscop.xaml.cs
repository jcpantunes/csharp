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
        }

        private void cmbLotacao_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)cmbLotacao.SelectedItem;
            string lotacao = Convert.ToString(item.Content);
            baseWindow.preencherListBoxFuncionario(lstFuncionario, lotacao);
        }

        private void btnAtualizar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lstFuncionario_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private bool validarExibicaoTabela()
        {
            if (cmbLotacao.SelectedIndex >= 0 &&
                lstFuncionario.Items.Count > 0 && lstFuncionario.SelectedItems.Count > 0)
            {
                return true;
            }
            else
            {
                Alerta alerta = new Alerta("Favor preencher todos os campos");
                alerta.Show();
            }
            return false;
        }

        private void executarAcao(DataGrid grid, int opcao, bool inteiro)
        {
            if (validarExibicaoTabela())
            {
                DateTime dtInicio = this.txtDtInicio.Text.Length > 0 ? Convert.ToDateTime(this.txtDtInicio.Text) : new DateTime(2014, 01, 01);
                DateTime dtFim = this.txtDtFinal.Text.Length > 0 ? Convert.ToDateTime(this.txtDtFinal.Text) : new DateTime(2020, 12, 31);

                FuncionarioDAO fDAO = new FuncionarioDAO();
                List<Funcionario> listaFuncionario = new List<Funcionario>();

                foreach (ListBoxItem item in lstFuncionario.SelectedItems)
                {
                    int codigo = Convert.ToInt32(item.Tag);
                    listaFuncionario.Add(fDAO.recuperar(codigo));
                }

                DataTable tabela = new DataTable();
                if (opcao == OpcaoIndicadorPonto.PONTO_BATIDO)
                {
                    List<Siscop> listaSiscop = new List<Siscop>();
                    foreach (Funcionario func in listaFuncionario)
                    {
                        SiscopBO tDAO = new SiscopBO();
                        listaSiscop.AddRange(tDAO.recuperarSiscopPorResponsavel(func.Codigo, dtInicio, dtFim));
                    }

                    object[] listaColunas = { "Data", "Total", "Manha", "Almoco", "Tarde", "Extra", "Entrada 1", "Saida 1", "Entrada 2", "Saida 2", "Extra 1", "Extra 2" };
                    foreach (string str in listaColunas)
                    {
                        tabela.Columns.Add(Convert.ToString(str));
                    }

                    string file = @"D:\julio\workspace-vs\csharp\GEP_DE607\GEP_DE607\Csv\siscopPadrao.csv";
                    string[] linhas = System.IO.File.ReadAllLines(file);

                    foreach (Siscop item in listaSiscop)
                    {
                        object[] linha = new object[listaColunas.Count()];
                        linha[0] = item.Data.ToShortDateString();
                        string diffManha = recuperarDiferencaHoras(item.Entrada1.Trim(), 1, item.Saida1.Trim(), 2, linhas, item.Responsavel.Nome);
                        linha[2] = diffManha;
                        string diffAlmoco = recuperarDiferencaHoras(item.Saida1.Trim(), 2, item.Entrada2.Trim(), 3, linhas, item.Responsavel.Nome);
                        linha[3] = diffAlmoco;
                        string diffTarde = recuperarDiferencaHoras(item.Entrada2.Trim(), 3, item.Saida2.Trim(), 4, linhas, item.Responsavel.Nome);
                        linha[4] = diffTarde;

                        linha[1] = calcularTotalDia(diffManha, diffTarde);

                        linha[5] = "";
                        linha[6] = item.Entrada1;
                        linha[7] = item.Saida1;
                        linha[8] = item.Entrada2;
                        linha[9] = item.Saida2;
                        linha[10] = item.Extra1;
                        linha[11] = item.Extra2;
                        tabela.Rows.Add(linha);
                    }
                }
                baseWindow.preencherGrid(grid, tabela, 80);
            }
        }

        private string calcularTotalDia(string horario1, string horario2)
        {
            if (horario1.Length == 0 && horario2.Length == 0)
            {
                return "";
            }
            else if (horario1.Length == 0 || horario2.Length == 0)
            {
                return horario1.Length == 0 ? horario2 : horario1;
            }
            else
            {
                int hora1 = Convert.ToInt32(horario1.Split(':')[0]);
                int min1 = Convert.ToInt32(horario1.Split(':')[1]);
                int hora2 = Convert.ToInt32(horario2.Split(':')[0]);
                int min2 = Convert.ToInt32(horario2.Split(':')[1]);
                return (new TimeSpan(hora1, min1, 0)).Add(new TimeSpan(hora2, min2, 0)).ToString().Substring(0, 5);
            }
            
        }

        private string recuperarDiferencaHoras(string horario1, int opcao1, string horario2, int opcao2, string[] linhas, string nome)
        {
            if (horario1.Length == 0 || horario2.Length == 0
                || (horario1.Length == 2 && !horario1.Equals("21"))
                || (horario2.Length == 2 && !horario2.Equals("21")))
            {
                return "";
            }
            TimeSpan time1 = recuperarHora(horario1, linhas, nome, opcao1);
            TimeSpan time2 = recuperarHora(horario2, linhas, nome, opcao2);
            return time2.Subtract(time1).ToString().Substring(0, 5);
        }

        private TimeSpan recuperarHora(string horario, string[] linhas, string nome, int opcao)
        {
            string[] linha = { "", "08:00", "12:00", "13:00", "17:00" };
            string[] hr = linha[opcao].Split(':');
            int hora = Convert.ToInt32(hr[0]);
            int min = Convert.ToInt32(hr[1]);
            int codigo = 0;

            if (horario.Length == 5 || horario.Length == 7)
            {
                hr = horario.Split(':');
                hora = Convert.ToInt32(hr[0]);
                min = Convert.ToInt32(hr[1].Substring(0, 2));
                codigo = horario.Length == 7 ? Convert.ToInt32(hr[1].Substring(2, 2)) : 0;
            }
            else if (horario.StartsWith("#") && (horario.Length == 6 || horario.Length == 8))
            {
                hr = horario.Replace("#", "").Split(':');
                hora = Convert.ToInt32(hr[0]);
                min = Convert.ToInt32(hr[1].Substring(0, 2));
                codigo = horario.Length == 8 ? Convert.ToInt32(hr[1].Substring(2, 2)) : 0;
            }
            if (codigo == 21 || codigo == 99)
            {
                if (linhas.Length > 1)
                {
                    for (int i = 1; i < linhas.Length; i++)
                    {
                        linha = linhas[i].Replace("\"", "").Split('\t');
                        if (linha[0].ToUpper().Equals(nome.ToUpper()))
                        {
                            hr = linha[opcao].Split(':');
                            hora = Convert.ToInt32(hr[0]);
                            min = Convert.ToInt32(hr[1]);
                        }
                    }
                }
            }
            return new TimeSpan(hora, min, 0);
        }

        private void pontoRealizado_Expanded(object sender, RoutedEventArgs e)
        {
            executarAcao(tblPontoBatido, OpcaoIndicadorPonto.PONTO_BATIDO, true);
        }
    }

    class OpcaoIndicadorPonto
    {
        public const int PONTO_BATIDO = 1;

    }

}
