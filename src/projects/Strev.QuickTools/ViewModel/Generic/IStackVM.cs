using Strev.QuickTools.DomainModel.Enumeration;
using System;

namespace Strev.QuickTools.ViewModel.Generic
{
    public interface IStackVM
    {
        IStackVM AddInput(string name, string marginText, string text);

        IStackVM AddInput(string name, string marginText, string text, TextSizeType textSize);

        IStackVM AddInputReadOnly(string name, string marginText, string text, TextSizeType textSize);

        IStackVM AddLabel(string name, string marginText, string text);

        IStackVM AddLabel(string name, string marginText, string text, TextSizeType textSize);

        IStackVM AddCenteredLabel(string name, string marginText, string text, TextSizeType textSize);

        IStackVM AddButton(string name, string text, Action onClick);

        IStackVM AddButton(string name, string text, TextSizeType textSize, Action onClick);

        IStackVM AddChoice(Action<ChoiceVM> onPopulate, Action<ChoiceElementVM> onNewCurrent);

        IStackVM OnChange(Action<string, string> action);

        IStackVM WithButton(string text, Action onClick);

        ILayoutVM EndStack();
    }
}