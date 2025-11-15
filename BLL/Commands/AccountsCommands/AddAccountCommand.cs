using AutoMapper;
using Core;
using Core.Entities;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Commands.AccountsCommands
{
    public class AddAccountCommand : AbstrCommandWithDA<bool>
    {
        private AccountDto accountDto;
        public AddAccountCommand(AccountDto accountDTO, IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            this.accountDto = accountDTO;
        }
        public override bool Execute()
        {
            var account = mapper.Map<Account>(accountDto);
            dAPoint.AccountRepository.Add(account);
            dAPoint.Save();
            return true;
        }
    }
}
