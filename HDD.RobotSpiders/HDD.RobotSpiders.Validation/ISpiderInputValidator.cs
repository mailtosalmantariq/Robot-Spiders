using HDD.RobotSpiders.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HDD.RobotSpiders.Validation
{
    public interface ISpiderInputValidator
    {
        void Validate(Position start, string commands, int maxX, int maxY);
    }

}
