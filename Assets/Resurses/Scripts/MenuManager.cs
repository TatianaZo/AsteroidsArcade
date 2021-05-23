using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject[] Canva;


     public async void CanvaFirst()
     {
        await Task.Delay(100);
        Canva[0].SetActive(false);
        Canva[1].SetActive(true);
     }

    public void BackToMenu1()
    {
        
        Canva[1].SetActive(false);
        Canva[0].SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
