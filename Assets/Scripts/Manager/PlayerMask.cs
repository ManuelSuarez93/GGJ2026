using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMask : MonoBehaviour
{
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private MaskType currentType;
    [SerializeField] private float changeAmount;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] List<Sprite> sprites;
    public MaskType CurrentType => currentType;
    private MaskType maskToChange = MaskType.NoMask;

    public enum MaskType
    {
        NoMask,
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
                currentType = MaskType.NoMask;
            }
            else
            {
                currentType = maskToChange;
                renderer.color = colorTo;
                currentTimeChange = 0;
                isChanging = false;
            }
        }
    }

    public void SetMaskImage()
    { 
        switch (currentType)
        {
            case MaskType.Mask1: renderer.sprite = sprites[0]; break;
            case MaskType.Mask2: renderer.sprite = sprites[1]; break;
            case MaskType.Mask3: renderer.sprite = sprites[2]; break;
        }
    }
    
    private void ChangeMask(MaskType newType)
    {
        if(isChanging) return;
        
        maskToChange = newType;
        isChanging = true; 
        renderer.sprite = null;
        
        switch (newType)
        {
            case MaskType.Mask1: 
                playerAnimator.Play("Mask1");
                break;
            case MaskType.Mask2: 
                playerAnimator.Play("Mask2");
                break;
            case MaskType.Mask3: 
                playerAnimator.Play("Mask3");
                break;
        }
    }
}
