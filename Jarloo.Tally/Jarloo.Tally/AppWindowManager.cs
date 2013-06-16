using System.Windows;
using Caliburn.Micro;

namespace Jarloo.Tally
{
    internal class AppWindowManager : WindowManager
    {
        protected override Window EnsureWindow(object model, object view, bool isDialog)
        {
            Window window = view as Window;

            if (window == null)
            {
                if (isDialog)
                {
                    window = new Window
                    {
                        Content = view,
                        SizeToContent = SizeToContent.WidthAndHeight
                    };
                }
                else
                {
                    window = new Window
                        {
                            Content = view,
                            SizeToContent = SizeToContent.Manual
                        };
                }

                window.SetValue(View.IsGeneratedProperty, true);
            }
            else
            {
                Window owner2 = InferOwnerOf(window);
                if (owner2 != null && isDialog)
                {
                    window.Owner = owner2;
                }
            }
            return window;
        }
    }
}