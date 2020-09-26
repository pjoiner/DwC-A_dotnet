using DwC_A.Config;
using System;
using System.IO;
using System.IO.Compression;

namespace DwC_A
{
    internal class ArchiveFolder : IArchiveFolder
    {
        private readonly string fileName;
        private readonly string folderPath;
        private readonly ArchiveFolderConfiguration config;

        public bool ShouldCleanup => config.ShouldCleanup;

        /// <summary>
        /// Extracts archive to a folder
        /// </summary>
        /// <param name="fileName">Zip archive file name</param>
        /// <param name="config"><see cref="ArchiveFolderConfiguration"/></param>
        public ArchiveFolder(string fileName, ArchiveFolderConfiguration config)
        {
            this.config = config;
            this.fileName = fileName;
            this.folderPath = string.IsNullOrEmpty(config.OutputPath) ? GetTempPath() : config.OutputPath;
        }

        public string Extract()
        {
            ZipFile.ExtractToDirectory(fileName, folderPath);
            return folderPath;
        }

        public void DeleteFolder()
        {
            if (config.ShouldCleanup)
            {
                Directory.Delete(folderPath, true);
            }
        }

        private string GetTempPath()
        {
            config.ShouldCleanup = true;
            var newPath = Path.Combine(Path.GetTempPath(), "dwca", Guid.NewGuid().ToString());
            return newPath;
        }

    }
}