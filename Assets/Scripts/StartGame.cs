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
        //SceneManager.LoadScene("Level1");
    }

    IEnumerator WaitForLoadFirstLevel()
    {
        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene("Level1");
    }
}
