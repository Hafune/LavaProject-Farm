using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Lib;
using UnityEngine;

namespace Core
{
    public class TaskSequence : IMyTask
    {
        private List<IMyTask> _tasks;
        private HashSet<IMyTask> _completedTasks = new();
        private IEnumerator<IMyTask> _sequence;
        private IMyContext _context;
        private ReRunException _reRunException = new();

        [CanBeNull] private Action<IMyTask> _onComplete;
        [CanBeNull] private Action<IMyTask> _onFail;

        public TaskSequence(List<IMyTask> tasks) => _tasks = tasks;

        public void Begin(IMyContext context, Action<IMyTask> onComplete = null, Action<IMyTask> onFail = null)
        {
            _reRunException.Run();

            _context = context;
            _onComplete = onComplete;
            _onFail = onFail;
            _sequence = _tasks.GetEnumerator();

            if (_sequence.MoveNext())
                _sequence.Current!.Begin(context, Complete, Fail);
            else
                _onComplete?.Invoke(this);
        }

        private void Complete(IMyTask task)
        {
            if (!_completedTasks.Add(task))
                throw new Exception($"Таск {task} уже вызывал onComplete");

            if (_sequence.MoveNext())
                _sequence.Current!.Begin(_context, Complete, Fail);
            else
                _onComplete?.Invoke(this);
        }

        private void Fail(IMyTask task) => _onFail?.Invoke(this);
    }
}