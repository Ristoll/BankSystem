namespace BLL;
public interface IBaseCommand<TResult>
{
    public TResult Execute();
}
