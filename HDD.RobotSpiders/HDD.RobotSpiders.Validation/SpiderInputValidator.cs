using HDD.RobotSpiders.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HDD.RobotSpiders.Validation
{
    public class SpiderInputValidator : ISpiderInputValidator
    {
        public void Validate(Position? start, string commands, int maxX, int maxY)
        {
            if (start is null)
                throw new ArgumentNullException(nameof(start));

            if (string.IsNullOrWhiteSpace(commands))
                throw new ArgumentException("Commands cannot be empty.", nameof(commands));

            if (start.X < 0 || start.Y < 0)
                throw new ArgumentException("Starting coordinates cannot be negative.");

            if (start.X > maxX || start.Y > maxY)
                throw new ArgumentException("Starting position is outside the wall boundaries.");

            foreach (char c in commands)
            {
                if (c != 'L' && c != 'R' && c != 'F')
                    throw new ArgumentException($"Invalid command character: '{c}'");
            }
        }
    }

}
