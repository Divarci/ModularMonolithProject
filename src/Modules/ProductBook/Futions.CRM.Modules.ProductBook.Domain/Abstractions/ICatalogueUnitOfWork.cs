using Futions.CRM.Common.Domain.Abstractions.IUnitOfWorks;

namespace Futions.CRM.Modules.Catalogue.Domain.Abstractions;
public interface ICatalogueUnitOfWork : IUnitOfWork, INeedTransactions;
