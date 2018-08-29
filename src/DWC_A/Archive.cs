using DWC_A.Meta;
using System;
using System.Collections.Generic;
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
            CoreFile = abstractFactory.CreateFileReader(coreFileName, coreRowFactory, coreTokenizer, meta.Core);
            //Create file readers for extensions
            Extensions = new Dictionary<string, IFileReader>();
            foreach(var extension in meta.Extension)
            {
                var extensionFileName = Path.Combine(OutputPath, extension.Files.FirstOrDefault());
                var extensionFieldTypes = extension.Field;
                var extensionRowFactory = abstractFactory.CreateRowFactory(extensionFieldTypes);
                var extensionTokenizer = abstractFactory.CreateTokenizer(extension);
                Extensions[extension.Files.FirstOrDefault()] = abstractFactory.CreateFileReader(extensionFileName, 
                    extensionRowFactory, extensionTokenizer, extension);
            }
        }

        public void Delete()
        {
            archiveFolder.DeleteFolder();
        }

        public readonly IFileReader CoreFile;

        public readonly IDictionary<string, IFileReader> Extensions;

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
                    foreach(var extension in Extensions)
                    {
                        extension.Value.Dispose();
                    }
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
