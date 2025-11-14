using Core.Entities;
using Core;

namespace DAL;

public class UnitOfWork : IUnitOfWork
{
    private bool disposedValue;

    private readonly BankDbContext context;
    private readonly IGenericRepository<Account>? accountRepository;
    private readonly IGenericRepository<AccountType>? accountTypeRepository;
    private readonly IGenericRepository<BankBranch>? bankBranchRepository;
    private readonly IGenericRepository<BranchType>? branchTypeRepository;
    private readonly IGenericRepository<Client>? clientRepository;
    private readonly IGenericRepository<Credit>? creditRepository;
    private readonly IGenericRepository<CreditStatus>? creditStatusRepository;
    private readonly IGenericRepository<Currency>? currencyRepository;
    private readonly IGenericRepository<Employee>? employeeRepository;
    private readonly IGenericRepository<EmployeeRole>? employeeRoleRepository;
    private readonly IGenericRepository<Payment>? paymentRepository;
    private readonly IGenericRepository<PaymentType>? paymentTypeRepository;
    private readonly IGenericRepository<Transaction>? transactionRepository;
    private readonly IGenericRepository<TransactionType>? transactionTypeRepository;

    public IGenericRepository<Account> AccountRepository => accountRepository ?? new GenericRepository<Account>(context);
    public IGenericRepository<AccountType> AccountTypeRepository => accountTypeRepository ?? new GenericRepository<AccountType>(context);
    public IGenericRepository<BankBranch> BankBranchRepository => bankBranchRepository ?? new GenericRepository<BankBranch>(context);
    public IGenericRepository<BranchType> BranchTypeRepository => branchTypeRepository ?? new GenericRepository<BranchType>(context);
    public IGenericRepository<Client> ClientRepository => clientRepository ?? new GenericRepository<Client>(context);
    public IGenericRepository<Credit> CreditRepository => creditRepository ?? new GenericRepository<Credit>(context);
    public IGenericRepository<CreditStatus> CreditStatusRepository => creditStatusRepository ?? new GenericRepository<CreditStatus>(context);
    public IGenericRepository<Currency> CurrencyRepository => currencyRepository ?? new GenericRepository<Currency>(context);
    public IGenericRepository<Employee> EmployeeRepository => employeeRepository ?? new GenericRepository<Employee>(context);
    public IGenericRepository<EmployeeRole> EmployeeRoleRepository => employeeRoleRepository ?? new GenericRepository<EmployeeRole>(context);
    public IGenericRepository<Payment> PaymentRepository => paymentRepository ?? new GenericRepository<Payment>(context);
    public IGenericRepository<PaymentType> PaymentTypeRepository => paymentTypeRepository ?? new GenericRepository<PaymentType>(context);
    public IGenericRepository<Transaction> TransactionRepository => transactionRepository ?? new GenericRepository<Transaction>(context);
    public IGenericRepository<TransactionType> TransactionTypeRepository => transactionTypeRepository ?? new GenericRepository<TransactionType>(context);

    public UnitOfWork(BankDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context, nameof(context));

        this.context = context;
    }

    public void Save()
    {
        context.SaveChanges();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                context.Dispose();
            }

            disposedValue = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
