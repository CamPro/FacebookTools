using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSS.Patterns.Web
{
    public class UploadFile
    {
        public string Name { get; set; }
        public string FileName { get; set; }
        public byte[] Data { get; set; }

        public UploadFile(byte[] data)
        {
            Data = data;
        }

        public UploadFile(byte[] data, string name, string fileName)
        {
            Name = name;
            FileName = fileName;
            Data = data;
        }
    }
}