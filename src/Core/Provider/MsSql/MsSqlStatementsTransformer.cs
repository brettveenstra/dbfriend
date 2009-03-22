using System;
using System.Text;

namespace DbFriend.Core.Provider.MsSql
{
    public class MsSqlStatementsTransformer : IMsSqlStatementsTransformer
    {
        public void Process(string line, StringBuilder builder)
        {
            line = CleanLineStart(line);

            if (line.StartsWith("GRANT ", StringComparison.OrdinalIgnoreCase))
            {
                builder.AppendLine("GO" + Environment.NewLine);
            }

            builder.AppendLine(line);

            if (line.ToLower().Contains("ANSI_NULLS".ToLower()))
            {
                builder.AppendLine("GO" + Environment.NewLine);
            }

            if (line.ToLower().Contains("QUOTED_IDENTIFIER".ToLower()))
            {
                builder.AppendLine("GO" + Environment.NewLine);
            }
        }

        private string CleanLineStart(string line)
        {
            char[] extraNewLineChars = new[]{'\r', '\n'};
            line = line.TrimStart(extraNewLineChars);
            return line;
        }
    }
}