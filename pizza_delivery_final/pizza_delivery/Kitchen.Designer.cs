namespace pizza_delivery
{
    partial class Kitchen
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
            this.TorderView = new System.Windows.Forms.DataGridView();
            this.btnUs = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.statusCombo = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.TorderView)).BeginInit();
            this.SuspendLayout();
            // 
            // TorderView
            // 
            this.TorderView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TorderView.Location = new System.Drawing.Point(53, 36);
            this.TorderView.Name = "TorderView";
            this.TorderView.RowTemplate.Height = 40;
            this.TorderView.Size = new System.Drawing.Size(3167, 903);
            this.TorderView.TabIndex = 0;
            // 
            // btnUs
            // 
            this.btnUs.Location = new System.Drawing.Point(553, 1032);
            this.btnUs.Name = "btnUs";
            this.btnUs.Size = new System.Drawing.Size(231, 76);
            this.btnUs.TabIndex = 1;
            this.btnUs.Text = "Update";
            this.btnUs.UseVisualStyleBackColor = true;
            this.btnUs.Click += new System.EventHandler(this.btnUs_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 1059);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 32);
            this.label1.TabIndex = 2;
            this.label1.Text = "order status:";
            // 
            // statusCombo
            // 
            this.statusCombo.FormattingEnabled = true;
            this.statusCombo.Items.AddRange(new object[] {
            "cooking",
            "cooked"});
            this.statusCombo.Location = new System.Drawing.Point(267, 1052);
            this.statusCombo.Name = "statusCombo";
            this.statusCombo.Size = new System.Drawing.Size(199, 39);
            this.statusCombo.TabIndex = 3;
            // 
            // Kitchen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(3042, 1132);
            this.Controls.Add(this.statusCombo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnUs);
            this.Controls.Add(this.TorderView);
            this.Name = "Kitchen";
            this.Text = "Kitchen";
            this.Load += new System.EventHandler(this.Kitchen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TorderView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView TorderView;
        private System.Windows.Forms.Button btnUs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox statusCombo;
    }
}