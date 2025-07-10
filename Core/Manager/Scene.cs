using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Core.Manager
{
    public interface Scene
    {
        public void Init();
        public void Update();
        public void Release();
    }
}
