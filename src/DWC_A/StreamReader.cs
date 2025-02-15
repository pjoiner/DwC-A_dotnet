﻿using DwC_A.Extensions;
using DwC_A.Factories;
using DwC_A.Meta;
using System.Collections.Generic;
using System.IO;

namespace DwC_A
{
    internal partial class StreamReader 
    {
        private readonly ITokenizer tokenizer;
        private readonly IRowFactory rowFactory;
        private readonly IFileMetaData fileMetaData;

        public StreamReader(
            IRowFactory rowFactory,
            ITokenizer tokenizer,
            IFileMetaData fileMetaData)
        {
            this.fileMetaData = fileMetaData;
            this.rowFactory = rowFactory;
            this.tokenizer = tokenizer;
        }

        public IEnumerable<IRow> ReadRows(Stream stream)
        {
            using(var reader = new System.IO.StreamReader(stream, fileMetaData.Encoding))
            {
                string line;
                while ((line = reader.ReadRow(fileMetaData)) != null)
                {
                    yield return rowFactory.CreateRow(tokenizer.Split(line), fileMetaData.Fields);
                }
            }
        }
    }
}
