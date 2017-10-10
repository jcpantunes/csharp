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

namespace GEP_DE607
{
    /// <summary>
    /// Lógica interna para VisualizarTarefas.xaml
    /// </summary>
    public partial class VisualizarTarefas : Window
    {
        public VisualizarTarefas()
        {
            InitializeComponent();

            prepararInterface();
        }

        private void prepararInterface()
        {
            FuncionarioBO funcBO = new FuncionarioBO();
            preencherCombo(cmbFuncionario, funcBO.Recuperar());

            //chkTodos.IsChecked = true;
            //CheckedAll(true);
        }

        private ComboBoxItem preencherComboItem(int codigo, string nome)
        {
            ComboBoxItem item = new ComboBoxItem();
            item.Tag = codigo;
            item.Content = nome;
            return item;
        }

        private void preencherCombo(ComboBox combo, List<Funcionario> lista)
        {
            combo.Items.Clear();
            int selectedItem = 0;
            foreach (Funcionario str in lista)
            {
                combo.Items.Add(preencherComboItem(str.Codigo, str.Nome));
            }
            combo.SelectedIndex = selectedItem;
        }

        private ListBoxItem preencherListItem(int codigo, string nome)
        {
            ListBoxItem item = new ListBoxItem();
            item.Tag = codigo;
            item.Content = nome;
            return item;
        }

        private void preencherListBox(ListBox lst, DateTime dtInicio, DateTime dtFinal)
        {
            lst.Items.Clear();

            FuncionarioBO funcBO = new FuncionarioBO();
            Funcionario funcionario = funcBO.Recuperar(Int32.Parse(((ComboBoxItem)cmbFuncionario.SelectedItem).Tag.ToString()));

            TarefaBO tarefaBO = new TarefaBO();
            List<Tarefa> lista = tarefaBO.Recuperar(funcionario, dtInicio, dtFinal);

            if (lista.Count > 0)
            {
                foreach (Tarefa t in lista)
                {
                    lst.Items.Add(preencherListItem(t.Codigo, t.Titulo));
                }
            }
        }
        private void preencherList(ListBox listBox, List<string> listaDominio)
        {
            listBox.Items.Clear();
            foreach (string str in listaDominio)
            {
                ListBoxItem item = new ListBoxItem();
                item.Content = str;
                listBox.Items.Add(item);
            }
        }

        private void chkTodos_Click(object sender, RoutedEventArgs e)
        {
            CheckedAll(chkTodos.IsChecked);
        }

        private void CheckedAll(bool? valor)
        {
            chkJUL.IsChecked = valor;
            chkAGO.IsChecked = valor;
            chkSET.IsChecked = valor;
            chkOUT.IsChecked = valor;
            chkNOV.IsChecked = valor;
            chkDEZ.IsChecked = valor;
            chkJAN.IsChecked = valor;
            chkFEV.IsChecked = valor;
            chkMAR.IsChecked = valor;
            chkABR.IsChecked = valor;
            chkMAI.IsChecked = valor;
            chkJUN.IsChecked = valor;
        }

        private void btnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            preencherListBox(lstJulItem, new DateTime(2016, 8, 1), new DateTime(2016, 10, 1));
        }
        
    }
}
