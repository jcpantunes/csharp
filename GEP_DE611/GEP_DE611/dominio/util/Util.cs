using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GEP_DE611.dominio.util
{
    class Util
    {

        public static bool verificarStringNumero(string s)
        {
            char[] vetor = s.ToCharArray();
            foreach (char c in vetor)
            {
                if (!char.IsNumber(c))
                {
                    return false;
                }
            }
            return true;
        }

        public static List<string> retornarListaLotacao()
        {
            List<string> lista = new List<string>();
            lista.Add("DEBHE/DE601");
            lista.Add("DEBHE/DE602");
            lista.Add("DEBHE/DE603");
            lista.Add("DEBHE/DE604");
            lista.Add("DEBHE/DE605");
            lista.Add("DEBHE/DE606");
            lista.Add("DEBHE/DE607");
            lista.Add("DEBHE/DE608");
            lista.Add("DEBHE/DE609");
            lista.Add("DEBHE/DE610");
            lista.Add("DEBHE/DE611");
            lista.Add("DEBHE/DE612");
            lista.Add("DEBHE/DE613");
            lista.Add("DEBHE/DE614");
            lista.Add("DEBHE/DE615");
            lista.Add("DEBHE/DE616");
            lista.Add("DEBHE/DE617");
            lista.Add("DEBHE/DE6ED");
            lista.Add("DEBHE/DE6CT");

            return lista;
        }

        public static OpenFileDialog retornarOpenDialogBox()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.Title = "Browse Text Files";

            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;

            openFileDialog.DefaultExt = "txt";
            openFileDialog.Filter = "Text files (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            openFileDialog.ReadOnlyChecked = true;
            openFileDialog.ShowReadOnly = true;

            return openFileDialog;
        }

        public static bool validarArquivoTarefa(string linha)
        {
            linha = linha.Replace("\"", "");
            string[] colunas = linha.Split('\t');
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

        public static bool validarArquivoItemBacklog(string linha)
        {
            linha = linha.Replace("\"", "");
            string[] colunas = linha.Split('\t');
            if (colunas[0].Equals("Tipo") &&
                    colunas[1].Equals("ID") &&
                    colunas[2].Equals("Título") &&
                    colunas[3].Equals("Status") &&
                    colunas[4].Equals("Planejado Para") &&
                    colunas[5].Equals("Valor definido para o Negócio") &&
                    colunas[6].Equals("Tamanho Estimado"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
