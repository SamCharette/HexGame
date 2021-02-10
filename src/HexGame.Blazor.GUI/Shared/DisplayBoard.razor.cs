using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace HexGame.Blazor.GUI.Shared
{
    public partial class DisplayBoard
    {
        [Parameter] public int Size { get; set; }
        public List<DisplayHex> HexList { get; set; }

    }
}
