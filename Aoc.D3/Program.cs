Console.WriteLine("Aoc D3");

var binaryNumbers = await File.ReadAllLinesAsync("input.txt");

string gammaRate = "";
string epsilonRate = "";
for (int i = 0; i < binaryNumbers[0].Length; i++)
{
    var (zeros, ones) = CountDigits(binaryNumbers, i);

    epsilonRate += ones >= zeros ? '1' : '0';
    gammaRate += zeros <= ones ? '0' : '1';
}

Console.WriteLine($"Part 1: Result is {Convert.ToInt32(epsilonRate, 2) * Convert.ToInt32(gammaRate, 2)}.");

var remainingNumbers = binaryNumbers.ToList();
int position = 0;
while (remainingNumbers.Count > 1)
{
    var (zeros, ones) = CountDigits(remainingNumbers, position);
    var digit = ones >= zeros ? '1' : '0';
    remainingNumbers = remainingNumbers.Where(number => number[position] == digit).ToList();
    position++;
}
var oxygenRating = remainingNumbers.Single();

remainingNumbers = binaryNumbers.ToList();
position = 0;
while (remainingNumbers.Count > 1)
{
    var (zeros, ones) = CountDigits(remainingNumbers, position);
    var digit = zeros <= ones ? '0' : '1';
    remainingNumbers = remainingNumbers.Where(number => number[position] == digit).ToList();
    position++;
}
var co2rating = remainingNumbers.Single();

Console.WriteLine($"Part 2: Result is {Convert.ToInt32(oxygenRating, 2) * Convert.ToInt32(co2rating, 2)}.");


(int Zeros, int Ones) CountDigits(IList<string> numbers, int position)
{
    var digitsInPosition = numbers.Select(number => number[position]);

    var zeros = digitsInPosition.Where(digit => digit == '0').Count();
    var ones = digitsInPosition.Where(digit => digit == '1').Count();

    return (zeros, ones);
}