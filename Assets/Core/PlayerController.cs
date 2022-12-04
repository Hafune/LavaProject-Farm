using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour
{
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");

    [SerializeField] private Animator _animator;

    private NavMeshAgent _agent;
    private WaitForSeconds _wait = new(.1f);
    private Action _onComplete;
    private Action _onFail;
    private bool _pathfindingInProgress;

    private void OnValidate() => _animator = _animator ??= GetComponentInChildren<Animator>();

    private void Awake() => _agent = GetComponent<NavMeshAgent>();

    public void Warp(Vector3 position) => _agent.Warp(position);

    public void GoToPoint(Vector3 point, Action onComplete, Action onFail = null)
    {
        _agent.SetDestination(point);
        _onComplete = onComplete;
        _onFail = onFail;

        if (_pathfindingInProgress)
            return;

        _animator.SetBool(IsMoving, true);
        StartCoroutine(WatchForDestination());
    }

    private IEnumerator WatchForDestination()
    {
        bool completed = false;
        _pathfindingInProgress = true;

        while (!completed)
        {
            if (_agent.path.status == NavMeshPathStatus.PathInvalid ||
                _agent.path.status == NavMeshPathStatus.PathPartial)
            {
                _pathfindingInProgress = false;
                _agent.isStopped = true;
                _onFail?.Invoke();
                yield break;
            }

            yield return _wait;

            if (_agent.remainingDistance <= _agent.stoppingDistance)
                completed = true;
        }

        _pathfindingInProgress = false;
        _agent.velocity = Vector3.zero;
        _animator.SetBool(IsMoving, false);
        _onComplete.Invoke();
    }
}