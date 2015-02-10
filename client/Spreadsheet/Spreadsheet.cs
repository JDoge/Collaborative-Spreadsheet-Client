/*
 * Client-Server Spreadsheet
 * CS3505
 * Spring 2014
 * 
 * Team Skyntax
 * Zach Lobato, Jared Potter, and Taylor Wilson
 * 
 * Based on the CS3500 project written by Taylor Wilson.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using SpreadsheetUtilities;
using System.Xml;
using CustomNetworking;
using System.Net.Sockets;
using System.Threading;

namespace SS
{



    /// <summary>
    /// Defines the implementation of an AbstractSpreadsheet.
    /// Represents a Spreadsheet of infinite number of cells.
    /// </summary>
    public class Spreadsheet : AbstractSpreadsheet
    {




        // Create two objects, one to hold a grid of cells and one to keep track of dependencies
        private Dictionary<string, Cell> grid;
        private SpreadsheetUtilities.DependencyGraph dg;

        // Use a flag to keep track of saved status of this spreadsheet
        private bool hasChanged;

        /// <summary>
        /// Construct an empty Spreadsheet and initialize fields.
        /// </summary>
        public Spreadsheet()
            : this(v => true, s => s, "default")
        {
            // Empty code. This constructor inherits another constructor which will do the work.
        }

        /// <summary>
        /// Construct an empty Spreadsheet and initialize fields.
        /// </summary>
        /// <param name="isValid"></param>
        /// <param name="normalize"></param>
        /// <param name="version"></param>
        public Spreadsheet(Func<string, bool> isValid, Func<string, string> normalize, string version)
            : base(isValid, normalize, version)
        {
            // Instantiate fields
            grid = new Dictionary<string, Cell>();
            dg = new SpreadsheetUtilities.DependencyGraph();
            hasChanged = false;
        }

        /// <summary>
        /// Construct an empty Spreadsheet and initialize fields.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="isValid"></param>
        /// <param name="normalize"></param>
        /// <param name="version"></param>
        public Spreadsheet(string filename, Func<string, bool> isValid, Func<string, string> normalize, string version)
            : base(isValid, normalize, version)
        {
            // Instantiate fields
            grid = new Dictionary<string, Cell>();
            dg = new SpreadsheetUtilities.DependencyGraph();

            // Initialize reader settings
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;
            settings.CloseInput = true;

            // Catch any error and continue to throw its message
            try
            {
                using (XmlReader reader = XmlReader.Create(filename, settings))
                {
                    // Validate proper version first
                    reader.ReadToFollowing("spreadsheet");
                    if (!reader.GetAttribute("version").Equals(version))
                    {
                        throw new SpreadsheetReadWriteException("Invalid version. " +
                            "Spreadsheet constructor version does not match version read from .xml file.");
                    }

                    // Condition of loop advances reader to beginning of "cell" element
                    while (reader.Read())
                    {
                        if (reader.IsStartElement())
                        {
                            // Advance reader to beginning of "name"
                            reader.Read();
                            if (reader.IsStartElement())
                            {
                                reader.ReadStartElement("name");
                                string name = reader.ReadString();
                                reader.ReadEndElement();
                                reader.ReadStartElement("contents");
                                string contents = reader.ReadString();

                                // Construct a new cell based on "name" and "contents" element of XML file
                                //     Normalization of string occurs in SetContents method; do not normalize here!
                                SetContentsOfCell(name, contents);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new SpreadsheetReadWriteException(e.Message);
            }

            hasChanged = false;   // At this point the spreadsheet should be considered unchanged
        }

        /// <summary>
        /// Enumerates the names of all the non-empty cells in the spreadsheet.
        /// </summary>
        public override IEnumerable<string> GetNamesOfAllNonemptyCells()
        {
            // Return every cell object within the grid as an IEnumerable
            foreach (KeyValuePair<string, Cell> kvp in grid)
            {
                yield return kvp.Key;
            }
        }

        /// <summary>
        /// If name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, returns the contents (as opposed to the value) of the named cell.  The return
        /// value should be either a string, a double, or a Formula.
        /// </summary>
        public override object GetCellContents(string name)
        {
            // Check and normalize inputs; throw any necessary exceptions
            name = CheckInputs(name);

            // If the cell already exists, simply return its content
            Cell v;
            if (grid.TryGetValue(name, out v))
            {
                return v.Content;
            }

            // If the cell did not yet exist, create a cell and set its content to the empty string
            else
            {
                return "";    // If a cell's contents or an empty string, then the cell is empty
            }
        }

        /// <summary>
        /// If content is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, if content parses as a double, the contents of the named
        /// cell becomes that double.
        /// 
        /// Otherwise, if content begins with the character '=', an attempt is made
        /// to parse the remainder of content into a Formula f using the Formula
        /// constructor.  There are then three possibilities:
        /// 
        ///   (1) If the remainder of content cannot be parsed into a Formula, a 
        ///       SpreadsheetUtilities.FormulaFormatException is thrown.
        ///       
        ///   (2) Otherwise, if changing the contents of the named cell to be f
        ///       would cause a circular dependency, a CircularException is thrown.
        ///       
        ///   (3) Otherwise, the contents of the named cell becomes f.
        /// 
        /// Otherwise, the contents of the named cell becomes content.
        /// 
        /// If an exception is not thrown, the method returns a set consisting of
        /// name plus the names of all other cells whose value depends, directly
        /// or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        public override ISet<string> SetContentsOfCell(string name, string content)
        {
            name = CheckInputs(name, content);  // Check and normalize inputs; throw any necessary exceptions

            // If the number is a double then the contents of the cell "name" become that double
            if (IsDouble(content))
            {
                // Update the values of all changed cells and return the set of all changed cells
                ISet<string> doubleCells = SetCellContents(name, double.Parse(content));
                UpdateCells(doubleCells);
                hasChanged = true;  // Modify saved status of this spreadsheet
                return doubleCells;
            }

            // Otherwise, check first if first character of content is '='
            // Remember that ElementAt uses type char!!!
            if (content.Length > 0 && content.ElementAt(0) == '=')
            {
                // Update the values of all changed cells and return the set of all changed cells
                ISet<string> formulaCells = SetCellContents(name, new Formula(content.Substring(1), Normalize, IsValid));
                UpdateCells(formulaCells);
                hasChanged = true;  // Modify saved status of this spreadsheet
                return formulaCells;
            }

            // Only option left is to keep content string the way it is and store it in the cell
            // Update the values of all changed cells and return the set of all changed cells
            ISet<string> stringCells = SetCellContents(name, content);
            UpdateCells(stringCells);
            hasChanged = true;  // Modify saved status of this spreadsheet
            return stringCells;
        }


        /// <summary>
        /// Updates the value of cells received via the parameter.
        /// </summary>
        /// <param name="cells">Cells whose value to update.</param>
        private void UpdateCells(ISet<string> cells)
        {
            Cell c;
            foreach (string name in cells)
            {
                if (grid.TryGetValue(name, out c))
                {
                    //Thread.Sleep(400);
                    c.Evaluate();   // Update each cell's value in the set
                }
            }
        }

        // MODIFIED PROTECTION FOR PS5
        /// <summary>
        /// If name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, the contents of the named cell becomes number.  The method returns a
        /// set consisting of name plus the names of all other cells whose value depends, 
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        protected override ISet<string> SetCellContents(string name, double number)
        {
            // If the cell currently exists then replace its content and perform spreadsheet maintanence
            Cell v;
            if (grid.TryGetValue(name, out v))
            {
                v.Content = number;                                 // Replace the content
                dg.ReplaceDependees(name, new HashSet<string>());   // Remove previous dependee and dependent pairs
            }
            else
            {
                v = new Cell(number, Lookup);    // If the cell did not exist then create it and return an empty set of dependants
                grid.Add(name, v);
            }

            // Manage new and changing dependencies within the grid
            LinkedList<string> dependants = (LinkedList<string>)GetCellsToRecalculate(name);
            HashSet<string> result = new HashSet<string>();
            foreach (string r in dependants)
            {
                result.Add(r);
            }

            return result;
        }

        // MODIFIED PROTECTION FOR PS5
        /// <summary>
        /// If text is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, the contents of the named cell becomes text.  The method returns a
        /// set consisting of name plus the names of all other cells whose value depends, 
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        protected override ISet<string> SetCellContents(string name, string text)
        {
            // If the cell currently exists then replace its content and perform spreadsheet maintanence
            Cell v;
            if (grid.TryGetValue(name, out v))
            {
                v.Content = text;                                  // Replace the content
                dg.ReplaceDependees(name, new HashSet<string>());  // Remove previous dependee and dependent pairs
            }
            else
            {
                v = new Cell(text, Lookup);    // If the cell did not exist then create it and return an empty set of dependants
                grid.Add(name, v);
            }

            // Manage new and changing dependencies within the grid
            LinkedList<string> dependants = (LinkedList<string>)GetCellsToRecalculate(name);
            HashSet<string> result = new HashSet<string>();
            foreach (string r in dependants)
            {
                result.Add(r);
            }

            // If the new contents are an empty string, remove the cell
            if (text.Equals(""))
            {
                grid.Remove(name);
            }

            return result;

        }

        // MODIFIED PROTECTION FOR PS5
        /// <summary>
        /// If formula parameter is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, if changing the contents of the named cell to be the formula would cause a 
        /// circular dependency, throws a CircularException.
        /// 
        /// Otherwise, the contents of the named cell becomes formula.  The method returns a
        /// Set consisting of name plus the names of all other cells whose value depends,
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        protected override ISet<string> SetCellContents(string name, SpreadsheetUtilities.Formula formula)
        {
            // If the cell currently exists then replace its content and perform spreadsheet maintanence
            Cell v = null;
            object o = null;
            if (grid.TryGetValue(name, out v))
            {
                o = v.Content;          // Keep track of the original content in case of error
                v.Content = formula;    // Replace the current content
            }
            else
            {
                v = new Cell(formula, Lookup);  // If the cell did not exist then create it and return an empty set of dependants
                grid.Add(name, v);
            }

            // Make a copy of the previous dependee and dependent pairs
            HashSet<string> old = (HashSet<string>)dg.GetDependees(name);

            // Remove previous dependee and dependent pairs
            HashSet<string> var = new HashSet<string>();
            foreach (string t in formula.GetVariables())
            {
                var.Add(t);
            }

            dg.ReplaceDependees(name, var);

            // Catch any potential circular exception
            try
            {
                // Manage new and changing dependencies within the grid
                LinkedList<string> dependants = (LinkedList<string>)GetCellsToRecalculate(name);
                HashSet<string> result = new HashSet<string>();
                foreach (string r in dependants)
                {
                    result.Add(r);
                }

                return result;
            }

            // Revert status of grid to state prior to this method
            catch (CircularException c)
            {
                dg.ReplaceDependees(name, old);   // Reset the dependees

                // If the cell existed before this method then restore its contents
                if (!ReferenceEquals(o, null))
                {
                    v.Content = o;
                }

                else
                {
                    grid.Remove(name);  // Otherwise, just remove the newly created cell because it didn't exist before
                }

                throw c;                // Continue to throw the exception
            }
        }

        /// <summary>
        /// If name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, returns the value (as opposed to the contents) of the named cell.  The return
        /// value should be either a string, a double, or a SpreadsheetUtilities.FormulaError.
        /// </summary>
        public override object GetCellValue(string name)
        {
            // Check the input
            string input = CheckInputs(name);

            Cell c;
            if (grid.TryGetValue(input, out c))
            {
                return c.Value;
            }

            return "";  // Cell does not exist
        }

        /// <summary>
        /// Function to get the value of a cell.
        /// </summary>
        /// <param name="name">Name of the cell whose value to get.</param>
        /// <returns>The value of a cell.</returns>
        public double Lookup(string name)
        {
            object o = GetCellValue(name);
            if (o is string)
            {
                throw new ArgumentException("Invalid variable. Cannot evaluate double with string.");
            }

            if (o is double)
            {
                return (double)o;
            }

            // Throw exception if a double cannot be returned. Formula will use 
            //       exception to construct a FormulaError.
            else
            {
                throw new ArgumentException("Invalid formula. Unable to evalute value of formula.");
            }
        }



        /// <summary>
        /// If name is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name isn't a valid cell name, throws an InvalidNameException.
        /// 
        /// Otherwise, returns an enumeration, without duplicates, of the names of all cells whose
        /// values depend directly on the value of the named cell.  In other words, returns
        /// an enumeration, without duplicates, of the names of all cells that contain
        /// formulas containing name.
        /// 
        /// For example, suppose that
        /// A1 contains 3
        /// B1 contains the formula A1 * A1
        /// C1 contains the formula B1 + A1
        /// D1 contains the formula B1 - C1
        /// The direct dependents of A1 are B1 and C1
        /// </summary>
        protected override IEnumerable<string> GetDirectDependents(string name)
        {
            // Throw proper exception if parameter is null
            if (ReferenceEquals(name, null))
            {
                throw new ArgumentNullException();
            }

            // Throw proper exception if parameter is invalid
            string n = Normalize(name);
            if (!IsValid(n))
            {
                throw new InvalidNameException();
            }

            return dg.GetDependents(n);
        }

        /// <summary>
        /// Returns the direct dependents of a given cell.
        /// </summary>
        /// <param name="name">The name of the parent cell.</param>
        /// <returns>A collection of dependent, children cells</returns>
        public IEnumerable<string> GetDependents(string name)
        {
            return GetDirectDependents(name);
        }

        /// <summary>
        /// True if this spreadsheet has been modified since it was created or saved                  
        /// (whichever happened most recently); false otherwise.
        /// </summary>
        public override bool Changed
        {
            get
            {
                return hasChanged;
            }
            protected set
            {
                hasChanged = value;
            }
        }

        /// <summary>
        /// Returns the version information of the spreadsheet saved in the named file.
        /// If there are any problems opening, reading, or closing the file, the method
        /// should throw a SpreadsheetReadWriteException with an explanatory message.
        /// </summary>
        public override string GetSavedVersion(string filename)
        {
            // Initialize reader settings
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;
            settings.CloseInput = true;

            try
            {
                using (XmlReader reader = XmlReader.Create(filename, settings))
                {
                    // Return the version information
                    reader.ReadToFollowing("spreadsheet");
                    return reader.GetAttribute("version");

                }
            }
            catch (Exception e)
            {
                throw new SpreadsheetReadWriteException(e.Message);
            }
        }

        /// <summary>
        /// Writes the contents of this spreadsheet to the named file using an XML format.
        /// The XML elements should be structured as follows:
        /// 
        /// <spreadsheet version="version information goes here">
        /// 
        /// <cell>
        /// <name>
        /// cell name goes here
        /// </name>
        /// <contents>
        /// cell contents goes here
        /// </contents>    
        /// </cell>
        /// 
        /// </spreadsheet>
        /// 
        /// There should be one cell element for each non-empty cell in the spreadsheet.  
        /// If the cell contains a string, it should be written as the contents.  
        /// If the cell contains a double d, d.ToString() should be written as the contents.  
        /// If the cell contains a Formula f, f.ToString() with "=" prepended should be written as the contents.
        /// 
        /// If there are any problems opening, writing, or closing the file, the method should throw a
        /// SpreadsheetReadWriteException with an explanatory message.
        /// </summary>
        public override void Save(string filename)
        {
            // Set the settings of writer to automatically indent new lines
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.WriteEndDocumentOnClose = true;

            try
            {
                using (XmlWriter w = XmlWriter.Create(filename, settings))
                {
                    // Write the header and root
                    w.WriteStartDocument();
                    w.WriteStartElement("spreadsheet");
                    w.WriteAttributeString("version", Version);

                    // Write the element of each non-empty cell to XML file
                    foreach (String name in GetNamesOfAllNonemptyCells())
                    {
                        Cell c = null;
                        if (grid.TryGetValue(name, out c))
                        {
                            w.WriteStartElement("cell");
                            w.WriteElementString("name", name);

                            // Must prepend "=" sign if the contents are a Formula
                            if (c.Content is Formula)
                            {
                                w.WriteElementString("contents", "=" + c.Content.ToString());
                            }
                            else
                            {
                                w.WriteElementString("contents", c.Content.ToString());
                            }
                            w.WriteEndElement();
                        }
                    }
                    w.WriteEndElement();  // Close the root
                    hasChanged = false;   // Mark the spreadsheet as saved
                }
            }
            catch (Exception e)
            {
                throw new SpreadsheetReadWriteException(e.Message);
            }
        }

        /// <summary>
        /// Determines if a given string pattern matches a double pattern.
        /// </summary>
        /// <param name="s">A string to compare to double pattern.</param>
        /// <returns>True, if parameter represents a double; otherwise, false.</returns>
        protected bool IsDouble(string s)
        {
            double result;
            return double.TryParse(s, out result);
        }

        /// <summary>
        /// If the input is not valid, this throws an InvalidNameException.
        /// </summary>
        /// <param name="name">The string to check for validity.</param>
        /// <returns>A normalized form of the input string name.</returns>
        protected string CheckInputs(string name)
        {
            // All cell names must match this pattern
            string pattern = @"^[a-zA-Z]+[0-9]+\d*$";

            // Throw proper exception when parameters are null or invalid
            if (ReferenceEquals(name, null) || !IsValid(name) || !(Regex.IsMatch(name, pattern)))
            {
                throw new InvalidNameException();
            }

            // Normalize name before checking for validity *********** NEW FOR PS5 ***********
            name = Normalize(name);
            if (!IsValid(name) || !(Regex.IsMatch(name, pattern)))
            {
                throw new InvalidNameException();
            }

            return name;
        }

        /// <summary>
        /// If the inputs are not valid, this throws an InvalidNameException or 
        /// throws an ArgumentNullExpection.
        /// </summary>
        /// <param name="name">The string to check for validity.</param>
        /// <param name="content_text">Another string to check against a null reference.</param>
        /// <returns>A normalized form of the input string name.</returns>
        protected string CheckInputs(string name, string content_text)
        {
            // Check for null references and validity; Throw necessary exceptions when needed
            if (ReferenceEquals(content_text, null))
            {
                throw new ArgumentNullException("Invalid content. The text was null.");
            }

            return CheckInputs(name);
        }
    }
}
