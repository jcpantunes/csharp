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
        public const string DEFEITO = "Defeito";
        public const string RELATO = "Relato";
        public const string FUNCIONARIO = "Funcionario";
        public const string APROPRIACAO = "Apropriacao";

        public static List<string> recuperarDominioTipoCarga()
        { 
            List<string> listaTipoCarga = new List<string>();
            listaTipoCarga.Add(TAREFA);
            listaTipoCarga.Add(DEFEITO);
            listaTipoCarga.Add(RELATO);
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

        public const string SOLICITACAO_ABERTA = "Aberta";
        public const string SOLICITACAO_EM_ATENDIMENTO = "Em Atendimento";
        public const string SOLICITACAO_ENTREGUE = "Entregue";
        public const string SOLICITACAO_EM_HOMOLOGACAO = "Em Homologação";
        public const string SOLICITACAO_SUSPENSA = "Suspensa";
        public const string SOLICITACAO_HOMOLOGADA = "Homologada";
        public const string SOLICITACAO_CONCLUIDA = "Concluída";

        public static List<string> recuperarDominioSolicitacaoStatus()
        {
            List<string> lista = new List<string>();
            lista.Add(SOLICITACAO_ABERTA);
            lista.Add(SOLICITACAO_EM_ATENDIMENTO);
            lista.Add(SOLICITACAO_ENTREGUE);
            lista.Add(SOLICITACAO_EM_HOMOLOGACAO);
            lista.Add(SOLICITACAO_SUSPENSA);
            lista.Add(SOLICITACAO_HOMOLOGADA);
            lista.Add(SOLICITACAO_CONCLUIDA);
            return lista;
        }

        public const string SOLICITACAO_APOIO = "Apoio";
        public const string SOLICITACAO_APURACAO_ESPECIAL = "Apuração Especial";
        public const string SOLICITACAO_CONSULTORIA = "Consultoria";
        public const string SOLICITACAO_MANUTENCAO_EVOLUTIVA = "Manutenção Evolutiva";
        public const string SOLICITACAO_MANUTENCAO_CORRETIVA = "Manutenção Corretiva";
        public const string SOLICITACAO_MANUTENCAO_ADAPTATIVA = "Manutenção Adaptativa";
        public const string SOLICITACAO_NOVO_SISTEMA = "Novo Sistema";
        public const string SOLICITACAO_ORCAMENTACAO = "Orçamentação";

        public static List<string> recuperarDominioSolicitacaoTipoDemanda()
        {
            List<string> lista = new List<string>();
            lista.Add(SOLICITACAO_APOIO);
            lista.Add(SOLICITACAO_APURACAO_ESPECIAL);
            lista.Add(SOLICITACAO_CONSULTORIA);
            lista.Add(SOLICITACAO_MANUTENCAO_EVOLUTIVA);
            lista.Add(SOLICITACAO_MANUTENCAO_CORRETIVA);
            lista.Add(SOLICITACAO_MANUTENCAO_ADAPTATIVA);
            lista.Add(SOLICITACAO_NOVO_SISTEMA);
            lista.Add(SOLICITACAO_ORCAMENTACAO);
            return lista;
        }

    }
}
