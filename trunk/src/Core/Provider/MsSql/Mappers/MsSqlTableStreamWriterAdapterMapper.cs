// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="MsSqlTableStreamWriterAdapterMapper.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the MsSqlTableStreamWriterAdapterMapper type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
using System.IO;
using DbFriend.Core.Generator.Settings;
using DbFriend.Core.Generator.Targets;

namespace DbFriend.Core.Provider.MsSql.Mappers
{
    /// <summary>
    /// </summary>
    public class MsSqlTableStreamWriterAdapterMapper : IMsSqlTableStreamWriterAdapterMapper
    {
        /// <summary>
        /// </summary>
        private readonly IDbScriptFolderConfigurationSetting setting;

        /// <summary>
        /// Initializes a new instance of the <see cref="MsSqlTableStreamWriterAdapterMapper"/> class.
        /// </summary>
        /// <param name="setting">
        /// The setting.
        /// </param>
        public MsSqlTableStreamWriterAdapterMapper(IDbScriptFolderConfigurationSetting setting)
        {
            this.setting = setting;
        }

        #region IMsSqlTableStreamWriterAdapterMapper Members

        /// <summary>
        /// </summary>
        /// <param name="from">
        /// The from.
        /// </param>
        /// <returns>
        /// </returns>
        public IDbObjectStreamWriterAdapter MapFrom(IMsSqlObject from)
        {
            string fileName = CleanOwnerDomain(from)
                              + "."
                              + from.Name
                              + ".TAB";

            string outputFileName = Path.Combine(
                    Path.Combine(
                            Path.Combine(
                                    setting.OutputFolder, "DbScripts"),
                            "Tables"),
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