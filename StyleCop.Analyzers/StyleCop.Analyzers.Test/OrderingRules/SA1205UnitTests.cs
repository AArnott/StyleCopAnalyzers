// Copyright (c) Tunnel Vision Laboratories, LLC. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

#nullable disable

namespace StyleCop.Analyzers.Test.OrderingRules
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.CodeAnalysis.Testing;
    using StyleCop.Analyzers.Lightup;
    using StyleCop.Analyzers.OrderingRules;
    using Xunit;
    using static StyleCop.Analyzers.Test.Verifiers.StyleCopCodeFixVerifier<
        StyleCop.Analyzers.OrderingRules.SA1205PartialElementsMustDeclareAccess,
        StyleCop.Analyzers.OrderingRules.SA1205CodeFixProvider>;

    /// <summary>
    /// Unit tests for the <see cref="SA1205PartialElementsMustDeclareAccess"/> class.
    /// </summary>
    public class SA1205UnitTests
    {
        private const string TestCodeTemplate = @"$$ Foo
{
}
";

        private const string FixedTestCodeTemplate = @"## $$ Foo
{
}
";

        public static IEnumerable<object[]> ValidDeclarations
        {
            get
            {
                yield return new object[] { "public partial class", LanguageVersionEx.CSharp7 };
                yield return new object[] { "internal partial class", LanguageVersionEx.CSharp7 };
                yield return new object[] { "public static partial class", LanguageVersionEx.CSharp7 };
                yield return new object[] { "internal static partial class", LanguageVersionEx.CSharp7 };
                yield return new object[] { "public sealed partial class", LanguageVersionEx.CSharp7 };
                yield return new object[] { "internal sealed partial class", LanguageVersionEx.CSharp7 };
                yield return new object[] { "public partial struct", LanguageVersionEx.CSharp7 };
                yield return new object[] { "internal partial struct", LanguageVersionEx.CSharp7 };
                yield return new object[] { "public partial interface", LanguageVersionEx.CSharp7 };
                yield return new object[] { "internal partial interface", LanguageVersionEx.CSharp7 };
                yield return new object[] { "class", LanguageVersionEx.CSharp7 };
                yield return new object[] { "struct", LanguageVersionEx.CSharp7 };
                yield return new object[] { "interface", LanguageVersionEx.CSharp7 };

                yield return new object[] { "public partial record", LanguageVersionEx.CSharp9 };
                yield return new object[] { "internal partial record", LanguageVersionEx.CSharp9 };
                yield return new object[] { "public sealed partial record", LanguageVersionEx.CSharp9 };
                yield return new object[] { "internal sealed partial record", LanguageVersionEx.CSharp9 };
                yield return new object[] { "record", LanguageVersionEx.CSharp9 };
                yield return new object[] { "public partial record class", LanguageVersionEx.CSharp10 };
                yield return new object[] { "internal partial record class", LanguageVersionEx.CSharp10 };
                yield return new object[] { "public sealed partial record class", LanguageVersionEx.CSharp10 };
                yield return new object[] { "internal sealed partial record class", LanguageVersionEx.CSharp10 };
                yield return new object[] { "record class", LanguageVersionEx.CSharp10 };

                yield return new object[] { "public partial record struct", LanguageVersionEx.CSharp10 };
                yield return new object[] { "internal partial record struct", LanguageVersionEx.CSharp10 };
                yield return new object[] { "record struct", LanguageVersionEx.CSharp10 };
            }
        }

        public static IEnumerable<object[]> InvalidDeclarations
        {
            get
            {
                yield return new object[] { "partial class", LanguageVersionEx.CSharp7 };
                yield return new object[] { "sealed partial class", LanguageVersionEx.CSharp7 };
                yield return new object[] { "static partial class", LanguageVersionEx.CSharp7 };
                yield return new object[] { "partial struct", LanguageVersionEx.CSharp7 };
                yield return new object[] { "partial interface", LanguageVersionEx.CSharp7 };

                yield return new object[] { "partial record", LanguageVersionEx.CSharp9 };
                yield return new object[] { "sealed partial record", LanguageVersionEx.CSharp9 };
                yield return new object[] { "partial record class", LanguageVersionEx.CSharp10 };
                yield return new object[] { "sealed partial record class", LanguageVersionEx.CSharp10 };

                yield return new object[] { "partial record struct", LanguageVersionEx.CSharp10 };
            }
        }

        public static IEnumerable<object[]> ValidNestedDeclarations
        {
            get
            {
                yield return new object[] { "public", "class", LanguageVersionEx.CSharp7 };
                yield return new object[] { "protected", "class", LanguageVersionEx.CSharp7 };
                yield return new object[] { "internal", "class", LanguageVersionEx.CSharp7 };
                yield return new object[] { "protected internal", "class", LanguageVersionEx.CSharp7 };
                yield return new object[] { "private", "class", LanguageVersionEx.CSharp7 };

                yield return new object[] { "public", "struct", LanguageVersionEx.CSharp7 };
                yield return new object[] { "protected", "struct", LanguageVersionEx.CSharp7 };
                yield return new object[] { "internal", "struct", LanguageVersionEx.CSharp7 };
                yield return new object[] { "protected internal", "struct", LanguageVersionEx.CSharp7 };
                yield return new object[] { "private", "struct", LanguageVersionEx.CSharp7 };

                yield return new object[] { "public", "interface", LanguageVersionEx.CSharp7 };
                yield return new object[] { "protected", "interface", LanguageVersionEx.CSharp7 };
                yield return new object[] { "internal", "interface", LanguageVersionEx.CSharp7 };
                yield return new object[] { "protected internal", "interface", LanguageVersionEx.CSharp7 };
                yield return new object[] { "private", "interface", LanguageVersionEx.CSharp7 };

                yield return new object[] { "private protected", "class", LanguageVersionEx.CSharp7_2 };
                yield return new object[] { "private protected", "struct", LanguageVersionEx.CSharp7_2 };
                yield return new object[] { "private protected", "interface", LanguageVersionEx.CSharp7_2 };

                yield return new object[] { "public", "record", LanguageVersionEx.CSharp9 };
                yield return new object[] { "protected", "record", LanguageVersionEx.CSharp9 };
                yield return new object[] { "internal", "record", LanguageVersionEx.CSharp9 };
                yield return new object[] { "protected internal", "record", LanguageVersionEx.CSharp9 };
                yield return new object[] { "private", "record", LanguageVersionEx.CSharp9 };
                yield return new object[] { "private protected", "record", LanguageVersionEx.CSharp9 };

                yield return new object[] { "public", "record class", LanguageVersionEx.CSharp10 };
                yield return new object[] { "protected", "record class", LanguageVersionEx.CSharp10 };
                yield return new object[] { "internal", "record class", LanguageVersionEx.CSharp10 };
                yield return new object[] { "protected internal", "record class", LanguageVersionEx.CSharp10 };
                yield return new object[] { "private", "record class", LanguageVersionEx.CSharp10 };
                yield return new object[] { "private protected", "record class", LanguageVersionEx.CSharp10 };

                yield return new object[] { "public", "record struct", LanguageVersionEx.CSharp10 };
                yield return new object[] { "protected", "record struct", LanguageVersionEx.CSharp10 };
                yield return new object[] { "internal", "record struct", LanguageVersionEx.CSharp10 };
                yield return new object[] { "protected internal", "record struct", LanguageVersionEx.CSharp10 };
                yield return new object[] { "private", "record struct", LanguageVersionEx.CSharp10 };
                yield return new object[] { "private protected", "record struct", LanguageVersionEx.CSharp10 };
            }
        }

        /// <summary>
        /// Verifies that a valid declaration (with an access modifier or not a partial type) will not produce a diagnostic.
        /// </summary>
        /// <param name="declaration">The declaration to verify.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Theory]
        [MemberData(nameof(ValidDeclarations))]
        public async Task TestValidDeclarationAsync(string declaration)
        {
            var testCode = TestCodeTemplate.Replace("$$", declaration);
            await VerifyCSharpDiagnosticAsync(testCode, DiagnosticResult.EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Verifies that an invalid type declaration will produce a diagnostic.
        /// </summary>
        /// <param name="declaration">The declaration to verify.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Theory]
        [MemberData(nameof(InvalidDeclarations))]
        public async Task TestInvalidDeclarationAsync(string declaration)
        {
            var testCode = TestCodeTemplate.Replace("$$", declaration);
            var fixedTestCode = FixedTestCodeTemplate.Replace("##", "internal").Replace("$$", declaration);

            await VerifyCSharpFixAsync(testCode, Diagnostic().WithLocation(1, 2 + declaration.Length), fixedTestCode, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Verifies that the code fix will properly copy over the access modifier defined in another fragment of the partial element.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task TestProperAccessModifierPropagationAsync()
        {
            var testCode = @"public partial class Foo
{
    private int field1;
}

partial class Foo
{
    private int field2;
}
";

            var fixedTestCode = @"public partial class Foo
{
    private int field1;
}

public partial class Foo
{
    private int field2;
}
";

            await VerifyCSharpFixAsync(testCode, Diagnostic().WithLocation(6, 15), fixedTestCode, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Verifies that the code fix will properly copy over the access modifier defined in another fragment of the partial element.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task TestCodeFixWithXmlDocumentationAsync()
        {
            var testCode = @"public partial class Foo
{
    private int field1;
}

/// <summary>
/// This is a summary
/// </summary>
partial class Foo
{
    private int field2;
}
";

            var fixedTestCode = @"public partial class Foo
{
    private int field1;
}

/// <summary>
/// This is a summary
/// </summary>
public partial class Foo
{
    private int field2;
}
";

            await VerifyCSharpFixAsync(testCode, Diagnostic().WithLocation(9, 15), fixedTestCode, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Verifies that all 5 access modifiers are accepted for nested types.
        /// This is a regression test for issue #2040.
        /// </summary>
        /// <param name="accessModifier">The access modifier to use for the nested type.</param>
        /// <param name="typeKeyword">The type keyword to use.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Theory]
        [MemberData(nameof(ValidNestedDeclarations))]
        public async Task TestNestedTypeAccessModifiersAsync(string accessModifier, string typeKeyword)
        {
            var testCode = $@"
internal static partial class TestPartial
{{
    {accessModifier} partial {typeKeyword} PartialInner
    {{
    }}
}}
";

            await VerifyCSharpDiagnosticAsync(languageVersion: null, testCode, DiagnosticResult.EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Verifies that a nested type without access modifiers will produce a diagnostic and can be fixed correctly.
        /// </summary>
        /// <param name="declaration">The declaration to verify.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Theory]
        [MemberData(nameof(InvalidDeclarations))]
        public async Task TestNestedTypeWithoutAccessModifierAsync(string declaration)
        {
            var testCode = $@"
public class Foo
{{
    {declaration} Bar
    {{
    }}
}}
";

            var fixedTestCode = $@"
public class Foo
{{
    private {declaration} Bar
    {{
    }}
}}
";

            await VerifyCSharpFixAsync(testCode, Diagnostic().WithLocation(4, 6 + declaration.Length), fixedTestCode, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Verifies that the code fix will properly copy over the access modifier defined in another fragment of the nested partial element.
        /// </summary>
        /// <param name="accessModifier">The access modifier to use for the nested type.</param>
        /// <param name="typeKeyword">The type keyword to use.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Theory]
        [MemberData(nameof(ValidNestedDeclarations))]
        public async Task TestProperNestedAccessModifierPropagationAsync(string accessModifier, string typeKeyword)
        {
            var testCode = $@"
public class Foo
{{
    {accessModifier} partial {typeKeyword} Bar
    {{
    }}

    partial {typeKeyword} Bar
    {{
    }}
}}
";

            var fixedTestCode = $@"
public class Foo
{{
    {accessModifier} partial {typeKeyword} Bar
    {{
    }}

    {accessModifier} partial {typeKeyword} Bar
    {{
    }}
}}
";

            await VerifyCSharpFixAsync(languageVersion: null, testCode, Diagnostic().WithLocation(8, 14 + typeKeyword.Length), fixedTestCode, CancellationToken.None).ConfigureAwait(false);
        }
    }
}
