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

int splitHappened = 0;
HashSet<int> currentBeams = [startCol];
for(int row=1; row < lines.Length; row++)
{
    Console.WriteLine();
    var nextBeams = moveLine(currentBeams, row);
    Console.WriteLine($"Line {row}: {string.Join(", ", currentBeams)}");
    Console.WriteLine($"       -> {string.Join(", ", nextBeams)}");
    currentBeams = nextBeams;
};

Console.WriteLine($"There were {splitHappened} splits.");

HashSet<int> moveLine(HashSet<int> currentBeams, int row)
{
    var nextBeams = new HashSet<int>();
    foreach(var beamCol in currentBeams)
    {
        if(splitters.Contains(new Point(row, beamCol)))
        {
            // Splitter found, create two beams
            nextBeams.Add(beamCol - 1); // Left beam
            nextBeams.Add(beamCol + 1); // Right beam
            splitHappened++;
        }
        else
        {
            // No splitter, beam continues straight down
            nextBeams.Add(beamCol);
        }
    }
    return nextBeams;   
}

record Point(int Row, int Col);
