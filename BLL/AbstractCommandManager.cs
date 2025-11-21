using Core;
using AutoMapper;

namespace BLL;

public abstract class AbstractCommandManager
{
    protected readonly IUnitOfWork unitOfWork;
    protected readonly IMapper mapper;

    protected AbstractCommandManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        ArgumentNullException.ThrowIfNull(unitOfWork, nameof(unitOfWork));
        ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));

        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    protected TResult ExecuteCommand<TResult>(IBaseCommand<TResult> command, string errorMessage)
    {
        var result = command.Execute();

        return result;
    }
}
