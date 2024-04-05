using UnityEngine;

public class UIGamePopup : MonoBehaviour
{
    [Header("공통 : 검은배경")]
    [SerializeField] private GameObject     overlayBackground;

    [Header("일시정지")]
    [SerializeField] private GameObject     popupPause;

    [Header("레벨 실패")]
    [SerializeField] private GameObject     popupLevelFailed;

    [Header("레벨 완료")]
    [SerializeField] private GameObject     popupLevelComplete;
    [SerializeField] private GameObject[]   starObjects;

    public void SetTimeScale(float _scale)
    {
        Time.timeScale = _scale;
    }

    public void Pause()
    {
        SetTimeScale(0);
        overlayBackground.SetActive(true);
        popupPause.SetActive(true);
    }

    public void LevelFailed()
    {
        SetTimeScale(0);
        overlayBackground.SetActive(true);
        popupLevelFailed.SetActive(true);
    }

    public void LevelComplete(bool[] _stars)
    {
        SetTimeScale(0);
        overlayBackground.SetActive(true);
        popupLevelComplete.SetActive(true);

        for (int i = 0; i < starObjects.Length; ++i)
        {
            starObjects[i].SetActive(_stars[i]);
        }
    }

    public void Resume()
    {
        SetTimeScale(1);
        overlayBackground.SetActive(false);
        popupPause.SetActive(false);
    }

    public void SelectLevel()
    {
        SetTimeScale(1);
        Utils.LoadScene(SceneNames.SelectLevel);
    }

    public void Restart()
    {
        SetTimeScale(1);
        Utils.LoadScene();
    }

    public void NextLevel()
    {
        SetTimeScale(1);
        Utils.LoadScene();
    }

}
