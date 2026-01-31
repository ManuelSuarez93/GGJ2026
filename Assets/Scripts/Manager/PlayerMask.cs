using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMask : MonoBehaviour
{
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private MaskType currentType;
    [SerializeField] private float changeAmount;
    
    public MaskType CurrentType => currentType;

    public enum MaskType
    {
        Mask1,
        Mask2,
        Mask3
    }
    private Color colorTo; // TODO change this to sprite or something
    private bool isChanging;
    private float currentTimeChange;
    void Start()
    {
        colorTo = renderer.color;
        isChanging = false;
        InputSystem.actions["ChangeMask1"].performed += (context) => ChangeMask(MaskType.Mask1);
        InputSystem.actions["ChangeMask2"].performed += (context) => ChangeMask(MaskType.Mask2);
        InputSystem.actions["ChangeMask3"].performed += (context) => ChangeMask(MaskType.Mask3);
    }

    private void Update()
    {
        if (isChanging)
        {
            if (currentTimeChange < changeAmount)
            {
                renderer.color = Color.Lerp(renderer.color, colorTo, Mathf.Clamp01(currentTimeChange/changeAmount));
                currentTimeChange += Time.deltaTime;
            }
            else
            {
                renderer.color = colorTo;
                currentTimeChange = 0;
                isChanging = false;
            }
        }
    }

    private void ChangeMask(MaskType newType)
    {
        isChanging = true;
        currentType = newType;
        
        //TODO User animator?
        switch (newType)
        {
            case MaskType.Mask1: 
                colorTo = Color.white;
                break;
            case MaskType.Mask2: 
                colorTo = Color.red;
                break;
            case MaskType.Mask3: 
                colorTo = Color.green;
                break;
        }
    }
}
