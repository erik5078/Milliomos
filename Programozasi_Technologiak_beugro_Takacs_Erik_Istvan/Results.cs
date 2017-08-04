using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wpf_beadando
{
    class Results
    {
        public int index;
        public List<string> result;

        public Results(int index, List<string> result)
        {
            this.index = index;
            this.result = result;
        }
    }
}
