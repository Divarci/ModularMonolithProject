using Futions.CRM.Common.Domain.Abstractions.IUnitOfWorks;

namespace Futions.CRM.Modules.Deals.Domain.Abstractions;
public interface IDealsUnitOfWork : IUnitOfWork, INeedTransactions;
