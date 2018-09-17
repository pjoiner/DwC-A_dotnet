# Readme [![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)

DWC_A is a simple Darwin Core Archive reader for dotnet.

## Install

To add DWC_A to your project run the following command in the Visual Studio Package Manager Console

	PM> Install-Package DWC_A

## Usage

To read a Darwin Core Archive file and display core data rows.

```
using DWC_A;

using (var archive = new ArchiveReader(fileName))
{
	foreach(var row in archive.CoreFile.Rows)
	{
		//Display field 0 of each row
		Console.WriteLine(row[0]);
	}
}
```

More information can be found in the [documentation](docs/Documentation.md).


