namespace EightQueens;

internal class Board
{
    internal Node[,] Nodes;
    internal int Size;
    private List<Tuple<int, int>> queens;

    internal Board(int size)
    {
        Size = size;
        Nodes = new Node[Size, Size];
        queens = new();

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
                Nodes[i, j].Intersect = false;
            }
        }
    }

    internal void Random()
    {
        List<int> ySpots = new();
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
            if (!node.HasQueen)
            {
                queens.Add(new(x, y));
                node.HasQueen = true;
                ySpots.RemoveAt(yIndex);
            }
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
        foreach (Tuple<int, int> queen in queens)
        {
            if (!IsValidQueen(queen))
            {
                return false;
            }
        }
        return true;
    }

    internal bool IsValidQueen(Tuple<int, int> tuple)
    {
        if (!CheckSingleLine(tuple, new(1, 1))) return false;
        if (!CheckSingleLine(tuple, new(1, -1))) return false;

        return true;
    }

    internal bool CheckSingleLine(Tuple<int, int> point, Tuple<int, int> direction)
    {
        Tuple<int, int> pointMove = point;
        while (true)
        {
            pointMove = new(pointMove.Item1 + direction.Item1, pointMove.Item2 + direction.Item2);

            if (pointMove.Item1 < 0 || pointMove.Item2 < 0 || pointMove.Item1 >= Size || pointMove.Item2 >= Size)
            {
                return true;
            }

            Node? node = Nodes[pointMove.Item1, pointMove.Item2];

            if (node.HasQueen)
            {
                node.Intersect = true;
                Nodes[point.Item1, point.Item2].Intersect = true;
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
        List<Tuple<int, int>> queens = this.queens.ToList();
        Reset();

        foreach (Tuple<int, int> queen in queens)
        {
            int x = (queen.Item1 + direction.Item1 + Size) % Size;
            int y = (queen.Item2 + direction.Item2 + Size) % Size;
            this.queens.Add(new(x, y));
        }

        foreach (Tuple<int, int> queen in this.queens)
        {
            Nodes[queen.Item1, queen.Item2].HasQueen = true;
        }
    }
}
