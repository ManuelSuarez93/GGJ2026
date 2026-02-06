using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Animator animatorLetter;
    [SerializeField] private UnityEvent onFlipFinsh; 
    [SerializeField] private float waitForFlip = 1f;

    [SerializeField] private UnityEvent onOpenLetter;
    [SerializeField] private UnityEvent onCloseLetter;
    [SerializeField] private float waitForOpen = 1f;
    [SerializeField] private float waitForClose = 0.75f;
    
    public void Flip(bool isFlipped)
    {
        animatorLetter.SetBool("Flipped", isFlipped);  
        if(!isFlipped)
            StartCoroutine(FlipFinishCoroutine());
    }

    public void OpenLetter()
    { 
        animatorLetter.Play("OpenLetter");
        StartCoroutine(WaitForOpenCoroutine());
    }
    
    
    public void CloseLetter()
    { 
        animatorLetter.Play("CloseLetter");
        StartCoroutine(WaitForCloseCoroutine());
    }

    IEnumerator FlipFinishCoroutine()
    {
        yield return new WaitForSeconds(waitForFlip);
        onFlipFinsh.Invoke();
    }
    
    
    IEnumerator WaitForOpenCoroutine()
    {
        yield return new WaitForSeconds(waitForOpen);
        onOpenLetter.Invoke();
    }
    
    IEnumerator WaitForCloseCoroutine()
    {
        yield return new WaitForSeconds(waitForClose);
        onOpenLetter.Invoke();
        SceneManager.LoadScene("2dScene");
    }
}
