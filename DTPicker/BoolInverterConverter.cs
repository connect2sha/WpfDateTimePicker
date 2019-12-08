using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DTPicker
{
    class BoolInverterConverter : IValueConverter
    {
        public Object Convert(Object value, System.Type targetType, Object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.GetType() == typeof(bool))
            {
                Console.WriteLine(!(bool)value);
                return !(bool)value;
            }

            return null;
        }

        public Object ConvertBack(Object value, System.Type targetType, Object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
