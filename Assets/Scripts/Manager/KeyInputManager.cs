using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInputManager : MonoBehaviour
{
    [SerializeField] Flats flat;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad7) || Input.GetKeyDown(KeyCode.Q))
        {
            flat.flat[0].FlatTouch();
            ClickEffect.Instance.Play_ClickEffect(flat.flat[0].transform.position);
        }
        if (Input.GetKeyDown(KeyCode.Keypad8) || Input.GetKeyDown(KeyCode.W))
        {
            flat.flat[1].FlatTouch();
            ClickEffect.Instance.Play_ClickEffect(flat.flat[1].transform.position);
        }
        if (Input.GetKeyDown(KeyCode.Keypad9) || Input.GetKeyDown(KeyCode.E))
        {
            flat.flat[2].FlatTouch();
            ClickEffect.Instance.Play_ClickEffect(flat.flat[2].transform.position);
        }
        if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.A))
        {
            flat.flat[3].FlatTouch();
            ClickEffect.Instance.Play_ClickEffect(flat.flat[3].transform.position);
        }
        if (Input.GetKeyDown(KeyCode.Keypad5) || Input.GetKeyDown(KeyCode.S))
        {
            flat.flat[4].FlatTouch();
            ClickEffect.Instance.Play_ClickEffect(flat.flat[4].transform.position);
        }
        if (Input.GetKeyDown(KeyCode.Keypad6) || Input.GetKeyDown(KeyCode.D))
        {
            flat.flat[5].FlatTouch();
            ClickEffect.Instance.Play_ClickEffect(flat.flat[5].transform.position);
        }
        if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Z))
        {
            flat.flat[6].FlatTouch();
            ClickEffect.Instance.Play_ClickEffect(flat.flat[6].transform.position);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.X))
        {
            flat.flat[7].FlatTouch();
            ClickEffect.Instance.Play_ClickEffect(flat.flat[7].transform.position);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.C))
        {
            flat.flat[8].FlatTouch();
            ClickEffect.Instance.Play_ClickEffect(flat.flat[8].transform.position);
        }
    }
}