using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exitbutton : MonoBehaviour
{
    public void Exit_Game()
    {
        StartCoroutine(CustomSceneManager.Instance.EXIT_APP());
    }
}
