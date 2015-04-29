namespace WirelessProfileManager
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
            if(disposing && (components != null))
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxInterface = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView_ScannedBssList = new System.Windows.Forms.DataGridView();
            this.form1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_ScannedBssList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Interfaces";
            // 
            // cbxInterface
            // 
            this.cbxInterface.FormattingEnabled = true;
            this.cbxInterface.Location = new System.Drawing.Point(15, 34);
            this.cbxInterface.Name = "cbxInterface";
            this.cbxInterface.Size = new System.Drawing.Size(370, 26);
            this.cbxInterface.TabIndex = 1;
            this.cbxInterface.SelectedIndexChanged += new System.EventHandler(this.cbxInterface_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Scanned SSID list";
            // 
            // dataGridView_ScannedBssList
            // 
            this.dataGridView_ScannedBssList.AllowUserToAddRows = false;
            this.dataGridView_ScannedBssList.AllowUserToDeleteRows = false;
            this.dataGridView_ScannedBssList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_ScannedBssList.Location = new System.Drawing.Point(15, 97);
            this.dataGridView_ScannedBssList.MultiSelect = false;
            this.dataGridView_ScannedBssList.Name = "dataGridView_ScannedBssList";
            this.dataGridView_ScannedBssList.ReadOnly = true;
            this.dataGridView_ScannedBssList.RowTemplate.Height = 30;
            this.dataGridView_ScannedBssList.Size = new System.Drawing.Size(749, 335);
            this.dataGridView_ScannedBssList.TabIndex = 3;
            // 
            // form1BindingSource
            // 
            this.form1BindingSource.DataSource = typeof(WirelessProfileManager.Form1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 444);
            this.Controls.Add(this.dataGridView_ScannedBssList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbxInterface);
            this.Controls.Add(this.label1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "WirelessProfileManager";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_ScannedBssList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxInterface;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView_ScannedBssList;
        private System.Windows.Forms.BindingSource form1BindingSource;
    }
}

