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

long freshIngredientCount = 0;
long lastStart = -1;
List<(long Start, long End)> processedRanges = new ();
foreach(var range in ranges)
{
    if(processedRanges.an)
}

Console.WriteLine($"Total Fresh Ingredients: {freshIngredients.Count}");