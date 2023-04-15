using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Stage_data
{
    public float bpm;
    public bool[] Usefattern;
    public Stage_data(float _bpm)
    {
        bpm = _bpm;
        Usefattern = new bool[5];
    }
}
public class FatternProbability_data
{
    public int[] Fattern_probability;
    public FatternProbability_data()
    {
        Fattern_probability= new int[5];
    }
}
public class StageDB : MonoBehaviour,ICSVRead
{
    public static Dictionary<int, Stage_data> stage_data { get; private set; }
    public static Dictionary<int,FatternProbability_data> probability_data { get; private set; }
    void Start()
    {
        stage_data= new Dictionary<int, Stage_data>(10);
        probability_data = new Dictionary<int, FatternProbability_data>(10);
        ReadCSV("StageData");
    }

    public void ReadCSV(string _file)
    {
        string[] lines = CSVReader.Line_Split(_file);
        for (var i = 1; i < lines.Length; i++)
        {

            var values = Regex.Split(lines[i], CSVReader.SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;
            stage_data.Add(CSVReader.GetIntData(values[0]), new Stage_data(CSVReader.GetFloatData(values[1])));
            for (int j = 0; j < 5; j++)
            {
                stage_data[CSVReader.GetIntData(values[0])].Usefattern[j] = CSVReader.GetBoolData(values[j + 2]);
            }
            probability_data.Add(CSVReader.GetIntData(values[0]), new FatternProbability_data());
            for (int j = 0; j < 5; j++)
            {
                probability_data[CSVReader.GetIntData(values[0])].Fattern_probability[j] = CSVReader.GetIntData(values[j+7]);
            }
        }
    }
}
