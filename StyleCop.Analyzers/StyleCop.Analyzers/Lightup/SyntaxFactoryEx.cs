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
        private static readonly Func<TypeSyntax, CSharpSyntaxNode> TupleElementAccessor1;
        private static readonly Func<TypeSyntax, SyntaxToken, CSharpSyntaxNode> TupleElementAccessor2;
        private static readonly Func<SeparatedSyntaxList<ArgumentSyntax>, ExpressionSyntax> TupleExpressionAccessor1;
        private static readonly Func<SyntaxToken, SeparatedSyntaxList<ArgumentSyntax>, SyntaxToken, ExpressionSyntax> TupleExpressionAccessor2;
        private static readonly Func<SeparatedSyntaxListWrapper<TupleElementSyntax>, TypeSyntax> TupleTypeAccessor1;
        private static readonly Func<SyntaxToken, SeparatedSyntaxListWrapper<TupleElementSyntax>, SyntaxToken, TypeSyntax> TupleTypeAccessor2;

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

            var tupleElementMethods = typeof(SyntaxFactory).GetTypeInfo().GetDeclaredMethods(nameof(TupleElement));
            var tupleElementMethod = tupleElementMethods.FirstOrDefault(method => method.GetParameters().Length == 1 && method.GetParameters()[0].ParameterType == typeof(TypeSyntax));
            if (tupleElementMethod is object)
            {
                var typeParameter = Expression.Parameter(typeof(TypeSyntax), "type");
                Expression<Func<TypeSyntax, CSharpSyntaxNode>> expression =
                    Expression.Lambda<Func<TypeSyntax, CSharpSyntaxNode>>(
                        Expression.Call(tupleElementMethod, typeParameter),
                        typeParameter);
                TupleElementAccessor1 = expression.Compile();
            }
            else
            {
                TupleElementAccessor1 = ThrowNotSupportedOnFallback<TypeSyntax, CSharpSyntaxNode>(nameof(SyntaxFactory), nameof(TupleElement));
            }

            tupleElementMethod = tupleElementMethods.FirstOrDefault(method => method.GetParameters().Length == 2 && method.GetParameters()[0].ParameterType == typeof(TypeSyntax) && method.GetParameters()[1].ParameterType == typeof(SyntaxToken));
            if (tupleElementMethod is object)
            {
                var typeParameter = Expression.Parameter(typeof(TypeSyntax), "type");
                var identifierParameter = Expression.Parameter(typeof(SyntaxToken), "identifier");
                Expression<Func<TypeSyntax, SyntaxToken, CSharpSyntaxNode>> expression =
                    Expression.Lambda<Func<TypeSyntax, SyntaxToken, CSharpSyntaxNode>>(
                        Expression.Call(tupleElementMethod, typeParameter, identifierParameter),
                        typeParameter,
                        identifierParameter);
                TupleElementAccessor2 = expression.Compile();
            }
            else
            {
                TupleElementAccessor2 = ThrowNotSupportedOnFallback<TypeSyntax, SyntaxToken, CSharpSyntaxNode>(nameof(SyntaxFactory), nameof(TupleElement));
            }

            var tupleExpressionMethods = typeof(SyntaxFactory).GetTypeInfo().GetDeclaredMethods(nameof(TupleExpression));
            var tupleExpressionMethod = tupleExpressionMethods.FirstOrDefault(method => method.GetParameters().Length == 1 && method.GetParameters()[0].ParameterType == typeof(SeparatedSyntaxList<ArgumentSyntax>));
            if (tupleExpressionMethod is object)
            {
                var argumentsParameter = Expression.Parameter(typeof(SeparatedSyntaxList<ArgumentSyntax>), "arguments");
                Expression<Func<SeparatedSyntaxList<ArgumentSyntax>, ExpressionSyntax>> expression =
                    Expression.Lambda<Func<SeparatedSyntaxList<ArgumentSyntax>, ExpressionSyntax>>(
                        Expression.Call(tupleExpressionMethod, argumentsParameter),
                        argumentsParameter);
                TupleExpressionAccessor1 = expression.Compile();
            }
            else
            {
                TupleExpressionAccessor1 = ThrowNotSupportedOnFallback<SeparatedSyntaxList<ArgumentSyntax>, ExpressionSyntax>(nameof(SyntaxFactory), nameof(TupleExpression));
            }

            tupleExpressionMethod = tupleExpressionMethods.FirstOrDefault(method => method.GetParameters().Length == 3
                && method.GetParameters()[0].ParameterType == typeof(SyntaxToken)
                && method.GetParameters()[1].ParameterType == typeof(SeparatedSyntaxList<ArgumentSyntax>)
                && method.GetParameters()[2].ParameterType == typeof(SyntaxToken));
            if (tupleExpressionMethod is object)
            {
                var openParenTokenParameter = Expression.Parameter(typeof(SyntaxToken), "openParenToken");
                var argumentsParameter = Expression.Parameter(typeof(SeparatedSyntaxList<ArgumentSyntax>), "arguments");
                var closeParenTokenParameter = Expression.Parameter(typeof(SyntaxToken), "closeParenToken");
                Expression<Func<SyntaxToken, SeparatedSyntaxList<ArgumentSyntax>, SyntaxToken, ExpressionSyntax>> expression =
                    Expression.Lambda<Func<SyntaxToken, SeparatedSyntaxList<ArgumentSyntax>, SyntaxToken, ExpressionSyntax>>(
                        Expression.Call(tupleExpressionMethod, openParenTokenParameter, argumentsParameter, closeParenTokenParameter),
                        openParenTokenParameter,
                        argumentsParameter,
                        closeParenTokenParameter);
                TupleExpressionAccessor2 = expression.Compile();
            }
            else
            {
                TupleExpressionAccessor2 = ThrowNotSupportedOnFallback<SyntaxToken, SeparatedSyntaxList<ArgumentSyntax>, SyntaxToken, ExpressionSyntax>(nameof(SyntaxFactory), nameof(TupleExpression));
            }

            var tupleTypeMethods = typeof(SyntaxFactory).GetTypeInfo().GetDeclaredMethods(nameof(TupleType));
            var tupleTypeMethod = tupleTypeMethods.FirstOrDefault(method => method.GetParameters().Length == 1 && method.GetParameters()[0].ParameterType == typeof(SeparatedSyntaxList<>).MakeGenericType(SyntaxWrapperHelper.GetWrappedType(typeof(TupleElementSyntax))));
            if (tupleTypeMethod is object)
            {
                var elementsParameter = Expression.Parameter(typeof(SeparatedSyntaxListWrapper<TupleElementSyntax>), "elements");
                var underlyingListProperty = typeof(SeparatedSyntaxListWrapper<TupleElementSyntax>).GetTypeInfo().GetDeclaredProperty(nameof(SeparatedSyntaxListWrapper<TupleElementSyntax>.UnderlyingList));
                Expression<Func<SeparatedSyntaxListWrapper<TupleElementSyntax>, TypeSyntax>> expression =
                    Expression.Lambda<Func<SeparatedSyntaxListWrapper<TupleElementSyntax>, TypeSyntax>>(
                        Expression.Call(
                            tupleTypeMethod,
                            Expression.Convert(
                                Expression.Call(elementsParameter, underlyingListProperty.GetMethod),
                                tupleTypeMethod.GetParameters()[0].ParameterType)),
                        elementsParameter);
                TupleTypeAccessor1 = expression.Compile();
            }
            else
            {
                TupleTypeAccessor1 = ThrowNotSupportedOnFallback<SeparatedSyntaxListWrapper<TupleElementSyntax>, TypeSyntax>(nameof(SyntaxFactory), nameof(TupleType));
            }

            tupleTypeMethod = tupleTypeMethods.FirstOrDefault(method => method.GetParameters().Length == 3
                && method.GetParameters()[0].ParameterType == typeof(SyntaxToken)
                && method.GetParameters()[1].ParameterType == typeof(SeparatedSyntaxList<>).MakeGenericType(SyntaxWrapperHelper.GetWrappedType(typeof(TupleElementSyntax)))
                && method.GetParameters()[2].ParameterType == typeof(SyntaxToken));
            if (tupleTypeMethod is object)
            {
                var openParenTokenParameter = Expression.Parameter(typeof(SyntaxToken), "openParenToken");
                var elementsParameter = Expression.Parameter(typeof(SeparatedSyntaxListWrapper<TupleElementSyntax>), "elements");
                var closeParenTokenParameter = Expression.Parameter(typeof(SyntaxToken), "closeParenToken");

                var underlyingListProperty = typeof(SeparatedSyntaxListWrapper<TupleElementSyntax>).GetTypeInfo().GetDeclaredProperty(nameof(SeparatedSyntaxListWrapper<TupleElementSyntax>.UnderlyingList));

                Expression<Func<SyntaxToken, SeparatedSyntaxListWrapper<TupleElementSyntax>, SyntaxToken, TypeSyntax>> expression =
                    Expression.Lambda<Func<SyntaxToken, SeparatedSyntaxListWrapper<TupleElementSyntax>, SyntaxToken, TypeSyntax>>(
                        Expression.Call(
                            tupleTypeMethod,
                            openParenTokenParameter,
                            Expression.Convert(
                                Expression.Call(elementsParameter, underlyingListProperty.GetMethod),
                                tupleTypeMethod.GetParameters()[1].ParameterType),
                            closeParenTokenParameter),
                        openParenTokenParameter,
                        elementsParameter,
                        closeParenTokenParameter);
                TupleTypeAccessor2 = expression.Compile();
            }
            else
            {
                TupleTypeAccessor2 = ThrowNotSupportedOnFallback<SyntaxToken, SeparatedSyntaxListWrapper<TupleElementSyntax>, SyntaxToken, TypeSyntax>(nameof(SyntaxFactory), nameof(TupleType));
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

        public static TupleElementSyntax TupleElement(TypeSyntax type)
        {
            return (TupleElementSyntax)TupleElementAccessor1(type);
        }

        public static TupleElementSyntax TupleElement(TypeSyntax type, SyntaxToken identifier)
        {
            return (TupleElementSyntax)TupleElementAccessor2(type, identifier);
        }

        public static TupleExpressionSyntax TupleExpression(SeparatedSyntaxList<ArgumentSyntax> arguments = default)
        {
            return (TupleExpressionSyntax)TupleExpressionAccessor1(arguments);
        }

        public static TupleExpressionSyntax TupleExpression(SyntaxToken openParenToken, SeparatedSyntaxList<ArgumentSyntax> arguments, SyntaxToken closeParenToken)
        {
            return (TupleExpressionSyntax)TupleExpressionAccessor2(openParenToken, arguments, closeParenToken);
        }

        public static TupleTypeSyntax TupleType(SeparatedSyntaxListWrapper<TupleElementSyntax> elements = default)
        {
            return (TupleTypeSyntax)TupleTypeAccessor1(elements);
        }

        public static TupleTypeSyntax TupleType(SyntaxToken openParenToken, SeparatedSyntaxListWrapper<TupleElementSyntax> elements, SyntaxToken closeParenToken)
        {
            return (TupleTypeSyntax)TupleTypeAccessor2(openParenToken, elements, closeParenToken);
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
