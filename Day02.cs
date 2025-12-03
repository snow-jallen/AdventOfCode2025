string path = args.FirstOrDefault() == "real" ? "real.txt" : "sample.txt";
var lines = File.ReadAllLines(path);

List<long> allInvalid = new();
foreach (var range in lines.SelectMany(l => l.Split(',', StringSplitOptions.RemoveEmptyEntries)))
{
    var parts = range.Split('-');
    var min = long.Parse(parts[0]);
    var max = long.Parse(parts[1]);
    for(long n = min; n <= max; n++)
    {
        if (IsInvalid(n))
            allInvalid.Add(n);
    }
}
Console.WriteLine(allInvalid.Sum());

bool IsInvalid(long number)
{
    var numberStr = number.ToString();
    if (numberStr.Length < 2 || numberStr.Length % 2 != 0)
        return false;
    
    var firstHalf = numberStr.Substring(0, numberStr.Length / 2);
    var secondHalf = numberStr.Substring(numberStr.Length / 2);
    return firstHalf.SequenceEqual(secondHalf);
}