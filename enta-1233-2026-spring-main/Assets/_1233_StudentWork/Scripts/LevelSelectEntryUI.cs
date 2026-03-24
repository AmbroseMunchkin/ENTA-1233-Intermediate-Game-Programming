using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class LevelSelectEntryUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _tittleText;

    private int _levelIndex;

    public void Setup(string level, int levelIndex)
    {
        _levelIndex = levelIndex;

        if (_tittleText != null) _tittleText.text = level;
    }
    public void ButtonPressed()
    {
        LevelMgr.Instance.SetCurrentLevel(_levelIndex);
        SceneMgr.Instance.LoadScene(GameScenes.Gameplay, GameMenus.InGameUI);
    }
}
