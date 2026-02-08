using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
 
    [SerializeField] private PlayerController player;
    [SerializeField] private PlayerMask currentPlayerMask;
    [SerializeField] private int amountOfPhotos;
    [SerializeField] private GameObject Level;
    private int currentPhotosGrabbed;
    private bool isPlaying = false;
    public PlayerMask CurrentPlayerMask => currentPlayerMask;
    public PlayerController Player => player;
    
    public bool IsPlaying => isPlaying;
    public static GameManager Instance
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

    public void StartGame()
    {
        isPlaying = true;
        Level.SetActive(isPlaying);
    }

    public void KillPlayer()
    {
        player.gameObject.SetActive(false);
        UIManager.Instance.Pause(new InputAction.CallbackContext());
    }

    private void Awake()
    { 
        instance = this;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
}
