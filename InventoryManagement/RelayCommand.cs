using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InventoryManagement
{
    public class RelayCommand : ICommand
    {

        private Action<object> execute;       // What happens when the command runs
        private Predicate<object> canExecute; // Whether the command is allowed to run

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute; // Optional? Somehow?
        }

        public bool CanExecute(object parameter) => canExecute?.Invoke(parameter) ?? true;
        public void Execute(object parameter) => execute(parameter);

        public event EventHandler CanExecuteChanged;
    }

}
