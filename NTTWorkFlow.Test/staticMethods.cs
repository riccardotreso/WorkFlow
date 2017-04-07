using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTWorkFlow.Test
{
    public class staticMethods
    {
        public static bool condition(int id = 0) {
            return (new Random().Next() * 100) % 2 == 0;
        }
    }
}
