namespace PetAdoptionWinForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.UIDLabel = new System.Windows.Forms.Label();
            this.passwordlabel = new System.Windows.Forms.Label();
            this.UIDBox = new System.Windows.Forms.TextBox();
            this.PwdBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(307, 286);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Login";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // UIDLabel
            // 
            this.UIDLabel.AutoSize = true;
            this.UIDLabel.Location = new System.Drawing.Point(205, 134);
            this.UIDLabel.Name = "UIDLabel";
            this.UIDLabel.Size = new System.Drawing.Size(31, 15);
            this.UIDLabel.TabIndex = 1;
            this.UIDLabel.Text = "UID";
            // 
            // passwordlabel
            // 
            this.passwordlabel.AutoSize = true;
            this.passwordlabel.Location = new System.Drawing.Point(208, 172);
            this.passwordlabel.Name = "passwordlabel";
            this.passwordlabel.Size = new System.Drawing.Size(71, 15);
            this.passwordlabel.TabIndex = 2;
            this.passwordlabel.Text = "Password";
            // 
            // UIDBox
            // 
            this.UIDBox.Location = new System.Drawing.Point(316, 124);
            this.UIDBox.Name = "UIDBox";
            this.UIDBox.Size = new System.Drawing.Size(100, 25);
            this.UIDBox.TabIndex = 3;
            // 
            // PwdBox
            // 
            this.PwdBox.Location = new System.Drawing.Point(316, 172);
            this.PwdBox.Name = "PwdBox";
            this.PwdBox.Size = new System.Drawing.Size(100, 25);
            this.PwdBox.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.PwdBox);
            this.Controls.Add(this.UIDBox);
            this.Controls.Add(this.passwordlabel);
            this.Controls.Add(this.UIDLabel);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label UIDLabel;
        private System.Windows.Forms.Label passwordlabel;
        private System.Windows.Forms.TextBox UIDBox;
        private System.Windows.Forms.TextBox PwdBox;
    }
}

