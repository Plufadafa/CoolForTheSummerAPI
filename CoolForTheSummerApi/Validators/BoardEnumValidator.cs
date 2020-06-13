using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoolForTheSummerApi.Models;
using Microsoft.OpenApi.Extensions;
using MongoDB.Driver;

namespace CoolForTheSummerApi.Validators
{
    public class BoardEnumValidator : IBoardEnumValidator
    {
        private readonly BoardEnum[] _boards = (BoardEnum[]) Enum.GetValues(typeof(BoardEnum));

        public bool ValidateStringAsBoard(string boardString)
        {
            foreach (var board in _boards)
            {
                var boardEnumConverted = board.GetDisplayName().ToLower();
                if (string.Equals(boardString, boardEnumConverted))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
