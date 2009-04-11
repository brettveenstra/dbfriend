// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="MsSqlDatabaseConnectionAdapter.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the MsSqlDatabaseConnectionAdapter type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace DbFriend.Core.Provider.MsSql.Adapters
{
    using System;
    using System.Collections.Generic;

    using DbFriend.Core.Provider.MsSql.Mappers;

    using Microsoft.SqlServer.Management.Common;
    using Microsoft.SqlServer.Management.Smo;

    /// <summary>
    /// </summary>
    public class MsSqlDatabaseConnectionAdapter : IMsSqlDatabaseConnectionAdapter
    {
        /// <summary>
        /// </summary>
        private readonly IUserDefinedFunctionAdapterSqlObjectMapper functionAdapterMapper;

        /// <summary>
        /// </summary>
        private readonly IMsSqlConnectionSettings settings;

        /// <summary>
        /// </summary>
        private readonly IStoredProcedureAdapterMsSqlObjectMapper storedProcMapper;

        /// <summary>
        /// </summary>
        private readonly ITableAdapterMsSqlObjectMapper tableAdapterMapper;

        /// <summary>
        /// </summary>
        private readonly IViewAdapterMsSqlObjectMapper viewAdapterMapper;

        /// <summary>
        /// </summary>
        private ServerConnection serverConnection;

        /// <summary>
        /// Initializes a new instance of the <see cref="MsSqlDatabaseConnectionAdapter"/> class.
        /// </summary>
        /// <param name="settings">
        /// The settings.
        /// </param>
        /// <param name="storedProcMapper">
        /// The stored Proc Mapper.
        /// </param>
        /// <param name="tableAdapterMapper">
        /// </param>
        /// <param name="viewAdapterMapper">
        /// </param>
        /// <param name="functionAdapterMapper">
        /// </param>
        public MsSqlDatabaseConnectionAdapter(
                IMsSqlConnectionSettings settings,
                IStoredProcedureAdapterMsSqlObjectMapper storedProcMapper,
                ITableAdapterMsSqlObjectMapper tableAdapterMapper,
                IViewAdapterMsSqlObjectMapper viewAdapterMapper,
                IUserDefinedFunctionAdapterSqlObjectMapper functionAdapterMapper)
        {
            this.settings = settings;
            this.functionAdapterMapper = functionAdapterMapper;
            this.viewAdapterMapper = viewAdapterMapper;
            this.tableAdapterMapper = tableAdapterMapper;
            this.storedProcMapper = storedProcMapper;
        }

        #region IMsSqlDatabaseConnectionAdapter Members

        /// <summary>
        /// Gets a value indicating whether SQL Server connection is open.
        /// </summary>
        /// <value>
        /// Returns true when underlying connection is open.  False when closed.
        /// </value>
        public bool IsOpen
        {
            get
            {
                if (this.serverConnection != null)
                {
                    return this.serverConnection.IsOpen;
                }

                return false;
            }
        }

        /// <summary>
        /// Gets StoredProcedures.
        /// </summary>
        /// <value>
        /// The stored procedures.
        /// </value>
        public IEnumerable<IMsSqlObject> StoredProcedures
        {
            get
            {
                this.MakeSureConnectionIsOpen();

                foreach (IStoredProcedureAdapter storedProcedure in this.GetDatabase().StoredProcedures)
                {
                    if (storedProcedure.IsSystemObject == false)
                    {
                        yield return this.storedProcMapper.MapFrom(storedProcedure);
                    }
                }
            }
        }

        /// <summary>
        /// Gets Tables.
        /// </summary>
        /// <value>
        /// The tables.
        /// </value>
        public IEnumerable<IMsSqlObject> Tables
        {
            get
            {
                this.MakeSureConnectionIsOpen();

                foreach (ITableAdapter table in this.GetDatabase().Tables)
                {
                    if (table.IsSystemObject == false)
                    {
                        yield return this.tableAdapterMapper.MapFrom(table);
                    }
                }
            }
        }

        /// <summary>
        /// Gets Views.
        /// </summary>
        /// <value>
        /// The views.
        /// </value>
        public IEnumerable<IMsSqlObject> Views
        {
            get
            {
                this.MakeSureConnectionIsOpen();

                foreach (IViewAdapter view in this.GetDatabase().Views)
                {
                    if (view.IsSystemObject == false)
                    {
                        yield return this.viewAdapterMapper.MapFrom(view);
                    }
                }
            }
        }

        /// <summary>
        /// Gets Functions.
        /// </summary>
        /// <value>
        /// The functions.
        /// </value>
        public IEnumerable<IMsSqlObject> Functions
        {
            get
            {
                this.MakeSureConnectionIsOpen();

                foreach (IUserDefinedFunctionAdapter userDefinedFunction in this.GetDatabase().UserDefinedFunctions)
                {
                    if (userDefinedFunction.IsSystemObject == false)
                    {
                        yield return this.functionAdapterMapper.MapFrom(userDefinedFunction);
                    }
                }
            }
        }

        /// <summary>
        /// </summary>
        public void Dispose()
        {
            if (this.serverConnection != null)
            {
                if (this.serverConnection.IsOpen)
                {
                    this.serverConnection.Disconnect();
                }
            }
        }

        /// <summary>
        /// Connects to the instance of SQL Server (calling ServerConnection.Connect())
        /// </summary>
        public void Connect()
        {
            if (this.serverConnection == null)
            {
                if (this.settings.Method == MsSqlCredentialMethod.SqlUser)
                {
                    this.serverConnection = new ServerConnection(this.settings.ServerInstance, this.settings.UserName, this.settings.Password);
                }
                else
                {
                    this.serverConnection = new ServerConnection(this.settings.ServerInstance);
                }
            }

            if (this.serverConnection.IsOpen == false)
            {
                this.serverConnection.Connect();
            }
        }

        /// <summary>
        /// Disconnects from the instace of SQL Server (calling ServerConnection.Disconnect())
        /// </summary>
        public void Disconnect()
        {
            if (this.serverConnection != null)
            {
                if (this.serverConnection.IsOpen)
                {
                    this.serverConnection.Disconnect();
                }
            }
        }

        #endregion

        /// <summary>
        /// </summary>
        /// <param name="server">
        /// The server.
        /// </param>
        private static void OptimizeSmoOperationsWithDefaultInitiFields(Server server)
        {
            // set the default properties we want upon partial instantiation - 
            // smo is *really* slow if you don't do this
            server.SetDefaultInitFields(typeof(Table), "IsSystemObject", "CreateDate");
            server.SetDefaultInitFields(typeof(StoredProcedure), "IsSystemObject", "CreateDate");
            server.SetDefaultInitFields(typeof(UserDefinedFunction), "IsSystemObject", "CreateDate", "FunctionType");
            server.SetDefaultInitFields(typeof(View), "IsSystemObject", "CreateDate");
            server.SetDefaultInitFields(typeof(Column), "Identity");
            server.SetDefaultInitFields(typeof(Index), "IndexKeyType");
        }

        /// <summary>
        /// </summary>
        private void MakeSureConnectionIsOpen()
        {
            if (!this.IsOpen)
            {
                this.Connect();
            }
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        private IDatabaseWrapper GetDatabase()
        {
            Server server = new Server(this.serverConnection);

            OptimizeSmoOperationsWithDefaultInitiFields(server);

            Database database = server.Databases[this.settings.DatabaseName];
            if (database == null)
            {
                throw new ArgumentOutOfRangeException(
                        string.Format("Database does not exist {0}.{1}", this.serverConnection.ServerInstance, this.settings.DatabaseName));
            }

            IDatabaseWrapper databaseWrapper = new DatabaseWrapper(database);
            return databaseWrapper;
        }
    }
}