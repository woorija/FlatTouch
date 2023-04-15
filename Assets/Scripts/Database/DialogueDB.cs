using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public struct Dialogue_data
{
    public int Character_num { get; private set; }
    public int emote_num { get; private set; }
    public string dialogue { get; private set; }

    public Dialogue_data(int _char, int _emote, string _dialogue)
    {
        Character_num = _char;
        emote_num = _emote;
        dialogue = _dialogue;
    }
}
public class DialogueDB : MonoBehaviour,ICSVRead
{
    public static Dictionary<int, Dialogue_data> dialogue_data { get; private set; }
    public static string[] character_name { get; private set; } = {
        "주인공",
        "토끼",
        "너구리",
        "백곰왕",
        "마녀",
        "주민들"
    };

    private void Start()
    {
        dialogue_data = new Dictionary<int, Dialogue_data>(60);
        ReadCSV("DialogueData");
    }
    public void ReadCSV(string _file)
    {
        string[] lines = CSVReader.Line_Split(_file);
        for (var i = 1; i < lines.Length; i++)
        {

            var values = Regex.Split(lines[i], CSVReader.SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;
            dialogue_data.Add(CSVReader.GetIntData(values[0]), new Dialogue_data(CSVReader.GetIntData(values[1]), CSVReader.GetIntData(values[2]), CSVReader.GetStringData(values[3])));
        }
    }
}
