using System;
using System.Windows.Input;

namespace InventoryManagement
{
    public class RelayCommand(Action<object> execute, Predicate<object> canExecute = null) : ICommand
    {
        private readonly Action<object> _execute = execute ?? throw new ArgumentNullException(nameof(execute));

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => canExecute == null || canExecute(parameter); // I dare not touch this

        public void Execute(object parameter) => _execute(parameter); // I dare not touch this either

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
