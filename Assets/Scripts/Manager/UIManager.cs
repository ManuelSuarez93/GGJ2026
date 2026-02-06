using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

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
    [SerializeField] private GameObject pauseMenu;
    private void Awake()
    {
        instance = this;
        InputSystem.actions["Cancel"].performed += Pause;
    }

    private void OnDestroy()
    {
        InputSystem.actions["Cancel"].performed -= Pause;
    }


    public void Pause(InputAction.CallbackContext context)
    {
        Time.timeScale =  Time.timeScale > 0 ? 0 : 1;
        pauseMenu.SetActive(Time.timeScale <= 0);
    }
}
