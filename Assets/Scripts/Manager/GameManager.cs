using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager>
{
    float audiovolume = 1f; //볼륨세팅
    public int stage_cleared { get; private set; } = 7;
    public int story_cleared { get; private set; } = 7;
    public int score { get; private set; } = 0;
    public int currentstage_sroce { get; private set; } = 0;
    public int currentstage { get; private set; } = 0; // 스테이지씬의 리소스 적용을 위한 현스테이지상태 변수
    public bool b_touchable { get; private set; } = false; //터치 가능 상태
    public bool b_toucheffect { get; private set; } = false; //터치이펙트 적용 유무
    protected override void Awake()
    {
        base.Awake();
        Application.targetFrameRate = 60;
    }
    private void Start()
    {
        LoadGame();
    }
    public void SetVolume(float _volume)
    {
        audiovolume = _volume;
        AudioManager.Instance.VolumeControl();
    }
    public void Get_score(int _score)
    {
        if (currentstage > stage_cleared)
        {
            if (_score > 0)
            {
                score += _score;
                if (score >= 100)
                {
                    stage_cleared++; //스테이지클리어
                    score = 0;
                }
                SaveGame();
            }
        }
    }

    public float GetVolume() { return audiovolume; }
    public void ChangeStage(int _stage)
    {
        currentstage = _stage;
    }
    public void StoryClear()
    {
        story_cleared++;
        SaveGame();
    }
    public void TouchLock()
    {
        b_touchable = false;
    }
    public void TouchUnlock()
    {
        b_touchable = true;
    }
    public void EffectOn()
    {
        b_toucheffect = true;
    }
    public void EffectOff()
    {
        b_toucheffect = false;
    }
    public void SaveGame()
    {
        if (b_toucheffect)
        {
            PlayerPrefs.SetInt("Sfx", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Sfx", 0);
        }
        PlayerPrefs.SetFloat("Volume", audiovolume);
        PlayerPrefs.SetInt("CStage", stage_cleared);
        PlayerPrefs.SetInt("CStory", story_cleared);
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.Save();
    }

    void LoadGame()
    {
        PlayerPrefs.DeleteAll();
        if (PlayerPrefs.GetInt("Sfx", 1) == 1)
        {
            b_toucheffect = true;
        }
        else
        {
            b_toucheffect = false;
        }
        audiovolume = PlayerPrefs.GetFloat("Volume", 0.3f);
        AudioManager.Instance.VolumeControl();
        stage_cleared = PlayerPrefs.GetInt("CStage", 7);
        story_cleared = PlayerPrefs.GetInt("CStory", 7);
        score = PlayerPrefs.GetInt("Score", 0);
    }
}
