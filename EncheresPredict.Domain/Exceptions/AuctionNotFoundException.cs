namespace EncheresPredict.Domain.Exceptions;

public class AuctionNotFoundException(Guid id)
    : Exception($"L'enchère avec l'identifiant '{id}' est introuvable.");
