// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="MainForm.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the MainForm type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
using System;
using System.IO;
using System.Windows.Forms;
using DbFriend.Core.Generator;
using DbFriend.Core.Provider.MsSql;
using DbFriend.Shell.Properties;

namespace DbFriendShell
{
    /// <summary>
    /// Main Form to show
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Fires up the DbFriendGenerator
        /// </summary>
        /// <param name="sender">
        /// The sender of the event.
        /// </param>
        /// <param name="e">
        /// The e for EventArgs.
        /// </param>
        private void ScriptDbButton_Click(object sender, EventArgs e)
        {
            Cursor originalCursor = Cursor;

            Cursor = Cursors.WaitCursor;

            string finalOutputPath = Path.Combine(
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Personal), "DbFriend"),
                databaseNameTextBox.Text);

            statusTextBox.Text = string.Format("Generating to: {0}", finalOutputPath) + Environment.NewLine;
            Application.DoEvents();

            MsSqlServerRegistry registry;
            if (integratedSecurityCheckBox.Checked)
            {
                registry = new MsSqlServerRegistry(
                    serverNameTextBox.Text,
                    databaseNameTextBox.Text);
            }
            else
            {
                registry = new MsSqlServerRegistry(
                    serverNameTextBox.Text,
                    databaseNameTextBox.Text,
                    userNameTextBox.Text,
                    passwordTextBox.Text);
            }

            IDbFriendGenerator generator = new DbFriendGenerator(
                registry,
                new DbFriendGeneratorRegistry(finalOutputPath));

            generator.Generate(update =>
                                   {
                                       statusTextBox.Text += update.UpdateMessage + Environment.NewLine;

                                       statusTextBox.Focus();
                                       statusTextBox.SelectionStart = statusTextBox.Text.Length + 1;
                                       statusTextBox.SelectionLength = 0;
                                       statusTextBox.ScrollToCaret();

                                       mainToolStripStatusLabel.Text = update.UpdateMessage;

                                       Application.DoEvents();
                                   });

            mainToolStripStatusLabel.Text = "Finished";

            Cursor = originalCursor;

            MessageBox.Show("All Done!");
        }

        /// <summary>
        /// Load the Main Form
        /// </summary>
        /// <param name="sender">
        /// The sender of the event.
        /// </param>
        /// <param name="e">
        /// The e of the EventArgs.
        /// </param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            mainToolStripStatusLabel.Text = string.Empty;

            LoadConfiguredValues();
        }

        /// <summary>
        /// Lookup Configuration Values as defaults
        /// </summary>
        private void LoadConfiguredValues()
        {
            serverNameTextBox.Text = Settings.Default.server;
            databaseNameTextBox.Text = Settings.Default.db;

            userNameTextBox.Text = Settings.Default.userid;
            passwordTextBox.Text = Settings.Default.pwd;
        }

        private void integratedSecurityCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            userNameTextBox.Enabled = !integratedSecurityCheckBox.Checked;
            passwordTextBox.Enabled = !integratedSecurityCheckBox.Checked;
        }
    }
}