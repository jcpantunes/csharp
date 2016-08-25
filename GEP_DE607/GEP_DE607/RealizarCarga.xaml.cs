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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using GEP_DE607.Dominio;
using GEP_DE607.Negocio;
using GEP_DE607.Componente;
using GEP_DE607.Persistencia;
using GEP_DE607.Util;

namespace GEP_DE607
{
    /// <summary>
    /// Interaction logic for RealizarCarga.xaml
    /// </summary>
    public partial class RealizarCarga : Window
    {
        public RealizarCarga()
        {
            InitializeComponent();

            prepararTela();
        }

        private void prepararTela()
        {
            ComboBoxItem itemTodos = new ComboBoxItem();
            
            List<string> lista = Constantes.recuperarDominioTipoCarga();
            foreach (string str in lista)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = str;
                cmbTipoCarga.Items.Add(item);
            }
            cmbTipoCarga.SelectedIndex = 0;
        }

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = MyFilePopup.criarMyFilePopup();
            if (openFileDialog.ShowDialog().ToString().Equals("OK"))
            {
                txtUpload.Text = openFileDialog.FileName;
            }
        }

        private void btnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            if (txtUpload.Text.Length == 0)
            {
                Alerta alerta = new Alerta("Favor preencher todos os campos");
                alerta.Show();
            }
            else
            {
                realizarUpload(txtUpload.Text);
            }
        }

        private void realizarUpload(string file)
        {
            string msg = "";
            ComboBoxItem item = (ComboBoxItem)cmbTipoCarga.SelectedItem;
            string[] linhas = System.IO.File.ReadAllLines(file);
            if (linhas.Length > 1 && validarArquivo(item.Content.ToString(), linhas[0]))
            {
                if (item.Content.Equals(Constantes.FUNCIONARIO))
                {
                    List<Funcionario> listaFuncionario = recuperarListaFuncionario(linhas);
                    FuncionarioBO funcBO = new FuncionarioBO();
                    funcBO.incluirLista(listaFuncionario);
                }
                else if (item.Content.Equals(Constantes.TAREFA))
                {
                    List<Tarefa> listaTarefa = recuperarListaTarefa(linhas);
                    TarefaBO tarefaBO = new TarefaBO();
                    tarefaBO.incluirLista(listaTarefa);
                }
                msg = "Arquivo incluido com sucesso";
            }
            else
            {
                msg = "Arquivo sem dados ou invalido";
            }
            Alerta alerta = new Alerta(msg);
            alerta.Show();
        }

        private bool validarArquivo(string tipoCarga, string linha)
        {
            if (tipoCarga.Equals(Constantes.FUNCIONARIO))
            {
                string[] campos = { "nome", "lotacao" };
                return Util.Util.validarArquivo(linha, campos);
            }
            else if (tipoCarga.Equals(Constantes.TAREFA))
            {
                string[] campos = { "Tipo", "ID", "Título", "Responsável", "Status", "Planejado Para", "Pai", "Data de Modificação", "ID do Projeto", "Classificação", "Estimativa", "Tempo Gasto" };
                return Util.Util.validarArquivo(linha, campos);
            }
            return false;
        }

        private List<Funcionario> recuperarListaFuncionario(string[] linhas)
        {
            List<Funcionario> listaFuncionario = new List<Funcionario>();
            for (int i = 1; i < linhas.Length; i++)
            {
                string[] linha = linhas[i].Replace("\"", "").Split('\t');
                Funcionario funcionario = new Funcionario();
                funcionario.Nome = linha[0];
                funcionario.Lotacao = linha[1];
                listaFuncionario.Add(funcionario);
            }
            return listaFuncionario;
        }

        private List<Tarefa> recuperarListaTarefa(string[] linhas)
        {
            FuncionarioDAO fDAO = new FuncionarioDAO();
            List<Funcionario> listaCacheFuncionario = fDAO.recuperar();

            List<Tarefa> listaTarefa = new List<Tarefa>();
            for (int i = 1; i < linhas.Length; i++)
            {
                string[] linha = linhas[i].Replace("\"", "").Split('\t');

                Tarefa tarefa = new Tarefa();
                tarefa.Tipo = linha[0];
                tarefa.Id = Convert.ToInt32(linha[1]);
                tarefa.Titulo = linha[2];

                string nomeResponsavel = linha[3];
                var funcExistente = listaCacheFuncionario.Where(t => t.Nome.Equals(nomeResponsavel));
                if (funcExistente.Count() == 0)
                {
                    Funcionario funcionario = new Funcionario(0, "SUPDE/DEBHE/DE607", nomeResponsavel);
                    fDAO.incluir(funcionario.encapsularLista());
                    tarefa.Responsavel = fDAO.recuperar(nomeResponsavel);
                    listaCacheFuncionario.Add(fDAO.recuperar(nomeResponsavel));
                }
                else
                {
                    tarefa.Responsavel = funcExistente.First();
                }

                tarefa.Status = linha[4];
                tarefa.PlanejadoPara = linha[5];
                tarefa.Pai = linha[6].Replace("#", "");
                tarefa.DataModificacao = Convert.ToDateTime(linha[7]);
                tarefa.Projeto = Convert.ToInt32(linha[8]);
                tarefa.Classificacao = linha[9];
                tarefa.Estimativa = DataHoraUtil.formatarHora(linha[10]);
                tarefa.TempoGasto = DataHoraUtil.formatarHora(linha[11]);
                listaTarefa.Add(tarefa);
            }
            return listaTarefa;
        }

    }
}
