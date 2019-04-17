using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = true;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Replay()
    {
        SceneManager.LoadScene(0);
    }
}
