using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblyLibrary
{
    class Browser
    {

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
        private List<AssemblyField> GetFields()
        {
            List<AssemblyField> currentFields = new List<AssemblyField>();

            return currentFields;
        }


        /// <summary>
        /// method for getting list of properties from current namespace
        /// </summary>
        /// <returns></returns>
        private List<AssemblyProperty> GetProperties()
        {
            List<AssemblyProperty> currentProperties = new List<AssemblyProperty>();

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
