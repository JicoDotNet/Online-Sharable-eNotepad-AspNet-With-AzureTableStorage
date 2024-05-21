namespace System
{
    using System.Collections.Generic;
    using System.Linq;
    public class GenericLogic
    {
        public static DateTime IstNow
        {
            get
            {
                return System.DateTime.UtcNow.AddHours(5.5);
            }
        }
        public static Dictionary<string, string> State()
        {
            Dictionary<string, string> pairs = new Dictionary<string, string>
            {
                { "01", "JAMMU AND KASHMIR" },
                { "02", "HIMACHAL PRADESH" },
                { "03", "PUNJAB" },
                { "04", "CHANDIGARH" },
                { "05", "UTTARAKHAND" },
                { "06", "HARYANA" },
                { "07", "DELHI" },
                { "08", "RAJASTHAN" },
                { "09", "UTTAR PRADESH" },
                { "10", "BIHAR" },
                { "11", "SIKKIM" },
                { "12", "ARUNACHAL PRADESH" },
                { "13", "NAGALAND" },
                { "14", "MANIPUR" },
                { "15", "MIZORAM" },
                { "16", "TRIPURA" },
                { "17", "MEGHLAYA" },
                { "18", "ASSAM" },
                { "19", "WEST BENGAL" },
                { "20", "JHARKHAND" },
                { "21", "ODISHA" },
                { "22", "CHATTISGARH" },
                { "23", "MADHYA PRADESH" },
                { "24", "GUJARAT" },
                { "25", "DAMAN & DIU" },
                { "26", "DADRA & NAGAR HAVELI" },
                { "27", "MAHARASHTRA" },
                { "28", "ANDHRA PRADESH(BEFORE DIVISION)" },
                { "29", "KARNATAKA" },
                { "30", "GOA" },
                { "31", "LAKSHWADEEP" },
                { "32", "KERALA" },
                { "33", "TAMIL NADU" },
                { "34", "PUDUCHERRY" },
                { "35", "ANDAMAN AND NICOBAR ISLANDS" },
                { "36", "TELANGANA" },
                { "37", "ANDHRA PRADESH (NEW)" },
                { "38", "Ladakh" }
            };

            return pairs.OrderBy(a => a.Value).ToDictionary(b => b.Key, b => b.Value);
        }

        public static long TimeStamp(DateTime DTobj)
        {
            try
            {
                return ((DTobj.Ticks - 621355968000000000) / 10000);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}