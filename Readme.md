Readme

Schema Import

Used dotnet core tool xscgen to generate classes for serializing TDWG schema found at
http://rs.tdwg.org/dwc/text/tdwg_dwc_text.xsd

Install as a global tool from NuGet using the following command
	dotnet tool install --global dotnet-xscgen --version 2.0.149

usage:
	xscgen -o ./src/DWC_A/Meta/tdwg_dwc_text.cs http://rs.tdwg.org/dwc/text/tdwg_dwc_text.xsd
