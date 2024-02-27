using Auction.API.Communications.Requests;
using Auction.API.Filters;
using Auction.API.UseCases.Offers.CreateOffer;
using Microsoft.AspNetCore.Mvc;

namespace Auction.API.Controllers;

public class OfferController : AuctionBaseController
{
    [HttpPost]
    [Route("{itemId}")]
    [ServiceFilter(typeof(AuthenticationUserAttribute))]
    public IActionResult CreateOffer([FromRoute] int itemId, [FromBody] RequestCreateOfferJson request, [FromServices] CreateOfferUseCase useCase)
    {
        var id = useCase.Execute(itemId, request);

        return Created(string.Empty, id);
    }
}
