using Strev.QuickTools.DomainModel.Enumeration;

namespace Strev.QuickTools.View.UserControls
{
    public static class Helpers
    {
        public static int GetFontSizeByTextSize(TextSizeType textSize)
        {
            switch (textSize)
            {
                case TextSizeType.Tiny:
                    return 15;

                case TextSizeType.Small:
                    return 18;

                case TextSizeType.Large:
                    return 23;

                case TextSizeType.Medium:
                default:
                    return 20;
            }
        }

        public static int GetControlHeightByTextSize(TextSizeType textSize)
        {
            switch (textSize)
            {
                case TextSizeType.Tiny:
                    return 18;

                case TextSizeType.Small:
                    return 23;

                case TextSizeType.Large:
                    return 30;

                case TextSizeType.Medium:
                default:
                    return 30;
            }
        }
    }
}