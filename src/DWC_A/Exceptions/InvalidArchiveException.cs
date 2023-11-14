using System;

namespace DwC_A.Exceptions
{
    public class InvalidArchiveException : Exception
    {
        private static string BuildMessage(string path)
        {
            return $"Path {path} contains more than one file and no meta.xml metadata file was found";
        }

        public InvalidArchiveException()
        {
        }

        public InvalidArchiveException(string path) :
            base(BuildMessage(path))
        {
        }

        public InvalidArchiveException(string path, Exception innerException) :
            base(BuildMessage(path), innerException)
        {
        }
    }
}
