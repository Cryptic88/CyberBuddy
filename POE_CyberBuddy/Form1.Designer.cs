using System;
using System.Drawing;
using System.Windows.Forms;
using POE_Cybersecurity_Chatbot;

namespace POE_CyberBuddy
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private RichTextBox rtbOutput;
        private TextBox txtInput;
        private Button btnSend;
        private Label lblLoggedIn;
        private Button btnCommands;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            rtbOutput = new RichTextBox();
            txtInput = new TextBox();
            btnSend = new Button();
            lblLoggedIn = new Label();
            btnCommands = new Button();
            SuspendLayout();
            // 
            // rtbOutput
            // 
            rtbOutput.BackColor = SystemColors.ActiveCaptionText;
            rtbOutput.Font = new Font("Consolas", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rtbOutput.ForeColor = Color.Cyan;
            rtbOutput.Location = new Point(12, 45);
            rtbOutput.Name = "rtbOutput";
            rtbOutput.ReadOnly = true;
            rtbOutput.ScrollBars = RichTextBoxScrollBars.Vertical;
            rtbOutput.Size = new Size(776, 289);
            rtbOutput.TabIndex = 0;
            rtbOutput.Text = "";
            // 
            // txtInput
            // 
            txtInput.Font = new Font("Consolas", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtInput.Location = new Point(168, 365);
            txtInput.Name = "txtInput";
            txtInput.Size = new Size(405, 29);
            txtInput.TabIndex = 1;
            txtInput.Text = "Type your message here";
            // 
            // btnSend
            // 
            btnSend.BackColor = Color.Cyan;
            btnSend.Font = new Font("Consolas", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSend.Location = new Point(579, 365);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(94, 29);
            btnSend.TabIndex = 2;
            btnSend.Text = "Send";
            btnSend.UseVisualStyleBackColor = false;
            // 
            // lblLoggedIn
            // 
            lblLoggedIn.AutoSize = true;
            lblLoggedIn.Location = new Point(12, 9);
            lblLoggedIn.Name = "lblLoggedIn";
            lblLoggedIn.Size = new Size(97, 20);
            lblLoggedIn.TabIndex = 3;
            lblLoggedIn.Text = "Logged in as:";
            // 
            // btnCommands
            // 
            btnCommands.BackColor = Color.PaleTurquoise;
            btnCommands.Cursor = Cursors.Hand;
            btnCommands.Font = new Font("Consolas", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnCommands.Location = new Point(12, 419);
            btnCommands.Name = "btnCommands";
            btnCommands.Size = new Size(94, 29);
            btnCommands.TabIndex = 4;
            btnCommands.Text = "Commands";
            btnCommands.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnCommands);
            Controls.Add(lblLoggedIn);
            Controls.Add(btnSend);
            Controls.Add(txtInput);
            Controls.Add(rtbOutput);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion
    }
}
