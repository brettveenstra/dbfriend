// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="MsSqlStatementsTransformer.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the MsSqlStatementsTransformer type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------

namespace DbFriend.Core.Provider.MsSql
{
    using System;
    using System.Text;

    /// <summary>
    /// </summary>
    public class MsSqlStatementsTransformer : IMsSqlStatementsTransformer
    {
        #region IMsSqlStatementsTransformer Members

        /// <summary>
        /// </summary>
        /// <param name="line">
        /// The line.
        /// </param>
        /// <param name="builder">
        /// The builder.
        /// </param>
        public void Process(string line, StringBuilder builder)
        {
            line = this.CleanLineStart(line);

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

        #endregion

        /// <summary>
        /// </summary>
        /// <param name="line">
        /// The line.
        /// </param>
        /// <returns>
        /// </returns>
        private string CleanLineStart(string line)
        {
            char[] extraNewLineChars = new[] { '\r', '\n' };
            line = line.TrimStart(extraNewLineChars);
            return line;
        }
    }
}