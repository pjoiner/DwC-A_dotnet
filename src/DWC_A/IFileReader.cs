using Dwc.Text;
using System;
using System.Collections.Generic;

namespace DWC_A
{
    public interface IFileReader : IDisposable
    {
        string FileName { get; }
        ICollection<FieldType> FieldTypes { get; }
        IEnumerable<IRow> DataRows { get; }
        IEnumerable<IRow> HeaderRows { get; }
        IEnumerable<IRow> Rows { get; }
    }
}