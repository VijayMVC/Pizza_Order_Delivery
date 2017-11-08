namespace pizza_delivery
{
    partial class Rewarding_Management
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnUp = new System.Windows.Forms.Button();
            this.txtUnit = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(71, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(267, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Rewarding Methods";
            // 
            // btnUp
            // 
            this.btnUp.Location = new System.Drawing.Point(673, 227);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(172, 57);
            this.btnUp.TabIndex = 2;
            this.btnUp.Text = "Update";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // txtUnit
            // 
            this.txtUnit.Location = new System.Drawing.Point(83, 246);
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.Size = new System.Drawing.Size(115, 38);
            this.txtUnit.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(257, 252);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(335, 32);
            this.label2.TabIndex = 9;
            this.label2.Text = "points for one small pizza";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(263, 331);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(324, 32);
            this.label3.TabIndex = 10;
            this.label3.Text = "+5 for one medium pizza";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(263, 411);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(303, 32);
            this.label4.TabIndex = 11;
            this.label4.Text = "+10 for one large pizza";
            // 
            // Rewarding_Management
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1630, 957);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtUnit);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.label1);
            this.Name = "Rewarding_Management";
            this.Text = "Rewarding_Management";
            this.Load += new System.EventHandler(this.Rewarding_Management_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.TextBox txtUnit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}