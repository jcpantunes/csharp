using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GEP_DE607.Dominio;
using GEP_DE607.Negocio;

namespace GEP_DE607.Test
{
    /// <summary>
    /// Descrição resumida para FuncionarioBOTest
    /// </summary>
    [TestClass]
    public class FuncionarioBOTest
    {
        FuncionarioBO funcionarioBO;

        public FuncionarioBOTest()
        {
            funcionarioBO = new FuncionarioBO();
        }

        #region Atributos de teste adicionais
        //
        // É possível usar os seguintes atributos adicionais enquanto escreve os testes:
        //
        // Use ClassInitialize para executar código antes de executar o primeiro teste na classe
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup para executar código após a execução de todos os testes em uma classe
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize para executar código antes de executar cada teste 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup para executar código após execução de cada teste
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void FuncionarioBO_Teste1_Recuperar()
        {
            List<Funcionario> lista = funcionarioBO.Recuperar();
            Assert.AreEqual(lista.Count, 1);
        }

        [TestMethod]
        public void FuncionarioBO_Teste2_Recuperar()
        {
            Funcionario objeto = funcionarioBO.Recuperar(1);
            Assert.AreEqual(objeto.Codigo, 1);
        }

        [TestMethod]
        public void FuncionarioBO_Teste3_Recuperar()
        {
            Funcionario funcionario = funcionarioBO.Recuperar(1);
            Assert.IsNotNull(funcionario);

            funcionario = funcionarioBO.RecuperarPorNome("Julio");
            Assert.IsNotNull(funcionario);
        }

        [TestMethod]
        public void FuncionarioBO_Teste5_RecuperarListaID()
        {
            List<int> lista = funcionarioBO.RecuperarListaID();
            Assert.AreEqual(lista.Count, 1);
        }

        [TestMethod]
        public void FuncionarioBO_Teste6_Incluir()
        {
            Funcionario objeto = new Funcionario(0, "DEBHE/DE6XX", "Natalia Ramalho");
            funcionarioBO.Incluir(objeto);

            Dictionary<string, object> parametros = new Dictionary<string, object> { { "Nome", "Natalia Ramalho" } };
            List<Funcionario> lista = funcionarioBO.Recuperar(parametros);
            Assert.AreEqual(lista.Count, 1);
        }

        //void Incluir(List<Funcionario> listaObjetos);

        [TestMethod]
        public void FuncionarioBO_Teste7_Atualizar()
        {
            // Funcionario objeto = funcionarioBO.Recuperar("Natalia");
            Dictionary<string, object> parametros = new Dictionary<string, object> { { "Nome", "Natalia Ramalho" } };
            List<Funcionario> lista = funcionarioBO.Recuperar(parametros);
            Assert.AreEqual(lista.Count, 1);

            if (lista.Count > 0)
            {
                Funcionario objeto = lista[0];
                objeto.Lotacao = "DEBHE/DE6YY";
                funcionarioBO.Atualizar(objeto);

                lista = funcionarioBO.Recuperar(parametros);
                Assert.AreEqual(lista[0].Lotacao, "DEBHE/DE6YY");
            }
        }
        

        //void Atualizar(List<Funcionario> listaObjetos);

        [TestMethod]
        public void FuncionarioBO_Teste8_Remover()
        {
            Dictionary<string, object> parametros = new Dictionary<string, object> { { "Nome", "Natalia Ramalho" } };
            List<Funcionario> lista = funcionarioBO.Recuperar(parametros);
            Assert.AreEqual(lista.Count, 1);

            if (lista.Count > 0)
            {
                Funcionario objeto = lista[0];
                funcionarioBO.Remover(objeto);

                lista = funcionarioBO.Recuperar(parametros);
                Assert.AreEqual(lista.Count, 0);
            }
        }

    }
}
