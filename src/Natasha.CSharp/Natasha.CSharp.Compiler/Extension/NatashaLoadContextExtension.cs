﻿using System;
using System.Reflection;

public static class NatashaLoadContextExtension
{

    /// <summary>
    /// 根据类型所在的 Assmely及其引用的程序集 增加元数据 和 using
    /// </summary>
    /// <param name="context">Natasha 加载上下文</param>
    /// <param name="type">要添加引用的类型</param>
    /// <param name="loadReferenceBehavior">加载行为</param>
    public static NatashaLoadContext AddReferenceAndUsingCode(this NatashaLoadContext context, Type type, AssemblyCompareInfomation loadReferenceBehavior = AssemblyCompareInfomation.None)
    {
        return context.AddReferenceAndUsingCode(type.Assembly, null, loadReferenceBehavior);
    }
    /// <summary>
    /// 根据类型所在的 Assmely及其引用的程序集 增加元数据 和 using
    /// </summary>
    /// <param name="context">Natasha 加载上下文</param>
    /// <param name="type">类型</param>
    /// <param name="excludeAssembliesFunc">过滤委托</param>
    public static NatashaLoadContext AddReferenceAndUsingCode(this NatashaLoadContext context, Type type, Func<AssemblyName, bool> excludeAssembliesFunc)
    {
        return context.AddReferenceAndUsingCode(type.Assembly, excludeAssembliesFunc, AssemblyCompareInfomation.None);
    }

    /// <summary>
    /// 根据 Assmely及其引用的程序集 增加元数据 和 using
    /// </summary>
    /// <param name="context">Natasha 加载上下文</param>
    /// <param name="assembly">程序集</param>
    /// <param name="excludeAssembliesFunc">过滤委托</param>
    public static NatashaLoadContext AddReferenceAndUsingCode(this NatashaLoadContext context, Assembly assembly, Func<AssemblyName, bool> excludeAssembliesFunc)
    {
        return context.AddReferenceAndUsingCode(assembly, excludeAssembliesFunc, AssemblyCompareInfomation.None);
    }

    /// <summary>
    /// 根据 Assmely及其引用的程序集 增加元数据 和 using
    /// </summary>
    /// <param name="context">Natasha 加载上下文</param>
    /// <param name="assembly">程序集</param>
    /// <param name="loadReferenceBehavior">加载行为</param>
    public static NatashaLoadContext AddReferenceAndUsingCode(this NatashaLoadContext context, Assembly assembly, AssemblyCompareInfomation loadReferenceBehavior)
    {
        return context.AddReferenceAndUsingCode(assembly, null, loadReferenceBehavior);
    }

}
