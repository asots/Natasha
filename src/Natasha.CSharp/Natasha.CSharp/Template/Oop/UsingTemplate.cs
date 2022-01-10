﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Natasha.CSharp.Template
{

    public class UsingTemplate<T> : FlagTemplate<T> where T : UsingTemplate<T>, new()
    {

        public StringBuilder UsingScript;
        internal readonly HashSet<string> _usings;

        public UsingTemplate()
        {

            UsingScript = new StringBuilder();
            _usings = new HashSet<string>();

        }



        public T Using(NamespaceConverter converter)
        {

            if (converter.NamespaceAssembly != default)
            {

                Using(converter.NamespaceAssembly);

            }
            else if (converter.NamespaceType != default)
            {

                Using(converter.NamespaceType);

            }
            else if (converter.NamespaceString != default)
            {

                Using(converter.NamespaceString);

            }
            else if (converter.NamespaceAssemblys != default)
            {

                Using(converter.NamespaceAssemblys);

            }
            else if (converter.NamespaceStrings != default)
            {

                Using(converter.NamespaceStrings);

            }
            else if (converter.NamespaceTypes != default)
            {

                Using(converter.NamespaceTypes);

            }
            return Link;

        }



        public T Using(NamespaceConverter[]? converters)
        {

            if (converters != default)
            {

                for (int i = 0; i < converters.Length; i++)
                {

                    Using(converters[i]);

                }

            }
            return Link;

        }




        public T Using(string? @using)
        {

            if (!string.IsNullOrEmpty(@using))
            {

                _usings.Add(@using);

            }
            else
            {
                HiddenNamespace();
            }
            return Link;

        }




        /// <summary>
        /// 从程序集里获取引用
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <returns></returns>
        public T Using(Assembly assembly)
        {

            if (assembly != default)
            {
                Using(assembly.ExportedTypes);
            }
            return Link;

        }




        /// <summary>
        /// 设置命名空间
        /// </summary>
        /// <param name="namespaces">命名空间</param>
        /// <returns></returns>
        public T Using(params string[] namespaces)
        {

            for (int i = 0; i < namespaces.Length; i++)
            {

                Using(namespaces[i]);

            }
            return Link;

        }


        /// <summary>
        /// 设置命名空间
        /// </summary>
        /// <param name="namespaces">命名空间</param>
        /// <returns></returns>
        public T Using(HashSet<string> namespaces)
        {

            foreach (var item in namespaces)
            {
                Using(item);
            }
            return Link;

        }


        protected T LoadCurrentDomainUsing()
        {
            return Using(AssemblyBuilder.Domain.UsingRecorder._usings);
        }


        /// <summary>
        /// 设置命名空间
        /// </summary>
        /// <param name="namespaces">命名空间</param>
        /// <returns></returns>
        public T Using(params Assembly[] namespaces)
        {

            for (int i = 0; i < namespaces.Length; i++)
            {

                Using(namespaces[i]);

            }
            return Link;

        }
        public T Using(IEnumerable<Assembly> namespaces)
        {

            foreach (var item in namespaces)
            {
                Using(item);
            }
            return Link;

        }


        // <summary>
        /// 设置命名空间
        /// </summary>
        /// <param name="namespaces">类型</param>
        /// <returns></returns>
        public T Using(params Type[] namespaces)
        {

            for (int i = 0; i < namespaces.Length; i++)
            {

                Using(namespaces[i]);

            }
            return Link;

        }


        public T Using(IEnumerable<Type> namespaces)
        {

            foreach (var item in namespaces)
            {

                Using(item);

            }
            return Link;

        }


        public T Using<S>()
        {

            Using(typeof(S));
            return Link;

        }

        public T Using(Type type)
        {

            if (type.Namespace!=null)
            {
                Using(type.Namespace);
            }
            var types = type.GetAllTypes();
            if (types.Count>1)
            {
                return Using(type.GetAllTypes());
            }
            return Link;

        }




        public T Using(MethodInfo info)
        {

            Using(info.ReturnType);
            Using(info.GetParameters().Select(item => item.ParameterType));
            return Link;

        }


        private bool _useGlobalUsing;

        public T NoGlobalUsing()
        {
            _useGlobalUsing = false;
            return Link;
        }
        public T UseGlobalUsing()
        {
            _useGlobalUsing = true;
            return Link;
        }

        public StringBuilder GetUsingBuilder()
        {

            Using(this.usingRecorder._usings);
            //如果用户想使用自定义的Using
            if (!_useGlobalUsing)
            {

                UsingScript = new StringBuilder();
                foreach (var @using in _usings)
                {

                    UsingScript.AppendLine($"using {@using};");

                }

            }
            else
            {

                //使用全局Using
                UsingScript.Append(DefaultUsing.UsingScript);
                //把当前域中的using全部加上
                Using(AssemblyBuilder.Domain.UsingRecorder._usings);
                foreach (var @using in _usings)
                {

                    //如果全局已经存在using了，就不加了
                    if (!DefaultUsing.HasElement(@using) && !AssemblyBuilder.Domain.UsingRecorder.HasUsing(@using))
                    {

                        UsingScript.AppendLine($"using {@using};");

                    }

                }

            }


            return UsingScript;
        }


        public override T BuilderScript()
        {

            //  [{this}]
            //  [{Flag}]
            //  [namspace]
            //  { 
            //      [comment]
            //      [attribute]
            //      [access] [modifier] [name] [:interface] 
            //      {
            //          [body]
            //      }
            //      [otherBody]
            //  }
            base.BuilderScript();
            _script.Insert(0, GetUsingBuilder());
            return Link;

        }

    }

}