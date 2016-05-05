namespace Kiehl.App.Data.Interception
{
    public interface IInterceptor
    {
        void Before(InterceptionContext context);
        void After(InterceptionContext context);
    }
}