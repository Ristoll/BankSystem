using Core.Entities;

namespace Core;

public interface IUnitOfWork : IDisposable
{
    public IGenericRepository<Account> AccountRepository { get; }
    public IGenericRepository<AccountType> AccountTypeRepository { get; }
    public IGenericRepository<BankBranch> BankBranchRepository { get; }
    public IGenericRepository<Client> ClientRepository { get; }
    public IGenericRepository<Credit> CreditRepository { get; }
    public IGenericRepository<CreditStatus> CreditStatusRepository { get; }
    public IGenericRepository<Currency> CurrencyRepository { get; }
    public IGenericRepository<Employee> EmployeeRepository { get; }
    public IGenericRepository<Payment> PaymentRepository { get; }
    public IGenericRepository<PaymentType> PaymentTypeRepository { get; }
    public IGenericRepository<Transaction> TransactionRepository { get; }
    public IGenericRepository<TransactionType> TransactionTypeRepository { get; }
    void Save();
}