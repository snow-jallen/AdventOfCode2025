string path = args.FirstOrDefault() == "real" ? "real.txt" : "sample.txt";
var lines = File.ReadAllLines(path);
var batteryBanks = new List<BatteryBank>();
var origColor = Console.ForegroundColor;

foreach (var bank in lines)
{
    batteryBanks.Add(new BatteryBank(bank));
    Console.Write($"Processing Bank {batteryBanks.Count}: ");
    batteryBanks.Last().Calculate();
}
Console.ForegroundColor = origColor;
Console.WriteLine($"Total Joltages Processed: {batteryBanks.Sum(b => b.JoltageValue)}");


public class BatteryBank
{
    public BatteryBank(string batteries)
    {
        Batteries = new Dictionary<int, (int, bool)>();
        for (int i = 0; i < batteries.Length; i++)
        {
            Batteries.Add(i, (int.Parse(batteries[i].ToString()), false));
        }
    }

    public BatteryBank Copy()
    {
        var copy = new BatteryBank("");
        foreach (var battery in Batteries)
        {
            copy.Batteries.Add(battery.Key, (battery.Value.Value, battery.Value.TurnedOn));
        }
        return copy;
    }

    public void Calculate()
    {
        TurnOn();

        var origColor = Console.ForegroundColor;
        print();
        Console.ForegroundColor = origColor;
    }

    public int BatteriesTurnedOn => Batteries.Values.Count(b => b.TurnedOn);
    public long JoltageValue
    {
        get
        {
            var numString = string.Join("", Batteries.Values.Where(b => b.TurnedOn).Select(b => b.Value.ToString()));
            if (numString.Length == 0)
            {
                return 0;
            }
            return long.Parse(numString);
        }
    }

    public void TurnOn()
    {
        var positionsToCheck = Batteries.Keys.Where(k => Batteries[k].TurnedOn is false).ToList();
        if (positionsToCheck.Count == 0 || BatteriesTurnedOn >= 12)
        {
            return;
        }
        long newJoltage = 0;
        int indexToTurnOn = -1;
        foreach (var pos in positionsToCheck)
        {
            var alternateBank = this.Copy();
            alternateBank.Batteries[pos] = (alternateBank.Batteries[pos].Value, true);
            if (alternateBank.JoltageValue > newJoltage)
            {
                newJoltage = alternateBank.JoltageValue;
                indexToTurnOn = pos;
            }
        }
        Batteries[indexToTurnOn] = (Batteries[indexToTurnOn].Value, true);
        if (positionsToCheck.Count > 1)
        {
            TurnOn();
        }
    }

    public Dictionary<int, (int Value, bool TurnedOn)> Batteries { get; set; }

    private void print()
    {
        foreach (var battery in Batteries)
        {
            if (battery.Value.TurnedOn)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.Write(battery.Value.Value);
        }
        Console.WriteLine();
    }
}