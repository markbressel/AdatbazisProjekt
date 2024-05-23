namespace WindowsFormsApp1
{
    partial class Form1
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
            this.LoginPanelLabel = new System.Windows.Forms.Label();
            this.usernameBox = new System.Windows.Forms.TextBox();
            this.PasswordBox = new System.Windows.Forms.TextBox();
            this.UsernameLabel = new System.Windows.Forms.Label();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.LoginButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LoginPanelLabel
            // 
            this.LoginPanelLabel.AutoSize = true;
            this.LoginPanelLabel.Location = new System.Drawing.Point(255, 7);
            this.LoginPanelLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LoginPanelLabel.Name = "LoginPanelLabel";
            this.LoginPanelLabel.Size = new System.Drawing.Size(63, 13);
            this.LoginPanelLabel.TabIndex = 0;
            this.LoginPanelLabel.Tag = "";
            this.LoginPanelLabel.Text = "Login Panel";
            this.LoginPanelLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // usernameBox
            // 
            this.usernameBox.Location = new System.Drawing.Point(247, 131);
            this.usernameBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.usernameBox.Name = "usernameBox";
            this.usernameBox.Size = new System.Drawing.Size(76, 20);
            this.usernameBox.TabIndex = 1;
            this.usernameBox.Tag = "";
            // 
            // PasswordBox
            // 
            this.PasswordBox.Location = new System.Drawing.Point(247, 192);
            this.PasswordBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.PasswordBox.Name = "PasswordBox";
            this.PasswordBox.Size = new System.Drawing.Size(76, 20);
            this.PasswordBox.TabIndex = 3;
            this.PasswordBox.Tag = "";
            this.PasswordBox.TextChanged += new System.EventHandler(this.PasswordBox_TextChanged);
            // 
            // UsernameLabel
            // 
            this.UsernameLabel.AutoSize = true;
            this.UsernameLabel.Location = new System.Drawing.Point(255, 115);
            this.UsernameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.UsernameLabel.Name = "UsernameLabel";
            this.UsernameLabel.Size = new System.Drawing.Size(55, 13);
            this.UsernameLabel.TabIndex = 4;
            this.UsernameLabel.Tag = "";
            this.UsernameLabel.Text = "Username";
            this.UsernameLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Location = new System.Drawing.Point(257, 176);
            this.PasswordLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(53, 13);
            this.PasswordLabel.TabIndex = 5;
            this.PasswordLabel.Tag = "";
            this.PasswordLabel.Text = "Password";
            this.PasswordLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // LoginButton
            // 
            this.LoginButton.Location = new System.Drawing.Point(257, 248);
            this.LoginButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(56, 19);
            this.LoginButton.TabIndex = 6;
            this.LoginButton.Tag = "";
            this.LoginButton.Text = "Login";
            this.LoginButton.UseVisualStyleBackColor = true;
            this.LoginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.LoginButton);
            this.Controls.Add(this.PasswordLabel);
            this.Controls.Add(this.UsernameLabel);
            this.Controls.Add(this.PasswordBox);
            this.Controls.Add(this.usernameBox);
            this.Controls.Add(this.LoginPanelLabel);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ford West Motors";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LoginPanelLabel;
        private System.Windows.Forms.TextBox usernameBox;
        private System.Windows.Forms.TextBox PasswordBox;
        private System.Windows.Forms.Label UsernameLabel;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.Button LoginButton;
    }
}

