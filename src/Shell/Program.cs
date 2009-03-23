// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="Program.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the Program type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------

using System;
using System.Windows.Forms;
using DbFriend.Core;
using StructureMap;

namespace DbFriendShell
{
    /// <summary>
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ObjectFactory.Initialize(x => x.AddRegistry<CoreStackRegistry>());

            Application.Run(new MainForm());
        }
    }
}