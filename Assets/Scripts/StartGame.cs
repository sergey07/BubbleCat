using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForLoadFirstLevel());
    }

    IEnumerator WaitForLoadFirstLevel()
    {
        yield return new WaitForSeconds(5.0f);

        SceneManager.LoadScene("Level1");
    }
}
