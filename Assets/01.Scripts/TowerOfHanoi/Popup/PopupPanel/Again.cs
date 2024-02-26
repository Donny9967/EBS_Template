using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Again : Popup
{
    public void OnClickAgain()
    {
        transform.gameObject.SetActive(false);
    }

    public void OnClickKeep()
    {

    }
}
