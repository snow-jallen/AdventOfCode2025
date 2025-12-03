using System;
using System.IO;
using System.Linq;

long num = 50;
int zerosHit = 0;
string path = args.FirstOrDefault() == "real" ? "real.txt" : "sample.txt";
var counter = new Counter(50);
foreach (var step in File.ReadAllLines(path))
{
    int direction = (step[0] == 'L') ? -1 : 1;
    int quantity = int.Parse(step.Substring(1));
    counter.Add(quantity * direction);
}
Console.WriteLine(counter.ZerosHit);

public class Counter
{
    int currentValue = 0;
    public int ZerosHit{get;set;} = 0;
    public Counter(int startingValue)
    {
        currentValue = startingValue;
    }

    public void Add(int value)
    {
        var quantity = Math.Abs(value);
        var increment = value >= 0 ? 1 : -1;
        for(int i = 0; i < quantity; i++)
        {
            currentValue += increment;
            if(currentValue < 0)
            {
                currentValue = 99;
            }
            else if(currentValue > 99)
            {
                currentValue = 0;
            }
            if(currentValue == 0)
            {
                Console.WriteLine("Zero hit!");
                ZerosHit++;
            }
        }
    }
}