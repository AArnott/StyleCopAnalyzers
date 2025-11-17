// Copyright (c) Tunnel Vision Laboratories, LLC. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

#nullable disable

namespace StyleCop.Analyzers.Lightup
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal static class SyntaxFactoryEx
    {
        private static readonly Func<SeparatedSyntaxListWrapper<SubpatternSyntax>, CSharpSyntaxNode> PositionalPatternClauseAccessor1;
        private static readonly Func<SyntaxToken, SeparatedSyntaxListWrapper<SubpatternSyntax>, SyntaxToken, CSharpSyntaxNode> PositionalPatternClauseAccessor2;
        private static readonly Func<SeparatedSyntaxListWrapper<SubpatternSyntax>, CSharpSyntaxNode> PropertyPatternClauseAccessor1;
        private static readonly Func<SyntaxToken, SeparatedSyntaxListWrapper<SubpatternSyntax>, SyntaxToken, CSharpSyntaxNode> PropertyPatternClauseAccessor2;

        static SyntaxFactoryEx()
        {
            var positionalPatternClauseMethods = typeof(SyntaxFactory).GetTypeInfo().GetDeclaredMethods(nameof(PositionalPatternClause));
            var positionalPatternClauseMethod = positionalPatternClauseMethods.FirstOrDefault(method => method.GetParameters().Length == 1 && method.GetParameters()[0].ParameterType == typeof(SeparatedSyntaxList<>).MakeGenericType(SyntaxWrapperHelper.GetWrappedType(typeof(SubpatternSyntax))));
            if (positionalPatternClauseMethod is object)
            {
                var subpatternsParameter = Expression.Parameter(typeof(SeparatedSyntaxListWrapper<SubpatternSyntax>), "subpatterns");
                var underlyingListProperty = typeof(SeparatedSyntaxListWrapper<SubpatternSyntax>).GetTypeInfo().GetDeclaredProperty(nameof(SeparatedSyntaxListWrapper<SubpatternSyntax>.UnderlyingList));
                Expression<Func<SeparatedSyntaxListWrapper<SubpatternSyntax>, CSharpSyntaxNode>> expression =
                    Expression.Lambda<Func<SeparatedSyntaxListWrapper<SubpatternSyntax>, CSharpSyntaxNode>>(
                        Expression.Call(
                            positionalPatternClauseMethod,
                            Expression.Convert(
                                Expression.Call(subpatternsParameter, underlyingListProperty.GetMethod),
                                positionalPatternClauseMethod.GetParameters()[0].ParameterType)),
                        subpatternsParameter);
                PositionalPatternClauseAccessor1 = expression.Compile();
            }
            else
            {
                PositionalPatternClauseAccessor1 = ThrowNotSupportedOnFallback<SeparatedSyntaxListWrapper<SubpatternSyntax>, CSharpSyntaxNode>(nameof(SyntaxFactory), nameof(PositionalPatternClause));
            }

            positionalPatternClauseMethod = positionalPatternClauseMethods.FirstOrDefault(method => method.GetParameters().Length == 3
                && method.GetParameters()[0].ParameterType == typeof(SyntaxToken)
                && method.GetParameters()[1].ParameterType == typeof(SeparatedSyntaxList<>).MakeGenericType(SyntaxWrapperHelper.GetWrappedType(typeof(SubpatternSyntax)))
                && method.GetParameters()[2].ParameterType == typeof(SyntaxToken));
            if (positionalPatternClauseMethod is object)
            {
                var openParenTokenParameter = Expression.Parameter(typeof(SyntaxToken), "openParenToken");
                var subpatternsParameter = Expression.Parameter(typeof(SeparatedSyntaxListWrapper<SubpatternSyntax>), "subpatterns");
                var closeParenTokenParameter = Expression.Parameter(typeof(SyntaxToken), "closeParenToken");

                var underlyingListProperty = typeof(SeparatedSyntaxListWrapper<SubpatternSyntax>).GetTypeInfo().GetDeclaredProperty(nameof(SeparatedSyntaxListWrapper<SubpatternSyntax>.UnderlyingList));

                Expression<Func<SyntaxToken, SeparatedSyntaxListWrapper<SubpatternSyntax>, SyntaxToken, CSharpSyntaxNode>> expression =
                    Expression.Lambda<Func<SyntaxToken, SeparatedSyntaxListWrapper<SubpatternSyntax>, SyntaxToken, CSharpSyntaxNode>>(
                        Expression.Call(
                            positionalPatternClauseMethod,
                            openParenTokenParameter,
                            Expression.Convert(
                                Expression.Call(subpatternsParameter, underlyingListProperty.GetMethod),
                                positionalPatternClauseMethod.GetParameters()[1].ParameterType),
                            closeParenTokenParameter),
                        openParenTokenParameter,
                        subpatternsParameter,
                        closeParenTokenParameter);
                PositionalPatternClauseAccessor2 = expression.Compile();
            }
            else
            {
                PositionalPatternClauseAccessor2 = ThrowNotSupportedOnFallback<SyntaxToken, SeparatedSyntaxListWrapper<SubpatternSyntax>, SyntaxToken, TypeSyntax>(nameof(SyntaxFactory), nameof(PositionalPatternClause));
            }

            var propertyPatternClauseMethods = typeof(SyntaxFactory).GetTypeInfo().GetDeclaredMethods(nameof(PropertyPatternClause));
            var propertyPatternClauseMethod = propertyPatternClauseMethods.FirstOrDefault(method => method.GetParameters().Length == 1 && method.GetParameters()[0].ParameterType == typeof(SeparatedSyntaxList<>).MakeGenericType(SyntaxWrapperHelper.GetWrappedType(typeof(SubpatternSyntax))));
            if (propertyPatternClauseMethod is object)
            {
                var subpatternsParameter = Expression.Parameter(typeof(SeparatedSyntaxListWrapper<SubpatternSyntax>), "subpatterns");
                var underlyingListProperty = typeof(SeparatedSyntaxListWrapper<SubpatternSyntax>).GetTypeInfo().GetDeclaredProperty(nameof(SeparatedSyntaxListWrapper<SubpatternSyntax>.UnderlyingList));
                Expression<Func<SeparatedSyntaxListWrapper<SubpatternSyntax>, CSharpSyntaxNode>> expression =
                    Expression.Lambda<Func<SeparatedSyntaxListWrapper<SubpatternSyntax>, CSharpSyntaxNode>>(
                        Expression.Call(
                            propertyPatternClauseMethod,
                            Expression.Convert(
                                Expression.Call(subpatternsParameter, underlyingListProperty.GetMethod),
                                propertyPatternClauseMethod.GetParameters()[0].ParameterType)),
                        subpatternsParameter);
                PropertyPatternClauseAccessor1 = expression.Compile();
            }
            else
            {
                PropertyPatternClauseAccessor1 = ThrowNotSupportedOnFallback<SeparatedSyntaxListWrapper<SubpatternSyntax>, CSharpSyntaxNode>(nameof(SyntaxFactory), nameof(PropertyPatternClause));
            }

            propertyPatternClauseMethod = propertyPatternClauseMethods.FirstOrDefault(method => method.GetParameters().Length == 3
                && method.GetParameters()[0].ParameterType == typeof(SyntaxToken)
                && method.GetParameters()[1].ParameterType == typeof(SeparatedSyntaxList<>).MakeGenericType(SyntaxWrapperHelper.GetWrappedType(typeof(SubpatternSyntax)))
                && method.GetParameters()[2].ParameterType == typeof(SyntaxToken));
            if (propertyPatternClauseMethod is object)
            {
                var openBraceTokenParameter = Expression.Parameter(typeof(SyntaxToken), "openBraceToken");
                var subpatternsParameter = Expression.Parameter(typeof(SeparatedSyntaxListWrapper<SubpatternSyntax>), "subpatterns");
                var closeBraceTokenParameter = Expression.Parameter(typeof(SyntaxToken), "closeBraceToken");

                var underlyingListProperty = typeof(SeparatedSyntaxListWrapper<SubpatternSyntax>).GetTypeInfo().GetDeclaredProperty(nameof(SeparatedSyntaxListWrapper<SubpatternSyntax>.UnderlyingList));

                Expression<Func<SyntaxToken, SeparatedSyntaxListWrapper<SubpatternSyntax>, SyntaxToken, CSharpSyntaxNode>> expression =
                    Expression.Lambda<Func<SyntaxToken, SeparatedSyntaxListWrapper<SubpatternSyntax>, SyntaxToken, CSharpSyntaxNode>>(
                        Expression.Call(
                            propertyPatternClauseMethod,
                            openBraceTokenParameter,
                            Expression.Convert(
                                Expression.Call(subpatternsParameter, underlyingListProperty.GetMethod),
                                propertyPatternClauseMethod.GetParameters()[1].ParameterType),
                            closeBraceTokenParameter),
                        openBraceTokenParameter,
                        subpatternsParameter,
                        closeBraceTokenParameter);
                PropertyPatternClauseAccessor2 = expression.Compile();
            }
            else
            {
                PropertyPatternClauseAccessor2 = ThrowNotSupportedOnFallback<SyntaxToken, SeparatedSyntaxListWrapper<SubpatternSyntax>, SyntaxToken, TypeSyntax>(nameof(SyntaxFactory), nameof(PropertyPatternClause));
            }
        }

        public static PositionalPatternClauseSyntax PositionalPatternClause(SeparatedSyntaxListWrapper<SubpatternSyntax> subpatterns = default)
        {
            return (PositionalPatternClauseSyntax)PositionalPatternClauseAccessor1(subpatterns);
        }

        public static PositionalPatternClauseSyntax PositionalPatternClause(SyntaxToken openParenToken, SeparatedSyntaxListWrapper<SubpatternSyntax> subpatterns, SyntaxToken closeParenToken)
        {
            return (PositionalPatternClauseSyntax)PositionalPatternClauseAccessor2(openParenToken, subpatterns, closeParenToken);
        }

        public static PropertyPatternClauseSyntax PropertyPatternClause(SeparatedSyntaxListWrapper<SubpatternSyntax> subpatterns = default)
        {
            return (PropertyPatternClauseSyntax)PropertyPatternClauseAccessor1(subpatterns);
        }

        public static PropertyPatternClauseSyntax PropertyPatternClause(SyntaxToken openBraceToken, SeparatedSyntaxListWrapper<SubpatternSyntax> subpatterns, SyntaxToken closeBraceToken)
        {
            return (PropertyPatternClauseSyntax)PropertyPatternClauseAccessor2(openBraceToken, subpatterns, closeBraceToken);
        }

        private static Func<T, TResult> ThrowNotSupportedOnFallback<T, TResult>(string typeName, string methodName)
        {
            return _ => throw new NotSupportedException($"{typeName}.{methodName} is not supported in this version");
        }

        private static Func<T1, T2, TResult> ThrowNotSupportedOnFallback<T1, T2, TResult>(string typeName, string methodName)
        {
            return (_, __) => throw new NotSupportedException($"{typeName}.{methodName} is not supported in this version");
        }

        private static Func<T1, T2, T3, TResult> ThrowNotSupportedOnFallback<T1, T2, T3, TResult>(string typeName, string methodName)
        {
            return (arg1, arg2, arg3) => throw new NotSupportedException($"{typeName}.{methodName} is not supported in this version");
        }
    }
}
