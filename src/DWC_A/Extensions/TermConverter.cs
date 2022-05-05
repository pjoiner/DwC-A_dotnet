using DwC_A.Exceptions;
using System;

namespace DwC_A.Extensions
{
    public static class TermConverter
    {
        /// <summary>
        /// Convert the value of a term to type T
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="row">A row object</param>
        /// <param name="term">The name of the term to convert</param>
        /// <returns>Value of type T</returns>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="TermNotFoundException"/>
        /// <exception cref="NotSupportedException"/>
        public static T Convert<T>(this IRow row, string term)
        {
            var converter = System.ComponentModel.TypeDescriptor.GetConverter(typeof(T));
            return (T)converter.ConvertFrom(row[term]);
        }

        /// <summary>
        /// Convert the value of a field at index to type T
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="row">A row object</param>
        /// <param name="index">Index of field to convert</param>
        /// <returns>Value of type T</returns>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        /// <exception cref="NotSupportedException"/>
        public static T Convert<T>(this IRow row, int index)
        {
            var converter = System.ComponentModel.TypeDescriptor.GetConverter(typeof(T));
            return (T)converter.ConvertFrom(row[index]);
        }

        /// <summary>
        /// Convert the value of a term to type T
        /// </summary>
        /// <param name="row">A row object</param>
        /// <param name="term">The name of the term to convert</param>
        /// <param name="type">Type to convert to</param>
        /// <returns>An object of the type given in the type parameter</returns>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="TermNotFoundException"/>
        /// <exception cref="NotSupportedException"/>
        public static object Convert(this IRow row, string term, Type type)
        {
            var converter = System.ComponentModel.TypeDescriptor.GetConverter(type);
            return converter.ConvertFrom(row[term]);
        }

        /// <summary>
        /// Convert the value of a field at index to type T
        /// </summary>
        /// <param name="row">A row object</param>
        /// <param name="index">Index of field to convert</param>
        /// <param name="type">Type to convert to</param>
        /// <returns>An object of the type given in the type parameter</returns>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        /// <exception cref="NotSupportedException"/>
        public static object Convert(this IRow row, int index, Type type)
        {
            var converter = System.ComponentModel.TypeDescriptor.GetConverter(type);
            return converter.ConvertFrom(row[index]);
        }

        /// <summary>
        /// Convert the value of a term to type T or null
        /// </summary>
        /// <typeparam name="T">Type to convert to</typeparam>
        /// <param name="row">A row object</param>
        /// <param name="term">The name of the term to convert</param>
        /// <returns>A value of type T</returns>
        /// <exception cref="TermNotFoundException"/>
        /// <exception cref="ArgumentException" />
        public static T? ConvertNullable<T>(this IRow row, string term) where T : struct
        {
            var result = new T?();
            var converter = System.ComponentModel.TypeDescriptor.GetConverter(typeof(T));
            if (!string.IsNullOrWhiteSpace(row[term]))
            {
                result = (T)converter.ConvertFrom(row[term]);
            }
            return result;
        }

        /// <summary>
        /// Convert the value of a term to type T or null
        /// </summary>
        /// <typeparam name="T">Type to convert to</typeparam>
        /// <param name="row">A row object</param>
        /// <param name="index">The index of the field to convert</param>
        /// <returns>A value of type T</returns>
        /// <exception cref="ArgumentOutOfRangeException"/>
        /// <exception cref="ArgumentException" />
        public static T? ConvertNullable<T>(this IRow row, int index) where T : struct
        {
            var result = new T?();
            var converter = System.ComponentModel.TypeDescriptor.GetConverter(typeof(T));
            if (!string.IsNullOrWhiteSpace(row[index]))
            {
                result = (T)converter.ConvertFrom(row[index]);
            }
            return result;
        }

