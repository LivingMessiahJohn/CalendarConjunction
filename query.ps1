<#
.SYNOPSIS
    Run SQL against Calendar5862to5867 on JOHNSDELLDT\SQLEXPRESS.

.EXAMPLE
    .\query.ps1

.EXAMPLE
    .\query.ps1 "SELECT TOP 20 Id, MonthHeading, Yom1 FROM dbo.Import"

.EXAMPLE
    .\query.ps1 -File .\my.sql
#>
param(
    [Parameter(Position = 0)]
    [string] $Query,

    [string] $File
)

$ErrorActionPreference = "Stop"

if ($Query -and $File) {
    throw "Pass a query or -File, not both."
}

[Console]::OutputEncoding = [System.Text.Encoding]::UTF8

$sqlcmd = @(
    "-S", "JOHNSDELLDT\SQLEXPRESS",
    "-d", "Calendar5862to5867",
    "-E",
    "-C",
    "-W",
    "-f", "65001"
)

if ($File) {
    & sqlcmd @sqlcmd -i $File
}
elseif ($Query) {
    & sqlcmd @sqlcmd -Q $Query
}
else {
    & sqlcmd @sqlcmd
}

exit $LASTEXITCODE
