// Copyright (c) Tunnel Vision Laboratories, LLC. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

#nullable disable

namespace StyleCop.Analyzers.Test.Lightup
{
    using Microsoft.CodeAnalysis.CSharp;
    using StyleCop.Analyzers.Lightup;
    using Xunit;

    public class StackAllocArrayCreationExpressionSyntaxExtensionsTests
    {
        [Fact]
        public void TestInitializer()
        {
            var stackAllocSyntax = SyntaxFactory.StackAllocArrayCreationExpression(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.IntKeyword)));
            Assert.Null(StackAllocArrayCreationExpressionSyntaxExtensions.Initializer(stackAllocSyntax));
        }

        [Fact]
        public void TestWithInitializer()
        {
            var stackAllocSyntax = SyntaxFactory.StackAllocArrayCreationExpression(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.IntKeyword)));

            // With default value is allowed
            var stackAllocWithDefaultInitializer = StackAllocArrayCreationExpressionSyntaxExtensions.WithInitializer(stackAllocSyntax, null);
            Assert.Null(StackAllocArrayCreationExpressionSyntaxExtensions.Initializer(stackAllocWithDefaultInitializer));
        }
    }
}
