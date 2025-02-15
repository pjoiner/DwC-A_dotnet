﻿using System;

namespace DwC_A.Exceptions
{
    internal class FileReaderNotFoundException : Exception
    {
        private static string BuildMessage(string fileName)
        {
            return $"FileReader for file {fileName} not found";
        }

        public FileReaderNotFoundException()
        {
        }

        public FileReaderNotFoundException(string fileName) :
            base(BuildMessage(fileName))
        {
        }

        public FileReaderNotFoundException(string fileName, Exception innerException) :
            base(BuildMessage(fileName), innerException)
        {
        }
    }
}
