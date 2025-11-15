using Core;
using Core.Entities;
using AutoMapper;

namespace BLL;

public abstract class AbstrCommandWithDA<TResult> : IBaseCommand<TResult>
{
    protected readonly IUnitOfWork dAPoint;
    protected readonly IMapper mapper;

    protected AbstrCommandWithDA(IUnitOfWork operateUnitOfWork, IMapper mapper)
    {
        this.dAPoint = operateUnitOfWork;
        this.mapper = mapper;
    }

    public abstract TResult Execute();
}
