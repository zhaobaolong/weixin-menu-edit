using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Zeroes.WeixinMenu.Helper
{
    public static class FileHelper
    {
        public static string ShareRead(string file, Encoding encoding)
        {
            
            string content = string.Empty;
            FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            try
            {
                if (fs.CanRead)
                {
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    content = encoding.GetString(buffer);
                }
            }
            finally
            {
                fs.Close();
                fs.Dispose();
            }
            return content;
        }
        public static void ShareAppend(string content, string file, Encoding encoding)
        {
            ShareWrite(content, file, encoding, FileMode.Append);
        }
        public static void ShareWrite(string content, string file, Encoding encoding, FileMode fileMode)
        {
            FileStream fs = new FileStream(file, fileMode, FileAccess.Write, FileShare.Read);
            try
            {
                if (fs.CanWrite)
                {
                    byte[] buffer = encoding.GetBytes(content);
                    if (buffer.Length > 0)
                    {
                        fs.Write(buffer, 0, buffer.Length);
                        fs.Flush();
                    }
                }
            }
            finally
            {
                fs.Close();
                fs.Dispose();
            }
        }
    } 
}