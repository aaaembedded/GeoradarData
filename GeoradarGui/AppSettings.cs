using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web.Script.Serialization; // Turn it on in  References item in your Solution Explorer

namespace GeoradarGui
{
    public class AppSettings<T> where T : new()
    {
        private const string DEFAULT_FILENAME = "app_settings.json";

        public void Save(string fileName = DEFAULT_FILENAME)
        {
            File.WriteAllText(fileName, (new JavaScriptSerializer()).Serialize(this));
        }

        public void SaveAdd(string fileName = DEFAULT_FILENAME)
        {
            File.AppendAllText(fileName, (new JavaScriptSerializer()).Serialize(this) + "\n");
        }

        public static void Save(T pSettings, string fileName = DEFAULT_FILENAME)
        {
            File.WriteAllText(fileName, (new JavaScriptSerializer()).Serialize(pSettings));
        }

        public static T Load(string fileName = DEFAULT_FILENAME)
        {
            T t = new T();
            if (File.Exists(fileName))
                t = (new JavaScriptSerializer()).Deserialize<T>(File.ReadAllText(fileName));
            return t;
        }
    }
}
