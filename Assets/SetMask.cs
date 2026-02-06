using UnityEngine;

public class SetMask : StateMachineBehaviour
{ 
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameManager.Instance.CurrentPlayerMask.SetMaskImage();
    }
}