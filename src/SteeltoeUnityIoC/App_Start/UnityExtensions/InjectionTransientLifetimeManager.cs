﻿using System;
using Unity.Lifetime;

// borrowed from https://github.com/unitycontainer/microsoft-dependency-injection/tree/master/src
namespace Unity.Microsoft.DependencyInjection.Lifetime
{
    /// <summary>
    /// A special lifetime manager which works like <see cref="TransienLifetimeManager"/>,
    /// except it makes container remember all Disposable objects it created. Once container
    /// is disposed all these objects are disposed as well.
    /// </summary>
    //internal class InjectionTransientLifetimeManager : LifetimeManager
    //{
    //    public override void SetValue(object newValue, ILifetimeContainer container = null)
    //    {
    //        if (newValue is IDisposable disposable)
    //            container?.Add(disposable);
    //    }

    //    public override object GetValue(ILifetimeContainer container = null)
    //    {
    //        return null;
    //    }

    //    public override void RemoveValue(ILifetimeContainer container = null)
    //    {
    //    }

    //    protected override LifetimeManager OnCreateLifetimeManager()
    //    {
    //        return this;
    //    }

    //    public override bool InUse { get => false; set => base.InUse = false; }
    //}
}