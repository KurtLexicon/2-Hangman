using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Reflection;

namespace ConsoleApp4
{
    internal class Trans
    {
        public enum Lang
        {
            en, sv,
        }

        private Lang lang;
        public Trans(Lang lang)
        {
            this.lang = lang;
        }

        private class LangTransItem
        {
            Lang lang { get; }
            string text { get; }
            public LangTransItem(Lang lang, string text)
            {
                this.lang = lang;
                this.text = text;
            }
        }

        public string t(string text)
        {
            return "";
        }

        Dictionary<Lang, Dictionary<string, string>> AllItems = new() {
            { Lang.en, new() }
         };
    }
}

