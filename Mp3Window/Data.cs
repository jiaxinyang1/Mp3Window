using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Lsj.Util.JSON;
using Microsoft.Win32;

namespace Mp3Window
{
    public class Data
    {

        //一些异常处理待做
        public static void Save(ref string text,string path)
        {
            StreamWriter sw = File.CreateText(path);
            sw.Write(text);
            sw.Close();
        }

        public static void Read(ref string   text, string path)
        {
            if (text == null) throw new ArgumentNullException(nameof(text));
            StreamReader sr = File.OpenText(path);
            text = sr.ReadToEnd();
            sr.Close();
        }
    }

 
}
