using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTouch : MonoBehaviour
{
    [SerializeField] StoryManager storymanager;
    private void OnMouseDown()
    {
        Debug.Log(111);
        storymanager.NPC_Touch();
    }
}
