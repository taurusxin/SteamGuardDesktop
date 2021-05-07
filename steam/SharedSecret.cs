using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamGuardDesktop
{
    public class SharedSecret
    {
        public SharedSecret()
        {

        }
        public SharedSecret(string content)
        {
            this.SharedSecretString = content;
        }
        public string SharedSecretString = string.Empty;
    }
}
