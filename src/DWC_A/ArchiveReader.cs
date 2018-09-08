using DWC_A.Factories;
using DWC_A.Meta;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DWC_A
{
    public class ArchiveReader : IDisposable
    {
        private readonly IAbstractFactory abstractFactory;
        private readonly IArchiveFolder archiveFolder;
        private readonly IMetaDataReader metaDataReader;
        private readonly Dwc.Text.Archive meta;

        /// <summary>
        /// Fully qualified name for the archive file
        /// </summary>
        public string FileName {get;}
        /// <summary>
        /// Path where archive is extracted to
        /// </summary>
        public string OutputPath { get; }
        /// <summary>
        /// Raw meta data for archive
        /// </summary>
        public Dwc.Text.Archive MetaData { get { return meta; } }
        /// <summary>
        /// File reader for Core file
        /// </summary>
        public IFileReader CoreFile { get; }
        /// <summary>
        /// Collection of file readers for extension files
        /// </summary>
        public FileReaderCollection Extensions { get; }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fileName">Fully qualified file name for archive file.  
        /// Extracts archive to a temp directory</param>
        public ArchiveReader(string fileName) : 
            this(fileName, null)
        {

        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fileName">Fully qualified file name for archive file</param>
        /// <param name="outputPath">Directory path to extract archive to.
        /// If you specify an outputPath then files will not be cleaned when
        /// the ArchiveReader is disposed.  If you wish to cleanup the outputPath
        /// then use the <seealso cref="Delete"/> method</param>
        public ArchiveReader(string fileName, string outputPath):
            this(fileName, outputPath, new DefaultFactory())
        {

        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fileName">Fully qualified file name for archive file</param>
        /// <param name="outputPath">Directory path to extract archive to.
        /// If you specify an outputPath then files will not be cleaned when
        /// the ArchiveReader is disposed.  If you wish to cleanup the outputPath
        /// then use the <seealso cref="Delete"/> method</param>
        /// <param name="abstractFactory">Factory to create tokenizers, readers etc.</param>
        public ArchiveReader(string fileName, string outputPath, IAbstractFactory abstractFactory)
        {
            FileName = fileName;
            this.abstractFactory = abstractFactory;
            if (string.IsNullOrEmpty(fileName))
            {
                OutputPath = outputPath;
            }
            else
            { 
                archiveFolder = abstractFactory.CreateArchiveFolder(fileName, outputPath);
                OutputPath = archiveFolder.Extract();
            }
            metaDataReader = abstractFactory.CreateMetaDataReader();
            meta = metaDataReader.ReadMetaData(OutputPath);
            //Create a core file reader
            var coreFileMetaData = abstractFactory.CreateCoreMetaData(meta.Core);
            CoreFile = CreateFileReader(coreFileMetaData);
            //Create file readers for extensions
            var fileReaders = new List<IFileReader>();
            foreach(var extension in meta.Extension)
            {
                var extensionFileName = extension.Files.FirstOrDefault();
                var extensionFileMetaData = abstractFactory.CreateExtensionMetaData(extension);
                fileReaders.Add(CreateFileReader(extensionFileMetaData));
            }
            Extensions = new FileReaderCollection(fileReaders);
        }

        private IFileReader CreateFileReader(IFileMetaData fileMetaData)
        {
            var fullFileName = Path.Combine(OutputPath, fileMetaData.FileName);
            var tokenizer = abstractFactory.CreateTokenizer(fileMetaData);
            return abstractFactory.CreateFileReader(fullFileName, tokenizer, fileMetaData);
        }
        /// <summary>
        /// Used to cleanup extracted files.
        /// </summary>
        public void Delete()
        {
            archiveFolder.DeleteFolder();
        }

        #region IDisposable
        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    CoreFile?.Dispose();
                    Extensions?.Dispose();
                    if(archiveFolder.ShouldCleanup)
                    {
                        Delete();
                    }
                }
            }
            disposed = true;
        }

        ~ArchiveReader()
        {
            Dispose(false);
        }
        #endregion
    }
}
