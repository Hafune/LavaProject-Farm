using System;
using System.Collections.Generic;
using System.Linq;
using Lib;
using UnityEngine;

namespace Core
{
    public class TaskParallelScript : MonoConstruct, IMyTask
    {
        [SerializeField] private List<MonoBehaviour> _iTasks;
        [SerializeField] private bool runSelf;

        private TaskParallel _tasks;
        private IMyContext _context;

        private void OnValidate()
        {
            HashSet<IMyTask> set = new();
            _iTasks = _iTasks.Select(task => task is IMyTask && set.Add((IMyTask)task) ? task : null).ToList();
        }

        protected override void Construct(IMyContext context)
        {
            _context = context;
            _tasks = new(_iTasks.Select(task => task as IMyTask).Where(task => task != null).ToList());
        }

        private void Start()
        {
            if (runSelf)
                Begin(_context);
        }

        public void Begin(IMyContext context, Action<IMyTask> onComplete = null, Action<IMyTask> onFail = null) =>
            _tasks.Begin(context, onComplete, onFail);
    }
}