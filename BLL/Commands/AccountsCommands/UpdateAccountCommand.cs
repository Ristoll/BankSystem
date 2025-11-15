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
    public class UpdateAccountCommand : AbstrCommandWithDA<bool>
    {
        private AccountDto accountDto;
        public UpdateAccountCommand(AccountDto accountDTO, IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            this.accountDto = accountDTO;
        }
        public override bool Execute()
        {
            var account = mapper.Map<Account>(accountDto);
            dAPoint.AccountRepository.Update(account);
            dAPoint.Save();
            return true;
        }
    }
}
