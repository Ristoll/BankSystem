using AutoMapper;
using Core;
using BLL.Services;
using System;
using DTO;

namespace BLL.Commands.EmployeesCommands;

public class LogInCommand : AbstrCommandWithDA<EmployeeLogInDto>
{
    private readonly string phone;
    private readonly string password;
    private readonly IPasswordHasher passwordHasher;
    private readonly ICurrentUserService currentUser;

    public LogInCommand(string phone, string password, IUnitOfWork unitOfWork, IMapper mapper,
                        IPasswordHasher passwordHasher, ICurrentUserService currentUser)
        : base(unitOfWork, mapper)
    {
        this.phone = phone;
        this.password = password;
        this.passwordHasher = passwordHasher;
        this.currentUser = currentUser;
    }

    public override EmployeeLogInDto Execute()
    {
        // Шукаємо працівника по телефону
        var employee = dAPoint.EmployeeRepository
            .FirstOrDefault(e => e.Phone == phone);

        if (employee == null)
            return null;

        // Перевіряємо пароль через PasswordHasher
        bool valid = passwordHasher.Verify(password, employee.PasswordHash);
        if (!valid)
            return null;
        EmployeeLogInDto employeeDto = new EmployeeLogInDto()
        {
            EmployeeId = employee.EmployeeId,
            BranchId = employee.BranchId,
            RoleId = employee.RoleId
        };
        // Зберігаємо ID поточного працівника
        currentUser.SetEmployee(employeeDto.EmployeeId, employeeDto.BranchId, employeeDto.RoleId);
        
        return employeeDto;
    }
}
