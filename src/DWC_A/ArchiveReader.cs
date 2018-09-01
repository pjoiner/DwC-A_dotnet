using Dwc.Text;
using DWC_A.Meta;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

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

        public IDictionary<string, IFileReader> Extensions { get; }

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
            CoreFile = CreateFileReader(meta.Core.Files.FirstOrDefault(), meta.Core, meta.Core.Field);
            //Create file readers for extensions
            Extensions = new Dictionary<string, IFileReader>();
            foreach(var extension in meta.Extension)
            {
                var extensionFileName = extension.Files.FirstOrDefault();
                Extensions[extensionFileName] = CreateFileReader(extensionFileName, extension, extension.Field);
            }
        }

        private IFileReader CreateFileReader(string fileName, FileType fileType, ICollection<FieldType> fieldTypes)
        {
            //Create a core file reader
            ValidateLineEnds(fileType);
            var fullFileName = Path.Combine(OutputPath, fileName);
            var rowFactory = abstractFactory.CreateRowFactory();
            var tokenizer = abstractFactory.CreateTokenizer(fileType);
            return abstractFactory.CreateFileReader(fullFileName, rowFactory, tokenizer, fileType, fieldTypes);
        }

        private void ValidateLineEnds(FileType fileType)
        {
            var linesTerminatedBy = Regex.Unescape(fileType.LinesTerminatedBy);
            if (new[] { "\n", "r", "\r\n" }.Contains(linesTerminatedBy) == false)
            {
                throw new NotSupportedException($"Lines terminated by {fileType.LinesTerminatedBy} not supported.  Only files terminated by '\n', '\r' or '\r\n' are supported.");
            }
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
                    foreach(var extension in Extensions)
                    {
                        extension.Value.Dispose();
                    }
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
