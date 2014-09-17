using System;
using System.Collections.Generic;
using System.Text;
using Tekla.Structures.Model;
using Tekla.Structures;
using System.Collections;

namespace TeklaBeans
{
    public class PrecastBeamSpan : Part
    {
        private string spanID;
        private string name;
        private Beam beam;
        public PrecastBeamSpan(string spanID)
        {
            this.spanID = spanID;
        }
        public PrecastBeamSpan(string spanID, string name)
        {
            this.spanID = spanID;
            this.name = name;
            this.Name = name;
        }
        public PrecastBeamSpan(string spanID, string name, Beam beam)
        {
            this.spanID = spanID;
            this.name = name;
            this.Name = name;
            this.beam = beam;
        }
        public string getName()
        {
            return this.name;
        }
        public void setName(string name)
        {
            this.name = name;
        }
        public string getSpanID()
        {
            return this.spanID;
        }
        public void setSpanID(string spanID)
        {
            this.spanID = spanID;
        }
        public Beam getBeam()
        {
            return this.beam;
        }
        public void SetBeam(Beam beam)
        {
            this.beam = beam;
        }

        public override bool Insert()
        {
            return beam.Insert();
        }
        public override bool Delete()
        {
            throw new Exception("The method or operation is not implemented.");
        }
        public override bool Modify()
        {
            return beam.Modify();
        }
        public override bool Select()
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
