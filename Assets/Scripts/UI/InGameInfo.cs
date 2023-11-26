using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameInfo : MonoBehaviour
{
    [SerializeField] private List<GunData>  _gunObjects;
    [SerializeField] private TextMeshProUGUI _textName;
    [SerializeField] private TextMeshProUGUI _textAmmo;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _gameWinPanel;
    [SerializeField] private GameObject _gamePausePanel;
    private SettingsCursor _settingsCursor;
    private int _selectedGun;

    private void Awake()
    {
        _settingsCursor = new SettingsCursor();
    }

    private void Update() 
    {
        SetInfo();
    }

    private void SetInfo()
    {
        _textName.text = _gunObjects[_selectedGun].gunName;
        _textAmmo.text = String.Format("{0}/{1} | {2}", _gunObjects[_selectedGun].currentBulletCount.ToString(), 
                                                        _gunObjects[_selectedGun].maxBulletCount.ToString(), 
                                                        _gunObjects[_selectedGun].gunMagazineCount.ToString()) ;
    }

    private void SetGameWin()
    {
        _gameWinPanel.SetActive(true);

        _settingsCursor.CursorOn();

        Time.timeScale = 0;
    }

    private void SetGameOver() 
    {
        _gameOverPanel.SetActive(true);

        _settingsCursor.CursorOn();

        Time.timeScale = 0;
    }

    private void StartGame()
    {
        _gameOverPanel.SetActive(false);
        _gameWinPanel.SetActive(false);
        _gamePausePanel.SetActive(false);

        _settingsCursor.CursorOff();

        Time.timeScale = 1;
    }

    private void PauseGame()
    {
        _gamePausePanel.SetActive(true);

        _settingsCursor.CursorOn();

        Time.timeScale = 0;
    }

    private void SetInfoByPistol()
    {
        _selectedGun = 1;
    }

    private void SetInfoByM4A4()
    {
        _selectedGun = 0;
    }
    
    private void OnEnable()
    {
        SelectGunActions.OnSelectM4A4 += SetInfoByM4A4;
        SelectGunActions.OnSelectPistol += SetInfoByPistol;

        PlayerActions.GameOver += SetGameOver;
        PlayerActions.GameWin += SetGameWin;
        PlayerActions.StartGame += StartGame;
        PlayerActions.PauseGame += PauseGame;
    }

    private void OnDisable()
    {
        SelectGunActions.OnSelectM4A4 -= SetInfoByM4A4;
        SelectGunActions.OnSelectPistol -= SetInfoByPistol;

        PlayerActions.GameOver -= SetGameOver;
        PlayerActions.GameWin -= SetGameWin;
        PlayerActions.StartGame -= StartGame;
        PlayerActions.PauseGame -= PauseGame;
    }
}
