using System;
using System.Collections.ObjectModel;

namespace WowExpCalculator;

public static class ExpCalculationService
{
    public static void CalculateExpForParty(ObservableCollection<Toon> party, int mobLevel)
    {
        if (mobLevel == 0 || party.Count == 0)
        {
            foreach (var toon in party)
            {
                toon.ExpEarned = 0;
            }
            return;
        }

        var mobXp = GetBaseMobXp(mobLevel);
        var groupModifier = GetGroupModifier(party.Count);
        var groupFactor = groupModifier / party.Count;

        foreach (var toon in party)
        {
            var expEarned = (int)(mobXp * groupFactor * (1 - ((toon.Level - mobLevel) * 0.2)));
            toon.ExpEarned = Math.Max(expEarned, 0);
        }
    }

    private static int GetBaseMobXp(int mobLevel)
    {
        return 45 + 5 * mobLevel;
    }

    private static double GetGroupModifier(int groupSize)
    {
        return groupSize switch
        {
            1 => 1.0,
            2 => 1.5,
            3 => 1.6,
            4 => 1.7,
            5 => 1.8,
            _ => 1.0
        };
    }
}
