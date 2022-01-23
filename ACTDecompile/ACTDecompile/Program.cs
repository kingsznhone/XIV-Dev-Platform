using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ACTHacker
{
    class Program
    {
        static void Main(string[] args)
        {
            string rootPath = Directory.GetCurrentDirectory();
            DirectoryInfo root = new DirectoryInfo(rootPath);
            FileInfo[] files = root.GetFiles();
            foreach (FileInfo f in files)
            {
                if(f.FullName.EndsWith(".dll.compressed"))
                {
                    Decompress(f.FullName);
                    Console.WriteLine(f.Name + " is Decompressed");
                }

            }
            Console.ReadLine();
        }

        private static void Decompress(string path)
        {
            string filename = path.Replace(".compressed", "");
            using (Stream rawasm = LoadStream(path))
            {
                using (FileStream fsw = File.Create(filename))
                {
                    byte[] bytes = new byte[rawasm.Length];
                    rawasm.Read(bytes, 0, bytes.Length);
                    fsw.Write(bytes, 0, bytes.Length);
                }
            } 
        }

        private static Stream LoadStream(string fullname)
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            if (fullname.EndsWith(".compressed"))
            {

                using (Stream stream = new FileStream(fullname, FileMode.Open)) 
                {
                    using (DeflateStream source = new DeflateStream(stream, CompressionMode.Decompress))
                    {
                        MemoryStream memoryStream = new MemoryStream();
                        CopyTo(source, memoryStream);
                        memoryStream.Position = 0L;
                        return memoryStream;
                    }


                }
            }
            return executingAssembly.GetManifestResourceStream(fullname);
        }

        private static void CopyTo(Stream source, Stream destination)
        {
            byte[] array = new byte[81920];
            int count;
            while ((count = source.Read(array, 0, array.Length)) != 0)
            {
                destination.Write(array, 0, count);
            }
        }
    }
}
