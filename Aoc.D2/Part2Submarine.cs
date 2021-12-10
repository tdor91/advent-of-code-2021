public class Part2Submarine
{
    private int aim;

    public int HorizontalPosition { get; set; }
    public int Depth { get; set; }

    public void Execute(Command command)
    {
        if (command.Direction == Direction.Forward)
        {
            HorizontalPosition += command.Value;
            Depth += aim * command.Value;
        }

        if (command.Direction == Direction.Down)
        {
            aim += command.Value;
        }

        if (command.Direction == Direction.Up)
        {
            aim -= command.Value;
        }
    }
}
