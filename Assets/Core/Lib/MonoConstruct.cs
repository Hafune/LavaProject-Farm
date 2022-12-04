using Reflex.Scripts.Attributes;
using UnityEngine;

namespace Lib
{
    public abstract class MonoConstruct : MonoBehaviour
    {
        [Inject]
        protected abstract void Construct(IMyContext context);
    }
}