using DwC_A.Exceptions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DwC_A
{
    public class FileReaderCollection : IEnumerable<IFileReader>
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

        /// <summary>
        /// Retrieves an IFileReader for the specified file name
        /// </summary>
        /// <param name="fileName">Name of the file in the archive (e.g. taxon.txt)</param>
        /// <returns>IFileReader</returns>
        /// <exception cref="FileReaderNotFoundException"/>
        public IFileReader GetFileReaderByFileName(string fileName)
        {
            var fileReader = fileReaders.FirstOrDefault(n => n.FileMetaData.FileName == fileName);
            if(fileReader == null)
            {
                throw new FileReaderNotFoundException(fileName);
            }
            return fileReader;
        }

        /// <summary>
        /// Returns a list of IFileReaders of a given row type
        /// </summary>
        /// <param name="rowType">Fully qualified name of the row type. <seealso cref="Terms.RowTypes"/></param>
        /// <returns>IEnumerable list of IFileReaders of rowType</returns>
        public IEnumerable<IFileReader> GetFileReadersByRowType(string rowType)
        {
            return fileReaders.Where(n => n.FileMetaData.RowType == rowType);
        }
    }
}
