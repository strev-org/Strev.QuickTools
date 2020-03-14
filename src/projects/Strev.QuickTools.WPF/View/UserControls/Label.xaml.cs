using Strev.QuickTools.DomainModel.Enumeration;
using System.Windows;
using System.Windows.Controls;

namespace Strev.QuickTools.View.UserControls
{
    /// <summary>
    /// Interaction logic for Label.xaml
    /// </summary>
    public partial class Label : UserControl
    {
        public Label()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(Label), null);

        public TextSizeType TextSize
        {
            get { return (TextSizeType)GetValue(TextSizeProperty); }
            set { SetValue(TextSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextSizeProperty = DependencyProperty.Register("TextSize", typeof(TextSizeType), typeof(Label), null);

        public int TextFontSize => Helpers.GetFontSizeByTextSize(TextSize);
    }
}