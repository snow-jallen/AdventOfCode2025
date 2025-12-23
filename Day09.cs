string path = args.FirstOrDefault() == "real" ? "real.txt" : "sample.txt";
var lines = File.ReadAllLines(path);

var coordinates = lines.Select(line => 
{
    var parts = line.Split(',');
    return (X: long.Parse(parts[0]), Y: long.Parse(parts[1]));
}).ToList();

long maxArea = 0;
for(int x = 0; x < coordinates.Count; x++)
{
    for(int y = x + 1; y < coordinates.Count; y++)
    {
        long area = (long)(Math.Abs(coordinates[x].X - coordinates[y].X)+1) * (long)(Math.Abs(coordinates[x].Y - coordinates[y].Y)+1);
        //Console.WriteLine($"Area between ({coordinates[x].X},{coordinates[x].Y}) and ({coordinates[y].X},{coordinates[y].Y}) is {area}");
        if(area > maxArea)
        {
            maxArea = area;
        }
    }
}
Console.WriteLine($"Maximum area: {maxArea}");