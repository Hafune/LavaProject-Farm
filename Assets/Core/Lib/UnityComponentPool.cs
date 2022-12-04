using System;
using System.Collections.Generic;
using System.Linq;
using Lib;
using UnityEngine;

public class UnityComponentPool<T> : IDisposable where T : Component
{
    private T _effectPrefab;
    private List<T> _pool = new();
    private IMyContext _context;

    public UnityComponentPool(IMyContext context, T effectPrefab, int startCapacity = 0)
    {
        _context = context;
        _effectPrefab = effectPrefab;

        startCapacity.RepeatTimes(() =>
        {
            var eff = BuildEffect();
            eff.gameObject.SetActive(false);
        });
    }

    public T GetComponent()
    {
        var effect = _pool.FirstOrDefault(effect => !effect.gameObject.activeSelf);

        if (!effect)
            return BuildEffect();

        effect.gameObject.SetActive(true);
        return effect;
    }

    private T BuildEffect()
    {
        var newEffect = _context.Instantiate(_effectPrefab);
        _pool.Add(newEffect);
        return newEffect;
    }

    public void Dispose() => _pool.Clear();
}