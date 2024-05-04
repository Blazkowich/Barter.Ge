namespace Barter.Shared;

public interface IClaimService
{
    string GetUserId();

    string GetClaim(string key);
}
