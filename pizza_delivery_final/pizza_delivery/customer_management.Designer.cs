namespace pizza_delivery
{
    partial class customer_management
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
            this.CusData = new System.Windows.Forms.DataGridView();
            this.btnC = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.CusData)).BeginInit();
            this.SuspendLayout();
            // 
            // CusData
            // 
            this.CusData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CusData.Location = new System.Drawing.Point(89, 85);
            this.CusData.Name = "CusData";
            this.CusData.RowTemplate.Height = 40;
            this.CusData.Size = new System.Drawing.Size(3153, 814);
            this.CusData.TabIndex = 0;
            // 
            // btnC
            // 
            this.btnC.Location = new System.Drawing.Point(89, 1003);
            this.btnC.Name = "btnC";
            this.btnC.Size = new System.Drawing.Size(262, 51);
            this.btnC.TabIndex = 3;
            this.btnC.Text = "clear rewards";
            this.btnC.UseVisualStyleBackColor = true;
            this.btnC.Click += new System.EventHandler(this.btnUP_Click);
            // 
            // customer_management
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(3184, 1405);
            this.Controls.Add(this.btnC);
            this.Controls.Add(this.CusData);
            this.Name = "customer_management";
            this.Text = "customer_management";
            this.Load += new System.EventHandler(this.customer_management_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CusData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView CusData;
        private System.Windows.Forms.Button btnC;
    }
}