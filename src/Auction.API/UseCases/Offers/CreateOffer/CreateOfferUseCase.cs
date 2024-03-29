﻿using Auction.API.Communications.Requests;
using Auction.API.Contracts;
using Auction.API.Entities;
using Auction.API.Services;

namespace Auction.API.UseCases.Offers.CreateOffer;

public class CreateOfferUseCase
{
    private readonly LoggedUser _loggedUser;
    private readonly IOfferRepository _repository;

    public CreateOfferUseCase(LoggedUser loggedUser, IOfferRepository repository){
        _loggedUser = loggedUser;
        _repository = repository;
    }

    public int Execute(int itemId, RequestCreateOfferJson request)
    {
        var user = _loggedUser.User();

        var offer = new Offer
        {
            CreatedOn = DateTime.Now,
            Price = request.Price,
            ItemId = itemId,
            UserId = user.Id
        };

        _repository.Add(offer);

        return offer.Id;
    }
}
