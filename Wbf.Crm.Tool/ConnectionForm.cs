using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wbf.Crm.Tool.Command;
using Wbf.Crm.Tool.Model;

namespace Wbf.Crm.Tool
{
    public partial class ConnectionForm : Form
    {
        private string key;
        public ConnectionForm(string key)
        {
            InitializeComponent();
            this.key = key;
            if (string.IsNullOrWhiteSpace(key))
            {
                this.key = Guid.NewGuid().ToString();
            }
            else
            {

                var con = CommonHelp.GetConnectionConfig().Where(x => x.Key == key).FirstOrDefault();
                var contrList = this.panel1.Controls;
                foreach (var item in contrList)
                {
                    var txtBox = item as TextBox;
                    if (txtBox != null)
                    {
                        txtBox.Text = con.GetType().GetProperty(txtBox.Name.Remove(0, 3)).GetValue(con).ToString();
                    }
                }
                this.authTypeList.SelectedItem = con.AuthType;
            }
        }

        private void txtCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ConnectionModel con = new ConnectionModel
            {
                AuthType = this.authTypeList.Text,
                Key = this.key
            };
            var contrList = this.panel1.Controls;
            foreach (var item in contrList)
            {
                var txtBox = item as TextBox;
                if (txtBox != null)
                {
                    con.GetType().GetProperty(txtBox.Name.Remove(0, 3)).SetValue(con, txtBox.Text);
                }
            }
            var list = CommonHelp.GetConnectionConfig();
            if (list.Exists(x=>x.Key==con.Key))
            {
                list.Remove(list.Find(x => x.Key == key));
            }
            list.Add(con);
            var conStrig = JsonConvert.SerializeObject(list);
            File.WriteAllText($"Config/ConnectionConfig.txt", conStrig);
            this.Close();
        }
    }
}
