using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GAME_STATE _gameState;
    [SerializeField] GameObject _menuUI;
    [SerializeField] GameObject _inGameUI;
    [SerializeField] GameObject _overUI;
    [SerializeField] GameObject heart1,heart2,heart3;

    public enum GAME_STATE {
        MENU = 0,
        PLAY = 1,
        OVER = 2,
    }
    // Start is called before the first frame update
    void Start()
    {
        _gameState = GAME_STATE.MENU;
        AudioManager.instance.PlaySound(AudioManager.instance.BGMusicClip, 0, true);
    }

    // Update is called once per frame
    void Update()
    {
        if( _gameState == GAME_STATE.MENU || _gameState == GAME_STATE.OVER)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ChangeState(GAME_STATE.PLAY);
                AudioManager.instance.PlaySound(AudioManager.instance.UIClips[0], 0, false);
            }
        }

        if(PlayerController.instance._lives == 3)
        {
            heart1.SetActive(true); heart2.SetActive(true); heart3.SetActive(true);
        }
        if (PlayerController.instance._lives == 2)
        {
            heart1.SetActive(true); heart2.SetActive(true); heart3.SetActive(false);
        }
        if (PlayerController.instance._lives == 1)
        {
            heart1.SetActive(true); heart2.SetActive(false); heart3.SetActive(false);
        }
        if (PlayerController.instance._lives == 0)
        {
            heart1.SetActive(false); heart2.SetActive(false); heart3.SetActive(false);
        }

    }

    public void ChangeState(GAME_STATE gameState)
    {
        if(gameState == _gameState)
            return;

        if(gameState == GAME_STATE.MENU)
        {
            PlayerController.instance.Init();
            SpawnerController.instance.Init();

            _menuUI.SetActive(true);
            _inGameUI.SetActive(false);
            _overUI.SetActive(false);
        }
        if(gameState == GAME_STATE.PLAY)
        {
            SpawnerController.instance.Init();
            PlayerController.instance.Init();
            _menuUI.SetActive(false);
            _inGameUI.SetActive(true);
            _overUI.SetActive(false);
        }
        if(gameState == GAME_STATE.OVER)
        {
            AudioManager.instance.PlaySound(AudioManager.instance.UIClips[2], 0, false);
            _menuUI.SetActive(false);
            _inGameUI.SetActive(false);
            _overUI.SetActive(true);
        }
        this._gameState = gameState;
    }
}
