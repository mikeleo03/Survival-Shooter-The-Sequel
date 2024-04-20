using UnityEngine;

public class FaerieCooling : FaerieStateBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        faerieCircle.SetMood(false);
        angerTimer = faerieCircle.happyFaerie.minimumTime;
    }
}