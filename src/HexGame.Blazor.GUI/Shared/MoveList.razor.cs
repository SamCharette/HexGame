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
        public List<Move> Moves { get; set; } = new List<Move>();

        public void AddMove(Move move)
        {
            Moves.Add(move);
        }
    }
}
