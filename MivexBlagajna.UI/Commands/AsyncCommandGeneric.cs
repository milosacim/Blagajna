using MivexBlagajna.UI.Commands.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MivexBlagajna.UI.Commands
{
    public abstract class AsyncCommandGeneric<T> : IAsyncCommandGeneric<T>
    {
        private ObservableCollection<Task> runningTasks;

        protected AsyncCommandGeneric()
        {
            runningTasks = new ObservableCollection<Task>();
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public IEnumerable<Task> RunningTasks
        {
            get => runningTasks;
        }

        public abstract bool CanExecute(T parameter);
        public abstract Task ExecuteAsync(T parameter);

        bool ICommand.CanExecute(object? parameter)
        {
            return CanExecute((T)parameter);
        }

        async void ICommand.Execute(object? parameter)
        {
            Task runningTask = ExecuteAsync((T)parameter);
            runningTasks.Add(runningTask);

            try
            {
                await runningTask;
            }
            finally
            {
                runningTasks.Remove(runningTask);
            }
        }
    }
}
