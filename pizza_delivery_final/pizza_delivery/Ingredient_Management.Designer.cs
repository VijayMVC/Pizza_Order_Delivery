namespace pizza_delivery
{
    partial class Ingredient_Management
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
            this.ingreData = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAmout = new System.Windows.Forms.TextBox();
            this.btnUpdateI = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ingreData)).BeginInit();
            this.SuspendLayout();
            // 
            // ingreData
            // 
            this.ingreData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ingreData.Location = new System.Drawing.Point(68, 159);
            this.ingreData.Name = "ingreData";
            this.ingreData.RowTemplate.Height = 40;
            this.ingreData.Size = new System.Drawing.Size(3131, 721);
            this.ingreData.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(74, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ingredient：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(74, 921);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 32);
            this.label2.TabIndex = 2;
            this.label2.Text = "Amout:";
            // 
            // txtAmout
            // 
            this.txtAmout.Location = new System.Drawing.Point(68, 988);
            this.txtAmout.Name = "txtAmout";
            this.txtAmout.Size = new System.Drawing.Size(235, 38);
            this.txtAmout.TabIndex = 3;
            // 
            // btnUpdateI
            // 
            this.btnUpdateI.Location = new System.Drawing.Point(442, 979);
            this.btnUpdateI.Name = "btnUpdateI";
            this.btnUpdateI.Size = new System.Drawing.Size(136, 47);
            this.btnUpdateI.TabIndex = 4;
            this.btnUpdateI.Text = "Update";
            this.btnUpdateI.UseVisualStyleBackColor = true;
            this.btnUpdateI.Click += new System.EventHandler(this.btnUpdateI_Click);
            // 
            // Ingredient_Management
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(3184, 1038);
            this.Controls.Add(this.btnUpdateI);
            this.Controls.Add(this.txtAmout);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ingreData);
            this.Name = "Ingredient_Management";
            this.Text = "Ingredient_Management";
            this.Load += new System.EventHandler(this.Ingredient_Management_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ingreData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ingreData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAmout;
        private System.Windows.Forms.Button btnUpdateI;
    }
}