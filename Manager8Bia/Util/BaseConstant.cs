using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager8Bia
{
    public enum TABLE
    {
        [Description("BÀN 1")]
        TableOne,
        [Description("BÀN 2")]
        TableTwo,
        [Description("BÀN 3")]
        TableThree,
    }
    public static class BaseConstant
    {
        public const double PERCENT = 0.69;
        public static readonly Dictionary<string, string> UNITS = new Dictionary<string, string>
        {
            { "chai","Chai" },
            { "lo","Lọ" },
            { "goi","Gói" },
            { "tui","Túi" },
            { "hop","Hộp" },
            { "ban","Bàn" },
            { "bat","Bát" },
            { "chiec","Chiếc" }, ///
            { "kg","Kgam" },
            { "gam","Gam" },
        };

        public static readonly Dictionary<string, string> DAYOFWEEKS = new Dictionary<string, string>
        {
            { "Monday","Thứ 2" },
            { "Tuesday","Thứ 3" },
            { "Wednesday","Thứ 4" },
            { "Thursday","Thứ 5" },
            { "Friday","Thứ 6" },
            { "Saturday","Thứ 7" },
            { "Sunday","Chủ nhật" },
        };

        private static readonly string FOLDER_DATA = "data/";
        private static readonly string FILEFOLDER_CATEGORY = "data/category.json";
        private static readonly string FOLDER_DAY = "data/days/";
        private static readonly string FOLDER_MONTH = "data/months/";
        private static readonly string PATH_BIN = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Substring(6);

        public static readonly string PATH_DAY = $"{PATH_BIN}/{FOLDER_DAY}";
        public static readonly string PATH_MONTH = $"{PATH_BIN}/{FOLDER_MONTH}";
        public static readonly string PATH_DATA = $"{PATH_BIN}/{FOLDER_DATA}";
        public static readonly string PATH_CATEGORY = $"{PATH_BIN}/{FILEFOLDER_CATEGORY}";
        public const string TABLE_ID = "e5e35320-e751-48ae-ae19-31221c0a87f8";
        public const string COLOR_YELLOW = "#fe6e00";
        public const string COLOR_GREEN = "#40a33c";
        public const string COLOR_RED = "#ce1f1f";
        public const string COLOR_GRAY = "#464646";

        
    }
}
