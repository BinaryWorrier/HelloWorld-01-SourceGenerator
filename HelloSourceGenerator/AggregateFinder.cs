using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloSourceGenerator
{
    internal class AggregateFinder : ISyntaxReceiver
    {
        public List<ClassDeclarationSyntax> Aggregates { get; }
            = new();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is ClassDeclarationSyntax ag)
            {
                if (ag.Identifier.ValueText.EndsWith("Aggregate"))
                {
                    Aggregates.Add(ag);
                }
            }
        }
    }
}
