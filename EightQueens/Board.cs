namespace EightQueens;

internal class Board
{
    internal Node[,] Nodes;
    internal int Size;
    private readonly List<Coordinate> queens = [];

    internal Board(int size)
    {
        Size = size;
        Nodes = new Node[Size, Size];

        for (int i = 0; i < Nodes.GetLength(0); i++)
        {
            for (int j = 0; j < Nodes.GetLength(1); j++)
            {
                Nodes[i, j] = new();
            }
        }
    }

    internal void Reset()
    {
        queens.Clear();

        for (int i = 0; i < Nodes.GetLength(0); i++)
        {
            for (int j = 0; j < Nodes.GetLength(1); j++)
            {
                Nodes[i, j].HasQueen = false;
            }
        }
    }

    internal void Random()
    {
        List<int> ySpots = [];
        for (int i = 0; i < Size; i++)
        {
            ySpots.Add(i);
        }

        Random rnd = new();
        for (int i = 0; i < Size; i++)
        {
            int x = i;
            int yIndex = rnd.Next(Size - i);
            int y = ySpots[yIndex];

            Node node = Nodes[x, y];
            queens.Add(new(x, y));
            node.HasQueen = true;
            ySpots.RemoveAt(yIndex);
        }
    }

    internal void TrySequence(List<int> sequence)
    {
        for (int i = 0; i < Size; i++)
        {
            int x = i;
            int y = sequence[x];

            Node node = Nodes[x, y];
            if (!node.HasQueen)
            {
                queens.Add(new(x, y));
                node.HasQueen = true;
            }
        }
    }

    internal bool IsValid()
    {
        //// Check if any queens overlaps
        //if (queens.GroupBy(o => o.x).Any(g => g.Count() > 1)) return false;
        //if (queens.GroupBy(o => o.y).Any(g => g.Count() > 1)) return false;

        foreach (var queen in queens)
        {
            if (!IsValidQueen(queen))
            {
                return false;
            }
        }
        return true;
    }

    internal bool IsValidQueen(Coordinate queenCoordinate)
    {
        if (!CheckSingleLine(queenCoordinate, new(1, 1))) return false;
        if (!CheckSingleLine(queenCoordinate, new(1, -1))) return false;

        return true;
    }

    internal bool CheckSingleLine(Coordinate queenCoordinate, Vector2 direction)
    {
        var pointMove = queenCoordinate;
        while (true)
        {
            pointMove = new(pointMove.x + direction.x, pointMove.y + direction.y);

            if (pointMove.x < 0 || pointMove.y < 0 || pointMove.x >= Size || pointMove.y >= Size) return true;

            Node? node = Nodes[pointMove.x, pointMove.y];

            if (node.HasQueen)
            {
                return false;
            }
        }
    }

    internal void Render()
    {
        char character = 'A';
        Console.Write($"    ");
        for (int i = 0; i < Size; i++)
        {
            Console.Write($"{character++} ");
        }
        Console.WriteLine();
        for (int y = 0; y < Nodes.GetLength(0); y++)
        {
            Console.Write($" {Size - y,2} ");
            for (int x = 0; x < Nodes.GetLength(1); x++)
            {
                SwitchColor((x + y) % 2 == 0);
                if (Nodes[x, y].HasQueen)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                }
                Console.Write("  ");
            }
            Console.ResetColor();
            Console.WriteLine($" {Size - y,-2} ");
        }
        character = 'A';
        Console.Write($"    ");
        for (int i = 0; i < Size; i++)
        {
            Console.Write($"{character++} ");
        }
        Console.WriteLine();
        Console.ResetColor();
    }

    private static void SwitchColor(bool color)
    {
        Console.BackgroundColor = color ? ConsoleColor.White : ConsoleColor.Black;
        Console.ForegroundColor = !color ? ConsoleColor.White : ConsoleColor.Black;
    }

    internal void Shift(Tuple<int, int> direction)
    {
        var queens = this.queens.ToList();
        Reset();

        foreach (var queen in queens)
        {
            int x = (queen.x + direction.Item1 + Size) % Size;
            int y = (queen.y + direction.Item2 + Size) % Size;
            this.queens.Add(new(x, y));
        }

        foreach (var queen in this.queens)
        {
            Nodes[queen.x, queen.y].HasQueen = true;
        }
    }
}
