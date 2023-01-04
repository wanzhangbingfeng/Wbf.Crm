using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wbf.Crm.Tool.Model
{
    public class ConnectionModel
    {
        /// <summary>
        /// 唯一标识符
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// http|https
        /// </summary>
        public string HttpPrefix
        {
            get
            {
                return AuthType == "AD" ? "http://" : "https://";
            }
        }

        /// <summary>
        /// 授权类型
        /// </summary>
        public string AuthType { get; set; }

        /// <summary>
        /// 服务器地址
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public string Port { get; set; }

        /// <summary>
        /// 组织名称
        /// </summary>
        public string OrgName { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 域
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool HasKey { get; set; } = true;
        /// <summary>
        /// 
        /// </summary>
        public string FullServer { get { return HttpPrefix + Server + Port; } }
    }
}
