using Strev.QuickTools.DomainModel.Enumeration;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Strev.QuickTools.ViewModel.Generic
{
    public class ChoiceVM : BaseVM, IStackElementVM, IItemElementVM
    {
        private readonly Action<ChoiceElementVM> _onNewCurrent;

        public ChoiceVM(Action<ChoiceElementVM> onNewCurrent)
        {
            _choiceElementVMs = new ObservableCollection<ChoiceElementVM>();
            _onNewCurrent = onNewCurrent;
        }

        private ObservableCollection<ChoiceElementVM> _choiceElementVMs;
        public ObservableCollection<ChoiceElementVM> ChoiceElementVMs => _choiceElementVMs;

        public string Name => null;

        public int MarginSize => 0;

        public ItemElementVM MarginElement => null;

        public IItemElementVM Element => this;

        public ElementType ElementType => ElementType.Choice;

        public string Text
        {
            get
            {
                return null;
            }

            set
            {
            }
        }

        public ICommand Command
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        public void Clear()
        {
            ChoiceElementVMs.Clear();
        }

        public void Add(ChoiceElementVM choiceElementVM)
        {
            ChoiceElementVMs.Add(choiceElementVM);
        }

        public void SetCurrent(ChoiceElementVM choiceElementVM)
        {
            bool newCurrent = (!choiceElementVM.IsCurrent);
            foreach (var elementVM in ChoiceElementVMs)
            {
                if (choiceElementVM == elementVM)
                {
                    elementVM.IsCurrent = true;
                }
                else
                {
                    elementVM.IsCurrent = false;
                }
            }
            if (newCurrent)
            {
                _onNewCurrent?.Invoke(choiceElementVM);
            }
        }
    }
}