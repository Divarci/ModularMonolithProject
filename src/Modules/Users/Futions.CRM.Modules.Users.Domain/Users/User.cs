using Futions.CRM.Common.Domain.Entities;
using Futions.CRM.Modules.Users.Domain.Roles;

namespace Futions.CRM.Modules.Users.Domain.Users;
public sealed partial class User : BaseEntity, IRootAggregate
{
    private User() { }

    private User(string email, string firstname,
        string lastname, string identityId)
    {
        Id = Guid.NewGuid();
        Email = email;
        Firstname = firstname;
        Lastname = lastname;    
        IdentityId = identityId;
    }

    public string Email { get; private set; }
    public string Firstname { get; private set; }
    public string Lastname { get; private set; }
    public string IdentityId { get; private set; }


    private readonly List<Role> _roles = [];
    public IReadOnlyCollection<Role> Roles => _roles.AsReadOnly(); 
}
