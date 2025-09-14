using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrammarReader.Code.Class
{
    public class GrammarToken(int index, int line)
    {
        public int Index { get; } = index;
        public int Line { get; } = line;

    }
}
