using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeRemoveBehavior : StateMachineBehaviour
{
    public float _fadeTime = 0.5f;
    private float _timeElapsed = 0f;
    SpriteRenderer _spriteRenderer;
    GameObject _objToRemove;
    Color _startColor;
    Collider2D _col;

    GameManager _gameManager;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timeElapsed = 0f;
        _spriteRenderer = animator.GetComponent<SpriteRenderer>();
        _startColor = _spriteRenderer.color;
        _objToRemove = animator.gameObject;
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _col = _objToRemove.GetComponent<Collider2D>();
        if (_col != null)
        {
            _col.enabled = false;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timeElapsed += Time.deltaTime;

        float newAlpha = _startColor.a * (1 - (_timeElapsed / _fadeTime));

        _spriteRenderer.color = new Color(_startColor.r, _startColor.g, _startColor.b, newAlpha);

        if (_timeElapsed > _fadeTime)
        {
            _gameManager.EnemyManager._totalEnemyLeft--;
            _gameManager.EnemyManager._currentEnemyLeft--;
            if (_gameManager.EnemyManager.IsWaveClear())
            {
                _gameManager.EnemyManager._waveLeft--;
                if (_gameManager.EnemyManager.IsLevelFinished())
                {
                    _gameManager.ChangeState(GameState.Victory);
                }
                else
                {
                    _gameManager.ChangeToEvent();
                }
            }
            Destroy(_objToRemove);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
