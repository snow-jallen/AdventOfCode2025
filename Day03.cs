string path = args.FirstOrDefault() == "real" ? "real.txt" : "sample.txt";
var lines = File.ReadAllLines(path);
var joltages = new List<Joltage>();
foreach (var bank in lines)
{
    joltages.Add(new Joltage(bank));
}
Console.WriteLine($"Total Joltages Processed: {joltages.Sum(j => j.JoltageValue)}");

public class Joltage
{
    public int Offset1 { get; set; }
    public int Offset2 { get; set; }
    public int JoltageValue { get; set; }
    public Joltage(string bank)
    {

        int maxJoltage = 0;
        for (int i = 0; i < bank.Length - 1; i++)
        {
            for (int j = i + 1; j < bank.Length; j++)
            {
                int joltage = int.Parse(bank[i].ToString()) * 10 + int.Parse(bank[j].ToString());
                if (joltage > maxJoltage)
                {
                    maxJoltage = joltage;
                    Offset1 = i;
                    Offset2 = j;
                    JoltageValue = joltage;
                }
            }
        }

        Console.WriteLine($"Max Joltage: {JoltageValue} (Offsets: {Offset1}, {Offset2})");
    }
}