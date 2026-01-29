using HDD.RobotSpiders.Domain.Enums;
using HDD.RobotSpiders.Domain.Models;
using HDD.RobotSpiders.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HDD.RobotSpiders.Services.SpiderNavigator
{
    public class SpiderNavigatorService(ISpiderInputValidator validator) : ISpiderNavigatorService
    {
        private readonly ISpiderInputValidator _validator = validator;

        public async Task<Position> ExecuteAsync(Position start, string commands, int maxX, int maxY)
        {
            try
            {
                //Validation can also be async later if needed
                _validator.Validate(start, commands, maxX, maxY);

                // Simulate async boundary (e.g., logging, telemetry, DB, API)
                await Task.Yield();

                var pos = new Position(start.X, start.Y, start.Facing);

                foreach (char cmd in commands)
                {
                    switch (cmd)
                    {
                        case 'L':
                            pos.Facing = TurnLeft(pos.Facing);
                            break;

                        case 'R':
                            pos.Facing = TurnRight(pos.Facing);
                            break;

                        case 'F':
                            MoveForward(pos, maxX, maxY);
                            break;
                    }
                }

                return pos;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to execute spider navigation.", ex);
            }
        }

        private static Direction TurnLeft(Direction d)
        {
            return d switch
            {
                Direction.Up => Direction.Left,
                Direction.Left => Direction.Down,
                Direction.Down => Direction.Right,
                Direction.Right => Direction.Up,
                _ => d
            };
        }

        private static Direction TurnRight(Direction d) =>
            d switch
            {
                Direction.Up => Direction.Right,
                Direction.Right => Direction.Down,
                Direction.Down => Direction.Left,
                Direction.Left => Direction.Up,
                _ => d
            };

        private static void MoveForward(Position pos, int maxX, int maxY)
        {
            switch (pos.Facing)
            {
                case Direction.Up:
                    if (pos.Y < maxY) pos.Y++;
                    break;

                case Direction.Down:
                    if (pos.Y > 0) pos.Y--;
                    break;

                case Direction.Left:
                    if (pos.X > 0) pos.X--;
                    break;

                case Direction.Right:
                    if (pos.X < maxX) pos.X++;
                    break;
            }
        }
    }




}
