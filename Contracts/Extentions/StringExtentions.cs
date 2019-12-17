using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerlinClock.Contracts.Extentions
{
    public static class StringExtentions
    {

        /// <summary>
        /// Relace given index char with 'value'
        /// </summary>
        /// <param name="source">source string</param>
        /// <param name="value">char to replace with</param>
        /// <param name="index">index to char to replace with 'value'</param>
        /// <returns></returns>
        public static string ReplaceAtIndexOf(this string source, char value, int index)
        {

            var builder = new StringBuilder(source);
            try
            {
                builder.Remove(index, 1);
                builder.Insert(index, value);
            }
            catch (Exception ex)
            {
                //possible exception IndexOutOfRangeException
                //Log the error or notify user
                throw ex;
            }

            return builder.ToString();

            
        }


    }
}
