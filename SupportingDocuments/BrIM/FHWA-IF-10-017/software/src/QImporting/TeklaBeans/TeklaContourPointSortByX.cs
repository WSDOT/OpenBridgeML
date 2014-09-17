using System;
using System.Collections.Generic;
using System.Text;
using Tekla.Structures.Model;
using System.Collections;

namespace TeklaBeans
{
    public class TeklaContourPointSortByX : IComparer
    {
        int IComparer.Compare(object x, object y)
        {
            ContourPoint p1 = (ContourPoint)x;
            ContourPoint p2 = (ContourPoint)y;
            return (Convert.ToInt32(p1.X - p2.X));
        }
    }
}
