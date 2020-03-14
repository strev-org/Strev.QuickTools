using System;
using System.Windows.Input;

namespace Strev.QuickTools.ViewModel
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private bool _active;

        public bool Active
        {
            get
            {
                return _active;
            }

            set
            {
                if (_active == value) return;
                _active = value;
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        public RelayCommand(Action<object> execute)
            : this(execute, true)
        {
        }

        public RelayCommand(Action<object> execute, bool activeParDefaut)
        {
            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            _execute = execute;
            _active = activeParDefaut;
        }

        // [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _active;
        }

        public event EventHandler CanExecuteChanged = delegate { };

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}