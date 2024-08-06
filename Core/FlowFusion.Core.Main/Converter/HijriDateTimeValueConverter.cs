using System.Globalization;
using System.Windows.Data;

namespace FarnahadFlowFusion.Core.Main.Converter;

public class HijriDateTimeValueConverter : IValueConverter
{
    private readonly PersianCalendar _persianCalendar;

    public HijriDateTimeValueConverter()
    {
        _persianCalendar = new PersianCalendar();
    }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is DateTime)
        {
            var time = (DateTime)value;

            return _persianCalendar.GetYear(time) + "/" + _persianCalendar.GetMonth(time) + "/" +
                   _persianCalendar.GetDayOfMonth(time) + "-" + time.Hour.ToString("D2") + ":" +
                   time.Minute.ToString("D2") + ":" + time.Second.ToString("D2");
        }

        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}