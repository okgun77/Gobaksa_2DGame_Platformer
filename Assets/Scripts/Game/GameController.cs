using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private UIGamePopup    uiGamePopup;
    [SerializeField] private PlayerData     playerData;

    private int     currentLevel = 1;
    private bool    isLevelFailed = false;
    private bool    isLevelComplete = false;

    public void LevelFailed()
    {
        if (isLevelFailed == true) return;
        
        isLevelFailed = true;

        uiGamePopup.LevelFailed();
    }

    public void LevelComplete()
    {
        if (isLevelComplete == true) return;

        isLevelComplete = true;

        uiGamePopup.LevelComplete(playerData.Stars);
        Constants.LevelComplete(currentLevel, playerData.Stars, playerData.Coin);
    }
}
