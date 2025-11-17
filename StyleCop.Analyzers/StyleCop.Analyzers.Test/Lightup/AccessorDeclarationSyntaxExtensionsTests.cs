// Copyright (c) Tunnel Vision Laboratories, LLC. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

#nullable disable

namespace StyleCop.Analyzers.Test.Lightup
{
    using Microsoft.CodeAnalysis.CSharp;
    using StyleCop.Analyzers.Lightup;
    using Xunit;

    public class AccessorDeclarationSyntaxExtensionsTests
    {
        [Fact]
        public void TestExpressionBody()
        {
            var accessorDeclarationSyntax = SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration);
            Assert.Null(AccessorDeclarationSyntaxExtensions.ExpressionBody(accessorDeclarationSyntax));
        }

        [Fact]
        public void TestWithExpressionBody()
        {
            var accessorDeclarationSyntax = SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration);

            // With default value is allowed
            var accessorWithDefaultBody = AccessorDeclarationSyntaxExtensions.WithExpressionBody(accessorDeclarationSyntax, null);
            Assert.Null(AccessorDeclarationSyntaxExtensions.ExpressionBody(accessorWithDefaultBody));
        }
    }
}
