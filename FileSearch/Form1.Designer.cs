namespace FileSearchApp
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.cmdSearch = new System.Windows.Forms.Button();
            this.lvFiles = new System.Windows.Forms.ListView();
            this.FullPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dgTargetFiles = new System.Windows.Forms.DataGridView();
            this.FullPathName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.txtKeyWord = new System.Windows.Forms.TextBox();
            this.labFileSearched = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgTargetFiles)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Folder";
            // 
            // txtFolder
            // 
            this.txtFolder.Location = new System.Drawing.Point(73, 34);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(315, 21);
            this.txtFolder.TabIndex = 1;
            this.txtFolder.Text = "txtFolder";
            // 
            // cmdSearch
            // 
            this.cmdSearch.Location = new System.Drawing.Point(313, 61);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(75, 23);
            this.cmdSearch.TabIndex = 2;
            this.cmdSearch.Text = "Search";
            this.cmdSearch.UseVisualStyleBackColor = true;
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // lvFiles
            // 
            this.lvFiles.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.lvFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lvFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.FullPath});
            this.lvFiles.Location = new System.Drawing.Point(14, 107);
            this.lvFiles.Name = "lvFiles";
            this.lvFiles.Size = new System.Drawing.Size(374, 462);
            this.lvFiles.TabIndex = 3;
            this.lvFiles.UseCompatibleStateImageBehavior = false;
            this.lvFiles.View = System.Windows.Forms.View.Details;
            // 
            // FullPath
            // 
            this.FullPath.Text = "FullPath";
            this.FullPath.Width = 350;
            // 
            // dgTargetFiles
            // 
            this.dgTargetFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgTargetFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FullPathName});
            this.dgTargetFiles.Location = new System.Drawing.Point(405, 13);
            this.dgTargetFiles.Name = "dgTargetFiles";
            this.dgTargetFiles.RowTemplate.Height = 23;
            this.dgTargetFiles.Size = new System.Drawing.Size(953, 556);
            this.dgTargetFiles.TabIndex = 4;
            // 
            // FullPathName
            // 
            this.FullPathName.HeaderText = "FullPathName";
            this.FullPathName.Name = "FullPathName";
            this.FullPathName.Width = 1000;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "KeyWord";
            // 
            // txtKeyWord
            // 
            this.txtKeyWord.Location = new System.Drawing.Point(73, 62);
            this.txtKeyWord.Name = "txtKeyWord";
            this.txtKeyWord.Size = new System.Drawing.Size(234, 21);
            this.txtKeyWord.TabIndex = 6;
            this.txtKeyWord.Text = "txtKeyWord";
            // 
            // labFileSearched
            // 
            this.labFileSearched.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labFileSearched.AutoSize = true;
            this.labFileSearched.Location = new System.Drawing.Point(12, 581);
            this.labFileSearched.Name = "labFileSearched";
            this.labFileSearched.Size = new System.Drawing.Size(125, 12);
            this.labFileSearched.TabIndex = 7;
            this.labFileSearched.Text = "FileAlreadySearched:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 602);
            this.Controls.Add(this.labFileSearched);
            this.Controls.Add(this.txtKeyWord);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgTargetFiles);
            this.Controls.Add(this.lvFiles);
            this.Controls.Add(this.cmdSearch);
            this.Controls.Add(this.txtFolder);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgTargetFiles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.Button cmdSearch;
        private System.Windows.Forms.ListView lvFiles;
        private System.Windows.Forms.DataGridView dgTargetFiles;
        private System.Windows.Forms.ColumnHeader FullPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtKeyWord;
        private System.Windows.Forms.DataGridViewTextBoxColumn FullPathName;
        private System.Windows.Forms.Label labFileSearched;
    }
}

