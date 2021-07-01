using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _menuUI, _inGameUI, _wimIU, _lostUI;
    [SerializeField]
    private GameObject _numberOfCartridges, _infinityCartridges;
    [SerializeField]
    private Image _levelBar, _healthBar;
    [SerializeField]
    private Text _textLevelWin, _textLevelCurent, _textLevelTarget, _textNumberOfCartridges;
    private LevelConditions _levelConditions;
    private void Awake()
    {
        FindObjectOfType<PlayerShot>().Shot += ShootingInformation;
        FindObjectOfType<PlayerLife>().HealthChange += HealthInformation;
        _levelConditions = FindObjectOfType<LevelConditions>();
    }

    private void Start()
    {
        _textLevelWin.text = "Level " + PlayerPrefs.GetInt("Level").ToString();
        _textLevelCurent.text = PlayerPrefs.GetInt("Level").ToString();
        _textLevelTarget.text = (PlayerPrefs.GetInt("Level") + 1).ToString();
    }
    private void FixedUpdate()
    {
        _levelBar.fillAmount = Mathf.Lerp(_levelBar.fillAmount,_levelConditions.PorocentOfTheKilled,0.3f);
    }
    public void GameStageWindow(Stage stageGame)
    {
        switch (stageGame)
        {
            case Stage.StartGame:

                _menuUI.SetActive(true);
                _inGameUI.SetActive(false);
                break;

            case Stage.StartLevel:

                _menuUI.SetActive(false);
                _inGameUI.SetActive(true);
                break;

            case Stage.WinGame:

                _inGameUI.SetActive(false);
                _wimIU.SetActive(true);
                //впиши сюда поднятие уровня и сцены 
                break;

            case Stage.LostGame:

                _inGameUI.SetActive(false);
                _lostUI.SetActive(true);
                break;
        }
    }
    public void HealthInformation(float Health)
    {
        _healthBar.fillAmount = Health;
    }
    public void ShootingInformation(int numberOfCartridges)
    {
        if (numberOfCartridges > 0)
        {
            _infinityCartridges.SetActive(false);
            _numberOfCartridges.SetActive(true);

            _textNumberOfCartridges.text = numberOfCartridges.ToString();
        }
        else
        {
            _infinityCartridges.SetActive(true);
            _numberOfCartridges.SetActive(false);
        }
    }
}
