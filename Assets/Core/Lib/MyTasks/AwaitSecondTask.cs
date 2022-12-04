using System;
using System.Collections;
using Lib;
using UnityEngine;

namespace Core.Tasks
{
    public class AwaitSecondTask : MonoBehaviour, IMyTask
    {
        [SerializeField] private float _time;

        public void Begin(IMyContext context, Action<IMyTask> onComplete = null, Action<IMyTask> onFail = null) =>
            StartCoroutine(StartSpawn(onComplete));

        private IEnumerator StartSpawn(Action<IMyTask> onComplete)
        {
            yield return new WaitForSeconds(_time);
            onComplete?.Invoke(this);
        }
    }
}