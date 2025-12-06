#:property PublishAot=false
string path = args.FirstOrDefault() == "real" ? "real.txt" : "sample.txt";
var lines = File.ReadAllLines(path);

var columns = new List<Column>();
foreach (var line in lines)
{
    var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

    for (int i = 0; i < parts.Length; i++)
    {
        if (parts[i] is "*" or "+")
        {
            string operation = parts[i];
            columns[i].Calculate(operation);
            continue;
        }

        if (columns.Count <= i)
            columns.Add(new Column());
        columns[i].Values.Add(long.Parse(parts[i]));
    }
}

Console.WriteLine($"Result: {columns.Sum(c => c.Result)}");

public class Column
{
    public List<long> Values = new();
    public long Calculate(string operation)
    {
        Result = operation switch
        {
            "*" => Values.Select(v => v).Aggregate((a, b) => a * b),
            "+" => Values.Sum(),
            _ => throw new ArgumentException("Invalid operation"),
        };
        Console.WriteLine($"{string.Join($" {operation} ", Values)} = {Result}");
        return Result;
    }
    public long Result { get; set; }
}