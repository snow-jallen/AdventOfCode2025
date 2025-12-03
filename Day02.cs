string path = args.FirstOrDefault() == "real" ? "real.txt" : "sample.txt";
var lines = File.ReadAllLines(path);
foreach (var range in string.Join("\r\n", lines).Split(',', StringSplitOptions.RemoveEmptyEntries))
{
    Console.WriteLine(range);
}