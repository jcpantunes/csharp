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

        public void realizarUpload(String file)
        {
            string[] campos = { "nome", "lotacao" };
            string[] lines = System.IO.File.ReadAllLines(file);
            if (Util.Util.validarArquivo(lines[0], campos) == true)
            {
                List<Funcionario> listaFuncionario = new List<Funcionario>();
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] linha = lines[i].Replace("\"", "").Split('\t');

                    Funcionario funcionario = new Funcionario();
                    funcionario.Nome = linha[0];
                    funcionario.Lotacao = linha[1];

                    listaFuncionario.Add(funcionario);
                }
      
                string msg = "";
                if (listaFuncionario.Count > 0)
                {
                    FuncionarioBO funcBO = new FuncionarioBO();
                    funcBO.incluirLista(listaFuncionario);
                    msg = "Arquivo incluido com sucesso!";
                }
                else
                {
                    msg = "Arquivo sem dados!";
                }
                Alerta alerta = new Alerta(msg);
                alerta.Show();

            }
            else
            {
                Alerta alerta = new Alerta("Arquivo invalido");
                alerta.Show();
            }
        }
    }
}
