using DwC_A.Exceptions;
using DwC_A.Meta;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DwC_A.Extensions
{
    public static class GetListExtensions
    {
        /// <summary>
        /// Splits a delimited field into a list
        /// </summary>
        /// <param name="row">a row object</param>
        /// <param name="term">Name of the term to split</param>
        /// <returns>String list of delimited items.  If there is no delimiter for this field then a list with a single item containing the text of the field.</returns>
        public static IEnumerable<string> GetListOf(this IRow row, string term)
        {
            return row.FieldMetaData.TryGetFieldType(term, out FieldType fieldType) ?
                row[term].Split(fieldType.DelimitedBy?.ToCharArray(),
                    StringSplitOptions.RemoveEmptyEntries) :
                throw new TermNotFoundException(nameof(term));
        }

        /// <summary>
        /// Splits a delimited field into a list
        /// </summary>
        /// <param name="row">a row object></param>
        /// <param name="index">Index of field to split</param>
        /// <returns>String list of delimited items.  If there is no delimiter for this field then a list with a single item containing the text of the field.</returns>
        public static IEnumerable<string> GetListOf(this IRow row, int index)
        {
            var delimiter = row.FieldMetaData[index].DelimitedBy;
            return row[index].Split(delimiter?.ToCharArray(),
                StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Splits a delimited field into a list
        /// </summary>
        /// <param name="row">a row object</param>
        /// <param name="term">Name of the term to split</param>
        /// <param name="list">String list of delimited items.  If there is no delimiter for this field then a list with a single item containing the text of the field.</param>
        /// <returns>true on success</returns>
        public static bool TryGetListOf(this IRow row, string term, out IEnumerable<string> list)
        {
            if(row.FieldMetaData.TryGetFieldType(term, out FieldType fieldType) && 
                row.TryGetField(term, out string value))
            {
                list = value.Split(fieldType.DelimitedBy?.ToCharArray(),
                    StringSplitOptions.RemoveEmptyEntries);
                return true;
            }
            list = default;
            return false;
        }
    }
}
