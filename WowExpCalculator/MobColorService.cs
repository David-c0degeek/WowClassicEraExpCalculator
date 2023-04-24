namespace WowExpCalculator;

public abstract class MobColorService
{
    public static string GetMobColor(int mobLevel, int toonLevel)
    {
        var levelDifference = mobLevel - toonLevel;

        return levelDifference switch
        {
            >= 5 => "Red",
            >= 3 => "Orange",
            >= -2 => "Yellow",
            >= -3 => "Green",
            _ => "Gray"
        };
    }
}
