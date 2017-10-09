using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GEP_DE607.Dominio;
using GEP_DE607.Negocio;

namespace GEP_DE607.Test
{

    [TestClass]
    public class TarefaBOTest
    {
        TarefaBO tarefaBO;

        public TarefaBOTest()
        {
            tarefaBO = new TarefaBO();
        }

        [TestMethod]
        public void TarefaBO_Teste1_Recuperar()
        {
            List<Tarefa> lista1 = tarefaBO.Recuperar();
            Assert.AreNotEqual(lista1.Count, 0);

            Tarefa objeto1 = tarefaBO.Recuperar(10);
            Assert.AreEqual(objeto1.Codigo, 10);

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("Id", 966621);
            List<Tarefa> lista2 = tarefaBO.Recuperar(parametros);
            Assert.AreEqual(lista2.Count, 1);

            Funcionario funcionario = new Funcionario(1, "DEBHE/DE607", "Julio Cesar Pereira Antunes");
            Tarefa objeto2 = new Tarefa(0, "Tarefa", 12345, "Tarefa Teste Unit", funcionario,
                "Aberta", "Sprint 1", "12340", DateTime.Now, 1000, "Implementação", 0, 0);
            tarefaBO.Incluir(objeto2);
            List<Tarefa> lista3 = tarefaBO.Recuperar();
            Assert.AreEqual(lista3.Count, lista1.Count + 1);

            Dictionary<string, object> parametros2 = new Dictionary<string, object>();
            parametros2.Add("Id", 12345);
            Tarefa objeto3 = tarefaBO.Recuperar(parametros2)[0];
            objeto3.Estimativa = 5;
            tarefaBO.Atualizar(objeto3);

            tarefaBO.Remover(objeto3);

        }
    }
}
