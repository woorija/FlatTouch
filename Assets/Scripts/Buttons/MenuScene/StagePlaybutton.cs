using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagePlaybutton : MonoBehaviour
{
    public void Move_StageScene()
    {
        CustomSceneManager.Instance.LoadScene("04_StageScene");
    }
}
