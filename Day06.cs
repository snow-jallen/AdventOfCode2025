#:property PublishAot=false
string path = args.FirstOrDefault() == "real" ? "real.txt" : "sample.txt";
var lines = File.ReadAllLines(path);
var lineLen = lines[0].Length;

var columnBreaks = Enumerable.Range(0, lineLen)
    .Where(i => isEntireColumnSpaces(i))
    .ToList();
columnBreaks.Insert(0, -1);
columnBreaks.Add(lineLen);

Console.WriteLine($"Found the following column breaks at indexes: {string.Join(", ", columnBreaks)}");


var columns = columnBreaks.Zip(columnBreaks.Skip(1), (start, end) => 
    lines.Select(line => line[(start+1)..end]).ToList()
).Select(chunk => new Column(chunk)).ToList();

Console.WriteLine($"Parsed {columns.Sum(c=>c.Result)} columns.");

bool isEntireColumnSpaces(int colIndex) => lines.All(line => line[colIndex] == ' ');

public class Column
{
    public Column(List<string> rows)
    {
        Operation = rows.Last()[0];
        Values = Enumerable.Repeat(0L, rows[0].Length).ToList();
        foreach(var row in rows.SkipLast(1))
        {
            foreach(var digit in row.Select((ch, idx) => (ch, idx)))
            {
                if(char.IsDigit(digit.ch))
                {
                    Values[digit.idx] = Values[digit.idx] * 10 + (digit.ch - '0');
                }
            }
        }
        Calculate(Operation.ToString());
    }
    public char Operation{get; private set;}
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

