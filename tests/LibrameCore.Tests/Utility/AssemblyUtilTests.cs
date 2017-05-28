﻿using LibrameCore.Utility;
using System;
using System.IO;
using System.Reflection;
using Xunit;

namespace LibrameCore.Tests.Utility
{
    public class AssemblyUtilTests
    {
        private readonly Assembly _assembly = typeof(AssemblyUtilTests).GetTypeInfo().Assembly;

        [Fact]
        public void GetAssemblyNameTest()
        {
            var assemblyName = _assembly.GetAssemblyName();

            Assert.NotNull(assemblyName);
        }

        [Fact]
        public void ManifestResourceSaveAsTest()
        {
            var outputFilePath = AppContext.BaseDirectory.AppendPath("AssemblyUtilTestResource.txt");
            var manifestResourceName = typeof(AssemblyUtilTests).Namespace + ".AssemblyUtilTestResource.txt";

            _assembly.ManifestResourceSaveAs(manifestResourceName, outputFilePath);
            Assert.True(File.Exists(outputFilePath));

            File.Delete(outputFilePath);
            Assert.False(File.Exists(outputFilePath));
        }

    }
}