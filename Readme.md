# Readme [![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)![Build Status](https://github.com/pjoiner/DwC-A_dotnet/actions/workflows/dotnet.yml/badge.svg)

DwC-A_dotnet is a simple Darwin Core Archive reader for dotnet.

## Install

To add DwC-A_dotnet to your project run the following command in the Visual Studio Package Manager Console

    PM> Install-Package DwC-A_dotnet

## Usage

To read a Darwin Core Archive file and display core data rows.

```
using DwC_A;

string fileName = "./dwca-uta_herps-v8.1.zip";
using (var archive = new ArchiveReader(fileName))
{
	foreach(var row in archive.CoreFile.Rows)
	{
		//Display field 0 of each row
		Console.WriteLine(row[0]);
	}
}
```

More information can be found in the [Documentation Wiki](https://github.com/pjoiner/DwC-A_dotnet/wiki).
