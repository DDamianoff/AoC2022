using System.Management.Automation;
using Advent22.Utils;

namespace Advent22;

public static class Day13
{
    public static void DayThirteen()
    {
        var script = File.ReadAllText("./powershell/Day13.ps1");
        
        using var powershell = PowerShell.Create();

        powershell
            .AddScript(script)
            .Invoke()[0]
            .Display("Sum of healthy-packet's indexes");
    }
}