﻿namespace StyleCop.Analyzers.OrderingRules
{
    using System.Collections.Immutable;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Diagnostics;

    /// <summary>
    /// A static readonly element is positioned beneath a static non-readonly element of the same type.
    /// </summary>
    /// <remarks>
    /// <para>A violation of this rule occurs when a static readonly element is positioned beneath a static non-readonly
    /// element of the same type.</para>
    /// </remarks>
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class SA1214StaticReadonlyElementsMustAppearBeforeStaticNonReadonlyElements : DiagnosticAnalyzer
    {
        /// <summary>
        /// The ID for diagnostics produced by the
        /// <see cref="SA1214StaticReadonlyElementsMustAppearBeforeStaticNonReadonlyElements"/> analyzer.
        /// </summary>
        public const string DiagnosticId = "SA1214";
        private const string Title = "Static readonly elements must appear before static non-readonly elements";
        private const string MessageFormat = "TODO: Message format";
        private const string Category = "StyleCop.CSharp.OrderingRules";
        private const string Description = "A static readonly element is positioned beneath a static non-readonly element of the same type.";
        private const string HelpLink = "http://www.stylecop.com/docs/SA1214.html";

        private static readonly DiagnosticDescriptor Descriptor =
            new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, AnalyzerConstants.DisabledNoTests, Description, HelpLink);

        private static readonly ImmutableArray<DiagnosticDescriptor> SupportedDiagnosticsValue =
            ImmutableArray.Create(Descriptor);

        /// <inheritdoc/>
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
        {
            get
            {
                return SupportedDiagnosticsValue;
            }
        }

        /// <inheritdoc/>
        public override void Initialize(AnalysisContext context)
        {
            // TODO: Implement analysis
        }
    }
}
