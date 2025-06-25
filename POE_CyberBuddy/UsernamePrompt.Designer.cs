namespace POE_CyberBuddy
{
    partial class UsernamePrompt
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
            lblEnterUser = new Label();
            txtUsername = new TextBox();
            btnOK = new Button();
            SuspendLayout();
            // 
            // lblEnterUser
            // 
            lblEnterUser.AutoSize = true;
            lblEnterUser.Location = new Point(29, 18);
            lblEnterUser.Name = "lblEnterUser";
            lblEnterUser.Size = new Size(193, 20);
            lblEnterUser.TabIndex = 0;
            lblEnterUser.Text = "Please enter your username:";
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(29, 59);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(272, 27);
            txtUsername.TabIndex = 1;
            // 
            // btnOK
            // 
            btnOK.BackColor = Color.Cyan;
            btnOK.Location = new Point(307, 59);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(94, 29);
            btnOK.TabIndex = 2;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = false;
            btnOK.Click += btnOK_Click;
            // 
            // UsernamePrompt
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(428, 143);
            Controls.Add(btnOK);
            Controls.Add(txtUsername);
            Controls.Add(lblEnterUser);
            Name = "UsernamePrompt";
            Text = "Form2";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblEnterUser;
        private TextBox txtUsername;
        private Button btnOK;
    }
}