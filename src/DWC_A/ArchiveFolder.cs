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

        public bool ShouldCleanup { get; private set; }

        /// <summary>
        /// Extracts archive to a folder
        /// </summary>
        /// <param name="fileName">Zip archive file name</param>
        /// <param name="config"><see cref="ArchiveFolderConfiguration"/></param>
        public ArchiveFolder(string fileName, ArchiveFolderConfiguration config)
        {
            this.config = config;
            this.fileName = fileName;
            ShouldCleanup = config.ShouldCleanup;
            this.folderPath = string.IsNullOrEmpty(config.OutputPath) ? GetTempPath() : config.OutputPath;
        }

        public string Extract()
        {
            using(var zipArchive = ZipFile.OpenRead(fileName))
            {
                foreach(var entry in zipArchive.Entries)
                {
                    var outputFile = Path.Combine(folderPath, entry.FullName);
                    var outputPath = Path.GetDirectoryName(outputFile);
                    if (!Directory.Exists(outputPath))
                    {
                        Directory.CreateDirectory(outputPath);
                    }
                    entry.ExtractToFile(outputFile, config.Overwrite);
                }
            }
            return folderPath;
        }

        public void DeleteFolder()
        {
            if (ShouldCleanup)
            {
                Directory.Delete(folderPath, true);
            }
        }

        private string GetTempPath()
        {
            ShouldCleanup = true;
            var newPath = Path.Combine(Path.GetTempPath(), "dwca", Guid.NewGuid().ToString());
            return newPath;
        }

    }
}