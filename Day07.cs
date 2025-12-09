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
        .Select(x => new Point(x.row, x.col)))
    .ToHashSet();

Console.WriteLine($"Start: column {startCol}");
Console.WriteLine($"Splitters: {splitters.Count}");

if(startCol != splitters.First().Col)
{
    Console.WriteLine("Error: Starting column does not match first splitter column.");
    return;
}

var solutions = new HashSet<List<Point>>() { new List<Point> { new Point(2, splitters.First().Col) } };
solve(solutions, 2);
Console.WriteLine($"Number of solutions: {solutions.Count}");

void solve(HashSet<List<Point>> solutions, int row)
{
    if(row >= lines.Length)
    {
        return;
    }

    var thisRowSplitters = splitters.Where(s => s.Row == row).ToList();
    foreach(var splitter in thisRowSplitters)
    {
        var pathsEnding = solutions.Where(path => path.Last().Row == row).ToList();
        foreach(var path in pathsEnding)
        {
            var copy = path.ToList();
            copy.Add(new Point(row+2, splitter.Col-1));
            solutions.Add(copy);
            path.Add(new Point(row+2, splitter.Col+1));
        }
    }

    solve(solutions, row + 2);
}




public record Point(int Row, int Col);

public class Splitter
{
    public Splitter(Point point)
    {
        Row = point.Row;
        Col = point.Col;
    }

    public int Row { get; }
    public int Col { get; }
    public IEnumerable<Splitter> Children => Enumerable.Empty<Splitter>();
    public override bool Equals(object? obj) => obj is Splitter other && Row == other.Row && Col == other.Col && Children == other.Children;
    public override int GetHashCode() => HashCode.Combine(Row, Col, Children);
}
