using System;
using System.Collections.Generic;
using System.Text;
using Tekla.Structures.Model;
using Tekla.Structures;

namespace TeklaBeans
{
    public class IBeam : Part
    {
        private Beam beam;
        public IBeam()
        {
            beam = new Beam();
        }
        public IBeam( Point stPoint , Point enPoint )
        {
            beam = new Beam(stPoint, enPoint);
        }
        public override bool Insert()
        {
            throw new Exception("The method or operation is not implemented.");
        }
        public override bool Delete()
        {
            throw new Exception("The method or operation is not implemented.");
        }
        public override bool Modify()
        {
            throw new Exception("The method or operation is not implemented.");
        }
        public override bool Select()
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
