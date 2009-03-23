// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="MsSqlDatabaseConnectionAdapter.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the MsSqlDatabaseConnectionAdapter type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using DbFriend.Core.Provider.MsSql.Mappers;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using StructureMap;
using StructureMap.Pipeline;

namespace DbFriend.Core.Provider.MsSql.Adapters
{
    /// <summary>
    /// </summary>
    public class MsSqlDatabaseConnectionAdapter : IMsSqlDatabaseConnectionAdapter
    {

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
        private readonly IUserDefinedFunctionAdapterSqlObjectMapper functionAdapterMapper;

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
                if (serverConnection != null)
                {
                    return serverConnection.IsOpen;
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
                MakeSureConnectionIsOpen();

                foreach (IStoredProcedureAdapter storedProcedure in GetDatabase().StoredProcedures)
                {
                    if (storedProcedure.IsSystemObject == false)
                    {
                        yield return storedProcMapper.MapFrom(storedProcedure);
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
                MakeSureConnectionIsOpen();

                foreach (ITableAdapter table in GetDatabase().Tables)
                {
                    if (table.IsSystemObject == false)
                    {
                        yield return tableAdapterMapper.MapFrom(table);
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
                MakeSureConnectionIsOpen();

                foreach (IViewAdapter view in GetDatabase().Views)
                {
                    if (view.IsSystemObject == false)
                    {
                        yield return viewAdapterMapper.MapFrom(view);
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
                MakeSureConnectionIsOpen();

                foreach (IUserDefinedFunctionAdapter userDefinedFunction in GetDatabase().UserDefinedFunctions)
                {
                    if (userDefinedFunction.IsSystemObject == false)
                    {
                        yield return functionAdapterMapper.MapFrom(userDefinedFunction);
                    }
                }
            }
        }

        /// <summary>
        /// </summary>
        public void Dispose()
        {
            if (serverConnection != null)
            {
                if (serverConnection.IsOpen)
                {
                    serverConnection.Disconnect();
                }
            }
        }

        /// <summary>
        /// Connects to the instance of SQL Server (calling ServerConnection.Connect())
        /// </summary>
        public void Connect()
        {
            if (serverConnection == null)
            {
                if (settings.Method == MsSqlCredentialMethod.SqlUser)
                {
                    serverConnection = new ServerConnection(settings.ServerInstance, settings.UserName, settings.Password);
                }
                else
                {
                    serverConnection = new ServerConnection(settings.ServerInstance);
                }
            }

            if (serverConnection.IsOpen == false)
            {
                serverConnection.Connect();
            }
        }

        /// <summary>
        /// Disconnects from the instace of SQL Server (calling ServerConnection.Disconnect())
        /// </summary>
        public void Disconnect()
        {
            if (serverConnection != null)
            {
                if (serverConnection.IsOpen)
                {
                    serverConnection.Disconnect();
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
            server.SetDefaultInitFields(typeof (Table), "IsSystemObject", "CreateDate");
            server.SetDefaultInitFields(typeof (StoredProcedure),
                                        "IsSystemObject",
                                        "CreateDate");
            server.SetDefaultInitFields(typeof (UserDefinedFunction),
                                        "IsSystemObject",
                                        "CreateDate",
                                        "FunctionType");
            server.SetDefaultInitFields(typeof (View), "IsSystemObject", "CreateDate");
            server.SetDefaultInitFields(typeof (Column), "Identity");
            server.SetDefaultInitFields(typeof (Index), "IndexKeyType");
        }

        /// <summary>
        /// </summary>
        private void MakeSureConnectionIsOpen()
        {
            if (!IsOpen)
            {
                Connect();
            }
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        private IDatabaseWrapper GetDatabase()
        {
            Server server = new Server(serverConnection);

            OptimizeSmoOperationsWithDefaultInitiFields(server);

            Database database = server.Databases[settings.DatabaseName];
            if (database == null)
            {
                throw new ArgumentOutOfRangeException(string.Format("Database does not exist {0}.{1}", serverConnection.ServerInstance, settings.DatabaseName));
            }

            IDatabaseWrapper databaseWrapper = new DatabaseWrapper(database);
            return databaseWrapper;

        }
    }
}