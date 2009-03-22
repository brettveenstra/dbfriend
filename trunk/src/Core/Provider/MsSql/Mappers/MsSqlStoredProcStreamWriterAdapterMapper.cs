// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="MsSqlStoredProcStreamWriterAdapterMapper.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the MsSqlStoredProcStreamWriterAdapterMapper type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
using System.IO;
using DbFriend.Core.Generator.Settings;
using DbFriend.Core.Generator.Targets;

namespace DbFriend.Core.Provider.MsSql.Mappers
{
    /// <summary>
    /// </summary>
    public class MsSqlStoredProcStreamWriterAdapterMapper : IMsSqlStoredProcStreamWriterAdapterMapper
    {
        /// <summary>
        /// </summary>
        private readonly IDbScriptFolderConfigurationSetting setting;

        /// <summary>
        /// Initializes a new instance of the <see cref="MsSqlStoredProcStreamWriterAdapterMapper"/> class.
        /// </summary>
        /// <param name="setting">
        /// The setting.
        /// </param>
        public MsSqlStoredProcStreamWriterAdapterMapper(IDbScriptFolderConfigurationSetting setting)
        {
            this.setting = setting;
        }

        #region IMsSqlStoredProcStreamWriterAdapterMapper Members

        /// <summary>
        /// </summary>
        /// <param name="from">
        /// The from.
        /// </param>
        /// <returns>
        /// </returns>
        public IDbObjectStreamWriterAdapter MapFrom(IMsSqlObject from)
        {
            string fileName = CleanOwnerDomain(from) + "." + from.Name + ".PRC";

            string outputFileName = Path.Combine(
                    Path.Combine(
                            Path.Combine(
                                    setting.OutputFolder, "DbScripts"),
                            "SPs"),
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