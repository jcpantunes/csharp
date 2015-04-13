﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using GEP_DE611.dominio;
using GEP_DE611.dominio.constante;

namespace GEP_DE611.persistencia
{

    class TarefaDAO : BaseDAO
    {
        private string TABELA = "Tarefa";

        public TarefaDAO()
        {
        }
        
        public List<Tarefa> recuperar()
        {
            string query = "SELECT * FROM " + TABELA;
            return executarSelect(query);
        }

        public Tarefa recuperar(int codigo)
        {
            string query = "SELECT * FROM " + TABELA + " WHERE codigo = " + codigo;
            List<Tarefa> lista = executarSelect(query);
            if (lista.Count > 0)
            {
                return lista[0];
            }
            return null;
        }

        public List<Tarefa> recuperar(Dictionary<string, string> parametros)
        {
            string query = "SELECT * FROM " + TABELA;
            if (parametros.Count > 0)
            {
                query += " WHERE ";

                foreach (string key in parametros.Keys)
                {
                    if (key.Equals(Tarefa.CODIGO))
                    {
                        query += Tarefa.CODIGO + " = " + parametros[key] + " and ";
                    }
                    else if (key.Equals(Tarefa.TITULO))
                    {
                        query += Tarefa.TITULO + " like '%" + parametros[key] + "%' and ";
                    }
                    else if (key.Equals(Tarefa.ID))
                    {
                        query += Tarefa.ID + " = '" + parametros[key] + "' and ";
                    }
                    else if (key.Equals(Tarefa.PAI))
                    {
                        query += Tarefa.PAI + " like '%" + parametros[key] + "%' and ";
                    }
                    else if (key.Equals(Tarefa.DTINICIO))
                    {
                        query += Tarefa.DATA_COLETA + " >= '" + Convert.ToDateTime(parametros[key]) + "' and ";
                    }
                    else if (key.Equals(Tarefa.DTFINAL))
                    {
                        query += Tarefa.DATA_COLETA + " <= '" + Convert.ToDateTime(parametros[key]) + "' and ";
                    }
                    else if (key.Equals(Tarefa.PLANEJADO_PARA))
                    {
                        query += Tarefa.PLANEJADO_PARA + " = '" + parametros[key] + "' and ";
                    }
                    else if (key.Equals(Tarefa.RESPONSAVEL))
                    {
                        query += Tarefa.RESPONSAVEL + " = " + parametros[key] + " and ";
                    }
                    else if (key.Equals(Tarefa.STATUS))
                    {
                        query += Tarefa.STATUS + " = '" + parametros[key] + "' and ";
                    }
                }
                query = query.Substring(0, (query.Length - 4));
            }
            return executarSelect(query);
        }

        public decimal recuperarEstimativaTotalPorSprint(string planejadoPara)
        {
            string query = "SELECT * FROM " + TABELA
                + " WHERE planejadoPara = '" + planejadoPara + "'"
                + " and dataColeta in (SELECT distinct MAX (dataColeta) "
                + " FROM " + TABELA
                + " WHERE planejadoPara = '" + planejadoPara + "')";

            List<Tarefa> lista = executarSelect(query);
            decimal estimativaTotal = 0;
            foreach (Tarefa t in lista)
            {
                if (t.EstimaticaCorrigida > 0)
                {
                    estimativaTotal += Convert.ToDecimal(t.EstimaticaCorrigida);
                }
                else
                {
                    estimativaTotal += t.Estimativa > 0 ? Convert.ToDecimal(t.Estimativa) : 0;
                }
            }
            return estimativaTotal;
        }

        public List<DateTime> recuperarListaDatasPorString(string planejadoPara)
        {
            string query = "SELECT distinct (dataColeta) FROM " + TABELA
                + " WHERE planejadoPara = '" + planejadoPara + "'"
                + " and tempoGasto <> 0 "
                + " ORDER BY dataColeta ASC ";

            List<DateTime> datas = new List<DateTime>();

            SqlConnection conn = null;
            SqlDataReader reader = select(conn, query);
            if (reader != null)
            {
                while (reader.Read())
                {
                    datas.Add(reader.GetDateTime(0));
                }
            }
            desconectar(conn);

            return datas;
        }

        public List<KeyValuePair<string, decimal>> recuperarTempoGastoTotalPorData(string planejadoPara)
        {
            List<KeyValuePair<string, decimal>> tempoGastoPorData = new List<KeyValuePair<string, decimal>>();

            List<DateTime> datas = recuperarListaDatasPorString(planejadoPara);
            
            foreach (DateTime data in datas)
            {
                string query = "SELECT * FROM " + TABELA
                    + " WHERE planejadoPara = '" + planejadoPara + "' "
                    + " and tempoGasto <> 0 "
                    + " and dataColeta = '" + data + "' "
                    + " ORDER BY dataColeta ASC ";

                List<Tarefa> lista = executarSelect(query);
                decimal tempoGasto = 0;
                foreach (Tarefa t in lista)
                {
                    tempoGasto += t.TempoGasto > 0 ? t.TempoGasto : 0;
                }
                tempoGastoPorData.Add(new KeyValuePair<string, decimal>(data.ToString(), tempoGasto));
            }
            
            return tempoGastoPorData;
        }

