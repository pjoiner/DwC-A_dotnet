using System;
using System.IO;

namespace DwC_A.Builders
{
    public class BuilderContext
    {
        private static BuilderContext defaultInstance;
        private readonly string path;
        private readonly bool shouldCleanup;

        public BuilderContext(string path = "", bool shouldCleanup = false)
        {
            if (string.IsNullOrEmpty(path))
            {
                this.path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "dwca", Guid.NewGuid().ToString());
            }
            else
            {
                this.path = path;
            }
            Directory.CreateDirectory(this.path);
            this.shouldCleanup = shouldCleanup;
        }

        public static BuilderContext Default
        {
            get
            {
                if (defaultInstance == null)
                {
                    defaultInstance = new BuilderContext(null, true);
                }
                return defaultInstance;
            }
        }

        public string Path => path;

        public bool ShouldCleanup => shouldCleanup;

        public void Cleanup()
        {
            if (shouldCleanup)
            {
                Directory.Delete(path, true);
            }
        }
    }
}
