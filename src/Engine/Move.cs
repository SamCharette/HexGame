using System;
using System.Collections.Generic;
using Engine.ValueTypes;

namespace Engine
{
    public class Move
    {
        public Coordinates Coordinates { get; }
        public PlayerNumber PlayerNumber { get; }
        public List<Log> Logs { get; set; } 

        public Move(int x, int y, PlayerNumber playerNumber, List<Log> logs = null)
        {
            Coordinates = new Coordinates(x, y);
            PlayerNumber = playerNumber;
            Logs = logs ?? new List<Log>();
        }

        public Move(Coordinates coordinates, PlayerNumber playerNumber, List<Log> logs = null)
        {
            Coordinates = coordinates;
            PlayerNumber = playerNumber;
            Logs = logs ?? new List<Log>();
        }

        public void AddLog(string text)
        {
            var log = new Log(DateTime.Now, text);
            AddLog(log);
        }
        public void AddLog(Log log)
        {
            Logs.Add(log);
        }

        public override string ToString()
        {
            return Coordinates + " : Player #" + PlayerNumber;
        }

        public bool Equals(Move other)
        {
            return Coordinates.Equals(other.Coordinates) && PlayerNumber == other.PlayerNumber;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Coordinates, (int) PlayerNumber);
        }
    }
}
