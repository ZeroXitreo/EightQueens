using EightQueens;

Board board = new(15);
//List<int> list = new();
//for (int i = 0; i < board.Size; i++)
//{
//	list.Add(i);
//}
//List<List<int>> sequences = new();

//Dive(sequences, list, new());

//static void Dive<T>(List<List<T>> combinations, List<T> list, List<T> combination)
//{
//	if (!list.Any())
//	{
//		combinations.Add(combination.ToList());
//		return;
//	}

//	foreach (T item in list)
//	{
//		List<T> strippedList = list.ToList();
//		strippedList.Remove(item);
//		List<T> combinedList = combination.ToList();
//		combinedList.Add(item);
//		Dive(combinations, strippedList, combinedList);
//	}
//}


//int solutions = 0;
//int attempts = 0;
//foreach (List<int> sequence in sequences)
//{
//	attempts++;
//	board.Reset();
//	board.TrySequence(sequence);
//	if (board.Check())
//	{
//		solutions++;
//	}
//}

//Console.WriteLine();
//Console.WriteLine($"Attempts: {attempts}");
//Console.WriteLine($"Solutions: {solutions}");

int attemptsRandom = 0;
bool solutionFound = false;
while (!solutionFound)
{
    attemptsRandom++;
    board.Reset();
    board.Random();
    solutionFound = board.IsValid();
}

Console.WriteLine();
board.Render();
Console.WriteLine();
Console.WriteLine($"Attempts: {attemptsRandom}");

Console.WriteLine();

Console.WriteLine("Dots between the T's is how much you'll have to shift the board in order to get another valid board.");

Console.WriteLine();

List<Tuple<int, int, bool>> map = new();
Tuple<int, int> currentLocation = new(0, 0);
for (int y = 0; y < board.Size; y++)
{
    for (int x = 0; x < board.Size; x++)
    {
        solutionFound = board.IsValid();
        map.Add(new(x, y, solutionFound));

        board.Shift(new(1, 0));
    }
    board.Shift(new(0, 1));
}
int mapSize = board.Size * 4;
for (int y = 0; y < mapSize; y++)
{
    for (int x = 0; x < mapSize; x++)
    {
        Tuple<int, int, bool>? mapPosition = map.FirstOrDefault(o => o.Item1 == (currentLocation.Item1 + x + board.Size * mapSize) % board.Size && o.Item2 == (currentLocation.Item2 + y + board.Size * mapSize) % board.Size);
        Console.Write(mapPosition is null ? "  " : mapPosition.Item3 ? " T" : " .");
    }
    Console.WriteLine();
}
