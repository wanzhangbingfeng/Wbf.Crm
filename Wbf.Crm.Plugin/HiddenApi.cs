using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Wbf.Crm.Plugin
{
    public abstract class HiddenApi : IPlugin
    {
        protected IOrganizationService service;
        protected IOrganizationService adminserver;
        public void Execute(IServiceProvider serviceProvider)
        {
            // 获取执行上下文
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            // 获取服务工厂
            IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            // 获取个人的组织服务
            service = factory.CreateOrganizationService(context.UserId);
            // 获取管理员组织服务
            adminserver = factory.CreateOrganizationService(null);
            ((OrganizationServiceProxy)adminserver).CallerId = new Guid();

            //读取Api参数，获取对应方法实例
            var Api = context.InputParameters["Api"] as string;
            if (string.IsNullOrWhiteSpace(Api))
            {
                throw new ArgumentNullException("参数：Api不能为空");
            }
            var routes = Api.Split('/');
            var type = Assembly.GetExecutingAssembly().DefinedTypes.Where(x => x.Name == routes[0]).FirstOrDefault();
            if (type == null)
            {
                throw new ArgumentNullException($"方法名:{Api}不存在");
            }
            var method = type.GetMethod(routes[1]);
            if (method == null)
            {
                throw new ArgumentNullException($"方法名:{Api}不存在");
            }
            var instance = Activator.CreateInstance(type, new object[] { service, adminserver, context });

            //解析传递的参数
            var parameter = context.InputParameters["Parameter"] as string;
            Dictionary<string, object> keyValues = JsonConvert.DeserializeObject<Dictionary<string, object>>(parameter);

            var parameters = method.GetParameters();
            object[] paras = new object[parameters.Count()];
            for (int i = 0; i < parameters.Count(); i++)
            {
                if (keyValues.ContainsKey(parameters[i].Name))
                {
                    paras[i] = keyValues[parameters[i].Name];
                }
                else
                {
                    if (parameters[i].ParameterType == typeof(string))
                    {
                        paras[i] = "";
                    }
                    else
                    {
                        paras[i] = parameters[i].ParameterType.IsValueType ? Activator.CreateInstance(parameters[i].ParameterType) : null;
                    }
                }
            }
            try
            {
                //执行方法
                var result = method.Invoke(instance, paras);
                context.OutputParameters["Data"] = JsonConvert.SerializeObject(result);
                context.OutputParameters["Code"] = "200";
                context.OutputParameters["Message"] = "Success";
            }
            catch (Exception ex)
            {
                context.OutputParameters["Message"] = ex.Message;
                context.OutputParameters["Code"] = "1";
                throw ex;
            }
        }
    }
}
