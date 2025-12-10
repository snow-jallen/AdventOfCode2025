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

var solutions = new HashSet<string>() { splitters.First().Col.ToString() };
solve(solutions, 2);
Console.WriteLine($"Number of solutions: {solutions.Count}");
// Console.WriteLine("Solutions:");
// foreach(var solution in solutions)
// {
//     Console.WriteLine(string.Join(" -> ", solution.Select(p => $"({p.Row},{p.Col})")));
// }

void solve(HashSet<string> solutions, int row)
{
    Console.WriteLine($"Solving row {row}, current number of solutions: {solutions.Count}");
    if(row >= lines.Length)
    {
        return;
    }

    var thisRowSplitters = splitters.Where(s => s.Row == row).ToHashSet();
    
    foreach(var path in solutions.ToList())
    {
        var currentCol = int.Parse(path.Split(',').Last());
        
        solutions.Remove(path);

        if(thisRowSplitters.Any(s => s.Col == currentCol))
        {

            // Split into two paths
            var leftPath = path+ $", {currentCol - 1}";
            solutions.Add(leftPath);
            
            var rightPath = path+ $", {currentCol + 1}";
            solutions.Add(rightPath);
        }
        else
        {
            // Continue straight down
            var continuedPath = path + $", {currentCol}";
            solutions.Add(continuedPath);
        }
    }
    
    
    solve(solutions, row + 1);  // Go to NEXT row, not row + 2
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
