﻿using Natasha;
using Natasha.CSharp.Operator;
using System;

namespace Natasha.CSharp
{
    public class NInstance
    {

        public static Func<T> Creator<T>()
        {

            return NDelegate.UseDomain(typeof(T).GetDomain()).Func<T>($"return new {typeof(T).GetDevelopName()}();");
        
        }




        public static Delegate Creator(Type type)
        {

            return FastMethodOperator
                .UseDomain(type.GetDomain())
                .Body($"return new {type.GetDevelopName()}();")
                .Return(type)
                .Compile();

        }

    }

}
