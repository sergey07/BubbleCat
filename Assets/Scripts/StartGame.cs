using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] private float timeBeforeTranslateCatByWitch = 2.0f;
    [SerializeField] private float timeBeforeBoilerBoils = 2.0f;
    [SerializeField] private float timeBeforeBubbleHasCat = 2.0f;
    [SerializeField] private float timeBeforeLoadFirstLevel = 1.0f;

    [SerializeField] private float scaleSpeed = 2.0f;
    [SerializeField] private float newScaleX = 12.0f;
    [SerializeField] private float newScaleY = 2.0f;

    [SerializeField] private Animator animator;

    [SerializeField] private GameObject witchObject;
    [SerializeField] private GameObject catObject;
    [SerializeField] private GameObject bubbleBoomObject;
    [SerializeField] private GameObject bubbleObject;

    [SerializeField] private Sprite witchWithCat;
    [SerializeField] private Sprite witchWithoutCat;

    // Start is called before the first frame update
    void Start()
    {
        bubbleObject.SetActive(false);
        bubbleBoomObject.SetActive(false);

        bubbleObject.transform.position = bubbleBoomObject.transform.position;
        catObject.transform.position = bubbleBoomObject.transform.position;

        //StartCoroutine(WaitForLoadFirstLevel());
        StartCoroutine(TranslateCatByWitch());
    }

    // Ведьма перемещает кота над котлом
    IEnumerator TranslateCatByWitch()
    {
        yield return new WaitForSeconds(timeBeforeTranslateCatByWitch);

        witchObject.transform.localScale = new Vector3(-witchObject.transform.localScale.x, witchObject.transform.localScale.y, witchObject.transform.localScale.z);
        //catObject.transform.parent = null;

        animator.SetTrigger("boom");

        StartCoroutine(BoilerBoils());
    }

    // Происходит взрыв из котла
    IEnumerator BoilerBoils()
    {
        yield return new WaitForSeconds(timeBeforeBoilerBoils);

        witchObject.GetComponent<SpriteRenderer>().sprite = witchWithoutCat;
        catObject.SetActive(true);
        bubbleBoomObject.SetActive(true);

        transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(newScaleX, newScaleY, 1), scaleSpeed * Time.fixedDeltaTime);

        StartCoroutine(BubbleHasCat());
    }

    IEnumerator BubbleHasCat()
    {
        //Debug.Log("BubbleHasCat");

        yield return new WaitForSeconds(timeBeforeBubbleHasCat);

        bubbleBoomObject.SetActive(false);

        catObject.transform.position = bubbleObject.transform.position;
        catObject.transform.parent = bubbleObject.transform;
        bubbleObject.SetActive(true);

        StartCoroutine(LoadFirstLevel());
    }

    IEnumerator LoadFirstLevel()
    {
        yield return new WaitForSeconds(timeBeforeLoadFirstLevel);

        SceneManager.LoadScene("Level1");
    }
}
