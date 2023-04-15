using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] TMP_Text dialogue_text;
    [SerializeField] TMP_Text charactername_text;

    StringBuilder dialogueBuilder;
    string origintext;

    Coroutine typing;
    private void Awake()
    {
        dialogueBuilder= new StringBuilder();
    }
    public void GetName(string _name)
    {
        charactername_text.text = _name;
    }
    public void GetDialogueText(string _origin)
    {
        origintext = _origin;
    }
    public void SetDialogueText()
    {
        dialogue_text.text = dialogueBuilder.ToString();
    }
    void SetDialogueOrigin()
    {
        dialogue_text.text = origintext;
    }
    public void PlayDialogue()
    {
        typing = StartCoroutine(Typing_dialogue(origintext));
    }
    IEnumerator Typing_dialogue(string _dialogue)
    {
        int index = 0;
        while (_dialogue.Length != index)
        {
            dialogueBuilder.Append(_dialogue[index++]);
            SetDialogueText();
            yield return new WaitForSeconds(0.07f);
        }
        dialogueBuilder.Clear();
        DialogueManager.b_IsTypingEnd = true;
        TutorialDialoguePlayer.b_IsTypingEnd = true;
    }
    public void DialogueSkip()
    {
        StopCoroutine(typing);
        dialogueBuilder.Clear();
        SetDialogueOrigin();
        DialogueManager.b_IsTypingEnd = true;
        TutorialDialoguePlayer.b_IsTypingEnd = true;
    }
}
