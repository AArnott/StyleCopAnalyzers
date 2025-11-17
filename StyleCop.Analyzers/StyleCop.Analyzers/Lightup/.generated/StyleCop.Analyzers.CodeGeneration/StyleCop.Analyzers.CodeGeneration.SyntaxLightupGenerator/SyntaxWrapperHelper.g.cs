// Copyright (c) Tunnel Vision Laboratories, LLC. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace StyleCop.Analyzers.Lightup
{
    using System;
    using System.Collections.Immutable;
    using System.Reflection;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    internal static class SyntaxWrapperHelper
    {
        private static readonly ImmutableDictionary<Type, Type> WrappedTypes;

        static SyntaxWrapperHelper()
        {
            var csharpCodeAnalysisAssembly = typeof(CSharpSyntaxNode).GetTypeInfo().Assembly;
            var builder = ImmutableDictionary.CreateBuilder<Type, Type>();
            builder.Add(typeof(BaseExpressionColonSyntaxWrapper), csharpCodeAnalysisAssembly.GetType(BaseExpressionColonSyntaxWrapper.WrappedTypeName));

            var baseNamespaceDeclarationSyntaxType = csharpCodeAnalysisAssembly.GetType(BaseNamespaceDeclarationSyntaxWrapper.WrappedTypeName) ?? csharpCodeAnalysisAssembly.GetType(BaseNamespaceDeclarationSyntaxWrapper.FallbackWrappedTypeName);
            builder.Add(typeof(BaseNamespaceDeclarationSyntaxWrapper), baseNamespaceDeclarationSyntaxType);
            builder.Add(typeof(ExpressionColonSyntaxWrapper), csharpCodeAnalysisAssembly.GetType(ExpressionColonSyntaxWrapper.WrappedTypeName));
            builder.Add(typeof(FileScopedNamespaceDeclarationSyntaxWrapper), csharpCodeAnalysisAssembly.GetType(FileScopedNamespaceDeclarationSyntaxWrapper.WrappedTypeName));
            builder.Add(typeof(LineDirectivePositionSyntaxWrapper), csharpCodeAnalysisAssembly.GetType(LineDirectivePositionSyntaxWrapper.WrappedTypeName));
            builder.Add(typeof(LineOrSpanDirectiveTriviaSyntaxWrapper), csharpCodeAnalysisAssembly.GetType(LineOrSpanDirectiveTriviaSyntaxWrapper.WrappedTypeName));
            builder.Add(typeof(LineSpanDirectiveTriviaSyntaxWrapper), csharpCodeAnalysisAssembly.GetType(LineSpanDirectiveTriviaSyntaxWrapper.WrappedTypeName));

            WrappedTypes = builder.ToImmutable();
        }

        /// <summary>
        /// Gets the type that is wrapped by the given wrapper.
        /// </summary>
        /// <param name = "wrapperType">Type of the wrapper for which the wrapped type should be retrieved.</param>
        /// <returns>The wrapped type, or null if there is no info.</returns>
        internal static Type GetWrappedType(Type wrapperType)
        {
            if (WrappedTypes.TryGetValue(wrapperType, out Type wrappedType))
            {
                return wrappedType;
            }

            return null;
        }
    }
}
