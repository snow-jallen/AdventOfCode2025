#:property PublishAot=false
string path = args.FirstOrDefault() == "real" ? "real.txt" : "sample.txt";
var lines = File.ReadAllLines(path);
long movableRolls = 0;
int generation = 0;
var newRollsMoved = moveRolls(ref generation);
movableRolls += newRollsMoved;
while(newRollsMoved > 0)
{
    newRollsMoved = moveRolls(ref generation);
    movableRolls += newRollsMoved;
}
Console.WriteLine($"Total Movable Rolls: {movableRolls}");

int moveRolls(ref int generation)
{
    Console.Write($"Generation {generation++}");

    int movableRolls = 0;
    for(int row = 0; row < lines.Length; row++)
    {
        for(int col = 0; col < lines[row].Length; col++)
        {
            var current = lines[row][col];
            if(current == '.')
            {
                continue;
            }
            var neighbors = neighborCount(row, col);
            if(neighbors < 4)
            {
                movableRolls++;
                var newLine = lines[row].Remove(col, 1).Insert(col, ".");
                lines[row] = newLine;
            }
        }
    }
    Console.WriteLine($" Moved Rolls: {movableRolls}");
    return movableRolls;
}

int neighborCount(int row, int col)
{
    int neighbors = 0;
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

char getValue(int row, int col)
{
    if(row < 0 || row >= lines.Length) return '.';
    if(col < 0 || col >= lines[row].Length) return '.';
    return lines[row][col];
}