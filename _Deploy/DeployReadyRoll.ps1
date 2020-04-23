[CmdletBinding(DefaultParameterSetName = 'None')]
param
(
  [Parameter(Mandatory=$true)]
  [hashtable]$parameters = $(Throw 'Please provide parameters for database deployment as a hashtable (eg @{DatabaseName="FCM15";DatabaseServer="local";})')
)

Write-Verbose "Entering script DeployReadyRollDatabaseTask.ps1"

foreach($key in $parameters.keys){
	Set-Variable -Name $key -Value $parameters[$key]
}
$ErrorActionPreference = "Continue"
Write-Verbose "PackagePath= $PackagePath"
Write-Verbose "ReleaseVersion= $ReleaseVersion"
Write-Verbose "DatabaseServer= $DatabaseServer"
Write-Verbose "DatabaseName= $DatabaseName"
Write-Verbose "UseWindowsAuth= $UseWindowsAuth"
Write-Verbose "DatabaseUserName= $DatabaseUserName"
Write-Verbose "DatabasePassword= $DatabasePassword"
$UseWindowsAuth = [boolean]::Parse("True")
(Get-Content "$PackagePath").replace('Write-Host','Write-Output') | Set-Content "$PackagePath"


# Execute the deployment script
$ExecutionResult = & $PackagePath
Write-Output $ExecutionResult
if ( $LASTEXITCODE -ne 0) {
	$ErrorMessage = $ExecutionResult.Split("`n")[-5..-1]
	Write-Error -Message [string]$ErrorMessage
}

Write-Verbose "Leaving script DeployReadyRollDatabaseTask.ps1"