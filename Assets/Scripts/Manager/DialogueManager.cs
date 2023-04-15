using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject[] dialogueUIs; //다이얼로그 오브젝트 전체

    [SerializeField] PortraitUI portraitUI;
    [SerializeField] DialogueUI dialogueUI;

    public static bool b_IsTypingEnd; // 타 스크립트에서 클릭 제어를 하기 위한 변수
    
    int index;
    Dialogue_data currentdata;
    public void StartDialogue()
    {
        index = GameManager.Instance.currentstage * 1000 + 1;
        foreach (GameObject go in dialogueUIs) {
             go.SetActive(true);
        }
        SetDialogueData();
    }
    void SetDialogueData()
    {
        b_IsTypingEnd = false;
        currentdata = DialogueDB.dialogue_data[index];
        portraitUI.GetPortrait(currentdata.Character_num);
        if (currentdata.Character_num != 6) // 마법봉 이펙트가 아닐경우
        {
            portraitUI.GetEmote(currentdata.emote_num);
            dialogueUI.GetName(DialogueDB.character_name[currentdata.Character_num]);
            dialogueUI.GetDialogueText(currentdata.dialogue);
        }

        SetDialogue();
    }
    void SetDialogue()
    {
        portraitUI.PortraitSetting();
        if (currentdata.Character_num != 6) // 마법봉 이펙트가 아닐경우
        {
            dialogueUI.PlayDialogue();
        }
    }
    public void PlayingDialogue()
    {
        if (GameManager.Instance.b_touchable)
        {
            if (b_IsTypingEnd)
            {
                index++;
                if (DialogueDB.dialogue_data.ContainsKey(index))
                {
                    SetDialogueData();
                }
                else
                {
                    if (GameManager.Instance.currentstage > GameManager.Instance.story_cleared) //미클리어 스토리시 저장
                    {
                        GameManager.Instance.StoryClear();
                        GameManager.Instance.SaveGame();
                    }
                    CustomSceneManager.Instance.LoadScene("03_MenuScene");
                }
            }
            else
            {
                dialogueUI.DialogueSkip();
            }
        }
    }
}
