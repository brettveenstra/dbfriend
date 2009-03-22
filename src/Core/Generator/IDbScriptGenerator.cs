// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="IDbScriptGenerator.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the IDbScriptGenerator type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------

using System;
using DbFriend.Core.Provider;

namespace DbFriend.Core.Generator
{
    public interface IDbScriptGenerator
    {
        void ScriptDb(Action<IDbScriptObjectUpdate> action);
    }
}