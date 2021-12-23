using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days.Day23
{
    /// <summary>
    /// A state of the hallways/rooms.
    /// </summary>
    public class State
    {
        /// <summary>
        /// The character used for an empty space.
        /// </summary>
        const char EMPTY_SPACE = '.';

        /// <summary>
        /// The costs of moving each type of amphipod
        /// </summary>
        public static readonly Dictionary<char, int> Costs = new Dictionary<char, int>{
            { 'A', 1 },
            { 'B', 10 },
            { 'C', 100 },
            { 'D', 1000 }
        };

        /// <summary>
        /// Where each room entrance is in the hallway.
        /// </summary>
        public static readonly int[] RoomEntrances = { 2, 4, 6, 8 };

        /// <summary>
        /// The current state of the hallway.
        /// </summary>
        public char[] Hallway { get; }

        /// <summary>
        /// The current state of the rooms.
        /// </summary>
        public List<char>[] Rooms { get; }
        
        /// <summary>
        /// How many spots there are in each room.
        /// </summary>
        public int RoomSize { get; }

        /// <summary>
        /// Constructor based on string inputs.
        /// </summary>
        /// <param name="inputLines">The inputs.</param>
        /// <param name="roomSize">The number of spots in each room.</param>
        public State(string[] inputLines, int roomSize)
        {
            // All positions in the hallway, even if some of them can't be occupied.
            // They should all be empty initially.
            Hallway = new char[11];
            for(int i = 0; i < Hallway.Length; i++)
            {
                Hallway[i] = EMPTY_SPACE;
            }

            // Create the lists for the rooms.
            Rooms = new List<char>[4];
            for(int i = 0;i < Rooms.Length; i++)
            {
                Rooms[i] = new List<char>();
            }
            RoomSize = roomSize;

            // Get what's in each room from the input strings.
            for(int i = 0; i < RoomSize; i++)
            {
                var line = 1 + RoomSize - i;
                for (int j = 0; j < Rooms.Length; j++)
                {
                    Rooms[j].Add(inputLines[line][3 + j * 2]);
                }
            }
        }

        /// <summary>
        /// Constructor based on information already parsed from the input string.
        /// </summary>
        /// <param name="hallway">The state of the hallway.</param>
        /// <param name="rooms">The state of the rooms.</param>
        /// <param name="roomSize">The size of the rooms.</param>
        public State(char[] hallway, List<char>[] rooms, int roomSize)
        {
            Hallway = hallway;
            Rooms = rooms;
            RoomSize = roomSize;
        }

        /// <summary>
        /// If all of the amphipods are in the correct places.
        /// </summary>
        /// <returns>True if they're all where they should be, false otherwise.</returns>
        public bool IsComplete()
        {
            for(int i = 0; i < Rooms.Length; i++)
            {
                if(Rooms[i].Count != RoomSize || !RoomOnlyContainsCorrectAmphipods(i)) return false;
            }
            return true;
        }

        /// <summary>
        /// If a specific room only contains the amphipods that should be there at the end.
        /// 
        /// This doesn't mean they're all there, just that there's no incorrect ones in the room.
        /// </summary>
        /// <param name="roomIndex">The room to check.</param>
        /// <returns>True if there are only correct amphipods in the room.</returns>
        protected bool RoomOnlyContainsCorrectAmphipods(int roomIndex)
        {
            var room = Rooms[roomIndex];
            return !room.Any(c => c != 'A' + roomIndex);
        }

        /// <summary>
        /// The length of the path between a hallway spot and the top empty spot in a room.
        /// 
        /// If there is no valid path returns -1
        /// </summary>
        /// <param name="hallway">The position in the hallway.</param>
        /// <param name="room">The room.</param>
        /// <returns>The length of the path or -1 if there is no valid path.</returns>
        public int PathLength(int hallway, int room)
        {
            int roomEntrance = 2 + 2 * room;

            for(int i = Math.Min(hallway, roomEntrance); i < Math.Max(hallway, roomEntrance); ++i)
            {
                // -1 if the path isn't clear.
                if (i != hallway && Hallway[i] != '.') return -1;
            }
            // Otherwise the path length is the distance through the hallway and the room.
            return Math.Abs(hallway - roomEntrance) + RoomSize - Rooms[room].Count;
        }

        /// <summary>
        /// Make a deep copy of the rooms list.
        /// </summary>
        /// <returns>A copy of the rooms list.</returns>
        public List<char>[] CloneRooms()
        {
            List<char>[] rooms = new List<char>[Rooms.Length];
            for(int i = 0; i < Rooms.Length; i++)
            {
                rooms[i] = new(Rooms[i]);
            }
            return rooms;
        }

        /// <summary>
        /// Get valid moves from the current state.
        /// 
        /// If anything in the hallway can be moved into a room it will only return that as those are the obvious moves to make.
        /// </summary>
        /// <returns>A list of valid moves from the current state.</returns>
        public (State State, int Cost)[] GetValidMoves()
        {
            // Check if anything in the hallway can be moved into a room.
            for (int i = 0; i < Hallway.Length; i++)
            {
                if (Hallway[i] != EMPTY_SPACE)
                {
                    // The only room they can move into is their final destination.
                    var room = Hallway[i] - 'A';
                    var pathLength = PathLength(i, room);
                    if (RoomOnlyContainsCorrectAmphipods(room) &&
                    pathLength != -1)
                    {

                        // Make copies of the room and hallway states.
                        var newRooms = CloneRooms();
                        var newHallway = (char[])Hallway.Clone();
                        // Move the amphipod from the hallway into the room in the copies.
                        newRooms[room].Add(Hallway[i]);
                        newHallway[i] = EMPTY_SPACE;
                        var newState = new State(newHallway, newRooms, RoomSize);

                        return new (State, int)[] { (newState, pathLength * Costs[Hallway[i]]) };
                    }
                }
            }

            List<(State, int)> validMoves = new();

            // Find all ways something could be moved out of a room.
            for(int room = 0; room < Rooms.Length; room++)
            {
                // Never move something out of a room that's arranged properly.
                if (!RoomOnlyContainsCorrectAmphipods(room))
                {
                    for (int hallway = 0; hallway < Hallway.Length; hallway++)
                    {
                        if (!RoomEntrances.Contains(hallway) && Hallway[hallway] == '.')
                        {
                            var pathLength = PathLength(hallway, room);

                            if (pathLength != -1)
                            {
                                // The path length calculation only considers the empty spaces in the room. Add one to deal with the fact that we're moving something out of a filled space.
                                ++pathLength;

                                // Create copies of the room and hallway states.
                                var newRooms = CloneRooms();
                                var newHallway = (char[])Hallway.Clone();

                                // Move from the room into the hallway.
                                newHallway[hallway] = newRooms[room][^1];
                                newRooms[room].RemoveAt(newRooms[room].Count - 1);

                                var newState = new State(newHallway, newRooms, RoomSize);

                                validMoves.Add((newState, pathLength * Costs[newHallway[hallway]]));
                            }
                        }
                    }
                }
            }

            return validMoves.ToArray();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            StringBuilder sb = new();
            sb.Append("H:");
            for(int hallway = 0; hallway < Hallway.Length; hallway++)
            {
                sb.Append(Hallway[hallway]);
            }
            for(int room = 0; room < Rooms.Length; room++)
            {
                sb.Append($"R{room}:");
                for(int i = 0; i < Rooms[room].Count; i++)
                {
                    sb.Append(Rooms[room][i]);
                }
            }
            return sb.ToString();
        }
    }
}
