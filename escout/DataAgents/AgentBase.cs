using escout.Helpers;
using Npgsql;
using System;
using System.Transactions;

namespace escout.DataAgents
{
    public class AgentBase : IDisposable
    {
        private NpgsqlConnection npgsqlConnection;
        private DataContext dataContext;
        private int dbConnTimeout = 60;

        public AgentBase()
        {
            OnInitializeSqlConnection(out npgsqlConnection);
            OnInitializeDataContext(out dataContext);
        }
        public AgentBase(NpgsqlConnection npgsqlConnection)
        {
            this.npgsqlConnection = npgsqlConnection;
            OnInitializeDataContext(out dataContext);
        }

        public AgentBase(AgentBase agent)
        {
            if (agent != null)
            {
                npgsqlConnection = agent.Connection;
            }
            else
            {
                OnInitializeSqlConnection(out npgsqlConnection);
            }

            OnInitializeDataContext(out dataContext);

            if (agent != null)
            {
                dataContext.dbConnectionString = agent.dataContext.dbConnectionString;
            }
        }

        ~AgentBase()
        {
            Dispose();
        }

        public int DbConnTimeout
        {
            get { return dbConnTimeout; }
            set { dbConnTimeout = value; }
        }

        protected virtual void OnInitializeSqlConnection(out NpgsqlConnection npgsqlConnection)
        {
            npgsqlConnection = GetConnection();
        }

        protected virtual void OnInitializeDataContext(out DataContext dataContext)
        {
            dataContext = new DataContext();
            dataContext.dbConnectionString = npgsqlConnection.ConnectionString;
        }

        public NpgsqlConnection Connection
        {
            get
            {
                return (npgsqlConnection);
            }
        }

        public DataContext DataContext
        {
            get
            {
                return (dataContext);
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        private NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(Configurations.getNpgsqlConnectionString());
        }

        public TransactionScope CreateTransactionScope()
        {
            return (new TransactionScope(TransactionScopeOption.Required, new TransactionOptions()
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted,
                Timeout = TransactionManager.MaximumTimeout
            }));
        }
    }
}
