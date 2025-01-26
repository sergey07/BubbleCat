using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] private float timeBeforeTranslateCatByWitch = 2.0f;

    [SerializeField] private GameObject witchObject;
    [SerializeField] private GameObject catObject;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(WaitForLoadFirstLevel());

    }

    IEnumerator TranslateCatByWitch()
    {
        yield return new WaitForSeconds(timeBeforeTranslateCatByWitch);

        witchObject.transform.localScale = new Vector3(-witchObject.transform.localScale.x, witchObject.transform.localScale.y, witchObject.transform.localScale.z);
    }

    IEnumerator WaitForLoadFirstLevel()
    {
        yield return new WaitForSeconds(5.0f);

        SceneManager.LoadScene("Level1");
    }
}
