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
            Console.WriteLine("=== HDD Robot Spiders Navigation ===");
            Console.WriteLine("Follow the prompts to simulate spider movement on the wall.");
            Console.WriteLine();

            // Read wall boundaries
            Console.Write("Enter wall size (e.g., 7 15): ");
            var wallInput = Console.ReadLine()?.Trim();
            var wall = wallInput?.Split(' ');

            if (wall == null || wall.Length != 2)
                throw new ApplicationException("Invalid wall dimensions. Expected format: 'maxX maxY'.");

            int maxX = int.Parse(wall[0]);
            int maxY = int.Parse(wall[1]);

            Console.WriteLine($"Wall boundaries set to: 0 0 → {maxX} {maxY}");
            Console.WriteLine();

            // Read starting position
            Console.Write("Enter spider starting position (e.g., 2 4 Left): ");
            var posInput = Console.ReadLine()?.Trim();
            var posParts = posInput?.Split(' ');

            if (posParts == null || posParts.Length != 3)
                throw new ApplicationException("Invalid starting position. Expected format: 'x y Direction'.");

            int x = int.Parse(posParts[0]);
            int y = int.Parse(posParts[1]);
            Direction facing = Parser.ParseDirection(posParts[2]);

            Console.WriteLine($"Spider starting at: {x} {y} facing {facing}");
            Console.WriteLine();

            // Read commands
            Console.Write("Enter movement commands (L, R, F): ");
            string? commands = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(commands))
                throw new ApplicationException("Commands cannot be empty.");

            Console.WriteLine($"Commands received: {commands}");
            Console.WriteLine();

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
            Console.WriteLine();
            Console.WriteLine("=== Final Spider Position ===");
            Console.WriteLine();
            Console.WriteLine($"{finalPos.X} {finalPos.Y} {finalPos.Facing}");
            Console.WriteLine();
            Console.WriteLine("==============================");
        }
        catch (Exception ex)
        {
            Console.WriteLine();
            Console.WriteLine("An error occurred:");
            Console.WriteLine(ex.Message);
        }

        Console.WriteLine();
        Console.WriteLine("Press Enter to exit...");
        Console.ReadLine();
    }
}
