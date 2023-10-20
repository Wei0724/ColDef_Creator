using System.Windows.Forms;

namespace WinForms
{
    partial class ColDef_Creator
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColDef_Creator));
            txtSelectPath = new TextBox();
            btnPath = new Button();
            textBoxResults = new TextBox();
            lstVwSubItems = new ListView();
            dGdVwHeaders = new DataGridView();
            btn_clearMessege = new Button();
            btn_run = new Button();
            dGV_innerObj = new DataGridView();
            Index = new DataGridViewTextBoxColumn();
            Type = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dGdVwHeaders).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dGV_innerObj).BeginInit();
            SuspendLayout();
            // 
            // txtSelectPath
            // 
            txtSelectPath.Location = new Point(26, 31);
            txtSelectPath.Multiline = true;
            txtSelectPath.Name = "txtSelectPath";
            txtSelectPath.Size = new Size(442, 56);
            txtSelectPath.TabIndex = 0;
            // 
            // btnPath
            // 
            btnPath.Location = new Point(480, 31);
            btnPath.Name = "btnPath";
            btnPath.Size = new Size(99, 56);
            btnPath.TabIndex = 1;
            btnPath.Text = "選文字檔";
            btnPath.UseVisualStyleBackColor = true;
            btnPath.Click += btnPath_Click;
            // 
            // textBoxResults
            // 
            textBoxResults.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxResults.Location = new Point(26, 342);
            textBoxResults.Multiline = true;
            textBoxResults.Name = "textBoxResults";
            textBoxResults.ScrollBars = ScrollBars.Both;
            textBoxResults.Size = new Size(1119, 219);
            textBoxResults.TabIndex = 0;
            // 
            // lstVwSubItems
            // 
            lstVwSubItems.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lstVwSubItems.GridLines = true;
            lstVwSubItems.Location = new Point(334, 109);
            lstVwSubItems.Name = "lstVwSubItems";
            lstVwSubItems.Size = new Size(811, 227);
            lstVwSubItems.TabIndex = 2;
            lstVwSubItems.UseCompatibleStateImageBehavior = false;
            lstVwSubItems.ItemCheck += lstVwSubItems_ItemCheck;
            // 
            // dGdVwHeaders
            // 
            dGdVwHeaders.AllowUserToAddRows = false;
            dGdVwHeaders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dGdVwHeaders.Columns.AddRange(new DataGridViewColumn[] { Index, Type });
            dGdVwHeaders.Location = new Point(28, 109);
            dGdVwHeaders.Name = "dGdVwHeaders";
            dGdVwHeaders.RowTemplate.Height = 25;
            dGdVwHeaders.Size = new Size(300, 227);
            dGdVwHeaders.TabIndex = 3;
            dGdVwHeaders.CellMouseDown += dGdVwHeaders_CellMouseDown;
            dGdVwHeaders.SelectionChanged += dGdVwHeaders_SelectionChanged;
            // 
            // btn_clearMessege
            // 
            btn_clearMessege.Location = new Point(585, 31);
            btn_clearMessege.Name = "btn_clearMessege";
            btn_clearMessege.Size = new Size(99, 56);
            btn_clearMessege.TabIndex = 4;
            btn_clearMessege.Text = "清MSG";
            btn_clearMessege.UseVisualStyleBackColor = true;
            btn_clearMessege.Click += btn_clearMessege_Click;
            // 
            // btn_run
            // 
            btn_run.Location = new Point(690, 31);
            btn_run.Name = "btn_run";
            btn_run.Size = new Size(99, 56);
            btn_run.TabIndex = 5;
            btn_run.Text = "執行";
            btn_run.UseVisualStyleBackColor = true;
            btn_run.Click += btn_run_Click;
            // 
            // dGV_innerObj
            // 
            dGV_innerObj.AllowUserToAddRows = false;
            dGV_innerObj.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dGV_innerObj.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dGV_innerObj.Location = new Point(12, 354);
            dGV_innerObj.Name = "dGV_innerObj";
            dGV_innerObj.RowTemplate.Height = 25;
            dGV_innerObj.Size = new Size(1145, 219);
            dGV_innerObj.TabIndex = 6;
            dGV_innerObj.Visible = false;
            // 
            // Index
            // 
            Index.HeaderText = "區段代號";
            Index.Name = "Index";
            Index.ReadOnly = true;
            // 
            // Type
            // 
            Type.HeaderText = "Model類型";
            Type.Name = "Type";
            Type.ToolTipText = "111";
            // 
            // ColDef_Creator
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1169, 597);
            Controls.Add(dGV_innerObj);
            Controls.Add(textBoxResults);
            Controls.Add(btn_run);
            Controls.Add(btn_clearMessege);
            Controls.Add(dGdVwHeaders);
            Controls.Add(lstVwSubItems);
            Controls.Add(btnPath);
            Controls.Add(txtSelectPath);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ColDef_Creator";
            Text = "ColDef Creator";
            ((System.ComponentModel.ISupportInitialize)dGdVwHeaders).EndInit();
            ((System.ComponentModel.ISupportInitialize)dGV_innerObj).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtSelectPath;
        private Button btnPath;
        private TextBox textBoxResults;
        private ListView lstVwSubItems;
        private DataGridView dGdVwHeaders;
        private Button btn_clearMessege;
        private Button btn_run;
        private DataGridView dGV_innerObj;
        private DataGridViewTextBoxColumn Index;
        private DataGridViewTextBoxColumn Type;
    }
}