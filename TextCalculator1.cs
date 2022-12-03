using System.Globalization;

namespace TextCalculator1;

public class InvalidOperationException : Exception
{

    public InvalidOperationException(string msg) : base(msg)
    {

    }


}

public class NegativeInputException : Exception
{

    public NegativeInputException(string msg) : base(msg)
    {

    }


}

public static class TextCalculator1
{
    public static string Add(string number)
    {
        var negatives = new List<string>();
        if (number == null || number == "")
        {
            return "0";
        }

        if (number.EndsWith(","))
        {
            throw new InvalidOperationException("Missing number in last position.");
        }

        var pom = number.Split(",");
        float sum = 0;
        foreach (var element in pom)
        {
            bool success = float.TryParse(element, CultureInfo.InvariantCulture.NumberFormat, out var temp);
            if (!success) throw new InvalidOperationException($"Found non number: {element}");
            if (temp < 0)
            {
                negatives.Add(element);
            }
            else
            {
                sum += temp;
            }
        }
        if (negatives.Any())
        {
            throw new NegativeInputException($"Negative not allowed : {string.Join(", ", negatives)}");
        }
        return sum.ToString();
    }

    public static void Main(string[] args)
    {
        try
        {
            string userInput = Console.ReadLine();
            Console.WriteLine(Add(userInput));
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (NegativeInputException e)
        {
            Console.WriteLine(e.Message);
        }

    }
}
