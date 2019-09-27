using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;

namespace DataObjectLayer
{
    internal sealed class RepositoryFactorySessions
    {
        public ISessionFactory SessionFactory;

        public Dictionary<string, ISession> SessionList;

        public RepositoryFactorySessions(ISessionFactory sessionFactory, Dictionary<string, ISession> sessionList)
        {
            this.SessionFactory = sessionFactory;
            this.SessionList = sessionList;
        }
    }
}
