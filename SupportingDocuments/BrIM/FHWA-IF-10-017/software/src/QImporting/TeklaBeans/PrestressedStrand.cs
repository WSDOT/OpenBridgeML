using System;
using System.Collections.Generic;
using System.Text;
using Tekla.Structures.Model;
using Tekla.Structures;

namespace TeklaBeans
{
    public class PrestressedStrand
    {
        private string materialString;
        private string name;
        private int rowNum;
        private int colNum;
        private Point stPoint;
        private Point enPoint;
        public PrestressedStrand()
        {
        }
        public PrestressedStrand(string name)
        {
            this.name = name;
        }
        public int getRowNum()
        {
            return this.rowNum;
        }
        public int getColNum()
        {
            return colNum;
        }
        public void setRowNum(int rowNum)
        {
            this.rowNum = rowNum;
        }
        public void setColNum(int colNum)
        {
            this.colNum = colNum;
        }
        public string getMaterialString()
        {
            return this.materialString;
        }
        public void setMaterialString(string materialString)
        {
            this.materialString = materialString;
        }
        public Point getStPoint()
        {
            return stPoint;
        }
        public Point getEnPoint()
        {
            return enPoint;
        }
        public void setStPoint(Point point)
        {
            this.stPoint = point;
        }
        public void setEnPoint(Point point)
        {
            this.enPoint = point;
        }
    }
}
