using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CoolForTheSummerApi.Services;
using CoolForTheSummerApi.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoolForTheSummerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly ILogger<PostController> _logger;
        private readonly IFourChanService _fourChanService;
        private readonly IBoardEnumValidator _boardEnumValidator;
        public PostController(ILogger<PostController> logger, IFourChanService fourChanService, IBoardEnumValidator boardEnumValidator)
        {
            _logger = logger;
            _fourChanService = fourChanService;
            _boardEnumValidator = boardEnumValidator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRandomPostRandomBoard()
        {
            var response = await _fourChanService.GetRandomPostFromRandomBoard();
            return Ok(response);
        }

        [HttpGet]
        [Route("board/{board}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRandomPostFromBoard(string board)
        {
            if (!_boardEnumValidator.ValidateStringAsBoard(board))
            {
                return NotFound(
                    "Board provided is either not a 4Chan board, or it's \"int\", \"out\" or \"3\" which aren't supported in this API because I'm lazy");
            }
            var response = await _fourChanService.GetRandomPostFromBoard(board);
            return Ok(response);
        }
    }
}
