using HDD.RobotSpiders.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HDD.RobotSpiders.Parser
{
    public static class Parser
    {
        public static Direction ParseDirection(string s) =>
            s.ToLower() switch
            {
                "up" => Direction.Up,
                "right" => Direction.Right,
                "down" => Direction.Down,
                "left" => Direction.Left,
                _ => throw new ArgumentException("Invalid direction")
            };
    }

}
