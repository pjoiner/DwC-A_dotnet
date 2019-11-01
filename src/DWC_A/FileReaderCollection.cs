using DwC_A.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DwC_A
{
    public class FileReaderCollection : IEnumerable<Tuple<IFileReader, IFileReaderAsync>>
    {
        private readonly IEnumerable<Tuple<IFileReader, IFileReaderAsync>> fileReaders;

        public FileReaderCollection(
            IEnumerable<IFileReader> fileReaders, 
            IEnumerable<IFileReaderAsync> asyncFileReaders)
        {
            this.fileReaders = fileReaders.Zip(asyncFileReaders,
                (fileReader, asyncFileReader) =>
                    new Tuple<IFileReader, IFileReaderAsync>(fileReader, asyncFileReader));
        }

        #region IEnumerable implementation
        public IEnumerator<Tuple<IFileReader, IFileReaderAsync>> GetEnumerator()
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
        /// Retrieves an IFileReaderAsync for the specified file name
        /// </summary>
        /// <param name="fileName">Name of the file in the archive (e.g. taxon.txt)</param>
        /// <returns>IFileReaderAsync</returns>
        /// <exception cref="FileReaderNotFoundException"/>
        public IFileReaderAsync GetFileReaderAsyncByFileName(string fileName)
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
        /// Returns a list of IFileReadersAsync of a given row type
        /// </summary>
        /// <param name="rowType">Fully qualified name of the row type. <seealso cref="Terms.RowTypes"/></param>
        /// <returns>IEnumerable list of IFileReadersAsync of rowType</returns>
        public IEnumerable<IFileReaderAsync> GetFileReadersAsyncByRowType(string rowType)
        {
            return fileReaders
                .Where(n => n.Item2.FileMetaData.RowType == rowType)
                .Select(n => n.Item2);
        }
    }
}
