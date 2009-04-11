// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="IDatabaseWrapper.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the IDatabaseWrapper type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------

namespace DbFriend.Core.Provider.MsSql.Adapters
{
    using System.Collections.Generic;

    /// <summary>
    /// </summary>
    public interface IDatabaseWrapper
    {
        /// <summary>
        /// Gets UserDefinedFunctions.
        /// </summary>
        /// <value>
        /// The user defined functions.
        /// </value>
        IEnumerable<IUserDefinedFunctionAdapter> UserDefinedFunctions { get; }

        /// <summary>
        /// Gets StoredProcedures.
        /// </summary>
        /// <value>
        /// The stored procedures.
        /// </value>
        IEnumerable<IStoredProcedureAdapter> StoredProcedures { get; }

        /// <summary>
        /// Gets Tables.
        /// </summary>
        /// <value>
        /// The tables.
        /// </value>
        IEnumerable<ITableAdapter> Tables { get; }

        /// <summary>
        /// Gets Views.
        /// </summary>
        /// <value>
        /// The views.
        /// </value>
        IEnumerable<IViewAdapter> Views { get; }
    }
}