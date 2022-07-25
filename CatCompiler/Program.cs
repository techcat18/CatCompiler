

using CatCompiler;

bool showParseTree = false;

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
    else if (line == "#cls")
    {
        Console.Clear();
        continue;
    }

    var syntaxTree = SyntaxTree.Parse(line);

    if (showParseTree)
    {
        var color = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.DarkGray;
        PrettyPrint(syntaxTree.Root);
        Console.ForegroundColor = color;
    }

    if (!syntaxTree.Diagnostics.Any())
    {
        var evaluator = new Evaluator(syntaxTree.Root);
        var result = evaluator.Evaluate();
        Console.WriteLine(result);
    }
    else
    {
        var color = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.DarkRed;
        foreach (var diagnostic in syntaxTree.Diagnostics)
        {
            Console.WriteLine(diagnostic);
        }
        Console.ForegroundColor = color;
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