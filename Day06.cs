#:property PublishAot=false
string path = args.FirstOrDefault() == "real" ? "real.txt" : "sample.txt";
var lines = File.ReadAllLines(path);
var lineLen = lines[0].Length;
var columns = new List<Column>();
var operations = lines.Last().Split(' ', StringSplitOptions.RemoveEmptyEntries);
foreach(var op in operations)
{
    columns.Add(new Column());
}

foreach (var line in lines.Take(lines.Length - 1))
{
    var columnIndex = 0;
    var newValue = 0L;
    for (int i = 0; i < lineLen; i++)
    {
        if (line[i] == ' ')
        {
            columns[columnIndex].Values.Add(newValue);
            newValue = 0;
            columnIndex++;
        }

        int digit = line[i] - '0';
        newValue *= 10;
        newValue += digit;
    }

}

for (int i = 0; i < columns.Count; i++)
{
    columns[i].Calculate(operations[i]);
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