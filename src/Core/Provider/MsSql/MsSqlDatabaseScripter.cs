// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="MsSqlDatabaseScripter.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the MsSqlDatabaseScripter type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using DbFriend.Core.Generator.Targets;
using DbFriend.Core.Provider.MsSql.Adapters;
using DbFriend.Core.Provider.MsSql.Mappers;

namespace DbFriend.Core.Provider.MsSql
{
    /// <summary>
    /// </summary>
    public class MsSqlDatabaseScripter : IMsSqlDatabaseScripter
    {
        /// <summary>
        /// </summary>
        private readonly IMsSqlDatabaseConnectionAdapter connectionAdapter;

        /// <summary>
        /// </summary>
        private readonly IMsSqlFunctionStreamWriterAdapterMapper functionMapper;

        /// <summary>
        /// </summary>
        private readonly IMsSqlStoredProcStreamWriterAdapterMapper storedProcMapper;

        /// <summary>
        /// </summary>
        private readonly IMsSqlTableStreamWriterAdapterMapper tableMapper;

        /// <summary>
        /// </summary>
        private readonly IMsSqlViewStreamWriterAdapterMapper viewMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MsSqlDatabaseScripter"/> class.
        /// </summary>
        /// <param name="connectionAdapter">
        /// The connection adapter.
        /// </param>
        /// <param name="storedProcMapper">
        /// The storedProcMapper.
        /// </param>
        /// <param name="tableMapper">
        /// </param>
        /// <param name="viewMapper">
        /// </param>
        /// <param name="functionMapper">
        /// </param>
        public MsSqlDatabaseScripter(
                IMsSqlDatabaseConnectionAdapter connectionAdapter,
                IMsSqlStoredProcStreamWriterAdapterMapper storedProcMapper,
                IMsSqlTableStreamWriterAdapterMapper tableMapper,
                IMsSqlViewStreamWriterAdapterMapper viewMapper,
                IMsSqlFunctionStreamWriterAdapterMapper functionMapper)
        {
            this.connectionAdapter = connectionAdapter;
            this.functionMapper = functionMapper;
            this.viewMapper = viewMapper;
            this.tableMapper = tableMapper;
            this.storedProcMapper = storedProcMapper;
        }

        #region IMsSqlDatabaseScripter Members

        /// <summary>
        /// </summary>
        /// <param name="outputPipeline">
        /// The output target.
        /// </param>
        /// <param name="notifyAction">
        /// The notify Action.
        /// </param>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public void ScriptTo(IDbScriptOutputPipeline outputPipeline, Action<IDbScriptObjectUpdate> notifyAction)
        {
            using (connectionAdapter)
            {
                connectionAdapter.Connect();

                notifyAction.Invoke(new DbScriptObjectUpdate("Gathering Stored Procs..."));
                WirePipelineWith(outputPipeline, connectionAdapter.StoredProcedures, storedProcMapper);

                notifyAction.Invoke(new DbScriptObjectUpdate("Gathering Tables..."));
                WirePipelineWith(outputPipeline, connectionAdapter.Tables, tableMapper);

                notifyAction.Invoke(new DbScriptObjectUpdate("Gathering Views..."));
                WirePipelineWith(outputPipeline, connectionAdapter.Views, viewMapper);

                notifyAction.Invoke(new DbScriptObjectUpdate("Gathering Functions..."));
                WirePipelineWith(outputPipeline, connectionAdapter.Functions, functionMapper);

                FlushPipeline(outputPipeline, notifyAction);
            }
        }

        #endregion

        /// <summary>
        /// </summary>
        /// <param name="outputPipeline">
        /// The output pipeline.
        /// </param>
        /// <param name="mssqlObjects">
        /// The mssql objects.
        /// </param>
        /// <param name="mapper">
        /// The mapper.
        /// </param>
        private void WirePipelineWith(
                IDbScriptOutputPipeline outputPipeline,
                IEnumerable<IMsSqlObject> mssqlObjects,
                IMsSqlObjectStreamWriterAdapterMapper mapper)
        {
            foreach (IMsSqlObject sqlObject in mssqlObjects)
            {
                outputPipeline.WireIn(mapper.MapFrom(sqlObject));
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="outputPipeline">
        /// The output pipeline.
        /// </param>
        /// <param name="notifyAction">
        /// The notify action.
        /// </param>
        private void FlushPipeline(IDbScriptOutputPipeline outputPipeline, Action<IDbScriptObjectUpdate> notifyAction)
        {
            foreach (string message in outputPipeline.Flush())
            {
                notifyAction.Invoke(new DbScriptObjectUpdate(message));
            }
        }
    }
}