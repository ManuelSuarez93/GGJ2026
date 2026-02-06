using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class FlipBehaviour : StateMachineBehaviour
{
    [SerializeField] private string levelTo;

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SceneManager.LoadScene(levelTo);
    }
}