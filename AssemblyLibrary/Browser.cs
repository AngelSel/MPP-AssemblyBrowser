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
            string typeName;
            Dictionary<string, List<TypeInfo>> assemblyInfo = new Dictionary<string, List<TypeInfo>>();
            foreach(Type t in types)
            {
                if (!IsCompilerGenerated(t))
                {

                    if (t.IsGenericType)
                        typeName = GenericType(t);
                    else
                        typeName = t.Name;

                    TypeInfo typeInfo = new TypeInfo(typeName);
                    typeInfo.fields = GetFields(t);
                    typeInfo.properties = GetProperties(t);
                    typeInfo.methods = GetMethods(t);
                    if (assemblyInfo.ContainsKey(t.Namespace))
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
            string fieldType;
            foreach(FieldInfo f in type.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly))
            {
                if (IsCompilerGenerated(f.FieldType))
                    continue;
                if (f.FieldType.IsGenericType)
                    fieldType = GenericType(f.FieldType);
                else
                    fieldType = f.FieldType.Name;

                AssemblyField assemblyField = new AssemblyField(fieldType, f.Name);

                currentFields.Add(assemblyField);
            }        
            return currentFields;
        }

        private List<AssemblyProperty> GetProperties(Type type)
        {
            List<AssemblyProperty> currentProperties = new List<AssemblyProperty>();
            string propertyType;
            foreach (PropertyInfo p in type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly))
            {
                if (p.PropertyType.IsGenericType)
                    propertyType = GenericType(p.PropertyType);
                else
                    propertyType = p.PropertyType.Name;

                AssemblyProperty assemblyProperty = new AssemblyProperty(propertyType, p.Name);

                if (!IsCompilerGenerated(p.PropertyType))
                    currentProperties.Add(assemblyProperty);
            }
            return currentProperties;
        }

        private List<AssemblyMethod> GetMethods(Type type)
        {
            List<AssemblyMethod> currentMethods = new List<AssemblyMethod>();

            string currentMethodSignature;
            foreach(MethodInfo m in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly))
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
   

            if(method.IsGenericMethod)
            {
                var args = method.GetGenericArguments();
                signature.Append('<');
                foreach(Type t in args)
                {
                    signature.Append(t.Name + ',');
                }
                signature[signature.Length - 1] = '>';
            }

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
            else
                signature.Append(')');

            return signature.ToString();
        }

        private string GenericType(Type type)
        {
            StringBuilder str = new StringBuilder();
            str.Append(type.Name.Split('`')[0]);
            str.Append('<');
            string s;
            foreach(Type t in type.GetGenericArguments())
            {
                if (t.IsGenericType)
                    s = GenericType(t);
                else
                    s = t.Name;
                str.Append(s + ',');
                               
            }
            str[str.Length - 1] = '>';

            return str.ToString();
        }

        private bool IsCompilerGenerated(Type type)
        {
            if(Attribute.GetCustomAttribute(type,typeof(CompilerGeneratedAttribute)) != null)
                return true; 
            return false;
        }
    }
}
