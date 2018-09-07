using Dwc.Text;
using DWC_A.Meta;
using System;
using System.Collections.Generic;

namespace DWC_A
{
    public interface IFileReader : IDisposable
    {
        string FileName { get; }
        //ICollection<FieldType> FieldTypes { get; }
        IFileMetaData FileMetaData { get; }
        IEnumerable<IRow> DataRows { get; }
        IEnumerable<IRow> HeaderRows { get; }
        IEnumerable<IRow> Rows { get; }
        IFileIndex CreateIndexOn(string term);
        IEnumerable<IRow> ReadRowsAtIndex(IFileIndex index, string indexValue);
    }
}