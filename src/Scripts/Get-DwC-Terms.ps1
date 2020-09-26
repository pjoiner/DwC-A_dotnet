# This script can be used to update the Terms.cs file with the latest terms
# Usage: ./Get-DwC-Terms.ps1 6>Terms.cs

$response = Invoke-Webrequest -Uri https://raw.githubusercontent.com/tdwg/dwc/master/vocabulary/term_versions.csv
$response.Content | Out-File -FilePath "./term_versions.csv" -Encoding utf8

$csv = Import-Csv -Path "./term_versions.csv"

$recommended = $csv | Where-Object -Propert "status" -Value "recommended" -EQ | Sort-Object -Property term_localname

[console]::bufferwidth = 32766
Write-Host "namespace DwC_A.Terms"
Write-Host "{"
Write-Host "    public partial class Terms"
Write-Host "    {"
foreach ($term in $recommended) { 
    if ( $term.term_iri -match "http://rs.tdwg.org/dwc/iri/") {
        continue;
    }
    Write-Host "        ///<summary>"
    $comments = $term.definition.Split([Environment]::NewLine)
    foreach ($comment in $comments) {
        Write-Host "        ///$comment"
    }
    Write-Host "        ///</summary>"
    Write-Host "        ///<value>"
    Write-Host "        ///$($term.term_iri)"
    Write-Host "        ///</value>"
    Write-Host "        public static readonly string $($term.term_localname) = ""$($term.term_iri)"";"
}
Write-Host "    }"
Write-Host "}"