        private List<Tarefa> executarSelect(string query)
        {
            List<Tarefa> lista = new List<Tarefa>();
            
            SqlConnection conn = null;

            List<Funcionario> listaFuncionario = new List<Funcionario>();

            SqlDataReader reader = select(conn, query);
            if (reader != null)
            {
                while (reader.Read())
                {
                    Tarefa t = new Tarefa();
                    t.Codigo = reader.GetInt32(0);
                    t.Tipo = reader.GetString(1);
                    t.Id = reader.GetInt32(2);
                    t.Titulo = reader.GetString(3);
                    t.Status = reader.GetString(4);
                    t.PlanejadoPara = reader.GetString(5);
                    t.Pai = reader.GetString(6);
                    t.DataColeta = reader.GetDateTime(7);
                    t.Estimativa = reader.GetDecimal(8);
                    t.EstimaticaCorrigida = reader.GetDecimal(9);
                    t.TempoGasto = reader.GetDecimal(10);
                    t.Responsavel = recuperarFuncionario(listaFuncionario, reader.GetInt32(11));

                    lista.Add(t);
                }
            }
            desconectar(conn);
            return lista;
        }

        private Funcionario recuperarFuncionario(List<Funcionario> listaFuncionario, int codigo)
        {
            Funcionario funcionario = null;
            foreach (Funcionario f in listaFuncionario)
            {
                if (f.Codigo == codigo)
                {
                    funcionario = f;
                    break;
                }
            }
            if (funcionario == null)
            {
                FuncionarioDAO dao = new FuncionarioDAO();
                funcionario = dao.recuperar(codigo);
                listaFuncionario.Add(funcionario);
            }
            return funcionario;
        }

        public void incluir (List<Tarefa> lista)
        {
            string queryInsert = "INSERT INTO " + TABELA
                + " (tipo, id, responsavel, titulo, status, planejadoPara, dataColeta, pai, "
                + "estimativa, estimaticaCorrigida, tempoGasto) " 
                + " VALUES (@tipo, @id, @responsavel, @titulo, @status, @planejadoPara, @dataColeta, "
	            + "@pai, @estimativa, @estimaticaCorrigida, @tempoGasto)";
            executarQuery(lista, queryInsert);
        }

        public void atualizar (List<Tarefa> lista)
        {
            string queryUpdate = "UPDATE " + TABELA 
                + " SET tipo = @tipo, "
                + "id = @id, "
                + "reponsavel = @responsavel, "
                + "titulo = @titulo, "
                + "status = @status, "
                + "planejadoPara = @planejadoPara, "
                + "dataColeta = @dataColeta, "
                + "pai = @pai, "
                + "estimativa = @estimativa, "
                + "estimaticaCorrigida = @estimaticaCorrigida, "
                + "tempoGasto = @tempoGasto "
                + "WHERE codigo = @codigo";
            executarQuery(lista, queryUpdate);
        }

        public void excluir(List<Tarefa> lista)
        {
            string query = "DELETE FROM " + TABELA + " WHERE codigo = @codigo";
            executarQuery(lista, query);
        }

        public void excluirPorSprintPorData(string planejadoPara, string data)
        {
            string query = "DELETE FROM " + TABELA
                    + " WHERE planejadoPara = '" + planejadoPara + "' "
                    + " and dataColeta = '" + data + "' ";
            
            SqlConnection conn = null;
            save(conn, query);
            desconectar(conn);
        }

        private void executarQuery(List<Tarefa> lista, string query)
        {
            SqlConnection conn = null;
            for (int i = 0; i < lista.Count; i++)
            {
                Tarefa f = lista[i];
                save(conn, query, criarListaParametros(f));
            }
            desconectar(conn);
        }

        private List<SqlParameter> criarListaParametros(Tarefa t) 
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("codigo", t.Codigo));
            parametros.Add(new SqlParameter("tipo", t.Tipo));
            parametros.Add(new SqlParameter("id", t.Id));
            parametros.Add(new SqlParameter("responsavel", t.Responsavel.Codigo));
            parametros.Add(new SqlParameter("titulo", t.Titulo));
            parametros.Add(new SqlParameter("status", t.Status));
            parametros.Add(new SqlParameter("planejadoPara", t.PlanejadoPara));
            parametros.Add(new SqlParameter("dataColeta", t.DataColeta));
            parametros.Add(new SqlParameter("pai", t.Pai));
            parametros.Add(new SqlParameter("estimativa", t.Estimativa));
            parametros.Add(new SqlParameter("estimaticaCorrigida", t.EstimaticaCorrigida));
            parametros.Add(new SqlParameter("tempoGasto", t.TempoGasto));
            return parametros;
        }
    }
}
