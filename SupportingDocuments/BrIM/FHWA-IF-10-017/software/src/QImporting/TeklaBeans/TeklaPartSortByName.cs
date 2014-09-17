using System;
using System.Collections.Generic;
using System.Text;
using Tekla.Structures.Model;
using System.Collections;

namespace TeklaBeans
{
    public class TeklaPartSortByName : IComparer
    {
        int IComparer.Compare(object x, object y)
        {
            Part part1 = (Part)x;
            Part part2 = (Part)y;
            string name1 = part1.Name;
            string name2 = part2.Name;
            int index1 = name1.IndexOf("_", name1.Length - 3) + 1;
            int index2 = name2.IndexOf("_", name2.Length - 3) + 1;

            int i1 = Convert.ToInt32(name1.Substring(index1, name1.Length - index1));
            int i2 = Convert.ToInt32(name2.Substring(index2, name2.Length - index2));
            return (i1 - i2);
        }
    }
}
