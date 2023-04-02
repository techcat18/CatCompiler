namespace CatCompiler.CodeAnalysis.Binding
{
    internal sealed class BoundBinaryExpression: BoundExpression
    {
        public BoundBinaryExpression(BoundExpression left, BoundBinaryOperatorKind operatorKind, BoundExpression right)
        {
            Left = left;
            OperatorKind = operatorKind;
            Right = right;
        }

        public BoundExpression Left { get; }
        public BoundBinaryOperatorKind OperatorKind { get; set; }
        public BoundExpression Right { get; set; }
        public override Type Type => Left.Type;
        public override BoundNodeKind Kind => BoundNodeKind.BinaryExpression;
    }
}
