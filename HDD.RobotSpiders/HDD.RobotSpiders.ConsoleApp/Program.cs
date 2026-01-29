using HDD.RobotSpiders.Parser;
using HDD.RobotSpiders.Services.SpiderNavigator;
using HDD.RobotSpiders.Validation;
using HDD.RobotSpiders.Domain.Enums;
using HDD.RobotSpiders.Domain.Models;

class Program
{
    static async Task Main()
    {
        try
        {
            // Read wall boundaries
            var wall = Console.ReadLine()?.Split(' ');
            if (wall == null || wall.Length != 2)
                throw new ApplicationException("Invalid wall dimensions input.");

            int maxX = int.Parse(wall[0]);
            int maxY = int.Parse(wall[1]);

            // Read starting position
            var posParts = Console.ReadLine()?.Split(' ');
            if (posParts == null || posParts.Length != 3)
                throw new ApplicationException("Invalid spider starting position input.");

            int x = int.Parse(posParts[0]);
            int y = int.Parse(posParts[1]);
            Direction facing = Parser.ParseDirection(posParts[2]);

            // Read commands
            string? commands = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(commands))
                throw new ApplicationException("Commands input cannot be empty.");

            // Setup services
            var validator = new SpiderInputValidator();
            var navigator = new SpiderNavigatorService(validator);

            // Execute navigation
            Position finalPos = await navigator.ExecuteAsync(
                new Position(x, y, facing),
                commands,
                maxX,
                maxY
            );

            // Output result
            Console.WriteLine($"{finalPos.X} {finalPos.Y} {finalPos.Facing}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.ReadLine();
    }
}
