namespace LSD.Edit.Forms
{
    partial class DataCollectionForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnCollectTxt = new System.Windows.Forms.Button();
            this.cmbYTxt = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbXTxt = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbTBBHTxt = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOpenTxt = new System.Windows.Forms.Button();
            this.textboxTxt = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnCollectExcel = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbYExcel = new System.Windows.Forms.ComboBox();
            this.cmbXExcel = new System.Windows.Forms.ComboBox();
            this.cmbTBBHExcel = new System.Windows.Forms.ComboBox();
            this.cmbSheets = new System.Windows.Forms.ComboBox();
            this.btnOpenExcel = new System.Windows.Forms.Button();
            this.textboxExcel = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(4, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(431, 243);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(423, 214);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "CAD";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnCollectTxt);
            this.tabPage2.Controls.Add(this.cmbYTxt);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.cmbXTxt);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.cmbTBBHTxt);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.btnOpenTxt);
            this.tabPage2.Controls.Add(this.textboxTxt);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(423, 214);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "TXT";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnCollectTxt
            // 
            this.btnCollectTxt.Location = new System.Drawing.Point(115, 171);
            this.btnCollectTxt.Name = "btnCollectTxt";
            this.btnCollectTxt.Size = new System.Drawing.Size(75, 23);
            this.btnCollectTxt.TabIndex = 8;
            this.btnCollectTxt.Text = "确定";
            this.btnCollectTxt.UseVisualStyleBackColor = true;
            this.btnCollectTxt.Click += new System.EventHandler(this.btnCollectTxt_Click);
            // 
            // cmbYTxt
            // 
            this.cmbYTxt.FormattingEnabled = true;
            this.cmbYTxt.Location = new System.Drawing.Point(115, 129);
            this.cmbYTxt.Name = "cmbYTxt";
            this.cmbYTxt.Size = new System.Drawing.Size(127, 23);
            this.cmbYTxt.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(61, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Y字段";
            // 
            // cmbXTxt
            // 
            this.cmbXTxt.FormattingEnabled = true;
            this.cmbXTxt.Location = new System.Drawing.Point(115, 89);
            this.cmbXTxt.Name = "cmbXTxt";
            this.cmbXTxt.Size = new System.Drawing.Size(127, 23);
            this.cmbXTxt.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(61, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "X字段";
            // 
            // cmbTBBHTxt
            // 
            this.cmbTBBHTxt.FormattingEnabled = true;
            this.cmbTBBHTxt.Location = new System.Drawing.Point(115, 47);
            this.cmbTBBHTxt.Name = "cmbTBBHTxt";
            this.cmbTBBHTxt.Size = new System.Drawing.Size(127, 23);
            this.cmbTBBHTxt.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "图斑编号字段";
            // 
            // btnOpenTxt
            // 
            this.btnOpenTxt.Location = new System.Drawing.Point(248, 9);
            this.btnOpenTxt.Name = "btnOpenTxt";
            this.btnOpenTxt.Size = new System.Drawing.Size(71, 25);
            this.btnOpenTxt.TabIndex = 1;
            this.btnOpenTxt.Text = "选择";
            this.btnOpenTxt.UseVisualStyleBackColor = true;
            this.btnOpenTxt.Click += new System.EventHandler(this.btnOpenTxt_Click);
            // 
            // textboxTxt
            // 
            this.textboxTxt.Location = new System.Drawing.Point(12, 9);
            this.textboxTxt.Name = "textboxTxt";
            this.textboxTxt.Size = new System.Drawing.Size(230, 25);
            this.textboxTxt.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnCollectExcel);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.cmbYExcel);
            this.tabPage3.Controls.Add(this.cmbXExcel);
            this.tabPage3.Controls.Add(this.cmbTBBHExcel);
            this.tabPage3.Controls.Add(this.cmbSheets);
            this.tabPage3.Controls.Add(this.btnOpenExcel);
            this.tabPage3.Controls.Add(this.textboxExcel);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(423, 214);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "EXCEL";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnCollectExcel
            // 
            this.btnCollectExcel.Location = new System.Drawing.Point(158, 185);
            this.btnCollectExcel.Name = "btnCollectExcel";
            this.btnCollectExcel.Size = new System.Drawing.Size(75, 23);
            this.btnCollectExcel.TabIndex = 12;
            this.btnCollectExcel.Text = "确定";
            this.btnCollectExcel.UseVisualStyleBackColor = true;
            this.btnCollectExcel.Click += new System.EventHandler(this.btnCollectExcel_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 15);
            this.label7.TabIndex = 11;
            this.label7.Text = "数据表";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 152);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 15);
            this.label6.TabIndex = 10;
            this.label6.Text = "Y字段";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 117);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "X字段";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "图斑编号字段";
            // 
            // cmbYExcel
            // 
            this.cmbYExcel.FormattingEnabled = true;
            this.cmbYExcel.Location = new System.Drawing.Point(106, 147);
            this.cmbYExcel.Name = "cmbYExcel";
            this.cmbYExcel.Size = new System.Drawing.Size(127, 23);
            this.cmbYExcel.TabIndex = 7;
            // 
            // cmbXExcel
            // 
            this.cmbXExcel.FormattingEnabled = true;
            this.cmbXExcel.Location = new System.Drawing.Point(106, 112);
            this.cmbXExcel.Name = "cmbXExcel";
            this.cmbXExcel.Size = new System.Drawing.Size(127, 23);
            this.cmbXExcel.TabIndex = 6;
            // 
            // cmbTBBHExcel
            // 
            this.cmbTBBHExcel.FormattingEnabled = true;
            this.cmbTBBHExcel.Location = new System.Drawing.Point(106, 77);
            this.cmbTBBHExcel.Name = "cmbTBBHExcel";
            this.cmbTBBHExcel.Size = new System.Drawing.Size(127, 23);
            this.cmbTBBHExcel.TabIndex = 5;
            // 
            // cmbSheets
            // 
            this.cmbSheets.FormattingEnabled = true;
            this.cmbSheets.Location = new System.Drawing.Point(106, 42);
            this.cmbSheets.Name = "cmbSheets";
            this.cmbSheets.Size = new System.Drawing.Size(127, 23);
            this.cmbSheets.TabIndex = 4;
            this.cmbSheets.SelectedIndexChanged += new System.EventHandler(this.cmbSheets_SelectedIndexChanged);
            // 
            // btnOpenExcel
            // 
            this.btnOpenExcel.Location = new System.Drawing.Point(297, 11);
            this.btnOpenExcel.Name = "btnOpenExcel";
            this.btnOpenExcel.Size = new System.Drawing.Size(71, 25);
            this.btnOpenExcel.TabIndex = 2;
            this.btnOpenExcel.Text = "选择";
            this.btnOpenExcel.UseVisualStyleBackColor = true;
            this.btnOpenExcel.Click += new System.EventHandler(this.btnOpenExcel_Click);
            // 
            // textboxExcel
            // 
            this.textboxExcel.Location = new System.Drawing.Point(10, 11);
            this.textboxExcel.Name = "textboxExcel";
            this.textboxExcel.Size = new System.Drawing.Size(281, 25);
            this.textboxExcel.TabIndex = 0;
            // 
            // DataCollectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 253);
            this.Controls.Add(this.tabControl1);
            this.Name = "DataCollectionForm";
            this.Text = "数据采集";
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ComboBox cmbYTxt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbXTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbTBBHTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOpenTxt;
        private System.Windows.Forms.TextBox textboxTxt;
        private System.Windows.Forms.Button btnCollectTxt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbYExcel;
        private System.Windows.Forms.ComboBox cmbXExcel;
        private System.Windows.Forms.ComboBox cmbTBBHExcel;
        private System.Windows.Forms.ComboBox cmbSheets;
        private System.Windows.Forms.Button btnOpenExcel;
        private System.Windows.Forms.TextBox textboxExcel;
        private System.Windows.Forms.Button btnCollectExcel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
    }
}