using HDD.RobotSpiders.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HDD.RobotSpiders.Services.SpiderNavigator
{
    public interface ISpiderNavigatorService
    {
       Task<Position> ExecuteAsync(Position start, string commands, int maxX, int maxY);
    }

}
