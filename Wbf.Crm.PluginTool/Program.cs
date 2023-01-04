using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Wbf.Crm.PluginTool
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> excludeFiles = this.GetExcludeFiles();
            ILMergeProxy proxy = new ILMergeProxy(this.textMergingFile.Text, this.textMergingFileKey.Text, excludeFiles);
            OperationResult<MergeResult> result = proxy.MergeAllDepencies();
            

        }

        public OperationResult<MergeResult> MergeAllDepencies()
        {
            //OperationResult<MergeResult> result2;
            AppDomain domain = AppDomain.CreateDomain("ilmerge", new Evidence(AppDomain.CurrentDomain.Evidence), AppDomain.CurrentDomain.BaseDirectory, ".", true);
            try
            {
                domain.SetupInformation.ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                domain.Load(typeof(ILMergeHelper).Assembly.FullName);
                domain.AssemblyResolve += new ResolveEventHandler(this.Newdomain_AssemblyResolve);
                ILMergeHelper helper1 = domain.CreateInstanceFromAndUnwrap(typeof(ILMergeHelper).Assembly.Location, typeof(ILMergeHelper).FullName) as ILMergeHelper;
                helper1.SetMasterFile(this._masterAssemblyFile);
                helper1.SetMasterKeyFile(this._masterAssemblyKeyFile);
                helper1.AddExcludeFiles(this.ExcludeFiles);
                OperationResult<MergeResult> result = helper1.MergeAllDepencies();
                this.OutputFile = result.Data.OutputFile;
                result2 = result;
            }
            catch (Exception exception1)
            {
                result2 = OperationResult.Fail<MergeResult>(exception1);
            }
            finally
            {
                AppDomain.Unload(domain);
            }
            return result2;
        }


    }
}
