using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Business.Helpers
{
    public class FileReaderHelper
    {
        public List<string> Read()
        {
            try
            {
                // Open the text file using a stream reader.
                using (var sr = new StreamReader(Directory.GetCurrentDirectory() + "\\wwwroot" + "/words.txt"))
                {
                    var data = sr.ReadToEnd().Split("\r\n").ToList();

                    return data;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
