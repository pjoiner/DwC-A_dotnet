Readme

Schema Import

Used dotnet core tool xscgen to generate classes for serializing TDWG schema found at
http://rs.tdwg.org/dwc/text/tdwg_dwc_text.xsd

Install as a global tool from NuGet using the following command
	dotnet tool install --global dotnet-xscgen --version 2.0.149

usage:
	xscgen -i=i -n "|tdwg_dwc_text.xsd=DWC_A.Meta" -o ./src/DWC_A/Meta/ http://rs.tdwg.org/dwc/text/tdwg_dwc_text.xsd