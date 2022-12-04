using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

public class EcsComponentAttribute : Attribute
{
    public readonly string Description;
    [CanBeNull] public readonly Type Relation;

    public EcsComponentAttribute(string description, Type type = null)
    {
        Description = description;
        Relation = type;
    }
}