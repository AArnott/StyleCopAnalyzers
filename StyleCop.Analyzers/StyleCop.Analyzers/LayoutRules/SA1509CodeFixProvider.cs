﻿namespace StyleCop.Analyzers.LayoutRules
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Composition;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CodeActions;
    using Microsoft.CodeAnalysis.CodeFixes;
    using Microsoft.CodeAnalysis.CSharp;

    /// <summary>
    /// Implements a code fix for <see cref="SA1509OpeningCurlyBracketsMustNotBePrecededByBlankLine"/>.
    /// </summary>
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(SA1509CodeFixProvider))]
    [Shared]
    public class SA1509CodeFixProvider : CodeFixProvider
    {
        private static readonly ImmutableArray<string> FixableDiagnostics =
            ImmutableArray.Create(SA1509OpeningCurlyBracketsMustNotBePrecededByBlankLine.DiagnosticId);

        /// <inheritdoc/>
        public override ImmutableArray<string> FixableDiagnosticIds => FixableDiagnostics;

        /// <inheritdoc/>
        public override FixAllProvider GetFixAllProvider()
        {
            return WellKnownFixAllProviders.BatchFixer;
        }

        /// <inheritdoc/>
        public override Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            foreach (Diagnostic diagnostic in context.Diagnostics.Where(d => FixableDiagnostics.Contains(d.Id)).ToList())
            {
                context.RegisterCodeFix(
                    CodeAction.Create(
                        LayoutResources.SA1509CodeFix,
                        token => this.GetTransformedDocumentAsync(context.Document, diagnostic, token)),
                    diagnostic);
            }

            return Task.FromResult(true);
        }

        private async Task<Document> GetTransformedDocumentAsync(Document document, Diagnostic diagnostic, CancellationToken cancellationToken)
        {
            var syntaxRoot = await document.GetSyntaxRootAsync(cancellationToken).ConfigureAwait(false);

            var openBrace = syntaxRoot.FindToken(diagnostic.Location.SourceSpan.Start);
            var leadingTrivia = openBrace.LeadingTrivia;

            var newTriviaList = SyntaxFactory.TriviaList();

            var previousEmptyLines = this.GetPreviousEmptyLines(openBrace).ToList();
            newTriviaList = newTriviaList.AddRange(leadingTrivia.Except(previousEmptyLines));

            var newOpenBrace = openBrace.WithLeadingTrivia(newTriviaList);
            var newSyntaxRoot = syntaxRoot.ReplaceToken(openBrace, newOpenBrace);

            return document.WithSyntaxRoot(newSyntaxRoot);
        }

        private IEnumerable<SyntaxTrivia> GetPreviousEmptyLines(SyntaxToken openBrace)
        {
            var result = new List<SyntaxTrivia>();

            var lineOfOpenBrace = openBrace.GetLocation().GetLineSpan().StartLinePosition.Line;
            var lineToCheck = lineOfOpenBrace - 1;

            while (lineToCheck > -1)
            {
                var trivias = openBrace.LeadingTrivia
                    .Where(t => t.GetLocation().GetLineSpan().StartLinePosition.Line == lineToCheck)
                    .ToList();
                var endOfLineTrivia = trivias.Where(t => t.IsKind(SyntaxKind.EndOfLineTrivia)).ToList();
                if (endOfLineTrivia.Any() && trivias.Except(endOfLineTrivia).All(t => t.IsKind(SyntaxKind.WhitespaceTrivia)))
                {
                    lineToCheck--;
                    result.AddRange(trivias);
                }
                else
                {
                    break;
                }
            }

            return result;
        }
    }
}
