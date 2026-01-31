using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameManager instance;

    [SerializeField] private PlayerMask currentPlayerMask;
    
    public GameManager Instance
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

    private void Awake()
    {
        instance = this;
    }
}
