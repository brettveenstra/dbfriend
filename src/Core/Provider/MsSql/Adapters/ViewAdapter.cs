using System.Collections.Specialized;
using Microsoft.SqlServer.Management.Smo;

namespace DbFriend.Core.Provider.MsSql.Adapters
{
    public class ViewAdapter : IViewAdapter
    {
        private readonly View view;

        public ViewAdapter(View view)
        {
            this.view = view;
        }

        #region IViewAdapter Members

        public string Urn
        {
            get { return view.Urn; }
        }

        public bool IsSystemObject
        {
            get { return view.IsSystemObject; }
        }

        public string Name
        {
            get { return view.Name; }
        }

        public string Owner
        {
            get { return view.Owner; }
        }

        public StringCollection Script(ScriptingOptions options)
        {
            return view.Script(options);
        }

        #endregion
    }
}