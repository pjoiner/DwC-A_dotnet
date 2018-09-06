using DWC_A.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DWC_A
{
    public class FileReaderCollection : IEnumerable<IFileReader>, IDisposable
    {
        private readonly IEnumerable<IFileReader> fileReaders;

        public FileReaderCollection(IEnumerable<IFileReader> fileReaders)
        {
            this.fileReaders = fileReaders;
        }

        #region IEnumerable implementation
        public IEnumerator<IFileReader> GetEnumerator()
        {
            return fileReaders.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion

        public IFileReader GetFileReaderByFileName(string fileName)
        {
            var fileReader = fileReaders.FirstOrDefault(n => n.FileMetaData.FileName == fileName);
            if(fileReader == null)
            {
                throw new FileReaderNotFoundException(fileName);
            }
            return fileReader;
        }

        public IFileReader GetFileReaderByRowType(string rowType)
        {
            var fileReader = fileReaders.FirstOrDefault(n => n.FileMetaData.RowType == rowType);
            if (fileReader == null)
            {
                throw new FileReaderNotFoundException(rowType);
            }
            return fileReader;
        }

        #region IDisposable implementation
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
                    foreach (var fileReader in fileReaders)
                    {
                        fileReader.Dispose();
                    }
                }
            }
            disposed = true;
        }

        ~FileReaderCollection()
        {
            Dispose(false);
        }
        #endregion
    }
}
