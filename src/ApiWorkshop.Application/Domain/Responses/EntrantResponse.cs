namespace ApiWorkshop.Application.Domain.Responses;

public class EntrantResponse
{
    public EntrantResponse(Guid? id, string name, string? cpf, string? phone, Guid drawId)
    {
        Id = id;
        Name = name;
        Cpf = cpf;
        Phone = phone;
        DrawId = drawId;
    }

    public Guid? Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Cpf { get; set; }
    public string? Phone { get; set; }
    public Guid DrawId { get; set; }
}
