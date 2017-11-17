using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WxToken.Common
{
    public static class LogHelper
    {
       public static string Path {
            get { return DateTime.Now.ToString("yyyyMMdd")+"BackUpLog.txt"; }
        }

        //创建文件夹
        //参数：path 文件夹路径
        public static bool CreateFolder(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    return true;
                }
                if (!Directory.Exists(path.Substring(0, path.LastIndexOf("\\"))))
                { //若路径中无“\”则表示路径错误
                    return false;
                }
                else
                {
                    //创建文件夹
                    DirectoryInfo dirInfo = Directory.CreateDirectory(path);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //创建文件
        //参数：path 文件路径
        public static void CreateFile(string path)
        {
            try
            {
                if (CreateFolder(path.Substring(0, path.LastIndexOf("\\"))))
                {
                    if (!File.Exists(path))
                    {
                        FileStream fs = File.Create(path);
                        fs.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                return;
            }

        }

        //删除文件
        //参数：path 文件夹路径
        public static void DeleteFile(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    return;
                }
                else
                {
                    File.Delete(path);
                }

            }
            catch (Exception ex)
            {
                return;
            }
        }

        //写文件
        //参数：path 文件夹路径 、content要写的内容
        public static void WriteFile(string path, string content)
        {
            try
            {
                if (!File.Exists(path))
                {
                    CreateFile(path);
                }
                FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(content+" "+DateTime.Now);
                sw.Close();
            }
            catch (Exception ex)
            {
                return;
            }
        }

        /// <summary>
        /// 将即时日志保存入日志文件
        /// </summary>
        public static void WriteLogFile(string directoryPath, string content)
        {
            if (!Directory.Exists(directoryPath))
            {
                CreateFolder(directoryPath);
            }
            try
            {
                //写入新的文件
                string filePath = directoryPath + "\\" + DateTime.Now.Date.ToString("yyyyMMdd") + ".log";
                FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(content);
                sw.Close();
                fs.Close();
            }
            catch (Exception ex)
            {

            }

        }
    }

}