using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace badlife
{
    public class Validator : IValidator
    {
        private const char aliveChar = '*';
        private const char deadChar = '_';
        public bool ContainInvalidChar(string line)
        {
            if (line.Where(x => x != aliveChar && x != deadChar).Count() > 0)
                return true;
            return false;
        }

        public bool HasInvalidLength(string[] lines)
        {
            if (lines.Select(x => x.Length).GroupBy(x => x).Count() > 1)
                return true;
            return false;

        }
    }
}
