namespace CatCompiler.CodeAnalysis.Binding
{
    internal sealed class BoundUnaryExpression: BoundExpression
    {
        public BoundUnaryExpression(BoundUnaryOperatorKind operatorKind, BoundExpression operand)
        {
            OperatorKind = operatorKind;
            Operand = operand;
        }

        public BoundUnaryOperatorKind OperatorKind { get; private set; }
        public BoundExpression Operand { get; private set; }
        public override Type Type => Operand.Type;
        public override BoundNodeKind Kind => BoundNodeKind.UnaryExpression;
    }
}
