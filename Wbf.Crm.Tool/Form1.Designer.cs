
namespace Wbf.Crm.Tool
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MergeMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.UserRoleMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.mergePanel = new System.Windows.Forms.Panel();
            this.userRolePanel = new System.Windows.Forms.Panel();
            this.txtEdit = new System.Windows.Forms.Button();
            this.txtOutPutUrl = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.btnMerge = new System.Windows.Forms.Button();
            this.txtExcludeFiles = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.depanceList = new System.Windows.Forms.CheckedListBox();
            this.btnLoadDepend = new System.Windows.Forms.Button();
            this.btnSign = new System.Windows.Forms.Button();
            this.btnDll = new System.Windows.Forms.Button();
            this.txtSign = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDll = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.connectionList = new System.Windows.Forms.ComboBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.mergePanel.SuspendLayout();
            this.userRolePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MergeMenu,
            this.UserRoleMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1554, 36);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // MergeMenu
            // 
            this.MergeMenu.Name = "MergeMenu";
            this.MergeMenu.Size = new System.Drawing.Size(116, 30);
            this.MergeMenu.Text = "程序集合并";
            this.MergeMenu.Click += new System.EventHandler(this.MergeMenu_Click);
            // 
            // UserRoleMenu
            // 
            this.UserRoleMenu.Name = "UserRoleMenu";
            this.UserRoleMenu.Size = new System.Drawing.Size(98, 30);
            this.UserRoleMenu.Text = "用户权限";
            this.UserRoleMenu.Click += new System.EventHandler(this.UserRoleMenu_Click);
            // 
            // mergePanel
            // 
            this.mergePanel.Controls.Add(this.txtOutPutUrl);
            this.mergePanel.Controls.Add(this.label4);
            this.mergePanel.Controls.Add(this.txtLog);
            this.mergePanel.Controls.Add(this.btnMerge);
            this.mergePanel.Controls.Add(this.txtExcludeFiles);
            this.mergePanel.Controls.Add(this.label3);
            this.mergePanel.Controls.Add(this.depanceList);
            this.mergePanel.Controls.Add(this.btnLoadDepend);
            this.mergePanel.Controls.Add(this.btnSign);
            this.mergePanel.Controls.Add(this.btnDll);
            this.mergePanel.Controls.Add(this.txtSign);
            this.mergePanel.Controls.Add(this.label2);
            this.mergePanel.Controls.Add(this.txtDll);
            this.mergePanel.Controls.Add(this.label1);
            this.mergePanel.Location = new System.Drawing.Point(146, 91);
            this.mergePanel.Name = "mergePanel";
            this.mergePanel.Size = new System.Drawing.Size(1396, 965);
            this.mergePanel.TabIndex = 2;
            // 
            // userRolePanel
            // 
            this.userRolePanel.Controls.Add(this.btnDelete);
            this.userRolePanel.Controls.Add(this.connectionList);
            this.userRolePanel.Controls.Add(this.txtEdit);
            this.userRolePanel.Location = new System.Drawing.Point(143, 94);
            this.userRolePanel.Name = "userRolePanel";
            this.userRolePanel.Size = new System.Drawing.Size(1396, 962);
            this.userRolePanel.TabIndex = 16;
            // 
            // txtEdit
            // 
            this.txtEdit.Location = new System.Drawing.Point(1100, 50);
            this.txtEdit.Name = "txtEdit";
            this.txtEdit.Size = new System.Drawing.Size(96, 34);
            this.txtEdit.TabIndex = 1;
            this.txtEdit.Text = "编辑";
            this.txtEdit.UseVisualStyleBackColor = true;
            this.txtEdit.Click += new System.EventHandler(this.txtEdit_Click);
            // 
            // txtOutPutUrl
            // 
            this.txtOutPutUrl.Location = new System.Drawing.Point(406, 187);
            this.txtOutPutUrl.Name = "txtOutPutUrl";
            this.txtOutPutUrl.ReadOnly = true;
            this.txtOutPutUrl.Size = new System.Drawing.Size(840, 28);
            this.txtOutPutUrl.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(266, 190);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 18);
            this.label4.TabIndex = 14;
            this.label4.Text = "合并程序集路径";
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(269, 221);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(1073, 754);
            this.txtLog.TabIndex = 13;
            // 
            // btnMerge
            // 
            this.btnMerge.Location = new System.Drawing.Point(1252, 148);
            this.btnMerge.Name = "btnMerge";
            this.btnMerge.Size = new System.Drawing.Size(90, 28);
            this.btnMerge.TabIndex = 12;
            this.btnMerge.Text = "合并";
            this.btnMerge.UseVisualStyleBackColor = true;
            this.btnMerge.Click += new System.EventHandler(this.btnMerge_Click);
            // 
            // txtExcludeFiles
            // 
            this.txtExcludeFiles.Location = new System.Drawing.Point(406, 148);
            this.txtExcludeFiles.Name = "txtExcludeFiles";
            this.txtExcludeFiles.Size = new System.Drawing.Size(840, 28);
            this.txtExcludeFiles.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(266, 151);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 18);
            this.label3.TabIndex = 10;
            this.label3.Text = "要排除的程序集";
            // 
            // depanceList
            // 
            this.depanceList.CheckOnClick = true;
            this.depanceList.FormattingEnabled = true;
            this.depanceList.Location = new System.Drawing.Point(38, 183);
            this.depanceList.Name = "depanceList";
            this.depanceList.Size = new System.Drawing.Size(201, 779);
            this.depanceList.TabIndex = 9;
            this.depanceList.SelectedIndexChanged += new System.EventHandler(this.depanceList_SelectedIndexChanged);
            // 
            // btnLoadDepend
            // 
            this.btnLoadDepend.Location = new System.Drawing.Point(38, 144);
            this.btnLoadDepend.Name = "btnLoadDepend";
            this.btnLoadDepend.Size = new System.Drawing.Size(201, 32);
            this.btnLoadDepend.TabIndex = 8;
            this.btnLoadDepend.Text = "加载依赖性";
            this.btnLoadDepend.UseVisualStyleBackColor = true;
            this.btnLoadDepend.Click += new System.EventHandler(this.btnLoadDepend_Click);
            // 
            // btnSign
            // 
            this.btnSign.Location = new System.Drawing.Point(1252, 61);
            this.btnSign.Name = "btnSign";
            this.btnSign.Size = new System.Drawing.Size(90, 34);
            this.btnSign.TabIndex = 7;
            this.btnSign.Text = "选择";
            this.btnSign.UseVisualStyleBackColor = true;
            this.btnSign.Click += new System.EventHandler(this.btnSign_Click);
            // 
            // btnDll
            // 
            this.btnDll.Location = new System.Drawing.Point(1252, 15);
            this.btnDll.Name = "btnDll";
            this.btnDll.Size = new System.Drawing.Size(90, 34);
            this.btnDll.TabIndex = 6;
            this.btnDll.Text = "选择";
            this.btnDll.UseVisualStyleBackColor = true;
            this.btnDll.Click += new System.EventHandler(this.btnDll_Click);
            // 
            // txtSign
            // 
            this.txtSign.Enabled = false;
            this.txtSign.Location = new System.Drawing.Point(123, 64);
            this.txtSign.Name = "txtSign";
            this.txtSign.ReadOnly = true;
            this.txtSign.Size = new System.Drawing.Size(1123, 28);
            this.txtSign.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(55, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "签名";
            // 
            // txtDll
            // 
            this.txtDll.Enabled = false;
            this.txtDll.Location = new System.Drawing.Point(123, 15);
            this.txtDll.Name = "txtDll";
            this.txtDll.ReadOnly = true;
            this.txtDll.Size = new System.Drawing.Size(1123, 28);
            this.txtDll.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "程序集";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // connectionList
            // 
            this.connectionList.FormattingEnabled = true;
            this.connectionList.Location = new System.Drawing.Point(151, 51);
            this.connectionList.Name = "connectionList";
            this.connectionList.Size = new System.Drawing.Size(943, 26);
            this.connectionList.TabIndex = 2;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(1239, 50);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(96, 34);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1554, 1068);
            this.Controls.Add(this.userRolePanel);
            this.Controls.Add(this.mergePanel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.mergePanel.ResumeLayout(false);
            this.mergePanel.PerformLayout();
            this.userRolePanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Panel mergePanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSign;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDll;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnDll;
        private System.Windows.Forms.Button btnSign;
        private System.Windows.Forms.Button btnLoadDepend;
        private System.Windows.Forms.CheckedListBox depanceList;
        private System.Windows.Forms.Button btnMerge;
        private System.Windows.Forms.TextBox txtExcludeFiles;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.TextBox txtOutPutUrl;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripMenuItem MergeMenu;
        private System.Windows.Forms.ToolStripMenuItem UserRoleMenu;
        private System.Windows.Forms.Panel userRolePanel;
        private System.Windows.Forms.Button txtEdit;
        private System.Windows.Forms.ComboBox connectionList;
        private System.Windows.Forms.Button btnDelete;
    }
}

