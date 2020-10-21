using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace AssemblyLibrary
{

    public class AssemblyInfo
    {
        public string assemblyName { get; set; }
        public List<AssemblyNamespace> namespaces;

        public AssemblyInfo(string name, List<AssemblyNamespace> n)
        {
            assemblyName = name;
            namespaces = n;
        }
    }

    public class AssemblyNamespace
    {
        public List<TypeInfo> info { get; set; }
        public string namespaceName { get; set; }

        public AssemblyNamespace(string name, List<TypeInfo> types )
        {
            namespaceName = name;
            info = types;
        }
    }

    public class TypeInfo
    {
        public string typeName { get; set; }
        public List<AssemblyField> fields { get; set; }
        public List<AssemblyProperty> properties { get; set; }
        public List<AssemblyMethod> methods { get; set; }
        public TypeInfo(string name)
        {
            typeName = name;
            fields = new List<AssemblyField>();
            properties = new List<AssemblyProperty>();
            methods = new List<AssemblyMethod>();
        }
    }

    public class AssemblyField
    {
        public string fieldType;
        public string fieldName { get; set; }
        public string ToTreeView { get { return fieldType + " " + fieldName; } }

        public AssemblyField(string type, string name)
        {
            fieldType = type;
            fieldName = name;
        }
    }

    public class AssemblyProperty
    {
        public string propertyType;
        public string propertyName { get; set; }
        public string ToTreeView { get { return propertyType + " " + propertyName; } }

        public AssemblyProperty(string type,string name)
        {
            propertyType = type;
            propertyName = name;
        }

    }

    public class AssemblyMethod
    {
        public string methodName { get; set; }
        public string methodSignature;
        public string ToTreeView { get { return methodName + " " + methodSignature; } }
        public AssemblyMethod(string name, string signature)
        {
            methodName = name;
            methodSignature = signature;
        }

    }




}
