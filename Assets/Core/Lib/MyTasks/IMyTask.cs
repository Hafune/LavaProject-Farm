using System;
using Lib;

namespace Core
{
    public interface IMyTask
    {
        public void Begin(IMyContext context,Action<IMyTask> onComplete = null, Action<IMyTask> onFail = null);
    }
}