        /// <summary>
        /// Try and convert the value of a term to type T
        /// </summary>
        /// <typeparam name="T">Type to convert to</typeparam>
        /// <param name="row">A row object</param>
        /// <param name="term">The name of the term to convert</param>
        /// <param name="value">Value to return</param>
        /// <returns>A ConvertResult object that is true on success.  On failure will return false an contain an error message</returns>
        public static ConvertResult TryConvert<T>(this IRow row, string term, out T value) 
        {
            if (!row.TryGetField(term, out string data))
            {
                value = default;
                return ConvertResult.Failed($"Term {term} not found");
            }
            var converter = System.ComponentModel.TypeDescriptor.GetConverter(typeof(T));
            if (!converter.IsValid(data))
            {
                value = default;
                return ConvertResult.Failed($"Term {term} with value {data ?? "(null)"} could not be converted to type {typeof(T)}");
            }
            value = (T)converter.ConvertFrom(data);
            return ConvertResult.Success;
        }

        /// <summary>
        /// Try and convert the value of a term to type T
        /// </summary>
        /// <typeparam name="T">Type to convert to</typeparam>
        /// <param name="row">A row object</param>
        /// <param name="index">The index of the field to convert</param>
        /// <param name="value">Value to return</param>
        /// <returns>A ConvertResult object that is true on success.  On failure will return false an contain an error message</returns>
        public static ConvertResult TryConvert<T>(this IRow row, int index, out T value)
        {
            if(index < 0 || index >= row.FieldMetaData.Length)
            {
                value = default;
                return ConvertResult.Failed($"Index {index} out of range");
            }
            string data = row[index];
            var converter = System.ComponentModel.TypeDescriptor.GetConverter(typeof(T));
            if (!converter.IsValid(data))
            {
                value = default;
                return ConvertResult.Failed($"Field at index {index} with value {data ?? "(null)"} could not be converted to type {typeof(T)}");
            }
            value = (T)converter.ConvertFrom(data);
            return ConvertResult.Success;
        }

        /// <summary>
        /// Try and convert the value of a term to Nullable&lt;T&gt;
        /// </summary>
        /// <typeparam name="T">Type to convert to</typeparam>
        /// <param name="row">A row objedt</param>
        /// <param name="term">The name of the term to convert</param>
        /// <param name="value">Value to return</param>
        /// <returns>A ConvertResult object that is true on success.  On failure will return false an contain an error message</returns>
        public static ConvertResult TryConvertNullable<T>(this IRow row, string term, out T? value) where T : struct
        {
            value = default;
            if (!row.TryGetField(term, out string data))
            {
                return ConvertResult.Failed($"Term {term} not found");
            }
            if (!string.IsNullOrWhiteSpace(data))
            {
                var converter = System.ComponentModel.TypeDescriptor.GetConverter(typeof(T));
                if (!converter.IsValid(data))
                {
                    return ConvertResult.Failed($"Term {term} with value {data ?? "(null)"} could not be converted to type {typeof(T)}");
                }
                value = (T)converter.ConvertFrom(data);
            }
            return ConvertResult.Success;
        }

        /// <summary>
        /// Try and convert the value of a term to Nullable&lt;T&gt;
        /// </summary>
        /// <typeparam name="T">Type to convert to</typeparam>
        /// <param name="row">A row objedt</param>
        /// <param name="index">Index of the field to convert</param>
        /// <param name="value">Value to return</param>
        /// <returns>A ConvertResult object that is true on success.  On failure will return false an contain an error message</returns>
        public static ConvertResult TryConvertNullable<T>(this IRow row, int index, out T? value) where T : struct
        {
            value = default;
            if(index < 0 || index >= row.FieldMetaData.Length)
            {
                return ConvertResult.Failed($"Index {index} out of range");
            }
            string data = row[index];
            if (!string.IsNullOrWhiteSpace(data))
            {
                var converter = System.ComponentModel.TypeDescriptor.GetConverter(typeof(T));
                if (!converter.IsValid(data))
                {
                    return ConvertResult.Failed($"Field at index {index} with value {data ?? "(null)"} could not be converted to type {typeof(T)}");
                }
                value = (T)converter.ConvertFrom(data);
            }
            return ConvertResult.Success;
        }
    }
}
