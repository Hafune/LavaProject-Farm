using System;
using Lib;
using UnityEngine;

namespace Core.Tasks
{
    public class SetActiveTask : MonoBehaviour, IMyTask
    {
        [SerializeField] private bool _activeState;
        [SerializeField] private GameObject _target;

        public void Begin(IMyContext context, Action<IMyTask> onComplete = null, Action<IMyTask> onFail = null)
        {
            _target.SetActive(_activeState);
            onComplete?.Invoke(this);
        }
    }
}