using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoolForTheSummerApi.Validators
{
    public interface IBoardEnumValidator
    {
        public bool ValidateStringAsBoard(string boardString);
    }
}
