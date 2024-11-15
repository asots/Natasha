﻿using Microsoft.CodeAnalysis;
using System.Linq;
using System.Runtime.Loader;


public class DomainPrepare
{

    protected static readonly int DefaultAssembliesCount;
    protected static readonly int DefaultUsingCount;
    protected static string _runtimeVersion;
    static DomainPrepare()
    {

#if NETCOREAPP3_1
        _runtimeVersion = "netcoreapp3.1";
#elif NET6_0
        _runtimeVersion = "net6.0";
#elif NET8_0
        _runtimeVersion = "net8.0";
#endif
        DefaultAssembliesCount = AssemblyLoadContext.Default.Assemblies.Count();
        NatashaManagement.Preheating((item, name) => name!.Contains("IO"), true, false);
        DefaultUsingCount = NatashaLoadContext.DefaultContext.ReferenceRecorder.Count;
    }

}

