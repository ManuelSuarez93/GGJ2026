using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    
    public static UIManager Instance
    { 
        get
        {
            if (instance == null)
            {
                Debug.Log("No game manager instance");
            }
            return instance;
        }
    }

    [SerializeField] private TextMeshProUGUI photosText;
    [SerializeField] private TextMeshProUGUI pausedText;
    private void Awake()
    {
        instance = this;
        InputSystem.actions["Cancel"].performed += (context) => Pause();
    }

    public void UpdatePhotos(int amount)
    {
        photosText.text = $"Paginas : {amount}";
    }

    private void Pause()
    {
        Time.timeScale =  Time.timeScale > 0 ? 0 : 1;
        pausedText.gameObject.SetActive(Time.timeScale > 0);
    }
}
