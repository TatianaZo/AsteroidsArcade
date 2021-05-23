using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURL : MonoBehaviour
{
   public void OpenUrl()
    {
        Application.OpenURL("https://ru.wikipedia.org/wiki/Asteroids");
    }
}
