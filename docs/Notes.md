# Notes

## Schema Import

Used dotnet core tool xscgen to generate classes for serializing TDWG schema found at
http://rs.tdwg.org/dwc/text/tdwg_dwc_text.xsd

Install as a global tool from NuGet using the following command
```
dotnet tool install --global dotnet-xscgen --version 2.0.149
```
usage:
```
xscgen -i=i -n "|tdwg_dwc_text.xsd=DWC_A.Meta" -o ./src/DWC_A/Meta/ http://rs.tdwg.org/dwc/text/tdwg_dwc_text.xsd
```

## Test Coverage

Test coverage tool coverlet has been installed in this project and can be activated by issuing the following command
```
dotnet test "./src/DwC-A_dotnet.sln" /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=".coverage/report"
```
This tool can be used to set threshold coverage for passing builds.  See https://github.com/tonerdo/coverlet for more information and more detailed reports.

Detailed coverage reports can be generated using ReportGenerator (See https://github.com/danielpalme/ReportGenerator).

Note: Replace the following example with dotnet-cli command
```
\users\pjoiner_2\.nuget\packages\reportgenerator\3.1.2\tools\reportgenerator.exe "-reports:report.opencover.xml" "-targetdir:./report"
```