using LibrameCore.Utilities;
using System;
using System.IO;
using System.Reflection;
using Xunit;

namespace LibrameCore.Tests.Utility
{
    public class AssemblyUtilityTests
    {
        private readonly Assembly _assembly = typeof(AssemblyUtilityTests).GetTypeInfo().Assembly;

        [Fact]
        public void GetAssemblyNameTest()
        {
            var assemblyName = _assembly.GetAssemblyName();

            Assert.NotNull(assemblyName);
        }

        [Fact]
        public void ManifestResourceSaveAsTest()
        {
            var outputFilePath = AppContext.BaseDirectory.AppendPath("AssemblyUtilityTestResource.txt");
            var manifestResourceName = typeof(AssemblyUtilityTests).Namespace + ".AssemblyUtilityTestResource.txt";

            _assembly.ManifestResourceSaveAs(manifestResourceName, outputFilePath);
            Assert.True(File.Exists(outputFilePath));

            File.Delete(outputFilePath);
            Assert.False(File.Exists(outputFilePath));
        }

    }
}
