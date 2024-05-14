using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SplitWise.Model.RequestModels
{
    public class TokenResponse
    {
        public string? Token { get; set; }
        public Profiles? Profiles { get; set; }
    }
}
