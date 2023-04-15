using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButton : MonoBehaviour
{
    public void Move_TutorialScene()
    {
        CustomSceneManager.Instance.LoadScene("07_TutorialScene");
    }
}
