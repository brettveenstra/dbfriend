using System;
using System.Collections.Specialized;
using Microsoft.SqlServer.Management.Smo;

namespace DbFriend.Core.Provider.MsSql.Adapters
{
    public class UserDefinedFunctionAdapter : IUserDefinedFunctionAdapter
    {
        private readonly UserDefinedFunction function;

        public UserDefinedFunctionAdapter(UserDefinedFunction function)
        {
            this.function = function;
        }

        public string Urn
        {
            get { return function.Urn;  }
        }

        public bool IsSystemObject
        {
            get { return function.IsSystemObject; }
        }

        public string Name
        {
            get { return function.Name; }
        }

        public string Owner
        {
            get { return function.Owner; }
        }

        public StringCollection Script(ScriptingOptions options)
        {
            return function.Script(options);
        }
    }
}