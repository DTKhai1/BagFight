using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunBehavior : StateMachineBehaviour, IFixedUpdateObserver
{
    private float speed = .5f;
    Animator _anim;

    Transform _player;
    Rigidbody2D _rb;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _anim = animator;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _rb = animator.GetComponent<Rigidbody2D>();
        UpdateManager.RegisterFixedUpdateObserver(this);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{

    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        UpdateManager.UnregisterFixedUpdateObserver(this);
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
    public void ObservedFixedUpdate()
    {
        Vector2 target = new Vector2(_player.position.x, _rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(_rb.position, target, speed * Time.deltaTime);
        _rb.MovePosition(newPos);

        if(Mathf.Abs(_rb.position.x - _player.position.x) < 1)
        {
            _anim.SetBool("Attack", true);
        }
    }
    private void OnDisable()
    {
        UpdateManager.UnregisterFixedUpdateObserver(this);
    }
    private void OnDestroy()
    {
        UpdateManager.UnregisterFixedUpdateObserver(this);  
    }
}
