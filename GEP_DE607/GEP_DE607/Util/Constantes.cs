using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEP_DE607.Util
{
    class Constantes
    {
        public const string TAREFA = "Tarefa";
        public const string FUNCIONARIO = "Funcionario";
        public const string APROPRIACAO = "Apropriacao";

        public static List<string> recuperarDominioTipoCarga()
        { 
            List<string> listaTipoCarga = new List<string>();
            listaTipoCarga.Add(TAREFA);
            listaTipoCarga.Add(FUNCIONARIO);
            listaTipoCarga.Add(APROPRIACAO);
            return listaTipoCarga;
        }

        public const string TIPO_SOFTWARE = "Software";
        public const string TIPO_NAO_SOFTWARE = "Não Software";
        
        public static List<string> recuperarDominioTipo()
        {
            List<string> lista = new List<string>();
            lista.Add(TIPO_SOFTWARE);
            lista.Add(TIPO_NAO_SOFTWARE);
            return lista;
        }

        public const string SISTEMA_ESOCIAL = "eSocial";
        public const string SISTEMA_DCTFWEB = "DCTFWeb";

        public static List<string> recuperarDominioSistema()
        {
            List<string> lista = new List<string>();
            lista.Add(SISTEMA_ESOCIAL);
            lista.Add(SISTEMA_DCTFWEB);
            return lista;
        }

        public const string LINGUAGEM_CSHARP = "CSharp";
        public const string LINGUAGEM_JAVA = "Java";

        public static List<string> recuperarDominioLinguagem()
        {
            List<string> lista = new List<string>();
            lista.Add(LINGUAGEM_CSHARP);
            lista.Add(LINGUAGEM_JAVA);
            return lista;
        }

        public const string PROCESSO_NENHUM = "";
        public const string PROCESSO_AGIL = "Agil";
        public const string PROCESSO_SUMARIO = "Sumario";
        public const string PROCESSO_EXPRESSO = "Expresso";

        public static List<string> recuperarDominioProcesso()
        {
            List<string> lista = new List<string>();
            lista.Add(PROCESSO_NENHUM);
            lista.Add(PROCESSO_AGIL);
            lista.Add(PROCESSO_SUMARIO);
            lista.Add(PROCESSO_EXPRESSO);
            return lista;
        }

        public const string PROJETO_NOVO = "Novo";
        public const string PROJETO_MANUTENCAO_EVOLUTIVA = "ME";
        public const string PROJETO_MANUTENCAO_CORRETIVA = "MC";
        public const string PROJETO_MANUTENCAO_ADAPTATIVA = "MA";
        public const string PROJETO_MANUTENCAO_PREVENTIVA = "MP";
        public const string PROJETO_APURACAO_ESPECIAL = "AESP";
        public const string PROJETO_EXECUCAO_AESP = "Exec AESP";
        public const string PROJETO_CONSULTORIA = "Consultoria";
        public const string PROJETO_APOIO = "Apoio";

        public static List<string> recuperarDominioTipoProjeto()
        {
            List<string> lista = new List<string>();
            lista.Add(PROJETO_NOVO);
            lista.Add(PROJETO_MANUTENCAO_EVOLUTIVA);
            lista.Add(PROJETO_MANUTENCAO_CORRETIVA);
            lista.Add(PROJETO_MANUTENCAO_ADAPTATIVA);
            lista.Add(PROJETO_MANUTENCAO_PREVENTIVA);
            lista.Add(PROJETO_APURACAO_ESPECIAL);
            lista.Add(PROJETO_EXECUCAO_AESP);
            lista.Add(PROJETO_CONSULTORIA);
            lista.Add(PROJETO_APOIO);
            return lista;
        }

        public const string SITUACAO_EM_ATENDIMENTO = "Em Atendimento";
        public const string SITUACAO_EM_HOMOLOGACAO = "Em Homologação";
        public const string SITUACAO_CONCLUIDO = "Concluido";
        public const string SITUACAO_CANCELADO = "Cancelado";

        public static List<string> recuperarDominioSituacao()
        {
            List<string> lista = new List<string>();
            lista.Add(SITUACAO_EM_ATENDIMENTO);
            lista.Add(SITUACAO_EM_HOMOLOGACAO);
            lista.Add(SITUACAO_CONCLUIDO);
            lista.Add(SITUACAO_CANCELADO);
            return lista;
        }

        public const string CONCLUSIVIDADE_25 = "25";
        public const string CONCLUSIVIDADE_50 = "50";
        public const string CONCLUSIVIDADE_70 = "70";
        public const string CONCLUSIVIDADE_80 = "80";
        public const string CONCLUSIVIDADE_90 = "90";
        public const string CONCLUSIVIDADE_100 = "100";

        public static List<string> recuperarDominioConclusividade()
        {
            List<string> lista = new List<string>();
            lista.Add(CONCLUSIVIDADE_25);
            lista.Add(CONCLUSIVIDADE_50);
            lista.Add(CONCLUSIVIDADE_70);
            lista.Add(CONCLUSIVIDADE_80);
            lista.Add(CONCLUSIVIDADE_90);
            lista.Add(CONCLUSIVIDADE_100);
            return lista;
        }
    }
}
