using System;

namespace Core
{
    public class ReRunException
    {
        private bool _isLaunched;

        public bool IsLaunched => _isLaunched;

        public void Run()
        {
            if (_isLaunched)
                throw new Exception("Повторный вызов!");
            
            _isLaunched = true;
        }
    }
}