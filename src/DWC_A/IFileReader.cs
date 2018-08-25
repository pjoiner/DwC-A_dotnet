using System;
using System.Collections.Generic;

namespace DWC_A
{
    public interface IFileReader : IDisposable
    {
        IEnumerable<IRow> DataRows { get; }
        IEnumerable<IRow> HeaderRows { get; }
        IEnumerable<IRow> Rows { get; }
    }
}