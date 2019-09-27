using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Connection;


namespace DataObjectLayer
{
    /// <summary>
    /// Gerencia criação de sessionFactories com suas respectivas sessions anexadas.
    /// </summary>
    public class NHibernateManager
    {
        #region Fields

        private static NHibernateManager instance = null;

        private Configuration configuration;

        private string factoryDefaultKey = "factoryDefaultKey";

        private string sessionDefaultKey = "sessionDefaultKey";

        private Dictionary<string, RepositoryFactorySessions> repositorySessionFactoryList = new Dictionary<string, RepositoryFactorySessions>();

        private Dictionary<string, ISession> sessionList = new Dictionary<string,ISession>();

        #endregion

        #region Properties 

        #endregion

        #region Constructor

        private NHibernateManager()
        {
        }

        /// <summary>
        /// Singleton de NHibernateManager;
        /// </summary>
        public static NHibernateManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NHibernateManager();
                }
                
                return instance;
            }
        }

        #endregion

        #region Checks Methods

        private void checkExistSessionFactory(string factoryKey)
        {
            if (!repositorySessionFactoryList.ContainsKey(factoryKey))
            {
                throw new NotExistFactoryKeyException();
            }
        }

        private void createIfNotExistSessionFactory(string factoryKey)
        {
            if (!repositorySessionFactoryList.ContainsKey(factoryKey))
            {
                try
                {
                    repositorySessionFactoryList.Add(factoryKey, new RepositoryFactorySessions(configuration.BuildSessionFactory(), new Dictionary<string, ISession>()));
                }
                catch(NHibernate.HibernateException e)
                {
                    throw new Exception(e.Message, e);
                }
            }
        }

        private void checkExistSession(string factoryKey, string sessionKey)
        {
            if (!repositorySessionFactoryList[factoryKey].SessionList.ContainsKey(sessionKey))
            {
                throw new NotExistSessionKeyException();
            }
        }

        private void createIfNotExistSession(string factoryKey, string sessionKey)
        {
            if (!repositorySessionFactoryList[factoryKey].SessionList.ContainsKey(sessionKey))
            {
                repositorySessionFactoryList[factoryKey].SessionList.Add(sessionKey, repositorySessionFactoryList[factoryKey].SessionFactory.OpenSession());
            }
        }

        #endregion

        #region SessionFactory Methods

        /// <summary>
        /// Cria uma configuração de conexão baseada no App.config da aplicação corrente.
        /// </summary>
        public void CreateConfiguration()
        {
            CreateConfiguration(string.Empty);
        }

        /// <summary>
        /// Cria uma configuração de conexão baseada no App.config informado pelo parâmetro arqConfig.
        /// </summary>
        /// <param name="resource">Nome do arquivo App.config junto com seu path.</param>
        public void CreateConfiguration(string arqConfig)
        {
            if (arqConfig != string.Empty)
            {
                configuration = new Configuration().Configure(arqConfig);
            }
            else
            {
                configuration = new Configuration();
            }
        }

        private ISessionFactory createSessionFactory(string arqConfig)
        {
            if (arqConfig != string.Empty)
            {
                configuration = new Configuration().Configure(arqConfig);
            }
            else
            {
                configuration = new Configuration().Configure();
            }

            return configuration.BuildSessionFactory();
        }

        public void OpenSessionFactory()
        {
            OpenSessionFactory(factoryDefaultKey);
        }

        public void OpenSessionFactory(string factoryKey)
        {
            OpenSessionFactory("App.config", factoryKey);
        }

        public void OpenSessionFactory(string resource, string factoryKey)
        {
            ISessionFactory sessionFactory = createSessionFactory(resource);

            sessionList = new Dictionary<string, ISession>();

            sessionList.Add(sessionDefaultKey, sessionFactory.OpenSession());

            repositorySessionFactoryList = new Dictionary<string, RepositoryFactorySessions>();

            repositorySessionFactoryList.Add(factoryKey, new RepositoryFactorySessions(sessionFactory, sessionList));
        }

        public ISessionFactory GetSessionFactory()
        {
            return GetSessionFactory(factoryDefaultKey);
        }

        public ISessionFactory GetSessionFactory(string factoryKey)
        {
            checkExistSessionFactory(factoryKey);

            return repositorySessionFactoryList[factoryKey].SessionFactory;
        }

        public void CloseSessionFactory()
        {
            CloseSessionFactory(factoryDefaultKey);
        }

        public void CloseSessionFactory(string factoryKey)
        {
            checkExistSessionFactory(factoryKey);

            Dictionary<string, ISession>.ValueCollection.Enumerator enumerador = repositorySessionFactoryList[factoryKey].SessionList.Values.GetEnumerator();

            while (enumerador.MoveNext())
            {
                ISession session = enumerador.Current as ISession;

                session.Close();
            }

            repositorySessionFactoryList[factoryKey].SessionList.Clear();

            repositorySessionFactoryList[factoryKey].SessionFactory.Close();

            repositorySessionFactoryList.Remove(factoryKey);
        }

        #endregion

        #region Session Methods

        public void OpenSessionWithTransaction()
        {
            OpenSession(factoryDefaultKey, sessionDefaultKey, true);
        }

        public void OpenSession()
        {
            OpenSession(sessionDefaultKey);
        }

        public void OpenSession(string sessionKey)
        {
            OpenSession(factoryDefaultKey, sessionKey);
        }

        public void OpenSession(string factoryKey, string sessionKey)
        {
            OpenSession(factoryKey, sessionKey, false);
        }

        public void OpenSession(string factoryKey, string sessionKey, bool IsSessionWithTransaction)
        {
            createIfNotExistSessionFactory(factoryKey);

            ISession session;

            if (repositorySessionFactoryList[factoryKey].SessionList.ContainsKey(sessionKey))
            {
                session = repositorySessionFactoryList[factoryKey].SessionList[sessionKey];
            }
            else
            {
                session = repositorySessionFactoryList[factoryKey].SessionFactory.OpenSession();

                repositorySessionFactoryList[factoryKey].SessionList.Add(sessionKey, session);
            }

            if (IsSessionWithTransaction)
            {
                session.BeginTransaction();
            }
        }

        public ISession GetSession()
        {
            return GetSession(sessionDefaultKey);
        }

        public ISession GetSession(string sessionKey)
        {
            return GetSession(factoryDefaultKey, sessionKey);
        }

        public ISession GetSession(string factoryKey, string sessionKey)
        {
            createIfNotExistSessionFactory(factoryKey);

            createIfNotExistSession(factoryKey, sessionKey);

            ISession session = repositorySessionFactoryList[factoryKey].SessionList[sessionKey];

            if (!session.IsOpen)
            {
                session = repositorySessionFactoryList[factoryKey].SessionFactory.OpenSession();
            }

            return session;
        }

        public void CloseSession()
        {
            CloseSession(sessionDefaultKey);
        }

        public void CloseSession(string sessionKey)
        {
            CloseSession(factoryDefaultKey, sessionKey);
        }

        public void CloseSession(string factoryKey, string sessionKey)
        {
            checkExistSessionFactory(factoryKey);

            checkExistSession(factoryKey, sessionKey);

            try
            {
                repositorySessionFactoryList[factoryKey].SessionList[sessionKey].Close();

                repositorySessionFactoryList[factoryKey].SessionList.Remove(sessionKey);
            }
            catch(Exception E)
            {
                throw new CloseSessionException(E.Message);
            }
        }

        #endregion

        #region Transaction Methods

        public void BeginTransaction()
        {
            BeginTransaction(sessionDefaultKey);
        }

        public void BeginTransaction(string sessionKey)
        {
            BeginTransaction(factoryDefaultKey, sessionKey);
        }

        public void BeginTransaction(string factoryKey, string sessionKey)
        {
            checkExistSessionFactory(factoryKey);

            checkExistSession(factoryKey, sessionKey);

            repositorySessionFactoryList[factoryKey].SessionList[sessionKey].BeginTransaction();
        }

        public void CommitTransaction()
        {
            CommitTransaction(sessionDefaultKey);
        }

        public void CommitTransaction(string sessionKey)
        {
            CommitTransaction(factoryDefaultKey, sessionKey);
        }

        public void CommitTransaction(string factoryKey, string sessionKey)
        {
            checkExistSessionFactory(factoryKey);

            checkExistSession(factoryKey, sessionKey);

            ISession session = repositorySessionFactoryList[factoryKey].SessionList[sessionKey];

            session.Transaction.Commit();
        }

        public void RollbackTransaction()
        {
            RollbackTransaction(sessionDefaultKey);
        }

        public void RollbackTransaction(string sessionKey)
        {
            RollbackTransaction(factoryDefaultKey, sessionKey);
        }

        public void RollbackTransaction(string factoryKey, string sessionKey)
        {
            checkExistSessionFactory(factoryKey);

            checkExistSession(factoryKey, sessionKey);

            ISession session = repositorySessionFactoryList[factoryKey].SessionList[sessionKey];

            session.Transaction.Rollback();

            session.Clear();
        }

        #endregion
    }
}
