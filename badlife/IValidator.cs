using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace badlife
{
    public interface IValidator
    {
        bool ContainInvalidChar(string line);
        bool HasInvalidLength(string[] lines);
        
    }
}
