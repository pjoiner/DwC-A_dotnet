$response = Invoke-Webrequest -Uri https://www.dublincore.org/specifications/dublin-core/dcmi-terms/
$response.Content | Out-File -FilePath "./dcmi_terms.html" -Encoding utf8

$dcmi_terms = $response.AllElements | 
    Where-Object {$_.class -eq "divider-title-row"} | 
    Select-Object -Property id, innerText |
    Sort-Object -Property innerText

#$namespace = "terms"
if( $namespace -eq "terms"){
    $terms_terms = $dcmi_terms |
        Where-Object {$_.id -match "http://purl.org/dc/terms/"}
}else{
    $terms_terms = $dcmi_terms |
        Where-Object {$_.id -match "http://purl.org/dc/elements/1.1/"}
}

[console]::bufferwidth = 32766
Write-Host "namespace DwC_A.Terms"
Write-Host "{"
if( $namespace -eq "terms" ){
    Write-Host "    public partial class DcmiTerms"
}else{
    Write-Host "    public partial class DcmiElements"
}
Write-Host "    {"
foreach($term in $terms_terms){
    if( $term -match "Term Name: (.*?) " ){
        Write-Host "        public static readonly string $($Matches.1) = ""$($term.id)"";" 
    }
}
Write-Host "    }"
Write-Host "}"


