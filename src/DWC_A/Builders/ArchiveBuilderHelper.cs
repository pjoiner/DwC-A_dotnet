using System;
using System.IO;

namespace DwC_A.Builders
{
    public class ArchiveBuilderHelper
    {
        private static string path;
        public static string Path 
        {
            get
            {
                if(string.IsNullOrEmpty(path))
                {
                    path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "dwca", Guid.NewGuid().ToString());
                    Directory.CreateDirectory(path);
                }
                return path;
            } 
            private set
            {
                path = value;
            }
        } 

        public static void SetPath( string path )
        {
            Path = path;
        }

        public static void Cleanup()
        {
            Directory.Delete(path, true);
        }
    }
}
