using System.Globalization;
using System.Windows.Data;

namespace FarnahadFlowFusion.Core.Main.Converter;

public class HijriDateValueConverter : IValueConverter
{
    private readonly PersianCalendar _persianCalendar;

    public HijriDateValueConverter()
    {
        _persianCalendar = new PersianCalendar();
    }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var dateTime = value as DateTime?;
        return dateTime != null ? string.Format("{0}/{1}/{2}", _persianCalendar.GetYear(dateTime.Value),
            _persianCalendar.GetMonth(dateTime.Value), _persianCalendar.GetDayOfMonth(dateTime.Value)) : null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}