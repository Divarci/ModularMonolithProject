using Futions.CRM.Common.Domain.Entities;

namespace Futions.CRM.Modules.Users.Domain.Users;
public sealed partial class User : BaseEntity, IRootAggregate
{
    private User() { }

    private User(string email, string fullname)
    {
        Id = Guid.NewGuid();
        Email = email;
        Fullname = fullname;
    }

    public string Email { get; private set; }
    public string Fullname { get; private set; }
}
