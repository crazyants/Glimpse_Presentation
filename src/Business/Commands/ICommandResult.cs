namespace Kiehl.App.Business.Commands
{
    public interface ICommandResult
    {
        bool IsSuccess { get; }
        bool IsFailure { get; }
        object Result { get; set; }
    }

    public class SuccessResult : ICommandResult
    {
        public SuccessResult()
        {
        }

        public SuccessResult(object result)
        {
            Result = result;
        }

        public bool IsSuccess => true;
        public bool IsFailure => false;
        public object Result { get; set; }
    }

    public class FailureResult : ICommandResult
    {
        public FailureResult(string failureMessage)
        {
            Result = failureMessage;
        }

        public bool IsSuccess => false;
        public bool IsFailure => true;
        public object Result { get; set; }
    }
}