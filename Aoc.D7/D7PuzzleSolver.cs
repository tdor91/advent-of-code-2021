namespace Aoc.D7
{
    internal class D7PuzzleSolver
    {
        public long SolvePart1(string input)
        {
            return Solve(input, GetPart1Fuel);
        }

        public long SolvePart2(string input)
        {
            return Solve(input, GetPart2Fuel);
        }

        private long Solve(string input, Func<int, int, int> getFuel)
        {
            var crabPositions = input.Split(",").Select(int.Parse).ToList();
            var maxPosition = crabPositions.Max() + 1;

            var fuelToTarget = new long[maxPosition];

            for (int targetPosition = 0; targetPosition < maxPosition; targetPosition++)
            {
                foreach (var crabPosition in crabPositions)
                {
                    var fuel = getFuel(targetPosition, crabPosition);
                    fuelToTarget[targetPosition] += fuel;
                }
            }

            return fuelToTarget.Where(fuel => fuel > 0).Min();
        }

        private int GetPart1Fuel(int targetPosition, int crabPosition)
        {
            return Math.Abs(targetPosition - crabPosition);
        }

        private int GetPart2Fuel(int targetPosition, int crabPosition)
        {
            var diff = Math.Abs(targetPosition - crabPosition);
            var range = Enumerable.Range(1, diff);
            return range.Sum();
        }
    }
}
