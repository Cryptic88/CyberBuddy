namespace POE_CyberBuddy
{
    partial class SetReminderForm
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
            lblPrompt = new Label();
            txtDays = new TextBox();
            btnSet = new Button();
            SuspendLayout();
            // 
            // lblPrompt
            // 
            lblPrompt.AutoSize = true;
            lblPrompt.Location = new Point(20, 20);
            lblPrompt.Name = "lblPrompt";
            lblPrompt.Size = new Size(214, 20);
            lblPrompt.TabIndex = 0;
            lblPrompt.Text = "Remind me in how many days?";
            // 
            // txtDays
            // 
            txtDays.Location = new Point(20, 50);
            txtDays.Name = "txtDays";
            txtDays.Size = new Size(100, 27);
            txtDays.TabIndex = 1;
            // 
            // btnSet
            // 
            btnSet.DialogResult = DialogResult.OK;
            btnSet.Location = new Point(130, 48);
            btnSet.Name = "btnSet";
            btnSet.Size = new Size(80, 30);
            btnSet.TabIndex = 2;
            btnSet.Text = "Set";
            btnSet.UseVisualStyleBackColor = true;
            btnSet.Click += btnSet_Click;
            // 
            // SetReminderForm
            // 
            AcceptButton = btnSet;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(282, 133);
            Controls.Add(btnSet);
            Controls.Add(txtDays);
            Controls.Add(lblPrompt);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SetReminderForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Set Reminder";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblPrompt;
        private TextBox txtDays;
        private Button btnSet;
    }
}