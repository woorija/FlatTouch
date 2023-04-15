using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlatEffect : MonoBehaviour
{
    ParticleSystem particle;
    ParticleSystem.MainModule mainModule;
    ParticleSystem.Particle[] particles;

    Vector3 endpos;
    Vector3[] controlpos1;
    Vector3 controlpos2;

    Light2D endposlight;

    float duration = 0.5f;
    float timer = 0f;

    bool Istransparent = false;
    Color transparent;
    private void Awake()
    {
        particle= GetComponent<ParticleSystem>();
        endposlight = GetComponentInChildren<Light2D>();
        endposlight.intensity = 0;
        endposlight.gameObject.SetActive(false);
        mainModule = particle.main;
        transparent = new Color(0, 0, 0, 0);
        particles = new ParticleSystem.Particle[mainModule.maxParticles];
        controlpos1 = new Vector3[mainModule.maxParticles];
    }
    public void ColorChange(Color _color)
    {
        mainModule.startColor = _color;
    }
    public void SetEndpos(Vector3 _endpos)
    {
        endpos = _endpos;
        endposlight.transform.position = endpos;
    }
    public void PlayEffect()
    {
        Istransparent = false;
        particle.Play();
        timer = 0f;
        SetRandomPos();
    }
    void SetRandomPos()
    {
        float deg = Random.Range(0f, 360f);
        controlpos2 = new Vector3(endpos.x + (10 * Mathf.Sin(deg)), endpos.y + (10 * Mathf.Cos(deg)), 0);
        for (int i = 0; i < particles.Length; i++)
        {
            controlpos1[i] = particles[i].position + particles[i].velocity*0.5f;
        }
    }
    private void LateUpdate()
    {
        if (!Istransparent)
        {
            timer += Time.deltaTime;
            if (timer > duration)
            {
                Istransparent = true;
                endposlight.gameObject.SetActive(true);
                ColorChange(transparent);
            }
            particle.GetParticles(particles);
            float t = timer / duration;
            if (t > 0.2f)
            {
                for (int i = 0; i < particles.Length; i++)
                {
                    particles[i].position = CalculateBezierPoint(t, transform.position, controlpos1[i], controlpos2, endpos);
                }

                particle.SetParticles(particles, particles.Length);
            }
        }
    }
    Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1f - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0;
        p += 3f * uu * t * p1;
        p += 3f * u * tt * p2;
        p += ttt * p3;
        return p;
    }
}
