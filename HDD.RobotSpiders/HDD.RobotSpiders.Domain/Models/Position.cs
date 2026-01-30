using HDD.RobotSpiders.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HDD.RobotSpiders.Domain.Models
{
    public class Position(int x, int y, Direction facing)
    {
        public int X { get; set; } = x; public int Y { get; set; } = y; 
        public Direction Facing { get; set; } = facing;

        public override string ToString() => $"{X} {Y} {Facing}";
    }
}
