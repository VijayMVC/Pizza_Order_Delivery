namespace pizza_delivery
{
    partial class Delivery
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
            this.DorderView = new System.Windows.Forms.DataGridView();
            this.statusCombo2 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDU = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DorderView)).BeginInit();
            this.SuspendLayout();
            // 
            // DorderView
            // 
            this.DorderView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DorderView.Location = new System.Drawing.Point(51, 43);
            this.DorderView.Name = "DorderView";
            this.DorderView.RowTemplate.Height = 40;
            this.DorderView.Size = new System.Drawing.Size(2297, 866);
            this.DorderView.TabIndex = 0;
            // 
            // statusCombo2
            // 
            this.statusCombo2.FormattingEnabled = true;
            this.statusCombo2.Items.AddRange(new object[] {
            "delivering",
            "delivered"});
            this.statusCombo2.Location = new System.Drawing.Point(292, 1028);
            this.statusCombo2.Name = "statusCombo2";
            this.statusCombo2.Size = new System.Drawing.Size(177, 39);
            this.statusCombo2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 1031);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 32);
            this.label1.TabIndex = 2;
            this.label1.Text = "order status:";
            // 
            // btnDU
            // 
            this.btnDU.Location = new System.Drawing.Point(560, 1028);
            this.btnDU.Name = "btnDU";
            this.btnDU.Size = new System.Drawing.Size(190, 61);
            this.btnDU.TabIndex = 3;
            this.btnDU.Text = "Update";
            this.btnDU.UseVisualStyleBackColor = true;
            this.btnDU.Click += new System.EventHandler(this.btnDU_Click);
            // 
            // Delivery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2397, 1176);
            this.Controls.Add(this.btnDU);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusCombo2);
            this.Controls.Add(this.DorderView);
            this.Name = "Delivery";
            this.Text = "Delivery";
            this.Load += new System.EventHandler(this.Delivery_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DorderView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DorderView;
        private System.Windows.Forms.ComboBox statusCombo2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDU;
    }
}