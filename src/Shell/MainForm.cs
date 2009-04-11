// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="MainForm.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the MainForm type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace DbFriendShell
{
    using System;
    using System.IO;
    using System.Windows.Forms;

    using DbFriend.Core.Generator;
    using DbFriend.Core.Provider.MsSql;
    using DbFriend.Shell.Properties;

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
            this.InitializeComponent();
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
            Cursor originalCursor = this.Cursor;

            this.Cursor = Cursors.WaitCursor;

            string finalOutputPath = Path.Combine(
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "DbFriend"), this.databaseNameTextBox.Text);

            this.statusTextBox.Text = string.Format("Generating to: {0}", finalOutputPath) + Environment.NewLine;
            Application.DoEvents();

            MsSqlServerRegistry registry;
            if (this.integratedSecurityCheckBox.Checked)
            {
                registry = new MsSqlServerRegistry(this.serverNameTextBox.Text, this.databaseNameTextBox.Text);
            }
            else
            {
                registry = new MsSqlServerRegistry(
                        this.serverNameTextBox.Text, this.databaseNameTextBox.Text, this.userNameTextBox.Text, this.passwordTextBox.Text);
            }

            IDbFriendGenerator generator = new DbFriendGenerator(registry, new DbFriendGeneratorRegistry(finalOutputPath));

            generator.Generate(
                    update =>
                    {
                        this.statusTextBox.Text += update.UpdateMessage + Environment.NewLine;

                        this.statusTextBox.Focus();
                        this.statusTextBox.SelectionStart = this.statusTextBox.Text.Length + 1;
                        this.statusTextBox.SelectionLength = 0;
                        this.statusTextBox.ScrollToCaret();

                        this.mainToolStripStatusLabel.Text = update.UpdateMessage;

                        Application.DoEvents();
                    });

            this.mainToolStripStatusLabel.Text = "Finished";

            this.Cursor = originalCursor;

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
            this.mainToolStripStatusLabel.Text = string.Empty;

            this.LoadConfiguredValues();
        }

        /// <summary>
        /// Lookup Configuration Values as defaults
        /// </summary>
        private void LoadConfiguredValues()
        {
            this.serverNameTextBox.Text = Settings.Default.server;
            this.databaseNameTextBox.Text = Settings.Default.db;

            this.userNameTextBox.Text = Settings.Default.userid;
            this.passwordTextBox.Text = Settings.Default.pwd;
        }

        /// <summary>
        /// Checked Changed handler for Integrated Security
        /// </summary>
        /// <param name="sender">
        /// The sender object.
        /// </param>
        /// <param name="e">
        /// The EventArgs e.
        /// </param>
        private void IntegratedSecurityCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.userNameTextBox.Enabled = !this.integratedSecurityCheckBox.Checked;
            this.passwordTextBox.Enabled = !this.integratedSecurityCheckBox.Checked;
        }
    }
}