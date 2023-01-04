using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wbf.Crm.Tool.Command;

namespace Wbf.Crm.Tool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.mergePanel.Show();
            this.userRolePanel.Hide();
        }

        private void btnDll_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.Filter = "程序集|*.dll";
            if (this.openFileDialog1.ShowDialog()== DialogResult.OK)
            {
                this.txtDll.Text = this.openFileDialog1.FileName;
            }
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.Filter = "签名文件|*.snk";
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtSign.Text = this.openFileDialog1.FileName;
            }
        }

        /// <summary>
        /// 加载依赖项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadDepend_Click(object sender, EventArgs e)
        {
            string dllUrl = this.txtDll.Text;
            if (string.IsNullOrWhiteSpace(dllUrl))
            {
                MessageBox.Show("请选择要合并的程序集");
            }

            depanceList.Items.Clear();
            var dllList = ILMergeHelp.GetReferencedAssemblies(dllUrl);
            foreach (var item in dllList)
            {
                this.depanceList.Items.Add(item);
            }
        }

        private void depanceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strCollected = string.Empty;
            for (int i = 0; i < depanceList.Items.Count; i++)
            {
                if (depanceList.GetItemChecked(i))
                {
                    strCollected += depanceList.GetItemText(depanceList.Items[i]) + ";";
                }
            }
            this.txtExcludeFiles.Text = strCollected;
        }

        private void btnMerge_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(this.txtDll.Text))
            {
                MessageBox.Show("请选择要合并的程序集");
            }
            if (string.IsNullOrWhiteSpace(this.txtSign.Text))
            {
                MessageBox.Show("请选择签名文件");
            }


            string outputPath = ILMergeHelp.MergeAssembly(this.txtDll.Text, this.txtSign.Text, this.txtExcludeFiles.Text);
            this.txtOutPutUrl.Text = outputPath;
            string[] lines = File.ReadAllLines($"{this.txtDll.Text}.log");
            foreach (string line in lines)
            {
                this.txtLog.Text += $"{line}\r\n";
            }
        }

        /// <summary>
        /// 合并菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MergeMenu_Click(object sender, EventArgs e)
        {
            this.mergePanel.Show();
            this.userRolePanel.Hide();
        }

        /// <summary>
        /// 用户权限菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserRoleMenu_Click(object sender, EventArgs e)
        {
            InitConfigList();

            this.userRolePanel.Show();
            this.mergePanel.Hide();
        }

        private void txtEdit_Click(object sender, EventArgs e)
        {
            var editForm = new ConnectionForm(this.connectionList.SelectedValue?.ToString());
            if (editForm.ShowDialog() == DialogResult.Cancel)
            {
                InitConfigList();
            }
        }

        #region 辅助方法
        private void InitConfigList()
        {
            var configList = CommonHelp.GetConnectionConfig();
            var keyList = CommonHelp.GetConnectionList(configList);
            this.connectionList.DataSource = keyList;
            this.connectionList.DisplayMember = "Value";
            this.connectionList.ValueMember = "Key";
        }
        #endregion

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var list = CommonHelp.GetConnectionConfig();
            if (list.Exists(x => x.Key == this.connectionList.SelectedValue.ToString()))
            {
                list.Remove(list.Find(x => x.Key == this.connectionList.SelectedValue.ToString()));
            }
            var conStrig = JsonConvert.SerializeObject(list);
            File.WriteAllText($"Config/ConnectionConfig.txt", conStrig);
            InitConfigList();
            MessageBox.Show("删除成功");
        }
    }
}
