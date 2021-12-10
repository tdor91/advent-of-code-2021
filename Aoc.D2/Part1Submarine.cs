public class Part1Submarine
{
    public int HorizontalPosition { get; set; }
    public int Depth { get; set; }

    public void Execute(Command command)
    {
        if (command.Direction == Direction.Forward)
        {
            HorizontalPosition += command.Value;
        }

        if (command.Direction == Direction.Down)
        {
            Depth += command.Value;
        }

        if (command.Direction == Direction.Up)
        {
            Depth -= command.Value;
        }
    }
}
