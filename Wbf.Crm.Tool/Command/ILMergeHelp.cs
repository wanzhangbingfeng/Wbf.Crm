using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Wbf.Crm.Tool.Command
{
    public static class ILMergeHelp
    {
        /// <summary>
        /// 合并程序集
        /// </summary>
        /// <param name="targetPath"></param>
        /// <param name="keyfile"></param>
        /// <param name="excludeFiles"></param>
        /// <returns></returns>
        public static string MergeAssembly(string targetPath,string keyfile,string excludeFiles)
        {
            if (string.IsNullOrEmpty(targetPath))
            {
                throw new Exception("目标程序集为空");
            }
            if (string.IsNullOrEmpty(keyfile))
            {
                throw new Exception("签名文件为空");
            }
            string filePath = Path.GetDirectoryName(targetPath);
            string filename = Path.GetFileName(targetPath);
            string outputPath = $@"{AppDomain.CurrentDomain.BaseDirectory}\OutPut\{filename}";
            StringBuilder builder = new StringBuilder();
            builder.Append($"-lib={filePath} ");
            // 定义ILMerge命令行工具的路径
            builder.Append("-target=library ");

            // 定义输出程序集的路径
            builder.Append($@"-out={outputPath} ");

            //定义日志文件输出路径
            builder.Append($"-out={targetPath}.log");

            //定义签名文件
            builder.Append($"-keyfile={keyfile} ");

            // 定义要合并的程序集的路径
            var excludeFilesList = excludeFiles.Split(';');
            var megerAssembly = GetReferencedAssemblies(targetPath).Except(excludeFilesList);

            builder.Append($"-targetplatform=v4 /copyattrs /keepFirst {targetPath} ");
            foreach (var item in megerAssembly)
            {
                builder.Append($@"{filePath}\{item} ");
            }

            // 使用ILMerge命令行工具合并程序集
            Process process = new Process();
            process.StartInfo.FileName = $@"{AppDomain.CurrentDomain.BaseDirectory}\ILMerge.exe ";
            process.StartInfo.Arguments = builder.ToString();
            process.Start();
            process.WaitForExit();
            return outputPath;
        }

        public static List<string> GetReferencedAssemblies(string assemblyUrl)
        {
            List<string> list = new List<string>();
            var dllList = Assembly.LoadFile(assemblyUrl).GetReferencedAssemblies();
            foreach (var item in dllList)
            {
                if (!item.Name.StartsWith("System", StringComparison.OrdinalIgnoreCase) && !item.Name.StartsWith("Microsoft", StringComparison.OrdinalIgnoreCase))
                {
                    list.Add($"{item.Name}.dll");
                }
            }
            return list;
        }
    }
}
