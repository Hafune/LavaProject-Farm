using JetBrains.Annotations;
using Lib;
using Reflex;
using UnityEngine;

public class MyContext : IMyContext
{
    private Container _container;

    public MyContext(Container container) => _container = container;

    public T Resolve<T>() => _container.Resolve<T>();

    public T Instantiate<T>(T prefab, Vector3 position, Quaternion rotation) where T : Component =>
        Instantiate(prefab, position, rotation, null);

    public T Instantiate<T>(T prefab, Vector3 position, Quaternion rotation, [CanBeNull] Transform parent)
        where T : Component
    {
        var obj = Instantiate(prefab, parent);
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        return obj;
    }

    public T Instantiate<T>(T prefab, Transform parent) where T : Component => _container.Instantiate(prefab, parent);

    public T Instantiate<T>(T prefab) where T : Component => _container.Instantiate(prefab);
}