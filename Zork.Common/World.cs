using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Microsoft.CodeAnalysis.CSharp.Scripting;

namespace Zork
{
    public class World
    {
        public HashSet<Room> Rooms { get; set; }

        [JsonIgnore]
        public IReadOnlyDictionary<string, Room> RoomsByName => new IReadOnlyDictionary<string, Room>(mRoomsByName);

        public Player SpawnPlayer() => new Player(this, StartingLocation);

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            mRoomsByName = Rooms.ToDictionary(room => room.Name, room => room);

            foreach(Room room in Rooms)
            {
                room.UpdateNeighbors(this);
            }
        } 

        [JsonProperty]
        private string StartingLocation { get; set; }

        private Dictionary<string, Room> mRoomsByName;
    }
}
