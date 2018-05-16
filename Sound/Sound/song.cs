using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sound
{
    class song
    {
        public string name, line;
        public int duration;
        public song()
        {
            name = line = "";
            duration = 0;
        }

        public song(string _name, string _line, int _duration)
        {
            name = _name;
            line = _line;
            duration = _duration;
        }
    }
}
