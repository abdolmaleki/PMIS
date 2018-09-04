using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMISUtility.DictionaryFactory
{
    public class DictionaryFactory
    {
        static Dictionary<int, string> dictionary;
        public static Dictionary<int, string> getRegistryType()
        {
            dictionary = new Dictionary<int, string>();
            dictionary.Add(1, "رسمی");
            dictionary.Add(2, "قراردادی");
            dictionary.Add(3, "پیمانی");
            dictionary.Add(4, "قراردادی ساعتی");
            dictionary.Add(5, "سایر");

            return dictionary;
        }
    }
}
