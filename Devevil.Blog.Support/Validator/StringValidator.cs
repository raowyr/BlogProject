using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Devevil.Blog.Support.Validator
{
    public class StringValidator
    {
        public static bool CheckIsValidMail(string prmEmail)
        {
            if (!String.IsNullOrEmpty(prmEmail))
                return Regex.IsMatch(prmEmail, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            else
                return false;
        }
    }
}
