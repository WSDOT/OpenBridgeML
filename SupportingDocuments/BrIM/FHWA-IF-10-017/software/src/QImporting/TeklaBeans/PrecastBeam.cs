using System;
using System.Collections.Generic;
using System.Text;
using Tekla.Structures.Model;
using Tekla.Structures;
using System.Collections;

namespace TeklaBeans
{
    public class PrecastBeam : Part
    {
        private string name;
        private ArrayList beamSpanList;

        public string getName()
        {
            return this.name;
        }
        public void setName(string name)
        {
            this.name = name;
        }
        public ArrayList getBeamSpanList()
        {
            return this.beamSpanList;
        }
        public void setBeamSpanList(ArrayList beamSpanList)
        {
            this.beamSpanList = beamSpanList;
        }
        public void addBeamSpan(PrecastBeamSpan beamSpan)
        {
            if (null == this.beamSpanList)
            {
                this.beamSpanList = new ArrayList();
            }
            this.beamSpanList.Add(beamSpan);
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
