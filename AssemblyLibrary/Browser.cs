using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace AssemblyLibrary
{
    public class Browser
    {
        private Assembly LoadAssembly(string assemblyPath)
        {
            FileInfo file = new FileInfo(assemblyPath);
            Assembly currentAssembly = Assembly.LoadFrom(file.FullName);
            return currentAssembly;
        }

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
                if(assemblyInfo.ContainsKey(t.Namespace))
                {
                    assemblyInfo[t.Namespace].Add(typeInfo);

                }
                else
                {
                    List<TypeInfo> infos = new List<TypeInfo>();
                    infos.Add(typeInfo);
                    assemblyInfo.Add(t.Namespace, infos);
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

        private List<AssemblyField> GetFields(Type type)
        {
            List<AssemblyField> currentFields = new List<AssemblyField>();

            foreach(FieldInfo f in type.GetFields())
            {
                AssemblyField assemblyField = new AssemblyField(f.FieldType.Name, f.Name);
                               
                if(!IsCompilerGenerated(f.FieldType))
                    currentFields.Add(assemblyField);
            }        
            return currentFields;
        }

        private List<AssemblyProperty> GetProperties(Type type)
        {
            List<AssemblyProperty> currentProperties = new List<AssemblyProperty>();

            foreach (PropertyInfo p in type.GetProperties())
            {
                AssemblyProperty assemblyProperty = new AssemblyProperty(p.PropertyType.Name, p.Name);


                if (!IsCompilerGenerated(p.PropertyType))
                    currentProperties.Add(assemblyProperty);
            }
            return currentProperties;
        }

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
            StringBuilder signature = new StringBuilder();
            signature.Append("(");

            ParameterInfo[] parameters = method.GetParameters();
            foreach(ParameterInfo p in parameters)
            {
                if (p.IsOut)
                    signature.Append("out ");
                else if (p.IsIn)
                    signature.Append("in ");
                else if (p.ParameterType.IsByRef)
                    signature.Append("ref ");
                signature.Append(p.ParameterType.Name+',');
            }
            if (signature[signature.Length - 1] == ',')
                signature[signature.Length - 1] = ')';

            return signature.ToString();
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
