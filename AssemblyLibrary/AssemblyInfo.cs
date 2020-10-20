using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace AssemblyLibrary
{

    public class AssemblyInfo
    {
        public string assemblyName { get; set; }
        public IList<AssemblyNamespace> namespaces;
        public AssemblyInfo()
        {
            namespaces = new List<AssemblyNamespace>();
        }
    }

    public class AssemblyNamespace
    {
        public IList<NamespaceInfo> info;
        public string namespaceName { get; set; }

        public AssemblyNamespace()
        {
            info = new List<NamespaceInfo>();
        }
    }

    public class NamespaceInfo
    {
        public IList<AssemblyField> fields { get; set; }
        public IList<AssemblyProperty> properties { get; set; }
        public IList<AssemblyMethod> methods { get; set; }
        public NamespaceInfo()
        {
            fields = new List<AssemblyField>();
            properties = new List<AssemblyProperty>();
            methods = new List<AssemblyMethod>();
        }
    }

    public class AssemblyField
    {
        public string fieldType;
        public string fieldName { get; set; }
    }

    public class AssemblyProperty
    {
        public string propertyType;
        public string propertyName { get; set; }
    }

    public class AssemblyMethod
    {
        public string methodName;
        public string methodSignature;
    }




}
