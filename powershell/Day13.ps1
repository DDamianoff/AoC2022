#!/bin/pwsh

function Invoke-MainFunction {

    New-Variable -Name rawInput `
    -Value (Get-Content -Path './inputs/D0X.txt' -Raw)

    New-Variable -Name parsedInput -Value (($rawInput -Replace '\[','(' -Replace '\]',')' | Select-Object) -Split '(?:\r?\n){2,}')

    New-Variable -Name distressSignalsPair -Value (New-Object -TypeName 'System.Collections.ArrayList');

    # Get-Variable -Name parsedInput -ValueOnly | ForEach-Object -Process {
    #     $distressSignalsPair.Add((Invoke-Expression ('$({0},{1})' -F $_[0],$_[1]) ));
    # }
    foreach ($signal in $parsedInput) {
        $signal = $signal -Split "\n"
        $arrayExpression = ('$(({0}),({1}))' -F $signal[0],$signal[1]) -Replace "\(\)", "{}";
        Write-Host $arrayExpression
        $distressSignalsPair.Add((Get-Variable -Name arrayExpression -ValueOnly | Invoke-Expression));
    }


}


function Find-FaultyPacket {
    params(
        [string[]]$asd
    )
    $times = $($asd[0])
}


Invoke-MainFunction