using BLL.AutoMapperProfiles;
using BLL.Commands;
using BLL.Commands.AccountsCommands;
using BLL.Commands.BranchesCommands;
using BLL.Commands.ClientsCommands;
using BLL.Commands.CreditsCommands;
using BLL.Commands.EmployeesCommands;
using BLL.Commands.PaymentsCommands;
using BLL.Commands.TransactionsCommands;
using DAL;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Services;

public static class BLLInitializer
{
    public static void AddAutoMapperToServices(IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddAutoMapper(cfg => { }, typeof(AccountAutoMapperProfile).Assembly);
        services.AddAutoMapper(cfg => { }, typeof(AccountTypeAutoMapperProfile).Assembly);
        services.AddAutoMapper(cfg => { }, typeof(BankBranchAutoMapperProfile).Assembly);
        services.AddAutoMapper(cfg => { }, typeof(BranchTypeAutoMapperProfile).Assembly);
        services.AddAutoMapper(cfg => { }, typeof(ClientAutoMapperProfile).Assembly);
        services.AddAutoMapper(cfg => { }, typeof(CreditAutoMapperProfile).Assembly);
        services.AddAutoMapper(cfg => { }, typeof(CreditStatusAutoMapperProfile).Assembly);
        services.AddAutoMapper(cfg => { }, typeof(CurrencyAutoMapperProfile).Assembly);
        services.AddAutoMapper(cfg => { }, typeof(EmployeeAutoMapperProfile).Assembly);
        services.AddAutoMapper(cfg => { }, typeof(EmployeeRoleAutoMapperProfile).Assembly);
        services.AddAutoMapper(cfg => { }, typeof(PaymentAutoMapperProfile).Assembly);
        services.AddAutoMapper(cfg => { }, typeof(PaymentTypeAutoMapperProfile).Assembly);
        services.AddAutoMapper(cfg => { }, typeof(TransactionAutoMapperProfile).Assembly);
        services.AddAutoMapper(cfg => { }, typeof(TransactionTypeAutoMapperProfile).Assembly);
    }

    public static void AddCommandDependenciesToServices(IServiceCollection services)
    {
        // ініціалізація залежностей рівня дал
        DALInitializer.AddDataAccessServices(services);

        services.AddScoped<AccountsCommandManager>();
        services.AddScoped<BranchesCommandManager>();
        services.AddScoped<ClientsCommandManager>();
        services.AddScoped<CreditsCommandManager>();
        services.AddScoped<EmployeesCommandManager>();
        services.AddScoped<PaymentsCommandManager>();
        services.AddScoped<TransactionsCommandManager>();
    }
}