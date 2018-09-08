using System.IO;
using System.IO.Compression;

namespace DWC_A
{
    internal class ArchiveFolder : IArchiveFolder
    {
        private readonly string fileName;
        private readonly string folderPath;
        
        public bool ShouldCleanup { get; private set; }

        /// <summary>
        /// Extracts archive to a folder
        /// </summary>
        /// <param name="fileName">Zip archive file name</param>
        /// <param name="folderPath">Path to extract to.  Leave null for a temp folder</param>
        public ArchiveFolder(string fileName, string folderPath = null)
        {
            this.fileName = fileName;
            this.folderPath = string.IsNullOrEmpty(folderPath) ? GetTempPath() : folderPath;
        }

        public string Extract()
        {
            Directory.CreateDirectory(folderPath);
            using (Stream stream = new FileStream(fileName, FileMode.Open))
            {
                using (ZipArchive zipArchive = new ZipArchive(stream))
                {
                    foreach (var entry in zipArchive.Entries)
                    {
                        entry.ExtractToFile(Path.Combine(folderPath, entry.Name), true);
                    }
                }
            }
            return folderPath;
        }

        public void DeleteFolder()
        {
            Directory.Delete(folderPath, true);
        }

        private string GetTempPath()
        {
            ShouldCleanup = true;
            return Path.Combine(Path.GetTempPath(), "dwca", Path.GetFileNameWithoutExtension(fileName));
        }

    }
}