using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Lib;
using UnityEngine;

namespace Core
{
    public class TaskParallel : IMyTask
    {
        private List<IMyTask> _tasks;
        private HashSet<IMyTask> _completedTasks = new();
        private ReRunException _reRunException = new();

        [CanBeNull] private Action<IMyTask> _onComplete;
        [CanBeNull] private Action<IMyTask> _onFail;

        public TaskParallel(List<IMyTask> tasks) => _tasks = tasks;

        public void Begin(IMyContext context, Action<IMyTask> onComplete = null, Action<IMyTask> onFail = null)
        {
            _reRunException.Run();

            _onComplete = onComplete;
            _onFail = onFail;

            _tasks.ForEach(task => task.Begin(context, Complete, Fail));
        }

        private void Complete(IMyTask task)
        {
            if (!_completedTasks.Add(task))
                throw new Exception($"Таск {task} уже вызывал onComplete");

            if (_completedTasks.Count < _tasks.Count)
                return;

            _onComplete?.Invoke(this);
        }

        private void Fail(IMyTask task) => _onFail?.Invoke(this);
    }
}