using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Helpers
{
    public static class StringExtensions
    {
        public static Icon ToIcon(this Bitmap bitmap)
        {
            return Icon.FromHandle(bitmap.GetHicon());
        }

        public static DateTime ToDate(this object obj)
        {
            try
            {
                return Convert.ToDateTime(obj);
            }
            catch (Exception e)
            {
                return DateTime.Now;
            }
        }
        public static int ToInt(this object obj)
        {
            try
            {
                return Convert.ToInt32(obj);
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public static int? ToInt(this object obj, bool isNullable)
        {
            try
            {
                if (isNullable && obj.ToInt() == 0)
                    return null;
                return Convert.ToInt32(obj);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public static Boolean ToBool(this object obj)
       {
            try
            {
                return Convert.ToBoolean(obj);
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static decimal ToDecimal(this object obj)
        {
            try
            {
                return Convert.ToDecimal(obj);
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public static string ImageToBase64(byte[] array)
        {
            try
            {
                var base64 = Convert.ToBase64String(array);
                return $"data:image/png;base64,{base64}";
            }
            catch (Exception e)
            {
                var ms = new MemoryStream();
                var img = Image.FromFile(HttpContext.Current.Server.MapPath("~/Content/admin-lte/img/avatar2.png"));
                img.Save(ms, ImageFormat.Png);
                var base64 = Convert.ToBase64String(ms.ToArray());
                return $"data:image/png;base64,{base64}";
            }

        }
        public static string NumberToWords(string amount)
        {
            var res = amount.Split('.').Select(int.Parse).ToList();
            return NumberToWords(res[0]) + $" and {res[1]}/100";
        }
        private static string NumberToWords(int number)
        {

            if (number == 0)
                return "Zero";

            if (number < 0)
                return "Minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " Million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " Thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
                var tensMap = new[] { "zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }
    }

}
