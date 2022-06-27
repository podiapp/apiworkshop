using System.ComponentModel;

namespace ApiWorkshop.Application.Domain.Enums;

public enum Status
{
    [Description("Rascunho")]
    DRAFT,
    [Description("Ativo")]
    ACTIVE,
    [Description("Inativo")]
    INACTIVE,
    [Description("Expirado")]
    EXPIRED,
    [Description("Deletado")]
    DELETED,
}