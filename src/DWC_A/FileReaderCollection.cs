using DwC_A.Exceptions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DwC_A
{
    public partial class FileReaderCollection  
    {
        private readonly IEnumerable fileReaders;

        public FileReaderCollection(
            IEnumerable fileReaders)
        {
            this.fileReaders = fileReaders;
        }

        /// <summary>
        /// Retrieves all the synchronous file readers
        /// </summary>
        /// <returns>Enumerable of IFileReader</returns>
        public IEnumerable<IFileReader> GetFileReaders()
        {
            return fileReaders.OfType<IFileReader>();
        }
        /// <summary>
        /// Retrieves an IFileReader for the specified file name
        /// </summary>
        /// <param name="fileName">Name of the file in the archive (e.g. taxon.txt)</param>
        /// <returns>IFileReader</returns>
        /// <exception cref="FileReaderNotFoundException"/>
        public IFileReader GetFileReaderByFileName(string fileName) 
        {
            var fileReader = fileReaders.OfType<IFileReader>()
                .FirstOrDefault(n => n.FileMetaData.FileName == fileName);
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
            return fileReaders.OfType<IFileReader>()
                .Where(n => n.FileMetaData.RowType == rowType);
        }
    }
}
