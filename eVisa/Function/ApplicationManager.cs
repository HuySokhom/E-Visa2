using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eVisa.Function
{
    public class ApplicationManager
    {
        public static Dictionary<int, string> LanguageList
        {
            get
            {
                return new Dictionary<int, string>
                {
                    {(int)Language.Khmer,"kh"},
                    {(int)Language.English,"en"},
                };
            }
        }

        public static Dictionary<int, string> getMessage
        {
            get
            {
                return new Dictionary<int, string>
                {
                    {1,"Invalid image size !"}
                };
            }
            
        }

        public enum Language
        {
            English =1,
            Khmer
            
        }
    }
}