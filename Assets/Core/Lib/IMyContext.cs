using UnityEngine;

namespace Lib
{
    public interface IMyContext
    {
        T Resolve<T>();

        T Instantiate<T>(T prefab, Vector3 position, Quaternion rotation, Transform parent)
            where T : Component;

        T Instantiate<T>(T prefab, Vector3 position, Quaternion rotation)
            where T : Component;

        T Instantiate<T>(T prefab, Transform parent)
            where T : Component;

        T Instantiate<T>(T prefab)
            where T : Component;
    }
}