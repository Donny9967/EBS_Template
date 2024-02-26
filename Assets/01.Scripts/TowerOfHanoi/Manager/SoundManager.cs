using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource BGM;
    [SerializeField] AudioSource Click;
    [SerializeField] AudioSource Correct;
    [SerializeField] AudioSource Wrong;
    [SerializeField] AudioSource Clear;
    public void ClickSound()
    {
        if (Click != null)
            Click.Play();
    }

    public void CorrectSound()
    {
        if (Correct != null)
            Correct.Play();
    }

    public void WrongSound()
    {
        if (Wrong != null)
            Wrong.Play();
    }

    public void ClearSound()
    {
        if (Clear != null)
            Clear.Play();
    }
}
