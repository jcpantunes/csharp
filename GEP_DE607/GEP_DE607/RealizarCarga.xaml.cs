﻿using System;
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
                msg = "Arquivo incluido com sucesso";
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
                else if (item.Content.Equals(Constantes.DEFEITO) || item.Content.Equals(Constantes.RELATO))
                {
                    List<Bug> listaBug = recuperarListaBug(linhas);
                    BugBO bugBO = new BugBO();
                    bugBO.incluirLista(listaBug);
                }
                else if (item.Content.Equals(Constantes.APROPRIACAO))
                {
                    List<Apropriacao> listaApropriacao = recuperarListaApropriacao(linhas);
                    ApropriacaoBO apropBO = new ApropriacaoBO();
                    List<int> listaTarefasInexistentes = apropBO.validarListaApropriacaoInexistente(listaApropriacao);
                    if (listaTarefasInexistentes.Count > 0)
                    {
                        msg = "As seguintes tarefas não estão cadastradas: ";
                        foreach (int i in listaTarefasInexistentes)
                        {
                            msg += i + ", ";
                        }
                        msg.Substring(0, msg.Length - 2);
                    }
                    else
                    {
                        apropBO.incluirLista(listaApropriacao);
                    }
                }
                else if (item.Content.Equals(Constantes.ITEM_BACKLOG))
                {
                    List<ItemBacklog> listaItemBacklog = recuperarListaItemBacklog(linhas);
                    ItemBacklogBO itemBO = new ItemBacklogBO();
                    itemBO.incluirLista(listaItemBacklog);
                }

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
            else if (tipoCarga.Equals(Constantes.DEFEITO) || tipoCarga.Equals(Constantes.RELATO))
            {
                string[] campos = { "Tipo", "ID", "Título", "Responsável", "Status", "Planejado Para", "Pai", "Data de Modificação", "ID do Projeto", "Criado Por", "Encontrado no Projeto", "Tipo do Relato", "Resolução" };
                return Util.Util.validarArquivo(linha, campos);
            }
            else if (tipoCarga.Equals(Constantes.APROPRIACAO))
            {
                string[] campos = { "Nome", "Data", "Horas", "Tarefa", "Macroatividade", "Mnemonico", "Projeto" };
                return Util.Util.validarArquivo(linha, campos);
            }
            else if (tipoCarga.Equals(Constantes.ITEM_BACKLOG))
            {
                string[] campos = { "Tipo", "ID", "Título", "Responsável", "Status", "Planejado Para", "Pai", "Data de Modificação", "ID do Projeto", "Valor definido para o Negócio", "Tamanho Estimado", "Complexidade", "PF" };
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
                tarefa.Responsavel = identificarFuncionario(linha[3], listaCacheFuncionario);
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

        private Funcionario identificarFuncionario(string nomeResponsavel, List<Funcionario> listaCacheFuncionario)
        {
            FuncionarioDAO fDAO = new FuncionarioDAO();
            var funcExistente = listaCacheFuncionario.Where(t => t.Nome.Equals(nomeResponsavel));
            if (funcExistente.Count() == 0)
            {
                Funcionario funcionario = new Funcionario(0, "SUPDE/DEBHE/DE607", nomeResponsavel);
                fDAO.incluir(funcionario.encapsularLista());
                funcionario = fDAO.recuperar(nomeResponsavel);
                listaCacheFuncionario.Add(funcionario);
                return funcionario;
            }
            else
            {
                return funcExistente.First();
            }
        }

        private List<Bug> recuperarListaBug(string[] linhas)
        {
            FuncionarioDAO fDAO = new FuncionarioDAO();
            List<Funcionario> listaCacheFuncionario = fDAO.recuperar();

            List<Bug> listaBug = new List<Bug>();
            for (int i = 1; i < linhas.Length; i++)
            {
                string[] linha = linhas[i].Replace("\"", "").Split('\t');

                Bug bug = new Bug();
                bug.Tipo = linha[0];
                bug.Id = Convert.ToInt32(linha[1]);
                bug.Titulo = linha[2];
                bug.Responsavel = identificarFuncionario(linha[3], listaCacheFuncionario);
                bug.Status = linha[4];
                bug.PlanejadoPara = linha[5];
                bug.Pai = linha[6].Replace("#", "");
                bug.DataModificacao = Convert.ToDateTime(linha[7]);
                bug.Projeto = Convert.ToInt32(linha[8]);
                bug.CriadoPor = linha[9];
                bug.EncontradoProjeto = linha[10];
                bug.TipoRelato = linha[11];
                bug.Resolucao = linha[12];
                listaBug.Add(bug);
            }
            return listaBug;
        }

        private List<Apropriacao> recuperarListaApropriacao(string[] linhas)
        {
            List<Apropriacao> listaApropriacao = new List<Apropriacao>();
            for (int i = 1; i < linhas.Length; i++)
            {
                string[] linha = linhas[i].Replace("\"", "").Split('\t');
                Apropriacao apropriacao = new Apropriacao();
                apropriacao.Nome = linha[0];
                apropriacao.Data = Convert.ToDateTime(linha[1]);
                apropriacao.Hora = Convert.ToDecimal(linha[2]);
                apropriacao.Tarefa = Convert.ToInt32(linha[3]);
                apropriacao.Macroatividade = Convert.ToString(linha[4]);
                apropriacao.Mnemonico = Convert.ToString(linha[5]);
                apropriacao.Projeto = Convert.ToInt32(linha[6]);
                listaApropriacao.Add(apropriacao);
            }
            return listaApropriacao;
        }

        private List<ItemBacklog> recuperarListaItemBacklog(string[] linhas)
        {
            FuncionarioDAO fDAO = new FuncionarioDAO();
            List<Funcionario> listaCacheFuncionario = fDAO.recuperar();

            List<ItemBacklog> listaItemBacklog = new List<ItemBacklog>();
            for (int i = 1; i < linhas.Length; i++)
            {
                string[] linha = linhas[i].Replace("\"", "").Split('\t');

                ItemBacklog itemBacklog = new ItemBacklog();
                itemBacklog.Tipo = linha[0];
                itemBacklog.Id = Convert.ToInt32(linha[1]);
                itemBacklog.Titulo = linha[2];
                itemBacklog.Status = linha[4];
                itemBacklog.PlanejadoPara = linha[5];
                itemBacklog.Pai = linha[6].Replace("#", "");
                itemBacklog.DataModificacao = Convert.ToDateTime(linha[7]);
                itemBacklog.Projeto = Convert.ToInt32(linha[8]);
                itemBacklog.ValorNegocio = Convert.ToInt32(linha[9]);
                itemBacklog.Tamanho = Convert.ToInt32(linha[10].Replace("pts", "").Replace("pt", "")); 
                itemBacklog.Complexidade = Convert.ToInt32(linha[11]); 
                itemBacklog.Pf = Convert.ToDecimal(linha[12]); 
                listaItemBacklog.Add(itemBacklog);
            }
            return listaItemBacklog;
        }

    }
}
