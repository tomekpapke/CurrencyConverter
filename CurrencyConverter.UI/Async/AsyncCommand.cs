using System;
using System.Threading.Tasks;

namespace CurrencyConverter.UI.Async
{
    public class AsyncCommand<TResult> : AsyncCommandBase
    {
        private readonly Func<Task<TResult>> command;

        public AsyncCommand(Func<Task<TResult>> command)
        {
            this.command = command;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override Task ExecuteAsync(object parameter)
        {
            return command();
        }
    }

    public static class AsyncCommand
    {
        public static AsyncCommand<object> Create(Func<Task> command)
        {
            return new AsyncCommand<object>(async () => { await command(); return null; });
        }

        public static AsyncCommand<TResult> Create<TResult>(Func<Task<TResult>> command)
        {
            return new AsyncCommand<TResult>(command);
        }
    }
}
