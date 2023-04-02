using CatCompiler.CodeAnalysis.Binding;
using CatCompiler.CodeAnalysis.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatCompiler
{
    internal sealed class Evaluator
    {
        private readonly BoundExpression _root;

        public Evaluator(BoundExpression root)
        {
            _root = root;
        }

        public int Evaluate()
        {
            return EvaluateExpression(_root);
        }

        public int EvaluateExpression(BoundExpression node)
        {
            if (node is BoundLiteralExpression literalExpression)
            {
                return (int) literalExpression.Value;
            }

            if (node is BoundUnaryExpression unaryExpression)
            {
                var operand = EvaluateExpression(unaryExpression.Operand);

                switch (unaryExpression.OperatorKind)
                {
                    case BoundUnaryOperatorKind.Identity:
                        return operand;

                    case BoundUnaryOperatorKind.Negation:
                        return -operand;

                    default:
                        throw new Exception($"Unexpected unary operator: {unaryExpression.OperatorKind}");
                }
            }

            if (node is BoundBinaryExpression binaryExpression)
            {
                var left = EvaluateExpression(binaryExpression.Left);
                var right = EvaluateExpression(binaryExpression.Right);

                switch (binaryExpression.OperatorKind)
                {
                    case BoundBinaryOperatorKind.Addition:
                        return left + right;

                    case BoundBinaryOperatorKind.Subtraction:
                        return left - right;

                    case BoundBinaryOperatorKind.Multiplication:
                        return left * right;

                    case BoundBinaryOperatorKind.Division:
                        return left / right;

                    default:
                        throw new Exception($"Unexpected binary operator {binaryExpression.OperatorKind}");
                }
            }

            throw new Exception($"Unexpected node {node.Kind}");
        }
    }
}
