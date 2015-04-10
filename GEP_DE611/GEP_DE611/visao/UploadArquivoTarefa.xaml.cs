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
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using GEP_DE611.dominio;
using GEP_DE611.dominio.constante;
using GEP_DE611.persistencia;
using GEP_DE611.componente;

namespace GEP_DE611.visao
{
    /// <summary>
    /// Interaction logic for UploadArquivoTarefa.xaml
    /// </summary>
    public partial class UploadArquivoTarefa : Window
    {
        public UploadArquivoTarefa()
        {
            InitializeComponent();

            txtData.Text = DateTime.Now.ToShortDateString();

            try
            {
                TarefaDAO tDAO = new TarefaDAO();
                tblTarefa.ItemsSource = tDAO.recuperar();
            }
            catch (Exception ex)
            {
                Alerta alerta = new Alerta();
                alerta.preencherMensagem("Problema ao tentar acessar o banco de dados: \n" + ex.Message);
                alerta.Show();

                this.Close();
            }

            preencherCombos();
        }

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Title = "Browse Text Files";

            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;

            openFileDialog1.DefaultExt = "txt";
            openFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            openFileDialog1.ReadOnlyChecked = true;
            openFileDialog1.ShowReadOnly = true;

            if (openFileDialog1.ShowDialog().ToString().Equals("OK"))
            {
                txtUpload.Text = openFileDialog1.FileName;
            }
        }

        private void btnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            if (cmbProjeto.SelectedIndex < 0 || cmbSprint.SelectedIndex < 0 || txtUpload.Text.Length == 0)
            {
                Alerta alerta = new Alerta();
                alerta.preencherMensagem("Favor preencher todos os campos");
                alerta.Show();
            }
            else
            {
                TarefaDAO tDAO = new TarefaDAO();
                List<DateTime> listaData = tDAO.recuperarListaDatasPorString(recuperarSprint());

                bool upload = false;
                if (txtData.Text.Length != 0)
                {
                    foreach (DateTime data in listaData)
                    {
                        if (Convert.ToDateTime(txtData.Text).Equals(data))
                        {
                            upload = true;
                            break;
                        }
                    }
                }

                if (upload == true)
                {
                    realizarUpload();
                }
            }
        }

        private void realizarUpload()
        {
            string[] lines = System.IO.File.ReadAllLines(txtUpload.Text);

            if (validarArquivo(lines[0]) == true)
            {
                List<Tarefa> lista = new List<Tarefa>();

                for (int i = 1; i < lines.Length; i++)
                {
                    string[] linha = lines[i].Replace("\"", "").Split('\t');

                    Tarefa t = new Tarefa();
                    // t.Codigo = reader.GetInt32(0);
                    t.Tipo = linha[0];
                    t.Id = Convert.ToInt32(linha[1]);
                    t.Titulo = linha[2];
                    t.Status = linha[4];
                    t.PlanejadoPara = linha[5];
                    t.Estimativa = DataHoraUtil.formatarHora(linha[6]);
                    t.EstimaticaCorrigida = DataHoraUtil.formatarHora(linha[7]);
                    t.TempoGasto = DataHoraUtil.formatarHora(linha[8]);
                    t.Pai = linha[9];

                    t.DataColeta = Convert.ToDateTime(txtData.Text);

                    FuncionarioDAO fDAO = new FuncionarioDAO();
                    String resposanvel = linha[3];
                    Funcionario f = fDAO.recuperar(resposanvel);
                    if (f == null)
                    {
                        f = new Funcionario(0, "DEBHE/DE611", resposanvel);
                        fDAO.incluir(f.encapsularLista());
                        f = fDAO.recuperar(resposanvel);
                    }
                    t.Responsavel = f;

                    lista.Add(t);

                }

                TarefaDAO tDAO = new TarefaDAO();
                tDAO.incluir(lista);

                tblTarefa.ItemsSource = tDAO.recuperar();

                Alerta alerta = new Alerta("Arquivo incluido com sucesso!");
                alerta.Show();
            }
            else
            {
                Alerta alerta = new Alerta("Arquivo invalido");
                alerta.Show();
            }
        }

        private bool validarArquivo(string linha1)
        {
            linha1 = linha1.Replace("\"", "");

            string[] colunas = linha1.Split('\t');
            if (colunas[0].Equals("Tipo") &&
                    colunas[1].Equals("ID") &&
                    colunas[2].Equals("Título") &&
                    colunas[3].Equals("Responsável") &&
                    colunas[4].Equals("Status") &&
                    colunas[5].Equals("Planejado Para") &&
                    colunas[6].Equals("Estimativa") &&
                    colunas[7].Equals("Estimativa Corrigida") &&
                    colunas[8].Equals("Tempo Gasto") &&
                    colunas[9].Equals("Pai"))
            {
                return true;
            }
            else
            {
                return false;
            }
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
            ComboBoxItem item = (ComboBoxItem)cmbProjeto.SelectedItem;
            int codigoProjeto = Convert.ToInt32(item.Tag);
            preencherComboSprint(codigoProjeto);
        }

        private string recuperarSprint()
        {
            if (cmbSprint.SelectedIndex >= 0)
            {
                ComboBoxItem item = (ComboBoxItem)cmbSprint.SelectedItem;
                return item.Content.ToString();
            }
            return "";
        }
    }
}
