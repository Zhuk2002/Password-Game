using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogTextController : MonoBehaviour
{
    public TextMeshProUGUI textOutput;
    [SerializeField] private string[] dialogs;
    public int startIndex;
    private int currentIndex;

    void Awake()
    {
        currentIndex = startIndex;
    }

    void Start()
    {
        //StartCoroutine(StartDelay());
    }

    void Update()
    {
        
    }
    
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Dialog"))
        {
            StartCoroutine(ShowText());
            Destroy(other.gameObject);
        }
        else if(other.gameObject.CompareTag("Dialog/start"))
        {
            StartCoroutine(ShowText(0));
            Destroy(other.gameObject);
        }
        else if(other.gameObject.CompareTag("Dialog/log"))
        {
            StartCoroutine(ShowText(1));
            Destroy(other.gameObject);
        }
        
    }
    
    IEnumerator ShowText()
    {
        textOutput.text = dialogs[currentIndex];
        currentIndex++;
        yield return new WaitForSeconds(7);
        textOutput.text = "";
    }
    
    IEnumerator ShowText(int index)
    {
        textOutput.text = dialogs[index];
        yield return new WaitForSeconds(5);
        textOutput.text = "";
    }
    
    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(7);
        StartCoroutine(ShowText());
    }
}
