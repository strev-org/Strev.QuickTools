namespace Strev.QuickTools.ViewModel.Generic
{
    public interface IStackElementVM
    {
        string Name { get; }

        int MarginSize { get; }

        ItemElementVM MarginElement { get; }

        IItemElementVM Element { get; }
    }
}