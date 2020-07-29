using System;
using System.IO;

namespace DwC_A.Builders
{
    /// <summary>
    /// A directory context where files should be written while building an archive.
    /// The default context may be used to assemble files in a temporary directory which will be cleaned up after the archive is compressed.
    /// </summary>
    public class BuilderContext
    {
        private static BuilderContext defaultInstance;
        private readonly string path;
        private readonly bool shouldCleanup;

        /// <summary>
        /// Constructor for custom context.  To use a temporary directory to assemble files use the static Default property.
        /// </summary>
        /// <param name="path">Provide the absolute path to a folder where the archive files will be written.</param>
        /// <param name="shouldCleanup">If set to true then the folder will be deleted after the archive is written.</param>
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

        /// <summary>
        /// Returns a default context that uses a temporary directory.
        /// </summary>
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

        /// <summary>
        /// Returns the absolute path that will be used to write and assemble files.
        /// </summary>
        public string Path => path;

        /// <summary>
        /// When set to true then the folder will be deleted after the archive is written.
        /// </summary>
        public bool ShouldCleanup => shouldCleanup;

        /// <summary>
        /// Call this method to delete the folder after the archive has be compressed.
        /// </summary>
        public void Cleanup()
        {
            if (shouldCleanup)
            {
                Directory.Delete(path, true);
            }
        }
    }
}
