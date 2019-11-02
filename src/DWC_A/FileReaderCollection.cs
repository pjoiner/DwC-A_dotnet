using DwC_A.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DwC_A
{
    public class FileReaderCollection : IEnumerable<Tuple<IFileReader, IAsyncFileReader>>
    {
        private readonly IEnumerable<Tuple<IFileReader, IAsyncFileReader>> fileReaders;

        public FileReaderCollection(
            IEnumerable<IFileReader> fileReaders, 
            IEnumerable<IAsyncFileReader> asyncFileReaders)
        {
            this.fileReaders = fileReaders.Zip(asyncFileReaders,
                (fileReader, asyncFileReader) =>
                    new Tuple<IFileReader, IAsyncFileReader>(fileReader, asyncFileReader));
        }

        #region IEnumerable implementation
        public IEnumerator<Tuple<IFileReader, IAsyncFileReader>> GetEnumerator()
        {
            return fileReaders.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion

        /// <summary>
        /// Retrieves an IFileReader for the specified file name
        /// </summary>
        /// <param name="fileName">Name of the file in the archive (e.g. taxon.txt)</param>
        /// <returns>IFileReader</returns>
        /// <exception cref="FileReaderNotFoundException"/>
        public IFileReader GetFileReaderByFileName(string fileName) 
        {
            var fileReader = fileReaders.FirstOrDefault(n => n.Item1.FileMetaData.FileName == fileName);
            if(fileReader == null)
            {
                throw new FileReaderNotFoundException(fileName);
            }
            return fileReader.Item1;
        }

        /// <summary>
        /// Retrieves an IAsyncFileReader for the specified file name
        /// </summary>
        /// <param name="fileName">Name of the file in the archive (e.g. taxon.txt)</param>
        /// <returns>IAsyncFileReader</returns>
        /// <exception cref="FileReaderNotFoundException"/>
        public IAsyncFileReader GetAsyncFileReaderByFileName(string fileName)
        {
            var fileReader = fileReaders.FirstOrDefault(n => n.Item2.FileMetaData.FileName == fileName);
            if (fileReader == null)
            {
                throw new FileReaderNotFoundException(fileName);
            }
            return fileReader.Item2;
        }

        /// <summary>
        /// Returns a list of IFileReaders of a given row type
        /// </summary>
        /// <param name="rowType">Fully qualified name of the row type. <seealso cref="Terms.RowTypes"/></param>
        /// <returns>IEnumerable list of IFileReaders of rowType</returns>
        public IEnumerable<IFileReader> GetFileReadersByRowType(string rowType)
        {
            return fileReaders
                .Where(n => n.Item1.FileMetaData.RowType == rowType)
                .Select(n => n.Item1);
        }

        /// <summary>
        /// Returns a list of IAsyncFileReaders of a given row type
        /// </summary>
        /// <param name="rowType">Fully qualified name of the row type. <seealso cref="Terms.RowTypes"/></param>
        /// <returns>IEnumerable list of IAsyncFileReaders of rowType</returns>
        public IEnumerable<IAsyncFileReader> GetAsyncFileReadersByRowType(string rowType)
        {
            return fileReaders
                .Where(n => n.Item2.FileMetaData.RowType == rowType)
                .Select(n => n.Item2);
        }
    }
}
