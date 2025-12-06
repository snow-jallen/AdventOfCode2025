#:property PublishAot=false
string path = args.FirstOrDefault() == "real" ? "real.txt" : "sample.txt";
//var lines = File.ReadAllLines(path);
var lines = File.ReadAllLines(path).Where(l => string.IsNullOrWhiteSpace(l) is false).ToList();
var ranges = from line in lines
             where line.Contains("-")
             let parts = line.Split('-')
             select (Start: long.Parse(parts[0]), End: long.Parse(parts[1]));

var spoiled = from line in lines
              where line.Contains("-") is false
              let ingredientId = long.Parse(line)
              where ranges.Any(r => ingredientId >= r.Start && ingredientId <= r.End)
              select ingredientId;

Console.WriteLine($"Total Spoiled Ingredients: {spoiled.Count()}");

List<(long Start, long End)> processedRanges = new();
foreach (var range in ranges)
{
    //early range
    var firstProcessedRange = processedRanges.OrderBy(p => p.Start).FirstOrDefault();
    if(firstProcessedRange == default || range.End < firstProcessedRange.Start)
    {
        processedRanges.Add(range);
        continue;
    }

    //early overlap
    var earlyOverlap = processedRanges.FirstOrDefault(p => range.End >= p.Start && range.End <= p.End);
    if(earlyOverlap != default)
    {
        if(range.Start < earlyOverlap.Start)
        {
            processedRanges.Remove(earlyOverlap);
            processedRanges.Add((range.Start, earlyOverlap.End));
            continue;
        }
    }

    // Merge overlapping ranges
    var overlapping = processedRanges.FirstOrDefault(p => range.Start >= p.Start && range.Start <= p.End);
    if (overlapping != default)
    {
        if (range.End > overlapping.End)
        {
            processedRanges.Remove(overlapping);
            processedRanges.Add((overlapping.Start, range.End));
            continue;
        }
    }
    else
    {
        processedRanges.Add(range);
        continue;
    }

    Console.WriteLine($"Unhandled range case: {range}");
}

var sum = processedRanges.Sum(r => r.End - r.Start + 1);
Console.WriteLine($"Total Fresh Ingredients: {sum}");