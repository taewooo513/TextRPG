using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;


namespace TextRPG.Core
{
    public class Singleton<T> where T : new()
    {
        private static T instance;

        static public T GetInstance()
        {
            if (instance == null)
            {
                instance = new T();
            }
            return instance;
        }
    }
}
