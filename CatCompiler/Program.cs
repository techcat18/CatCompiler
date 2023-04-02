using CatCompiler;
using CatCompiler.CodeAnalysis.Binding;
using CatCompiler.CodeAnalysis.Syntax;

var showParseTree = false;

while (true)
{
    Console.Write("> ");
    var line = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(line))
    {
        return;
    }

    if (line == "#showTree")
    {
        showParseTree = !showParseTree;
        Console.WriteLine(showParseTree ? "Showing parse trees." : "Not showing parse trees.");
        continue;
    }

    if (line == "#cls")
    {
        Console.Clear();
        continue;
    }

    var syntaxTree = SyntaxTree.Parse(line);
    var binder = new Binder();
    var boundExpression = binder.BindExpression(syntaxTree.Root);

    if (showParseTree)
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        PrettyPrint(syntaxTree.Root);
        Console.ResetColor();
    }

    if (!syntaxTree.Diagnostics.Any())
    {
        var evaluator = new Evaluator(boundExpression);
        var result = evaluator.Evaluate();
        Console.WriteLine(result);
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;

        foreach (var diagnostic in syntaxTree.Diagnostics)
        {
            Console.WriteLine(diagnostic);
        }

        Console.ResetColor();
    }
}

static void PrettyPrint(SyntaxNode node, string indent = "", bool isLast = true)
{
    var marker = isLast ? "└──" : "├──";

    Console.Write(indent);
    Console.Write(marker);
    Console.Write(node.Kind);

    if (node is SyntaxToken token && token.Value != null)
    {
        Console.Write(" ");
        Console.Write(token.Value);
    }

    Console.WriteLine();

    indent += isLast ? "    " : "│   ";

    var lastChild = node.GetChildren().LastOrDefault();
    var children = node.GetChildren();

    foreach (var child in children)
    {
        PrettyPrint(child, indent, child == lastChild);
    }
}