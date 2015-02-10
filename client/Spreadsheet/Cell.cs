// Cell class written by Taylor Wilson, U0323893
// CS 3500, Fall 2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpreadsheetUtilities;
using SS;

namespace SS
{
    /// The value of a cell can be (1) a string, (2) a double, or (3) a FormulaError.  
    /// (By analogy, the value of an Excel cell is what is displayed in that cell's position
    /// in the grid.)
    /// 
    /// If a cell's contents is a string, its value is that string.
    /// 
    /// If a cell's contents is a double, its value is that double.
    /// 
    /// If a cell's contents is a Formula, its value is either a double or a FormulaError,
    /// as reported by the Evaluate method of the Formula class.  The value of a Formula,
    /// of course, can depend on the values of variables.  The value of a variable is the 
    /// value of the spreadsheet cell it names (if that cell's value is a double) or 
    /// is undefined (otherwise).
    class Cell
    {
        // Properties to hold a content and value of the Cell
        private object contents;
        private object val;
        private Func<string, double> look;

        public Cell(object _content, Func<string,double> _look)
        {
            contents = _content;
            look = _look;
            Evaluate();
        }

        public object Content
        {
            get { return contents; }
            set { contents = value; }
        }

        public object Value
        {
            get { return val; }
            set { val = value; }
        }

        internal void Evaluate()
        {
            if (!(contents is Formula))
            {
                val = contents;
            }
            else
            {
                Formula f = (Formula)contents;
                val = f.Evaluate(look);
            }
        }
    }
}
