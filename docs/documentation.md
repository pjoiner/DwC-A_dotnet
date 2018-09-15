# Documentation

## Contents
- Basic Usage
  * [Reading Core File Data](##Reading_Core_File_Data)
  * [Read Archive Meta data](##Read_Archive_Meta_data)
  * [Reading Extension Files](##Reading_Extension_Files)
  * [Indexing Files](##Indexing_Files)

## Reading Core File Data

```
    using DWC_A;
    using DWC_A.Terms;
    using System.Linq;

    class Program
    {
        static void Main()
        {

            string fileName = "./dwca-global-v2.1.zip";
            using (var archive = new ArchiveReader(fileName))
            {
                foreach(var row in archive.CoreFile.Rows)
                {
                    //Access fields by index
                    Console.WriteLine(row[0]);
                    //Access fields by Term
                    Console.WriteLine(row[Terms.type]);
                    //Iterate over or query fields in a row using LinQ
                    var fields = row.Fields.ToList().Aggregate((current, next) => $"{current}\t{next}");
                    Console.WriteLine(fields);
                }
            }
        }
    }
```

## Read Archive Meta data

Archive meta data can be accessed through the `ArchiveReader.MetaData` property.

## Reading Extension Files

Extension files can be accessed through the `ArchiveReader.Extensions` `FileReaderCollection`.  Extension file readers can be referenced by filename
```
IFileReader fileReader = archive.Extensions.GetFileReaderByFileName("event.txt");
```
OR they can referenced by the row type associated with the extension file.
```
IFileReader fileReader = archive.Extensions.GetFileReaderByRowType(RowTypes.Event);
```

## Indexing Files

Indexes can be created on fields in a file to improve lookup performance.  This is useful for reading extension rows for a specified core id value.  An index is created using the `IFileReader.CreateIndexOn` method as follows.
```
IFileIndex index = fileReader.CreateIndexOn(Terms.eventID);
```
Rows can then be read from the `IFileReader` using the `ReadRowsAtIndex` method as follows.
```
string indexValue = "1234";  //Value of index to lookup
IEnumerable<IRow> rows = fileReader.ReadRowsAtIndex(index, indexValue);
```
Multiple indexes can be created for a file.

 