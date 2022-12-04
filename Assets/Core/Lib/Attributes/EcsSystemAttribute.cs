using System;

public class EcsSystemAttribute : EcsComponentAttribute
{
    public EcsSystemAttribute(string description, Type type = null) : base(description, type)
    {
    }
}