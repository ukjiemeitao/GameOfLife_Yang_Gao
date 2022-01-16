using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace badlife
{
    public interface IGameOfLife
    {
        void InitializeWorld(string input);
        void Evolve();
        void OutputNewWorld();

    }
}
