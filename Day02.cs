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
        if (IsReallyInvalid(n))
            allInvalid.Add(n);
    }
}
Console.WriteLine(allInvalid.Sum());

bool IsInvalid(long number)
{
    var numberStr = number.ToString();
    if (numberStr.Length % 2 != 0)
        return false;
    
    var firstHalf = numberStr.Substring(0, numberStr.Length / 2);
    var secondHalf = numberStr.Substring(numberStr.Length / 2);
    return firstHalf.SequenceEqual(secondHalf);
}

bool IsReallyInvalid(long number)
{
    var numberStr = number.ToString();
    
    for(int i = 1; i <= numberStr.Length / 2; i++)
    {
        var part = numberStr.Substring(0, i);
        var repeated = string.Concat(Enumerable.Repeat(part, numberStr.Length / part.Length));
        if (repeated == numberStr)
        {
            Console.WriteLine($"Found really invalid number: {number}");
            return true;
        }
    }

    return false;
}