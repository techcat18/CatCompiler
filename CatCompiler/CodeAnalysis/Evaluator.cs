using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatCompiler
{
    public sealed class Evaluator
    {
        private readonly ExpressionSyntax _root;

        public Evaluator(ExpressionSyntax root)
        {
            _root = root;
        }

        public int Evaluate()
        {
            return EvaluateExpression(_root);
        }

        public int EvaluateExpression(ExpressionSyntax node)
        {
            if (node is LiteralExpressionSyntax numberExpression)
            {
                return (int) numberExpression.NumberToken.Value;
            }

            if (node is BinaryExpressionSyntax binaryExpression)
            {
                var left = EvaluateExpression(binaryExpression.Left);
                var right = EvaluateExpression(binaryExpression.Right);

                if (binaryExpression.OperatorToken.Kind == SyntaxKind.PlusToken)
                {
                    return left + right;
                }
                else if (binaryExpression.OperatorToken.Kind == SyntaxKind.MinusToken)
                {
                    return left - right;
                }
                else if (binaryExpression.OperatorToken.Kind == SyntaxKind.StarToken)
                {
                    return left * right;
                }
                else if (binaryExpression.OperatorToken.Kind == SyntaxKind.SlashToken)
                {
                    return left / right;
                }
                else
                {
                    throw new Exception($"Unexpected binary operator {binaryExpression.OperatorToken.Kind}");
                }
            }

            if (node is ParenthesizedExpressionSyntax parenthesizedExpression)
            {
                return EvaluateExpression(parenthesizedExpression.Expression);
            }

            throw new Exception($"Unexpected node {node.Kind}");
        }
    }
}
