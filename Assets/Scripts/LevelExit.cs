using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(SceneLoad());
    }

    IEnumerator SceneLoad()
    {
        yield return new WaitForSeconds(2f);

        Cursor.visible = false;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
