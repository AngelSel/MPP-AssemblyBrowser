using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
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

        public AssemblyInfo GetResult()
        {
           
            //here should be some method for loading namespaces from current assembly
            
            //call of GetInformation method

        }
        /// <summary>
        /// method for loading namespaces from current assembly
        /// </summary>
        /// <returns></returns>
        private List<AssemblyNamespace> GetInformation()
        {
            List<AssemblyNamespace> currentNamespaces = new List<AssemblyNamespace>();

            //here we should work with each namespace: get lists of fields,properties and methods


            return currentNamespaces;
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

            foreach (PropertyInfo f in type.GetProperties())
            {
                AssemblyProperty assemblyProperty = new AssemblyProperty(f.PropertyType.Name, f.Name);


                //check for compilator generated fileds
                currentProperties.Add(assemblyProperty);
            }
            return currentProperties;

        }

        /// <summary>
        /// methods for getting list of methods from current namespace
        /// </summary>
        /// <returns></returns>

        private List<AssemblyMethod> GetMethods()
        {
            List<AssemblyMethod> currentMethods = new List<AssemblyMethod>();

            return currentMethods;
        }


    }
}
