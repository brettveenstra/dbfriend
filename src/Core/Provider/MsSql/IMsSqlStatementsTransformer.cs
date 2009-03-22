using System.Text;

namespace DbFriend.Core.Provider.MsSql
{
    public interface IMsSqlStatementsTransformer
    {
        void Process(string line, StringBuilder builder);
    }
}