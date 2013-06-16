
using System;
using System.Globalization;
using System.Windows.Data;

namespace Jarloo.Tally.Views
{
    public class FileExtensionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string ext = (string) value;

            switch (ext.ToUpper())
            {
                case ".XAML":
                    return new Uri("/images/xaml.ico", UriKind.Relative);
                    
                case ".ASM":
                case ".ASA":
                case ".ASAX":
                case ".ASCX":
                case ".ASHX":
                case ".ASMX":
                case ".ASP":
                case ".ASPX":
                    return new Uri("/images/aspx.ico", UriKind.Relative);
                case ".CONFIG":
                    return new Uri("/images/config.ico", UriKind.Relative);
                case ".VB":
                    return new Uri("/images/vb.ico", UriKind.Relative);
                case ".CS":
                    return new Uri("/images/csharp.ico", UriKind.Relative);
                case ".HTML":
                case ".HTM":
                case ".js":
                    return new Uri("/images/html.ico", UriKind.Relative);
                default:
                    return new Uri("/images/vsdoc.ico", UriKind.Relative);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}