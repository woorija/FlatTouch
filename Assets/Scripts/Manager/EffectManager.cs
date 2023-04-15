using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] WordBubble wordBubble;
    [SerializeField] ParticleSystem P_Particle;
    [SerializeField] ParticleSystem G_Particle;
    [SerializeField] ParticleSystem M_Particle;
    [SerializeField] EmoteAnimate emote;

    public FlatEffect[] flatEffect;
    Vector3 endpos;
    public void EffectInit()
    {
        emote.EmoteInit(GameManager.Instance.currentstage);
        wordBubble.WordInit(GameManager.Instance.currentstage);
    }
    public void Play_perfect_effect()
    {
        emote.EmoteSetting(0);
        emote.Start_emoteanimation();
        P_Particle.Play();
    }
    public void Play_good_effect()
    {
        emote.EmoteSetting(0);
        emote.Start_emoteanimation();
        G_Particle.Play();
    }
    public void Play_miss_effect()
    {
        emote.EmoteSetting(1);
        emote.Start_emoteanimation();   
        M_Particle.Play();  
    }
    public void Play_Warning_effect()
    {
        emote.EmoteSetting(2);
        emote.Start_emoteanimation();
        wordBubble.SpeechStart();
        M_Particle.Play();
    }
    public void SetEndpos()
    {
        endpos =  new Vector3(Random.Range(-500f, 500f), Random.Range(200f, 500f), 0);
    }
    public Vector3 GetEndpos()
    {
        return endpos;
    }
}
