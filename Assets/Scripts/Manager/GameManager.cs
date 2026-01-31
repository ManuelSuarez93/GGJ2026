using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [SerializeField] private GameObject currentPlayer;
    [SerializeField] private PlayerMask currentPlayerMask;
    [SerializeField] private int amountOfPhotos;

    private int currentPhotosGrabbed;

    public PlayerMask CurrentPlayerMask => currentPlayerMask;
    
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

    public void AddPhotoCollected()
    {
        currentPhotosGrabbed++;
        UIManager.Instance.UpdatePhotos(currentPhotosGrabbed);
    }

    public void KillPlayer()
    {
        currentPlayer.SetActive(false);
    }
    private void Awake()
    {
        instance = this;
    }
}
