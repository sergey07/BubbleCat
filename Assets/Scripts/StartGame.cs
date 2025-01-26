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
        bubbleBoomObject.SetActive(true);


        transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(newScaleX, newScaleY, 1), scaleSpeed * Time.fixedDeltaTime);

        BubbleHasCat();
    }

    IEnumerator BubbleHasCat()
    {
        yield return new WaitForSeconds(timeBeforeBubbleHasCat);

        catObject.transform.position = bubbleObject.transform.position;
        catObject.transform.parent = bubbleObject.transform;
        bubbleObject.SetActive(true);
    }

    IEnumerator WaitForLoadFirstLevel()
    {
        yield return new WaitForSeconds(5.0f);

        SceneManager.LoadScene("Level1");
    }
}
