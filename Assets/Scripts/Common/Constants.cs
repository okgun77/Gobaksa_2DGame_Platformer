using UnityEngine;

public enum ItemType { Random = -1, Coin = 0, Invincibility, HPPotion, Projectile, Star, Count }

public class Constants
{
    public static readonly int      MaxLevel        = 10;
    public static readonly int      StarCount       = 3;

    public static readonly string   CurrentLevel    = "CURRENT_LEVEL";
    public static readonly string   LevelUnlock     = "LEVEL_UNLOCK";
    public static readonly string   LevelStar       = "LEVEL_STAR_";
    public static readonly string   Coin            = "COINCOUNT";

    public static (bool, bool[]) LoadLevelData(int _level)
    {
        bool isUnlock = PlayerPrefs.GetInt($"{LevelUnlock}{_level}", 0) == 1 ? true : false;

        bool[] stars = new bool[StarCount];
        for (int index = 0; index < StarCount; ++index)
        {
            stars[index] = PlayerPrefs.GetInt($"{LevelStar}{_level}{index}", 0) == 1 ? true : false;
        }

        return (isUnlock, stars);
    }

    public static void LevelComplete(int _level, bool[] _stars, int _coinCount)
    {
        PlayerPrefs.SetInt(Coin, _coinCount);

        if (_level+1 <= MaxLevel)
        {
            PlayerPrefs.SetInt($"{LevelUnlock}{_level + 1}", 1);
        }

        for (int index = 0; index < StarCount; ++index)
        {
            PlayerPrefs.SetInt($"{LevelStar}{_level}{index}", _stars[index] == true ? 1 : 0);
        }
    }
}