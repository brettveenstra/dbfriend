// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="IMsSqlDatabaseConnectionAdapter.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the IMsSqlDatabaseConnectionAdapter type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace DbFriend.Core.Provider.MsSql.Adapters
{
    /// <summary>
    /// </summary>
    public interface IMsSqlDatabaseConnectionAdapter : IDisposable
    {
        /// <summary>
        /// Gets a value indicating whether IsOpen.
        /// </summary>
        /// <value>
        /// The is open.
        /// </value>
        bool IsOpen { get; }

        /// <summary>
        /// Gets StoredProcedures.
        /// </summary>
        /// <value>
        /// The stored procedures.
        /// </value>
        IEnumerable<IMsSqlObject> StoredProcedures { get; }

        /// <summary>
        /// Gets Tables.
        /// </summary>
        /// <value>
        /// The tables.
        /// </value>
        IEnumerable<IMsSqlObject> Tables { get; }

        /// <summary>
        /// Gets Views.
        /// </summary>
        /// <value>
        /// The views.
        /// </value>
        IEnumerable<IMsSqlObject> Views { get; }

        /// <summary>
        /// Gets Functions.
        /// </summary>
        /// <value>
        /// The functions.
        /// </value>
        IEnumerable<IMsSqlObject> Functions { get; }

        /// <summary>
        /// </summary>
        void Connect();

        /// <summary>
        /// </summary>
        void Disconnect();
    }
}