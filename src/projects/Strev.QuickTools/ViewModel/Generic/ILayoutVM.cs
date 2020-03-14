using System;

namespace Strev.QuickTools.ViewModel.Generic
{
    public interface ILayoutVM
    {
        IStackVM TopStack();

        IStackVM BottomStack();

        ILayoutVM OnChange(string name, Action<string, string> action);

        ILayoutVM StartLayout(int marginSize);

        LayoutVM EndLayout();

        ILayoutVM AddTextZone(string name, string marginText, string text);
    }
}