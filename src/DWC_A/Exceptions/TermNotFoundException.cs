﻿using System;

namespace DwC_A.Exceptions
{
    internal class TermNotFoundException : Exception
    {
        private static string BuildMessage(string term)
        {
            return $"Term {term} not found";
        }

        public TermNotFoundException(string term) :
            base(BuildMessage(term))
        {
        }

        public TermNotFoundException(string term, Exception innerException) :
            base(BuildMessage(term), innerException)
        {
        }
    }
}
