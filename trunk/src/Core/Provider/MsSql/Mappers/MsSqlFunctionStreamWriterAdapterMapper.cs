// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="MsSqlFunctionStreamWriterAdapterMapper.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the MsSqlFunctionStreamWriterAdapterMapper type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
using System.IO;
using DbFriend.Core.Generator.Settings;
using DbFriend.Core.Generator.Targets;

namespace DbFriend.Core.Provider.MsSql.Mappers
{
    /// <summary>
    /// </summary>
    public class MsSqlFunctionStreamWriterAdapterMapper : IMsSqlFunctionStreamWriterAdapterMapper
    {
        /// <summary>
        /// </summary>
        private readonly IDbScriptFolderConfigurationSetting setting;

        /// <summary>
        /// Initializes a new instance of the <see cref="MsSqlFunctionStreamWriterAdapterMapper"/> class.
        /// </summary>
        /// <param name="setting">
        /// The setting.
        /// </param>
        public MsSqlFunctionStreamWriterAdapterMapper(IDbScriptFolderConfigurationSetting setting)
        {
            this.setting = setting;
        }

        #region IMsSqlFunctionStreamWriterAdapterMapper Members

        /// <summary>
        /// </summary>
        /// <param name="from">
        /// The from.
        /// </param>
        /// <returns>
        /// </returns>
        public IDbObjectStreamWriterAdapter MapFrom(IMsSqlObject from)
        {
            string fileName = CleanOwnerDomain(from) + "." + from.Name + ".UDF";


            string outputFileName = Path.Combine(
                    Path.Combine(
                            Path.Combine(
                                    setting.OutputFolder, "DbScripts"),
                            "UDFs"),
                    fileName);

            return new DbObjectStreamWriterAdapter(from, outputFileName);
        }

        #endregion

        /// <summary>
        /// </summary>
        /// <param name="from">
        /// The from.
        /// </param>
        /// <returns>
        /// </returns>
        private string CleanOwnerDomain(IMsSqlObject from)
        {
            return from.Owner.Replace('\\', '_');
        }
    }
}