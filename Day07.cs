#:property PublishAot=false
string path = args.FirstOrDefault() == "real" ? "real.txt" : "sample.txt";
var lines = File.ReadAllLines(path);

// Find starting column (where 'S' is)
var startCol = lines[0].IndexOf('S');

// Find all splitter positions
var splitters = lines
    .SelectMany((line, row) => line
        .Select((ch, col) => (ch, row, col))
        .Where(x => x.ch == '^')
        .Select(x => (Row: x.row, Col: x.col)))
    .ToHashSet();

Console.WriteLine($"Start: column {startCol}");
Console.WriteLine($"Splitters: {splitters.Count}");

if(startCol != splitters.First().Col)
{
    Console.WriteLine("Error: Starting column does not match first splitter column.");
    return;
}

// DP: memo[row, col] = number of distinct paths that reach (row, col)
var memo = new Dictionary<(int row, int col), long>();

long CountPaths(int row, int col)
{
    // Base case: reached bottom
    if (row >= lines.Length)
        return 1;
    
    if (memo.ContainsKey((row, col)))
        return memo[(row, col)];
    
    long count;
    if (splitters.Contains((row, col)))
    {
        // Split: count paths from both branches
        count = CountPaths(row + 1, col - 1) + CountPaths(row + 1, col + 1);
    }
    else
    {
        // Continue straight down
        count = CountPaths(row + 1, col);
    }
    
    memo[(row, col)] = count;
    return count;
}

long result = CountPaths(0, startCol);
Console.WriteLine($"Number of timelines: {result}");