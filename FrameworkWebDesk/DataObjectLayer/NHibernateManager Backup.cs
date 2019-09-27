using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Connection;
using DataObjectLayer.Exceptions;

namespace DataObjectLayer
{
    internal sealed class FactorySessions
    {
        public ISessionFactory SessionFactory;

        public Dictionary<string, ISession> SessionList;

        public FactorySessions(ISessionFactory sessionFactory, Dictionary<string, ISession> sessionList)
        {
            this.SessionFactory = sessionFactory;
            this.SessionList = sessionList;
        }
    }

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

        private Dictionary<string, FactorySessions> sessionFactoryList = new Dictionary<string,FactorySessions>();

        private Dictionary<string, ISession> sessionList = new Dictionary<string,ISession>();

        #endregion

        #region Constructor

        private NHibernateManager()
        {
        }

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
            if (!sessionFactoryList.ContainsKey(factoryKey))
            {
                sessionFactoryList.Add(factoryKey, new FactorySessions(configuration.BuildSessionFactory(),new Dictionary<string,ISession>()));
            }
        }

        private void checkExistSession(string factoryKey, string sessionKey)
        {
            if (!sessionFactoryList[factoryKey].SessionList.ContainsKey(sessionKey))
            {
                sessionFactoryList[factoryKey].SessionList.Add(sessionKey, sessionFactoryList[factoryKey].SessionFactory.OpenSession());
            }
        }

        #endregion

        #region SessionFactory Methods

        public void CreateConfiguration()
        {
            CreateConfiguration(string.Empty);
        }

        public void CreateConfiguration(string resource)
        {
            if (resource != string.Empty)
            {
                configuration = new Configuration().Configure(resource);
            }
            else
            {
                configuration = new Configuration();
            }
        }

        private ISessionFactory createSessionFactory(string resource)
        {
            if (resource != string.Empty)
            {
                configuration = new Configuration().Configure(resource);

                return configuration.BuildSessionFactory();
            }
            else
            {
                configuration = new Configuration().Configure();

                return configuration.BuildSessionFactory();
            }
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

            sessionFactoryList = new Dictionary<string, FactorySessions>();

            sessionFactoryList.Add(factoryKey, new FactorySessions(sessionFactory, sessionList));
        }

        public ISessionFactory getSessionFactory()
        {
            return getSessionFactory(factoryDefaultKey);
        }

        public ISessionFactory getSessionFactory(string factoryKey)
        {
            checkExistSessionFactory(factoryKey);

            return sessionFactoryList[factoryKey].SessionFactory;
        }

        public void CloseSessionFactory()
        {
            CloseSessionFactory(factoryDefaultKey);
        }

        public void CloseSessionFactory(string factoryKey)
        {
            checkExistSessionFactory(factoryKey);

            Dictionary<string, ISession>.ValueCollection.Enumerator enumerador = sessionFactoryList[factoryKey].SessionList.Values.GetEnumerator();

            while (enumerador.MoveNext())
            {
                ISession session = enumerador.Current as ISession;

                session.Close();
            }

            sessionFactoryList[factoryKey].SessionList.Clear();

            sessionFactoryList[factoryKey].SessionFactory.Close();

            sessionFactoryList.Remove(factoryKey);
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
            checkExistSessionFactory(factoryKey);

            ISession session;

            if (sessionFactoryList[factoryKey].SessionList.ContainsKey(sessionKey))
            {
                session = sessionFactoryList[factoryKey].SessionList[sessionKey];
            }
            else
            {
                session = sessionFactoryList[factoryKey].SessionFactory.OpenSession();

                sessionFactoryList[factoryKey].SessionList.Add(sessionKey, session);
            }

            if (IsSessionWithTransaction)
            {
                session.BeginTransaction();
            }
        }

        public ISession getSession()
        {
            return getSession(sessionDefaultKey);
        }

        public ISession getSession(string sessionKey)
        {
            return getSession(factoryDefaultKey, sessionKey);
        }

        public ISession getSession(string factoryKey, string sessionKey)
        {
            checkExistSessionFactory(factoryKey);

            checkExistSession(factoryKey, sessionKey);

            return sessionFactoryList[factoryKey].SessionList[sessionKey];
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
                sessionFactoryList[factoryKey].SessionList[sessionKey].Close();

                sessionFactoryList[factoryKey].SessionList.Remove(sessionKey);
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

            sessionFactoryList[factoryKey].SessionList[sessionKey].BeginTransaction();
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

            ISession session = sessionFactoryList[factoryKey].SessionList[sessionKey];

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

            ISession session = sessionFactoryList[factoryKey].SessionList[sessionKey];

            session.Transaction.Rollback();

            session.Clear();
        }

        #endregion
    }
}
