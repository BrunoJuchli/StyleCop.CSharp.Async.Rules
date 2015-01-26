namespace Specifications.ExampleSourceLineFinder
{
    using Mono.Cecil;
    using Mono.Cecil.Rocks;
    using System.Linq;
    using System.Reflection;

    public class SourceLineFinder
    {
        // todo does not work for async methods :(
        public static int FindSourceLine(MethodInfo methodInfo)
        {
            AssemblyDefinition assemblyOfMethod = AssemblyDefinition.ReadAssembly(
                methodInfo.DeclaringType.Assembly.Location,
                new ReaderParameters
                {
                    ReadSymbols = true
                });

            TypeDefinition classOfMethod = assemblyOfMethod.MainModule.GetType(methodInfo.DeclaringType.FullName);
            MethodDefinition methodDefinition = classOfMethod.GetMethods().Single(x => x.Name == methodInfo.Name);

            int firstInstructionLineNumber = methodDefinition
                .Body
                .Instructions
                .Where(x => x.SequencePoint != null)
                .First()
                .SequencePoint.StartLine;

            // PDB doesn't store the actual method definition source linber number
            // so i'm going to assume that the first isntruction is on the next line.
            return firstInstructionLineNumber - 1;
        }
    }
}