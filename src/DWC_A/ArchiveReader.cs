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

        public string FileName {get;}

        public string OutputPath { get; }

        public Dwc.Text.Archive MetaData { get { return meta; } }

        public IFileReader CoreFile { get; }

        public FileReaderCollection Extensions { get; }

        public ArchiveReader(string fileName) : 
            this(fileName, null)
        {

        }

        public ArchiveReader(string fileName, string outputPath):
            this(fileName, outputPath, new DefaultFactory())
        {

        }

        public ArchiveReader(string fileName, string outputPath, IAbstractFactory abstractFactory)
        {
            FileName = fileName;
            this.abstractFactory = abstractFactory;
            archiveFolder = abstractFactory.CreateArchiveFolder(fileName, outputPath);
            OutputPath = archiveFolder.Extract();
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
                    Delete();
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
