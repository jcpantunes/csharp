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
using System.Windows.Navigation;
using System.Windows.Shapes;
using GEP_DE611.chart;
using GEP_DE611.visao;
using GEP_DE611.dominio;
using GEP_DE611.dominio.constante;
using GEP_DE611.persistencia;

namespace GEP_DE611
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // testarFuncionario();
            // testarTarefa();
            // testarProjeto();
            // testarSprint();
            // testarHora();

        }

        private void btnBurndown_Click(object sender, RoutedEventArgs e)
        {
            VisualizarBurndown burndown = new VisualizarBurndown();
            burndown.Show();
        }

        private void btnUploadArquivoTarefa_Click(object sender, RoutedEventArgs e)
        {
            UploadArquivoTarefa upload = new UploadArquivoTarefa();
            upload.Show();
        }

        private void btnProjeto_Click(object sender, RoutedEventArgs e)
        {
            CadastrarProjeto tela = new CadastrarProjeto();
            tela.Show();
        }

        private void btnSprint_Click(object sender, RoutedEventArgs e)
        {
            CadastrarSprint tela = new CadastrarSprint();
            tela.Show();
        }


        private void btnFuncionario_Click(object sender, RoutedEventArgs e)
        {
            CadastrarFuncionario tela = new CadastrarFuncionario();
            tela.Show();
        }










        private void testarFuncionario()
        {
            Funcionario f = new Funcionario();
            f.Lotacao = "DEBHE/DE611";
            f.Nome = "Joao da Silva";

            FuncionarioDAO dao = new FuncionarioDAO();
            dao.incluir(f.encapsularLista());

            List<Funcionario> lista = dao.recuperar();

            Funcionario f2 = dao.recuperar(lista[lista.Count - 1].Codigo);
            f2.Nome = "nomeAtualizado";
            dao.atualizar(f2.encapsularLista());

            dao.excluir(f2.encapsularLista());
        }

        private void testarTarefa()
        {
            Tarefa t = new Tarefa();
            // t.Codigo = reader.GetInt32(0);
            t.Tipo = TipoItemTrabalho.TAREFA;
            t.Id = 100102;
            t.Titulo = "Tarefa Teste 2";
            t.Status = StatusUtil.EM_ANDAMENTO;
            t.PlanejadoPara = "eSocial-281573-1.0.1-CONS-01";
            t.DataColeta = new DateTime(2015, 04, 07);
            t.Pai = "#100100";
            t.Estimativa = 5;
            t.EstimaticaCorrigida = 6;
            t.TempoGasto = 6;
            FuncionarioDAO fDao = new FuncionarioDAO();
            t.Responsavel = fDao.recuperar(1);

            TarefaDAO dao = new TarefaDAO();
            dao.incluir(t.encapsularLista());

            List<Tarefa> lista = dao.recuperar();

            Tarefa t2 = dao.recuperar(lista[lista.Count - 1].Codigo);
            t2.Titulo = "Titulo Atualizado";
            dao.atualizar(t2.encapsularLista());

            dao.excluir(t2.encapsularLista());
        }

        private void testarProjeto()
        {
            Projeto t = new Projeto();
            // t.Codigo = reader.GetInt32(0);
            t.Nome = "Projeto Teste";
            t.Id = 100099;
            t.DtInicio = new DateTime(2015, 04, 01);
            t.DtFinal = new DateTime(2015, 04, 08);

            ProjetoDAO dao = new ProjetoDAO();
            dao.incluir(t.encapsularLista());

            List<Projeto> lista = dao.recuperar();

            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add(Projeto.CODIGO, Convert.ToString(lista[lista.Count - 1].Codigo));

            List<Projeto> lista2 = dao.recuperar(param);

            param.Add(Projeto.NOME, Convert.ToString(lista[lista.Count - 1].Nome));
            List<Projeto> lista3 = dao.recuperar(param);

            if (lista.Count > 0)
            {
                Projeto p2 = lista2[0];
                p2.Nome = "Projeto Atualizado";
                dao.atualizar(p2.encapsularLista());

                dao.excluir(p2.encapsularLista());
            }
        }


        private void testarSprint()
        {
            Sprint t = new Sprint();
            // t.Codigo = reader.GetInt32(0);
            t.Nome = "Sprint Teste";
            t.DtInicio = new DateTime(2015, 04, 01);
            t.DtFinal = new DateTime(2015, 04, 08);

            SprintDAO dao = new SprintDAO();
            dao.incluir(t.encapsularLista());

            List<Sprint> lista = dao.recuperar();

            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add(Sprint.CODIGO, Convert.ToString(lista[lista.Count - 1].Codigo));

            List<Sprint> lista2 = dao.recuperar(param);

            param.Add(Sprint.NOME, Convert.ToString(lista[lista.Count - 1].Nome));
            List<Sprint> lista3 = dao.recuperar(param);

            if (lista.Count > 0)
            {
                Sprint p2 = lista2[0];
                p2.Nome = "Sprint Atualizado";
                dao.atualizar(p2.encapsularLista());

                dao.excluir(p2.encapsularLista());
            }
        }

        private void testarHora()
        {
            //DataHoraUtil util = new DataHoraUtil();
            //string hora = util.formatarHora("2 horas");
            //hora = util.formatarHora("1 hora");
            //hora = util.formatarHora("36 horas 18 m");
            //hora = util.formatarHora("36 hora 6 m");
            //hora = util.formatarHora("1 hora 6 m");
            //hora = util.formatarHora("1 hora 35 m");
            //hora = util.formatarHora("12 h 52 m");
        }

        
    }
}
