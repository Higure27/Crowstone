using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons3D : MonoBehaviour {

    [SerializeField]
    private string buttonName;

    private void OnMouseDown()
    {
        switch (buttonName)
        {
            case "start":
                SoundManager.Instance.playMenuStartClicked();
                GameManager.gameManager.setCurrentDay(1);
                LevelManager.Instance.startLoadSpecificScene("Town");
                break;
            case "quit":
                SoundManager.Instance.playMenuClick();
                Application.Quit();
                break;
        }
    }
}
