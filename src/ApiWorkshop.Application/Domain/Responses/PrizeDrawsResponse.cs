namespace ApiWorkshop.Application.Domain.Responses;

public class PrizeDrawsResponse : PrizeDrawDataResponse
{
    public PrizeDrawsResponse(Guid id,
                        string name,
                        string? gift,
                        Guid? giftId,
                        DateTime createdAt,
                        string? giftPhoto,
                        string? mallName,
                        DateTime expireAt,
                        string code) : base(id,
                                                   name,
                                                   gift,
                                                   giftId,
                                                   createdAt)
    {
        GiftPhoto = giftPhoto;
        MallName = mallName;
        ExpireAt = expireAt;
        Code = code;
    }

    public string? GiftPhoto { get; }
    public string? MallName { get; }
    public DateTime ExpireAt { get; }
    public string Code { get; }
}
