using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipsController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckPressing();
    }
    
    void CheckPressing()
    {
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            Animator animator = GetComponent<Animator>();
            animator.Play("TextFadeOut");
            StartCoroutine(DelayBeforeOff());
        }
    }
    
    private IEnumerator DelayBeforeOff()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}
