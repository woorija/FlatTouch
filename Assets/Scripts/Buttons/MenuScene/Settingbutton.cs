using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settingbutton : MonoBehaviour
{
    public void Start_Game()
    {
        CustomSceneManager.Instance.LoadScene("03_MenuScene");
    }
}
