using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class FailedPauseMenu : MonoBehaviour
{
    public GameObject _failedMenu;
    [SerializeField] private GameObject _pauseMenu;
    

    public void GameOver()
    {
        _failedMenu.SetActive(true);
        //GameOverPanelIntro();
    }
    public void StopGameOver()
    {
       // GameOverPanelOutro();
        _failedMenu.SetActive(false);

    }
    public void PauseMenu()
    {
        _pauseMenu.SetActive(true);
        //PausePanelIntro();

    }
    public void Unpause()
    {
        //PausePanelOutro();
        _pauseMenu.SetActive(false);
        
    }
   
}
