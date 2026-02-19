using UnityEngine;
using UnityEngine.AI;

public class UnitFollwState : StateMachineBehaviour
{

    AttackController attackController;
    NavMeshAgent agent;
    public float attackingDistance=1f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       attackController = animator.transform.GetComponent<AttackController>();
       agent = animator.transform.GetComponent<NavMeshAgent>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // should unit change to idel state
        if(attackController.targetToAttack == null)
        {
            animator.SetBool("isFollowing",false);
        }

        // moving toward enemy
        // if(attackController.targetToAttack != null)
        // {
            agent.SetDestination(attackController.targetToAttack.position);
            animator.transform.LookAt(attackController.targetToAttack);
        // }

        // should unit attack?
        // float distanceFromTarget = Vector3.Distance(attackController.targetToAttack.position,animator.transform.position);
        // if(distanceFromTarget < attackingDistance)
        // {
        //     animator.SetBool("isAttacking",true);
        // }
       
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       agent.SetDestination(animator.transform.position);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
