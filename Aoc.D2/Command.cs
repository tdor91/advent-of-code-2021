public class Command
{
    public Command(string command)
    {
        var parts = command.Split(" ");
        Direction = Enum.Parse<Direction>(parts[0], ignoreCase: true);
        Value = int.Parse(parts[1]);
    }

    public Direction Direction { get; set; }
    public int Value { get; set; }
}
