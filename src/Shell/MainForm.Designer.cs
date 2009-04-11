namespace DbFriendShell
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.serverNameTextBox = new System.Windows.Forms.TextBox();
            this.databaseNameTextBox = new System.Windows.Forms.TextBox();
            this.userNameTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.scriptDbButton = new System.Windows.Forms.Button();
            this.vstudioVersionRadioButton = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.statusTextBox = new System.Windows.Forms.TextBox();
            this.databaseTargetRadioButton = new System.Windows.Forms.RadioButton();
            this.mainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.mainToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.integratedSecurityCheckBox = new System.Windows.Forms.CheckBox();
            this.mainStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // serverNameTextBox
            // 
            this.serverNameTextBox.Location = new System.Drawing.Point(145, 12);
            this.serverNameTextBox.Name = "serverNameTextBox";
            this.serverNameTextBox.Size = new System.Drawing.Size(218, 20);
            this.serverNameTextBox.TabIndex = 0;
            // 
            // databaseNameTextBox
            // 
            this.databaseNameTextBox.Location = new System.Drawing.Point(145, 38);
            this.databaseNameTextBox.Name = "databaseNameTextBox";
            this.databaseNameTextBox.Size = new System.Drawing.Size(218, 20);
            this.databaseNameTextBox.TabIndex = 1;
            // 
            // userNameTextBox
            // 
            this.userNameTextBox.Enabled = false;
            this.userNameTextBox.Location = new System.Drawing.Point(145, 106);
            this.userNameTextBox.Name = "userNameTextBox";
            this.userNameTextBox.Size = new System.Drawing.Size(218, 20);
            this.userNameTextBox.TabIndex = 2;
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Enabled = false;
            this.passwordTextBox.Location = new System.Drawing.Point(145, 132);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '*';
            this.passwordTextBox.Size = new System.Drawing.Size(218, 20);
            this.passwordTextBox.TabIndex = 3;
            // 
            // scriptDbButton
            // 
            this.scriptDbButton.Location = new System.Drawing.Point(557, 105);
            this.scriptDbButton.Name = "scriptDbButton";
            this.scriptDbButton.Size = new System.Drawing.Size(117, 23);
            this.scriptDbButton.TabIndex = 5;
            this.scriptDbButton.Text = "&Script";
            this.scriptDbButton.UseVisualStyleBackColor = true;
            this.scriptDbButton.Click += new System.EventHandler(this.ScriptDbButton_Click);
            // 
            // vstudioVersionRadioButton
            // 
            this.vstudioVersionRadioButton.AutoSize = true;
            this.vstudioVersionRadioButton.Checked = true;
            this.vstudioVersionRadioButton.Location = new System.Drawing.Point(491, 64);
            this.vstudioVersionRadioButton.Name = "vstudioVersionRadioButton";
            this.vstudioVersionRadioButton.Size = new System.Drawing.Size(66, 17);
            this.vstudioVersionRadioButton.TabIndex = 6;
            this.vstudioVersionRadioButton.TabStop = true;
            this.vstudioVersionRadioButton.Text = "VS 2008";
            this.vstudioVersionRadioButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(36, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "Server DbObjectName:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(39, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Database:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(39, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "User DbObjectName:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(39, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Password:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // statusTextBox
            // 
            this.statusTextBox.Location = new System.Drawing.Point(12, 208);
            this.statusTextBox.Multiline = true;
            this.statusTextBox.Name = "statusTextBox";
            this.statusTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.statusTextBox.Size = new System.Drawing.Size(732, 141);
            this.statusTextBox.TabIndex = 11;
            // 
            // databaseTargetRadioButton
            // 
            this.databaseTargetRadioButton.AutoSize = true;
            this.databaseTargetRadioButton.Checked = true;
            this.databaseTargetRadioButton.Location = new System.Drawing.Point(491, 41);
            this.databaseTargetRadioButton.Name = "databaseTargetRadioButton";
            this.databaseTargetRadioButton.Size = new System.Drawing.Size(93, 17);
            this.databaseTargetRadioButton.TabIndex = 12;
            this.databaseTargetRadioButton.TabStop = true;
            this.databaseTargetRadioButton.Text = "MS Sql Server";
            this.databaseTargetRadioButton.UseVisualStyleBackColor = true;
            // 
            // mainStatusStrip
            // 
            this.mainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainToolStripStatusLabel});
            this.mainStatusStrip.Location = new System.Drawing.Point(0, 356);
            this.mainStatusStrip.Name = "mainStatusStrip";
            this.mainStatusStrip.Size = new System.Drawing.Size(756, 22);
            this.mainStatusStrip.SizingGrip = false;
            this.mainStatusStrip.TabIndex = 14;
            // 
            // mainToolStripStatusLabel
            // 
            this.mainToolStripStatusLabel.Name = "mainToolStripStatusLabel";
            this.mainToolStripStatusLabel.Size = new System.Drawing.Size(142, 17);
            this.mainToolStripStatusLabel.Text = "mainToolStripStatusLabel";
            // 
            // integratedSecurityCheckBox
            // 
            this.integratedSecurityCheckBox.AutoSize = true;
            this.integratedSecurityCheckBox.Checked = true;
            this.integratedSecurityCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.integratedSecurityCheckBox.Location = new System.Drawing.Point(145, 83);
            this.integratedSecurityCheckBox.Name = "integratedSecurityCheckBox";
            this.integratedSecurityCheckBox.Size = new System.Drawing.Size(115, 17);
            this.integratedSecurityCheckBox.TabIndex = 15;
            this.integratedSecurityCheckBox.Text = "Integrated Security";
            this.integratedSecurityCheckBox.UseVisualStyleBackColor = true;
            this.integratedSecurityCheckBox.CheckedChanged += new System.EventHandler(this.IntegratedSecurityCheckBox_CheckedChanged);
            // 
            // MainForm
            // 
            this.AcceptButton = this.scriptDbButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 378);
            this.Controls.Add(this.integratedSecurityCheckBox);
            this.Controls.Add(this.mainStatusStrip);
            this.Controls.Add(this.databaseTargetRadioButton);
            this.Controls.Add(this.statusTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.vstudioVersionRadioButton);
            this.Controls.Add(this.scriptDbButton);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.userNameTextBox);
            this.Controls.Add(this.databaseNameTextBox);
            this.Controls.Add(this.serverNameTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "MainForm";
            this.Text = "DbFriend";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.mainStatusStrip.ResumeLayout(false);
            this.mainStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox serverNameTextBox;
        private System.Windows.Forms.TextBox databaseNameTextBox;
        private System.Windows.Forms.TextBox userNameTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Button scriptDbButton;
        private System.Windows.Forms.RadioButton vstudioVersionRadioButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox statusTextBox;
        private System.Windows.Forms.RadioButton databaseTargetRadioButton;
        private System.Windows.Forms.StatusStrip mainStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel mainToolStripStatusLabel;
        private System.Windows.Forms.CheckBox integratedSecurityCheckBox;
    }
}

