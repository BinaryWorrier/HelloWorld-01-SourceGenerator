using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace HelloSourceGenerator
{
    [Generator]
    public class MySourceGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            Debugger.Break();
            var aggregates = ((AggregateFinder)context.SyntaxReceiver!)?.Aggregates;

            foreach (var aggregate in aggregates!)
            {
                var types = new List<string>();
                foreach (var when in aggregate.Members)
                {
                    if(when is MethodDeclarationSyntax member && member.Identifier.ToString() == "When")
                    {
                        if(member.ParameterList.Parameters.Count == 1)
                        {
                            var parameter = member.ParameterList.Parameters[0].Type.ToString();
                            types.Add(parameter);
                        }
                    }
                }
                if (types.Count > 0)
                {
                    using var writer = new System.IO.StringWriter();
                    //
                    writer.WriteLine($"namespace HelloWorld_01");
                    writer.WriteLine("{");
                    writer.WriteLine($"public partial class {aggregate.Identifier} : AggregateRoot");
                    writer.WriteLine("{");
                    writer.WriteLine("    protected override void DoApply(object item)");
                    writer.WriteLine("    {");
                    writer.WriteLine("        switch(item)");
                    writer.WriteLine("        {");
                    foreach(var (type, index) in types.Select((s, i) => (s, i)))
                    {
                        writer.WriteLine($"            case {type} typedValue_{index}:");
                        writer.WriteLine($"                When(typedValue_{index});");
                        writer.WriteLine($"                break;");
                    }
                    writer.WriteLine("            default: throw new Exception($\"When for type '{item.GetType().FullName}' not found\");");
                    writer.WriteLine("        }");
                    writer.WriteLine("    }");
                    writer.WriteLine("}");
                    writer.WriteLine("}");

                    context.AddSource($"{aggregate.Identifier}.g.cs", SourceText.From(writer.ToString(), Encoding.UTF8));

                }
            }

        }

        public void Initialize(GeneratorInitializationContext context)
        {
            if (!Debugger.IsAttached)
            {
                //Debugger.Launch();
            }

            context.RegisterForSyntaxNotifications(() => new AggregateFinder());
        }
    }
}
