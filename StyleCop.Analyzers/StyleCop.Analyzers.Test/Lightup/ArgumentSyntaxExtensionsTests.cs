// Copyright (c) Tunnel Vision Laboratories, LLC. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

#nullable disable

namespace StyleCop.Analyzers.Test.Lightup
{
    using Microsoft.CodeAnalysis.CSharp;
    using StyleCop.Analyzers.Lightup;
    using Xunit;

    public class ArgumentSyntaxExtensionsTests
    {
        [Fact]
        public void TestRefKindKeyword()
        {
            var argumentSyntax = SyntaxFactory.Argument(SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression));
            Assert.Equal(default, ArgumentSyntaxExtensions.RefKindKeyword(argumentSyntax));
        }

        [Fact]
        public void TestWithRefKindKeyword()
        {
            var argumentSyntax = SyntaxFactory.Argument(SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression));

            // With default value is allowed
            var argumentWithDefaultRefKind = ArgumentSyntaxExtensions.WithRefKindKeyword(argumentSyntax, default);
            Assert.Equal(default, ArgumentSyntaxExtensions.RefKindKeyword(argumentWithDefaultRefKind));
        }
    }
}
