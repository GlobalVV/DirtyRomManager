using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirtyRomManager
{
    interface IGDBCommInterface
    {
        void IgdbCommunicator();
        void setKey(string k);
        string getKey();
        List<string> getGame(string name);
    }
}
