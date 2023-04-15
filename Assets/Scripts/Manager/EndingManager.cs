using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingManager : StoryManager
{
    int currentBGindex;
    //[SerializeField] 성 오브젝트
    [SerializeField] GameObject[] NPCs;
    [SerializeField] GameObject trigger;
    [SerializeField] GameObject castle;

    [SerializeField] SpriteRenderer Player;
    protected override void Start()
    {
        Init();
        base.Start();
    }
    void Init()
    {
        currentBGindex = 8;
        bgchanger.ChangeGrayScale(100);
        PlayerSetting();
        BGChange();
    }
    void PlayerSetting()
    {
        if (currentBGindex == 4)
        {
            PlayerSetting_left();
        }
        else
        {
            PlayerSetting_right();
        }
    }
    void PlayerSetting_right()
    {
        Player.gameObject.transform.position = new Vector3(3200, -900, 0);
        Player.flipX = true;
    }
    void PlayerSetting_left()
    {
        Player.gameObject.transform.position = new Vector3(-100, -900, 0);
        Player.flipX = false;
    }
    protected override void BGChange()
    {
        currentBGindex--;
        switch (currentBGindex)
        {
            case 4: // 성 내부는 성 주변에서 성 클릭으로 이동하기때문에 패스
                currentBGindex--;
                break;
            case 1:
                trigger.SetActive(false);
                castle.SetActive(true);
                break;
            case 0:
                currentBGindex = 4;
                castle.SetActive(false);
                Enable_NPC();
                break;
        }
        bgchanger.BGChange(currentBGindex);
    }
    void Enable_NPC()
    {
        foreach(GameObject go in NPCs)
        {
            go.SetActive(true);
        }
    }

    public void SceneChange()
    {
        if (check == null)
        {
            check=StartCoroutine(CustomSceneManager.Instance.Fade_event(Co_SceneChange()));
        }
    }
    IEnumerator Co_SceneChange()
    {
        Time.timeScale = 10;
        BGChange();
        PlayerSetting();
        yield return null;
        check = null;
        yield return null;
        Time.timeScale = 1;
    }
}
