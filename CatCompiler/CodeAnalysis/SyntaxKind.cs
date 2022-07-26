using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatCompiler
{
    public enum SyntaxKind
    {
        // Tokens
        BadToken,
        EnfOfFileToken,
        WhiteSpaceToken,
        NumberToken,
        PlusToken,
        MinusToken,
        StarToken,
        SlashToken,
        OpenParenthesisToken,
        CloseParenthesisToken,

        // Experessions
        NumberExpression,
        BinaryExpression,
        ParenthesizedExpression
    }
}
