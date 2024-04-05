using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Unity.VisualScripting;

public class UILevel : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Sprite             spriteLevelLock;
    [SerializeField] private Image              imageLevelIcon;
    [SerializeField] private TextMeshProUGUI    textLevel;
    [SerializeField] private GameObject         starBackgroundObject;
    [SerializeField] private GameObject[]       starObjects;

    private int     level;
    private bool    isUnlock;
    private Image   imageFadeScreen;

    public void SetLevel(int _level, bool _isUnlock, bool[] _stars, Image _imageFadeScreen)
    {
        this.level              = _level;
        this.isUnlock           = _isUnlock;
        this.imageFadeScreen    = _imageFadeScreen;

        if (_isUnlock == true)
        {
            textLevel.enabled   = true;
            textLevel.text      = _level.ToString();
        }
        else
        {
            imageLevelIcon.sprite   = spriteLevelLock;
            textLevel.enabled       = false;

            starBackgroundObject.SetActive(false);
        }

        for (int i = 0; i < starObjects.Length; ++i)
        {
            starObjects[i].SetActive(_stars[i]);
        }
    }

    public void OnPointerClick(PointerEventData _data)
    {
        if (isUnlock == true)
        {
            imageFadeScreen.gameObject.SetActive(true);
            StartCoroutine(FadeEffect.Fade(imageFadeScreen, 0, 1, 1, AfterFadeEffect));
        }
    }

    private void AfterFadeEffect()
    {
        PlayerPrefs.SetInt(Constants.CurrentLevel, level);
        Utils.LoadScene(SceneNames.Game);
    }

}
