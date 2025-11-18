// Copyright (c) Tunnel Vision Laboratories, LLC. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

#nullable disable

namespace StyleCop.Analyzers.Lightup
{
    using System;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal static class BaseMethodDeclarationSyntaxExtensions
    {
        private static readonly Func<BaseMethodDeclarationSyntax, ArrowExpressionClauseSyntax> ExpressionBodyAccessor;

        static BaseMethodDeclarationSyntaxExtensions()
        {
            ExpressionBodyAccessor = LightupHelpers.CreateSyntaxPropertyAccessor<BaseMethodDeclarationSyntax, ArrowExpressionClauseSyntax>(typeof(BaseMethodDeclarationSyntax), nameof(ExpressionBody));
        }

        public static ArrowExpressionClauseSyntax ExpressionBody(this BaseMethodDeclarationSyntax syntax)
        {
            return ExpressionBodyAccessor(syntax);
        }
    }
}
