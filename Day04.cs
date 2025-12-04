string path = args.FirstOrDefault() == "real" ? "real.txt" : "sample.txt";
var lines = File.ReadAllLines(path);
long movableRolls = 0;
for(long row = 0; row < lines.Length; row++)
{
    for(long col = 0; col < lines[row].Length; col++)
    {
        var current = lines[(int)row][(int)col];
        if(current == '.')
        {
            continue;
        }
        var neighbors = neighborCount(row, col);
        if(neighbors < 4)
        {
            movableRolls++;
        }
    }
}
Console.WriteLine($"Total Movable Rolls: {movableRolls}");

long neighborCount(long row, long col)
{
    long neighbors = 0;
    if(getValue(row, col-1) == '@') neighbors++;
    if(getValue(row, col+1) == '@') neighbors++;
    if(getValue(row-1, col-1) == '@') neighbors++;
    if(getValue(row-1, col) == '@') neighbors++;
    if(getValue(row-1, col+1) == '@') neighbors++;
    if(getValue(row+1, col-1) == '@') neighbors++;
    if(getValue(row+1, col) == '@') neighbors++;
    if(getValue(row+1, col+1) == '@') neighbors++;
    return neighbors;
}

char getValue(long row, long col)
{
    if(row < 0 || row >= lines.Length) return '.';
    if(col < 0 || col >= lines[row].Length) return '.';
    return lines[(int)row][(int)col];
}