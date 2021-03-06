using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;
using Fody;
#pragma warning disable 618

public static class AssemblyWeaver
{
    static AssemblyWeaver()
    {
        var weavingTask = new ModuleWeaver
        {
            Config = new XElement("NullGuard",
                new XAttribute("IncludeDebugAssert", false),
                new XAttribute("ExcludeRegex", "^ClassToExclude$")),
            DefineConstants = new List<string> {"DEBUG"} // Always testing the debug weaver
        };

        TestResult = weavingTask.ExecuteTestRun(
            assemblyPath: "AssemblyToProcessExplicit.dll",
            ignoreCodes: new[] {"0x80131854", "0x801318DE", "0x80131252" });
        Assembly = TestResult.Assembly;
        AfterAssemblyPath = TestResult.AssemblyPath;
    }

    public static string AfterAssemblyPath;

    public static Assembly Assembly;

    public static TestResult TestResult;
}