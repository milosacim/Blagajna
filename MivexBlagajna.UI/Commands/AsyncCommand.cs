using MivexBlagajna.UI.Commands.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MivexBlagajna.UI.Commands
{
    public abstract class AsyncCommand : IAsyncCommand
    {
        private readonly ObservableCollection<Task> runningTasks;

        protected AsyncCommand()
        {
            runningTasks = new ObservableCollection<Task>();
            runningTasks.CollectionChanged += OnRunningTasksChanged;
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

        public abstract bool CanExecute();
        public abstract Task ExecuteAsync();

        bool ICommand.CanExecute(object? parameter)
        {
            return CanExecute();
        }

        async void ICommand.Execute(object? parameter)
        {
            Task runningTask = ExecuteAsync();
            runningTasks.Add(runningTask);

            try
            {
                if (runningTask != null)
                {
                    await runningTask;
                }
            }
            finally
            {
                runningTasks.Remove(runningTask);
            }
        }

        private void OnRunningTasksChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
