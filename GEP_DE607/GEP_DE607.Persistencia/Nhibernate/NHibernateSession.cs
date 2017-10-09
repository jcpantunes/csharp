using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace GEP_DE607.Persistencia.Nhibernate
{
    public class NHibernateSession
    {
        public static ISession OpenSession()
        {
            var configuration = new Configuration();
            // var configurationPath = HostingEnvironment.MapPath("hibernate.cfg.xml");

            // var configurationPath = Directory.GetCurrentDirectory() + @"\..\..\Nhibernate\hibernate.cfg.xml";
            var configurationPath = @"D:\julio\workspace-vs\csharp\GEP_DE607\GEP_DE607.Test\Nhibernate\";

            configuration.Configure(configurationPath + @"hibernate.cfg.xml");

            var funcionarioFile = configurationPath + @"Funcionario.hbm.xml";
            configuration.AddFile(funcionarioFile);

            var tarefaFile = configurationPath + @"Tarefa.hbm.xml";
            configuration.AddFile(tarefaFile);

            ISessionFactory sessionFactory = configuration.BuildSessionFactory();
            return sessionFactory.OpenSession();
        }
    }
}
