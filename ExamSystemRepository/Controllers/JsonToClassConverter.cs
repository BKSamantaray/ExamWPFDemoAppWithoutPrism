using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ExamSystemRepository.Controllers
{
    public class JsonToClassConverter
    {
        public T ConvertJsonToClassObject<T>(string filePath)
        {
            T tObj = default(T);
            if (!string.IsNullOrEmpty(filePath))
            {
                try
                {
                    string jsonObj = File.ReadAllText(filePath);
                    if (!string.IsNullOrEmpty(jsonObj))
                    {
                        JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                        tObj = javaScriptSerializer.Deserialize<T>(jsonObj);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return tObj;
        }
    }
}
