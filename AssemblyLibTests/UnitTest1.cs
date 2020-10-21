using Microsoft.VisualStudio.TestTools.UnitTesting;
using AssemblyLibrary;
using System.Linq;
using System.IO;

namespace AssemblyLibTests
{
    [TestClass]
    public class UnitTest1
    {
        public Browser browser;
        public AssemblyInfo assemblyInfo;

        [TestInitialize]
        public void Setup()
        {
            browser = new Browser();
            string pluginsPath = Directory.GetCurrentDirectory() + "\\FakerLibrary.dll";
            assemblyInfo = browser.GetResult(pluginsPath);
        }


        [TestMethod]
        public void NamespaceNameTest()
        {
            var namespaces = assemblyInfo.namespaces;
            Assert.IsTrue(namespaces.Where(n => n.namespaceName == "FakerLibrary").Any());
        }

        [TestMethod]
        public void TypesCount()
        {
            int typesCountExpectedFaker = 5;
            int typesCountExpectedGens = 7;

            var namespaces = assemblyInfo.namespaces;
            var typesFaker = namespaces.First(n => n.namespaceName == "FakerLibrary").info;
            int typesCountActualFaker = typesFaker.Count;

            var typesGenerators = namespaces.First(n => n.namespaceName == "FakerLibrary.Generators.TypesGenerators").info;
            int typesCountActualGenerators = typesGenerators.Count;

            Assert.AreEqual(typesCountExpectedFaker, typesCountActualFaker);
            Assert.AreEqual(typesCountExpectedGens, typesCountActualGenerators);
        }

        [TestMethod()]
        public void MethodsCheck()
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

        [TestMethod()]
        public void TypeCheck()
        {
            var namespaces = assemblyInfo.namespaces;
            var testType = namespaces.First(n => n.namespaceName == "FakerLibrary").info.FirstOrDefault(t => t.typeName == "Faker");
            int expectedFieldsAmounts = 4;
            int expectedPropertiesAmounts = 1;
            int expectedMethodsAmounts = 10;

            Assert.IsNotNull(testType);
            Assert.AreEqual(expectedFieldsAmounts,testType.fields.Count);
            Assert.AreEqual(expectedPropertiesAmounts, testType.properties.Count);
            Assert.AreEqual(expectedMethodsAmounts, testType.methods.Count);
        }


    }
}
