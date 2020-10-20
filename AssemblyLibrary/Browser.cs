using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace AssemblyLibrary
{
    class Browser
    {
        private Assembly LoadAssembly(string assemblyPath)
        {
            FileInfo file = new FileInfo(assemblyPath);
            Assembly currentAssembly = Assembly.LoadFrom(file.FullName);
            return currentAssembly;
        }

        /// <summary>
        /// method for loading namespaces from current assembly
        /// </summary>
        /// <returns></returns>
        private List<AssemblyNamespace> GetNamespaces(Assembly assembly)
        {
            List<AssemblyNamespace> namespaces = new List<AssemblyNamespace>();
            Type[] types = assembly.GetTypes();
            Dictionary<string, List<TypeInfo>> assemblyInfo = new Dictionary<string, List<TypeInfo>>();
            foreach(Type t in types)
            {
                TypeInfo typeInfo = new TypeInfo(t.Name);
                typeInfo.fields = GetFields(t);
                typeInfo.properties = GetProperties(t);
                typeInfo.methods = GetMethods(t);
                if(assemblyInfo.ContainsKey(t.Name))
                {
                    assemblyInfo[t.Name].Add(typeInfo);

                }
                else
                {
                    List<TypeInfo> infos = new List<TypeInfo>();
                    infos.Add(typeInfo);
                    assemblyInfo.Add(t.Name, infos);
                }
            }

            foreach(string k in assemblyInfo.Keys)
            {
                AssemblyNamespace assemblyNamespace = new AssemblyNamespace(k,assemblyInfo[k]);
                namespaces.Add(assemblyNamespace);
            }

            return namespaces;
        }



        public AssemblyInfo GetResult(string assemblyPath)
        {

            Assembly assembly = LoadAssembly(assemblyPath);

            List<AssemblyNamespace> namespaces = GetNamespaces(assembly);

            AssemblyInfo assemblyInfo = new AssemblyInfo(assembly.FullName,namespaces);

            return assemblyInfo;
        }


        /// <summary>
        /// method for getting list of fields from current namespace
        /// </summary>
        /// <returns></returns>
        private List<AssemblyField> GetFields(Type type)
        {
            List<AssemblyField> currentFields = new List<AssemblyField>();

            foreach(FieldInfo f in type.GetFields())
            {
                AssemblyField assemblyField = new AssemblyField(f.FieldType.Name, f.Name);
                
                
                //check for compilator generated fileds
                currentFields.Add(assemblyField);

            }        

            return currentFields;
        }


        /// <summary>
        /// method for getting list of properties from current namespace
        /// </summary>
        /// <returns></returns>
        private List<AssemblyProperty> GetProperties(Type type)
        {
            List<AssemblyProperty> currentProperties = new List<AssemblyProperty>();

            foreach (PropertyInfo p in type.GetProperties())
            {
                AssemblyProperty assemblyProperty = new AssemblyProperty(p.PropertyType.Name, p.Name);


                //check for compilator generated fileds
                currentProperties.Add(assemblyProperty);
            }
            return currentProperties;

        }

        /// <summary>
        /// methods for getting list of methods from current namespace
        /// </summary>
        /// <returns></returns>

        private List<AssemblyMethod> GetMethods(Type type)
        {
            List<AssemblyMethod> currentMethods = new List<AssemblyMethod>();

            string currentMethodSignature;
            foreach(MethodInfo m in type.GetMethods())
            {
                currentMethodSignature = GetMethodSignature(m);
                
                AssemblyMethod assemblyMethod = new AssemblyMethod(m.Name, currentMethodSignature);
                currentMethods.Add(assemblyMethod);

            }

            return currentMethods;
        }

        private string GetMethodSignature(MethodInfo method)
        {
            string signature = null;

            //

            return signature;
        }

        private bool IsCompilerGenerated(Type type)
        {
            if(Attribute.GetCustomAttribute(type,typeof(CompilerGeneratedAttribute)) == null)
            {
                return false;
            }
            return true;
        }


    }
}
