using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    protected int current_story;
    protected Coroutine check;
    [SerializeField] protected GameObject[] PlayableModeObject;
    [SerializeField] protected GameObject PauseUI;

    [SerializeField] protected BGChanger bgchanger;
    [SerializeField] protected DialogueManager dialogueManager;
    
    protected void Awake()
    {
        current_story = GameManager.Instance.currentstage;
    }
    protected virtual void Start() 
    {
        check = null;
        BGChange();
    }
    protected virtual void BGChange()
    {
        bgchanger.BGChange(current_story+1);
        switch (current_story)
        {
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
                bgchanger.ChangeGrayScale(0);
                break;
            case 6:
                bgchanger.ChangeGrayScale(30);
                break;
            case 7:
                bgchanger.ChangeGrayScale(100);
                break;
        }
    }
    public void NPC_Touch()
    {
        if (check == null)
        {
            check= StartCoroutine(CustomSceneManager.Instance.Fade_event(Co_NPC_Touch()));
        }
    }
    protected IEnumerator Co_NPC_Touch()
    {
        foreach(GameObject go in PlayableModeObject)
        {
            go.SetActive(false);
        }
        dialogueManager.StartDialogue();
        yield break;
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        GameManager.Instance.TouchLock();
        AudioManager.Instance.PauseBgm();
        PauseUI.SetActive(true);
    }
    public void UnPauseGame()
    {
        Time.timeScale = 1f;
        GameManager.Instance.TouchUnlock();
        AudioManager.Instance.UnpauseBgm();
        PauseUI.SetActive(false);
    }
    public void SceneMove_Menu()
    {
        CustomSceneManager.Instance.LoadScene("03_MenuScene");
    }
}
