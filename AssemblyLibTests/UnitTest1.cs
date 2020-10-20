using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AssemblyLibrary;
using System.Linq;

namespace AssemblyLibTests
{
    [TestClass]
    public class UnitTest1
    {

        public Browser browser;
        public AssemblyInfo assemblyInfo;

        [TestInitialize]
        public void Init()
        {
            browser = new Browser();
            assemblyInfo = browser.GetResult(@"d:\Ангелина\5 сем\5 сем\СПП\Lab2-MPP\MPP-Faker\FakerLibrary\bin\Debug\netstandard2.0\FakerLibrary.dll");
        }

        [TestMethod()]
        public void NamespaceNameTest()
        {
            var namespaces = assemblyInfo.namespaces;
            Assert.IsTrue(namespaces.Where(n => n.namespaceName == "FakerLibrary").Any());
        }

        [TestMethod]
        public void TypesCount()
        {
            int typesCountExpected = 7;

            var namespaces = assemblyInfo.namespaces;
            var typesFaker = namespaces.First(n => n.namespaceName == "FakerLibrary").info;
            int typesCountActualFaker = typesFaker.Count;

            var typesGenerators = namespaces.First(n => n.namespaceName == "FakerLibrary.Generators.TypesGenerators").info;
            int typesCountActualGenerators = typesGenerators.Count;

            Assert.AreEqual(typesCountExpected, typesCountActualFaker);
            Assert.AreEqual(typesCountExpected, typesCountActualGenerators);
        }

        [TestMethod()]
        public void TypeCheck()
        {
            var namespaces = assemblyInfo.namespaces;
            var testType = namespaces.First(n => n.namespaceName == "FakerLibrary").info.FirstOrDefault(t => t.typeName == "IGenerator");
            string methodname = "CanGenerate";
            string methodSignature = "(Type)";

            Assert.IsNotNull(testType);
            Assert.IsTrue(testType.methods.Count == 2);
            Assert.IsTrue(testType.methods.Where(m => m.methodName == methodname).Any());
            Assert.IsTrue(testType.methods.FirstOrDefault(m => m.methodName == methodname).methodSignature == methodSignature);
        }



    }
}
