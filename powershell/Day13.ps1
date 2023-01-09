#!/bin/pwsh

function Invoke-MainFunction {

    New-Variable    -Name rawInput `
                    -Value (Get-Content -Path './inputs/D0X.txt' -Raw)

    New-Variable    -Name parsedInput `
                    -Value ($rawInput -Split '(?:\r?\n){2,}')

    New-Variable    -Name distressSignalsPair `
                    -Value (New-Object -TypeName 'System.Collections.ArrayList');

    foreach ($item in $parsedInput) {
        $current = $item
        $signal = ($current -Split "\n") -replace "\[\]",'[[[-1]]]';
        
        $left = $signal[0] | ConvertFrom-Json -Depth 100;
        $right = $signal[1] | ConvertFrom-Json -Depth 100;
        $pair = @{
            left = [Object[]]$($left)
            right = [Object[]]$($right)
        };
        $distressSignalsPair.Add($pair);
    }
    
    foreach ($item in $distressSignalsPair) {
        Write-Host ($item.left) vs ($item.right)
        $result = Find-FaultyPacket -SignalPair $item
        Write-Host ($result ? "faulty" : "not faulty")
        Write-Host "-------------"
    }

    
}


function Find-FaultyPacket {
    param(
        [Hashtable]$signalPair
    )


    function Test-SingleIntegerPair {
        param ($left, $right)
        if (($left -eq -1 -and $right -ne -1)) {
            return $false;
        }

        return $left -gt $right;
    }

    function Test-ListPair {
        param ($leftList, $rightList)

        $times = $leftList.Length -gt $rightList.Length ? $rightList.Length : $leftList.Length;

        for ($i = 0; $i -lt $times; $i++) {
            $parsedLeft = $leftList -is [array] ? $leftList : [array]$leftList;
            $parsedRight = $rightList -is [array] ? $rightList : [array]$rightList;

            if ($left -is [array] -or $right -is [array]) {
                [bool]$faulty = (Test-ListPair -leftList ($parsedLeft) -rightList ($parsedRight));
                
                if ($faulty) {
                    return $faulty;
                }
            }

            $faulty = (Test-SingleIntegerPair -left $left -right $right)

            if ($faulty) {
                return $true;
            }

        }

        return $false;
    }

    $times = $signalPair.left.Length -gt $signalPair.right.Length ? $signalPair.right.Length : $signalPair.left.Length;

    for ($current = 0; $current -lt $times; $current++) {

        $left = ($signalPair.left)[$current];
        $right = ($signalPair.right)[$current];
        [bool]$faulty = $false;

        <#
         # IF left -eq right but with any kind of object
         # Compare-Object returns an array of differences, or null
         # if no one.
         # Type inference makes most of the job: 
         # - null: false
         # - differences[]: true
         # - $(2) vs 2:
         #   - $(2) vs $(2)
         #   - no differences
         #
         # Most precise description should be:
         # "if there is not differences between these two objects..."
         #>
        # if (-Not (Compare-Object $left $right)) {
        #     continue;
        # }
        
        if ($left -is [array] -or $right -is [array]) {
            $parsedLeft = $left -is [array] ? $left : [array]$left;
            $parsedRight = $right -is [array] ? $right : [array]$right;
            $faulty = (Test-ListPair -leftList ($parsedLeft) -rightList ($parsedRight))

            if ($faulty){
                return $true;
            }

            continue;
        }

        $faulty = (Test-SingleIntegerPair -left $left -right $right);
        if ($faulty) {
            return $true;
        }
    
    }
    return $signalPair.left.Length -gt $signalPair.right.Length;
}


Invoke-MainFunction