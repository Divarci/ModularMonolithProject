using Futions.CRM.Common.Domain.Abstractions.IUnitOfWorks;

namespace Futions.CRM.Modules.Users.Domain.Abstractions;
public interface IUsersUnitOfWork : IUnitOfWork, INeedTransactions;
