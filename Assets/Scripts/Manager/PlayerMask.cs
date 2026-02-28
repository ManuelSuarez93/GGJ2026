using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMask : MonoBehaviour
{
    [SerializeField] private MaskType currentType;
    [SerializeField] private float maskOffTime;
    [SerializeField] private float maskOnTime;
    [SerializeField] private Animator playerAnimator;
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
    private bool maskOff;
    private bool maskOn;
    private float currentTimeChange;
    void Start()
    { 
        maskOff = false;
        playerAnimator.SetBool("MaskOff", maskOff);
        playerAnimator.SetBool("MaskOn", maskOff);
    
    }

    private void Update()
    {
        if (maskOff)
        {
            if (currentTimeChange < maskOffTime)
            {
                currentTimeChange += Time.deltaTime;
                currentType = MaskType.NoMask;
            }
            else
            {
                currentTimeChange = 0f; 
                maskOff = false; 
                maskOn = true;
                playerAnimator.SetBool("MaskOff", maskOff);
                playerAnimator.SetBool("MaskOn", maskOn);
            }
        }

        if (maskOn)
        {
            if (currentTimeChange < maskOnTime)
            {
                currentTimeChange += Time.deltaTime;
            }
            else
            {
                currentTimeChange = 0f; 
                currentType = MaskType.Mask1; 
                maskOn = false;
                playerAnimator.SetBool("MaskOn", maskOn);
            }
        }
    }

    public void ChangeMask(int newType)
    {
        if(maskOff || maskOn) return;
        
        maskToChange = (MaskType) Enum.ToObject(typeof(MaskType), newType);
        maskOff = true;
        maskOn = false;
    
        playerAnimator.SetBool("MaskOff", maskOff);
        
        switch (maskToChange)
        {
            case MaskType.Mask1: 
                playerAnimator.SetInteger("Mask", 1);
                break;
            case MaskType.Mask2: 
                playerAnimator.SetInteger("Mask", 2);
                break;
            case MaskType.Mask3: 
                playerAnimator.SetInteger("Mask", 3);
                break;
        }

    }
}
