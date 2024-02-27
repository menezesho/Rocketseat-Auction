using Auction.API.Entities;
using Auction.API.UseCases.Auctions.GetCurrent;
using Microsoft.AspNetCore.Mvc;

namespace Auction.API.Controllers;

public class AuctionController : AuctionBaseController
{
    /*
     * Deve ser passado um atributo acima do endpoint para definir o método (get, post, put, delete, etc) e o path do endpoint.
     * Os controllers devem retornar IActionResult, que é um tipo de retorno que pode ser qualquer coisa, desde um objeto até um erro.
     * Tipos de retorno mais comuns: Ok, BadRequest, NotFound, CreatedAtAction, NoContent, Unauthorized, etc.
     * [HttpGet("test")] Este é um modelo de endpoint no path: /auction/test
    */

    [HttpGet]
    [ProducesResponseType(typeof(AuctionEntity), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult GetCurrentAuction([FromServices] GetCurrentAuctionUseCase useCase)
    {
        var result = useCase.Execute();

        if (result == null)
            return NoContent();

        return Ok(result);
    }
}
