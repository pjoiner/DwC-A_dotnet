using DwC_A.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace DwC_A
{
#if !NETSTANDARD2_0
    public partial class FileReaderCollection
    {
        /// <summary>
        /// Retrieves all the asyncronous file readers
        /// </summary>
        /// <returns>Enumerable of IAsyncFileReader</returns>
        public IEnumerable<IAsyncFileReader> GetAsyncFileReaders()
        {
            return fileReaders.OfType<IAsyncFileReader>();
        }
        /// <summary>
        /// Retrieves an IAsyncFileReader for the specified file name
        /// </summary>
        /// <param name="fileName">Name of the file in the archive (e.g. taxon.txt)</param>
        /// <returns>IAsyncFileReader</returns>
        /// <exception cref="FileReaderNotFoundException"/>
        public IAsyncFileReader GetAsyncFileReaderByFileName(string fileName)
        {
            var fileReader = fileReaders.OfType<IAsyncFileReader>()
                .FirstOrDefault(n => n.FileMetaData.FileName == fileName);
            if (fileReader == null)
            {
                throw new FileReaderNotFoundException(fileName);
            }
            return fileReader;
        }
        /// <summary>
        /// Returns a list of IAsyncFileReaders of a given row type
        /// </summary>
        /// <param name="rowType">Fully qualified name of the row type. <seealso cref="Terms.RowTypes"/></param>
        /// <returns>IEnumerable list of IAsyncFileReaders of rowType</returns>
        public IEnumerable<IAsyncFileReader> GetAsyncFileReadersByRowType(string rowType)
        {
            return fileReaders.OfType<IAsyncFileReader>()
                .Where(n => n.FileMetaData.RowType == rowType);
        }
    }
#endif
}
