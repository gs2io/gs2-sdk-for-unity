// Copyright 2016-2026 Game Server Services, Inc. or its affiliates. All Rights Reserved.
//
// Pure C# (non-Unity) build compatibility shim.
//
// When this SDK is compiled outside of Unity (UNITY_2017_1_OR_NEWER is not
// defined), the UnityEngine assembly is unavailable. The Ez layer POCO/model
// types are annotated with [SerializeField] (UnityEngine) and [Preserve]
// (UnityEngine.Scripting) purely for Unity serialization / code-stripping.
// These no-op stubs let those annotations resolve in a plain .NET build so the
// types remain usable from pure C# hosts (servers, tests, tooling).
#if !UNITY_2017_1_OR_NEWER
using System;

// ReSharper disable once CheckNamespace
namespace UnityEngine
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public sealed class SerializeField : Attribute
    {
    }
}

// ReSharper disable once CheckNamespace
namespace UnityEngine.Scripting
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
    public sealed class PreserveAttribute : Attribute
    {
    }
}
#endif
