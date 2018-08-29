using DWC_A.Meta;
using System;
using System.IO;
using System.Linq;

namespace DWC_A
{
    public class Archive : IDisposable
    {
        private readonly IAbstractFactory abstractFactory;
        private readonly IArchiveFolder archiveFolder;
        private readonly IMetaDataReader metaDataReader;
        private readonly Dwc.Text.Archive meta;
        private readonly IFileReader coreFileReader;

        public string FileName {get;}

        public string OutputPath { get; }

        public Archive(string fileName) : 
            this(fileName, null)
        {

        }

        public Archive(string fileName, string outputPath):
            this(fileName, outputPath, new DefaultFactory())
        {

        }

        public Archive(string fileName, string outputPath, IAbstractFactory abstractFactory)
        {
            FileName = fileName;
            this.abstractFactory = abstractFactory;
            archiveFolder = abstractFactory.CreateArchiveFolder(fileName, outputPath);
            OutputPath = archiveFolder.Extract();
            metaDataReader = abstractFactory.CreateMetaDataReader();
            meta = metaDataReader.ReadMetaData(OutputPath);
            //Create a core file reader
            var coreFileName = Path.Combine(OutputPath, meta.Core.Files.FirstOrDefault());
            var coreFieldTypes = meta.Core.Field;
            var coreRowFactory = abstractFactory.CreateRowFactory(coreFieldTypes);
            var coreTokenizer = abstractFactory.CreateTokenizer(meta.Core);
            coreFileReader = abstractFactory.CreateFileReader(coreFileName, coreRowFactory, coreTokenizer, meta.Core);
        }

        public void Delete()
        {
            archiveFolder.DeleteFolder();
        }

        public IFileReader CoreFile
        {
            get
            {
                return coreFileReader;
            }

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
                    coreFileReader.Dispose();
                    Delete();
                }
            }
            disposed = true;
        }

        ~Archive()
        {
            Dispose(false);
        }
        #endregion
    }
}
