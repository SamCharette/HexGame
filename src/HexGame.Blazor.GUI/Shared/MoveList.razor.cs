using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Engine;
using Microsoft.AspNetCore.Components;

namespace HexGame.Blazor.GUI.Shared
{
    public partial class MoveList
    {
        [Parameter] public List<Move> Moves { get; set; }
    }
}
