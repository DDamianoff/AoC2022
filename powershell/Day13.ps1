#!/bin/pwsh

enum EvaluationResult {
    faulty
    neutral
    healty
}

function Invoke-MainFunction {
    Set-StrictMode  -Version Latest

    New-Variable    -Name rawInput `
                    -Value (Get-Content -Path './inputs/D0X.txt' -Raw)

    New-Variable    -Name parsedInput `
                    -Value ($rawInput -Split '(?:\r?\n){2,}')

    New-Variable    -Name distressSignalsPair `
                    -Value (New-Object -TypeName 'System.Collections.ArrayList');

    $parsedInput | ForEach-Object -Process {
        $signal = ("$_" -Split "\n");
        
        $left = ($signal[0] | ConvertFrom-Json -Depth 1000 -NoEnumerate);
        $right = ($signal[1] | ConvertFrom-Json -Depth 1000 -NoEnumerate);
        
        $pair = @{
            left = $left
            right = $right
        };

        $distressSignalsPair.Add($pair) | Out-Null;
    }

    # Part one:

    New-Variable -Name Counter -Value 0;

    1..($distressSignalsPair.Count) | ForEach-Object -Process {

        $item = $distressSignalsPair[$_ - 1];

        $faulty = Find-FaultyPacket -SignalPair $item

        if (-Not $faulty) {
            $Counter += $_;
        }
    }

    return $Counter
    
    # /part one
    
    # Part two:
    & {
#        function Get-trueValue {
#            param ($corr)
#
#            if ($corr -is [array]) {
#                if ($corr.Length -eq 0) {
#                    return $corr;
#                }
#
#                return Get-trueValue $corr[0];
#            }
#            return $corr;
#        }
#
#        New-Variable -Name signalPool -Value (New-Object -TypeName 'System.Collections.ArrayList');
#        New-Variable -Name keyPair -Value (@{
#            left = ('[[2]]'     | ConvertFrom-Json -NoEnumerate);
#            right = ('[[6]]'    | ConvertFrom-Json -NoEnumerate);
#        })
#
#        $distressSignalsPair.Add($keyPair) | Out-Null;
#
#        $distressSignalsPair | ForEach-Object -Process {
#            $signalPool.Add($_.left) | Out-Null;
#            $signalPool.Add($_.right) | Out-Null;
#        }
#
#        $sorted = $signalPool | Sort-Object {Get-trueValue $_}
#        
#        $sorted | ForEach-Object -Process {
#            Write-Host ($_ | ConvertTo-Json -Depth 100 -Compress)
#        }

    }
}


function Find-FaultyPacket {
    param(
        [Hashtable]$signalPair
    )

    New-Variable    -Name leftPart `
                    -Value $signalPair.left `
                    -Description "left segment containing integers and lists" `
                    -Option ReadOnly;
    
    New-Variable    -Name rightPart `
                    -Value $signalPair.right `
                    -Description "right segment containing integers and lists" `
                    -Option ReadOnly;
    
    New-Variable    -Name times `
                    -Value ($leftPart.Count -gt $rightPart.Count `
                                    ? $rightPart.Count `
                                    : $leftPart.Count) `
                    -Description "How many times the comparison should be done. 
                                  Determined by the side with less elements" `
                    -Option ReadOnly;

    [EvaluationResult]$result = Test-UndefinedPair -leftSide $leftPart -rightSide $rightPart;

    if ($result -eq [EvaluationResult]::faulty) {
        return $true;
    }

    return $false;
}

function Test-SingleIntegerPair {
    param ([int]$left, [int]$right)

    $evaluation = [Math]::Sign($right - $left)

    switch ($evaluation) {
        -1 {
            return [EvaluationResult]::faulty
        }
         1 {
            return [EvaluationResult]::healty
        }
         0 {
            return [EvaluationResult]::neutral
        }
    }
}

function Test-ListPair {
    param ($leftList, $rightList)

    New-Variable    -Name times `
                    -Value ($($leftList.Length, $rightList.Length) | Sort-Object -Descending | Select-Object -Last 1)

    for ($i = 0; $i -lt $times; $i++) {
        $leftElement = $leftList[$i];
        $rightElement = $rightList[$i];

        [EvaluationResult]$result = Test-UndefinedPair -leftSide $leftElement -rightSide $rightElement

        if ($result -eq [EvaluationResult]::neutral) {
            continue;
        }

        return $result
    }

    # Right ran out first?
    if ($leftList.Length -gt $rightList.Length) {
        return [EvaluationResult]::faulty;
    }

    # Left ran out first?
    if ($leftList.Length -lt $rightList.Length) {
        return [EvaluationResult]::healty;
    }

    return [EvaluationResult]::neutral;
}

function Test-MixedPair {
    param ($leftElement, $rightElement)

    return (Test-ListPair -leftList ([array]$leftSide) -rightList ([array]$rightSide));
}

function Test-UndefinedPair {
    param ($leftSide, $rightSide)

    $leftIsList = $leftSide -is [array];
    $rightIsList = $rightSide -is [array];

    New-Variable -Name result;

    # if both lists:
    if ($leftIsList -and $rightIsList) {
        Set-Variable -name result -Value (Test-ListPair -leftList $leftSide -rightList $rightSide)
    }

    # if both int:
    elseif (-not $leftIsList -and -not $rightIsList) {
        Set-Variable -name result -Value (Test-SingleIntegerPair -left $leftSide -right $rightSide)
    }

    # mixed:
    elseif ($leftIsList -xor $rightIsList) {
        Set-Variable -name result -Value (Test-MixedPair -leftElement $leftSide -rightElement $rightSide)
    }

    return $result;
}

Invoke-MainFunction -Verbose