using UnityEngine;

public class FaerieStateBehaviour : StateMachineBehaviour
{
    internal FaerieCircle faerieCircle;
    internal float angerTimer = 0f;
    public int nextState;

    public void Setup(FaerieCircle fc)
    {
        faerieCircle = fc;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ProcessAnger(animator);
    }

    private void ProcessAnger(Animator animator)
    {
        if (angerTimer > 0f)
        {
            angerTimer -= Time.deltaTime;
            if (angerTimer <= 0f)
            {
                animator.SetInteger("Anger", nextState);
            }
        }
    }
}
