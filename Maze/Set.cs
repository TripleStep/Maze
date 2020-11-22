//#define VALIDATE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Maze
{
    public class ArrayOfSets
    {
        int[] setIDs;
        static int nextSetID = 101;
        Dictionary<int, List<int>> positionsOfSetID = new Dictionary<int, List<int>>();

        public ArrayOfSets(int len)
        {
            setIDs = new int[len];
            for (int pos = 0; pos < len; ++pos)
            {
                int id = NextSetID();
                setIDs[pos] = id;
                List<int> list = new List<int>();
                list.Add(pos);
                positionsOfSetID[id] = list;
            }
#if VALIDATE
            Validate();
#endif
        }

        public int this[int i]
        {
            get
            {
                return setIDs[i];
            }
            set
            {
                {
                    int oldID = setIDs[i];
                    List<int> list = positionsOfSetID[oldID];
                    list.Remove(i);
                    if (list.Count == 0)
                        positionsOfSetID.Remove(oldID);
                }
                setIDs[i] = value;
                {
                    List<int> list;
                    if (!positionsOfSetID.TryGetValue(value, out list))
                        positionsOfSetID[value] = (list = new List<int>());
                    list.Add(i);
                }
#if VALIDATE
                Validate();
#endif
            }
        }

        public List<int> SetIDs
        {
            get
            {
                List<int> ret = new List<int>();
                ret.AddRange(positionsOfSetID.Keys);
                return ret;
            }
        }

        public int[] PositionsOf(int id)
        {
            List<int> list;
            if (positionsOfSetID.TryGetValue(id, out list))
                return list.ToArray();
            return new int[0];
        }

        public void JoinAt(int pos0, int pos1)
        {
            JoinSets(setIDs[pos0], setIDs[pos1]);
        }

        public void JoinSets(int id0, int id1)
        {
            List<int> list0 = positionsOfSetID[id0];
            List<int> list1 = positionsOfSetID[id1];
            foreach (int i in list1)
                setIDs[i] = id0;
            list0.AddRange(list1);
            positionsOfSetID.Remove(id1);
#if VALIDATE
            Validate();
#endif
        }

        static int NextSetID()
        {
            unchecked
            {
                return nextSetID++;
            }
        }

#if VALIDATE
        void Validate()
        {
            int t = 0;
            foreach (KeyValuePair<int, List<int>> kvp in positionsOfSetID)
                t += kvp.Value.Count;
            Trace.Assert(t == setIDs.Length);
            foreach (KeyValuePair<int, List<int>> kvp in positionsOfSetID)
                foreach (int pos in kvp.Value)
                    Trace.Assert(setIDs[pos] == kvp.Key);
        }
#endif
    }
}
