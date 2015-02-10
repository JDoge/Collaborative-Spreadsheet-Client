// Testing class written by Taylor Wilson, U0323893
// CS 3500, Fall 2013
//
// NOTE: StressTest requires 8 seconds to run on Intel i7.
// NOTE: XML reader/writer tests require specific XML files. These files are created
//       with relative paths in a particular order based on test methods. That means,
//       some testing methods are dependent on the creation of XML file in other
//       test methods.
// NOTE: Code Coverage is not 100% due to extra error checking in library class.
// NOTE: Grading tests from PS4 inserted at bottom of project but commented out. 
//       Code Coverage reflects only the code I have written.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SpreadsheetUtilities;
using SS;
using System.IO;
using System.Text;
using System.Xml;
using System.Threading;

namespace SpreadsheetTests
{
    /// <summary>
    /// Tests the functionality of the Spreadsheet.cs class and its methods.
    /// </summary>
    [TestClass]
    public class SpreadsheetTests
    {
        /// <summary>
        /// A string validation delegate for testing purposes.
        /// </summary>
        /// <param name="s">The string to check for validity.</param>
        /// <returns>True, if the input is valid; otherwise, false.</returns>
        private static bool isValid(string s)
        {
            // If the parameter matches the pattern for variable defined 
            // in the GetTokens method then consider it a proper variable
            //return Regex.IsMatch(s, @"^[a-zA-Z]+[1-9]+\d*", RegexOptions.Singleline);
            return true;
        }

        /// <summary>
        /// A string normalization delegate for testing purposes.
        /// </summary>
        /// <param name="s">The string to normalize.</param>
        /// <returns>The normalized version of the input.</returns>
        private static string normalize(string s)
        {
            // Convert the input to an all upper case output
            return s.ToUpper();
        }

        ///// <summary>
        ///// Tests opening of a large XML file.
        ///// </summary>
        //[TestMethod]
        //public void OpenTest()
        //{
        //    string VersionString = "ps6";
        //    string FilePath = "Open_Test_XML.xml";

        //    AbstractSpreadsheet sheet1 = new Spreadsheet(FilePath, isValid, normalize, VersionString);

            
        //}

        ///// <summary>
        ///// Checks for proper value of cell value.
        ///// </summary>
        //[TestMethod]
        //public void NewTestForRegrade()
        //{
        //    // Create a value for size N
        //    double N = 27;

        //    // Instantiate delegates variables for testing
        //    Func<string, bool> ValidityDelegate = isValid;
        //    Func<string, string> NormalizeDelegate = normalize;
        //    string VersionString = "ps6";
        //    //string PathToFile = "New_stress_test_XML.xml";

        //    AbstractSpreadsheet sheet1 = new Spreadsheet(ValidityDelegate, NormalizeDelegate, VersionString);

        //    char c = 'A';
        //    // Create a collection of dependencies
        //    for (int i = 1; i < N; i++) // Columns A - Z
        //    {
        //        int j;
        //        for (j = 1; j < 99; j++) // Rows 1 - 99
        //        {
        //            if (c > 90)
        //            {
        //                c = 'A';
        //            }

        //            sheet1.SetContentsOfCell("" + c + j, "=" + c + (j + 1) + "+1");
        //            if (!(sheet1.GetCellValue("" + c + j) is FormulaError))
        //            {
        //                Assert.Fail();  // All current cells must contain a FormulaError
        //            }
        //        }
        //        c++;
        //    }

        //    // Set the final cell
        //    sheet1.SetContentsOfCell("A99", "=B1 + 1");
        //    sheet1.SetContentsOfCell("B99", "=C1 + 1");
        //    sheet1.SetContentsOfCell("C99", "=D1 + 1");
        //    sheet1.SetContentsOfCell("D99", "=E1 + 1");
        //    sheet1.SetContentsOfCell("E99", "=F1 + 1");
        //    sheet1.SetContentsOfCell("F99", "=G1 + 1");
        //    sheet1.SetContentsOfCell("G99", "=H1 + 1");
        //    sheet1.SetContentsOfCell("H99", "=I1 + 1");
        //    sheet1.SetContentsOfCell("I99", "=J1 + 1");
        //    sheet1.SetContentsOfCell("J99", "=K1 + 1");
        //    sheet1.SetContentsOfCell("K99", "=L1 + 1");
        //    sheet1.SetContentsOfCell("L99", "=M1 + 1");
        //    sheet1.SetContentsOfCell("M99", "=N1 + 1");
        //    sheet1.SetContentsOfCell("N99", "=O1 + 1");
        //    sheet1.SetContentsOfCell("O99", "=P1 + 1");
        //    sheet1.SetContentsOfCell("P99", "=Q1 + 1");
        //    sheet1.SetContentsOfCell("Q99", "=R1 + 1");
        //    sheet1.SetContentsOfCell("R99", "=S1 + 1");
        //    sheet1.SetContentsOfCell("S99", "=T1 + 1");
        //    sheet1.SetContentsOfCell("T99", "=U1 + 1");
        //    sheet1.SetContentsOfCell("U99", "=V1 + 1");
        //    sheet1.SetContentsOfCell("V99", "=W1 + 1");
        //    sheet1.SetContentsOfCell("W99", "=X1 + 1");
        //    sheet1.SetContentsOfCell("X99", "=Y1 + 1");
        //    sheet1.SetContentsOfCell("Y99", "=Z1 + 1");
        //    sheet1.SetContentsOfCell("Z99", "1");

        //    // Save the current spreadsheet
        //    sheet1.Save("New_stress_test_XML.xml");

        //    //// Create another spreadsheet from the recently created XML; the two spreadsheets must be the same
        //    //AbstractSpreadsheet sheet2 = new Spreadsheet(PathToFile, ValidityDelegate, NormalizeDelegate, VersionString);

        //    //// Spreadsheets must have same version
        //    //Assert.AreEqual(sheet1.Version, sheet2.Version);

        //    //// Each cell must have the same value as the cell with the same name in the duplicate spreadsheet
        //    //foreach (string name in sheet2.GetNamesOfAllNonemptyCells())
        //    //{
        //    //    Assert.AreEqual(sheet1.GetCellValue(name), sheet2.GetCellValue(name));
        //    //}
        //}
        
        /// <summary>
        /// Checks proper parsing of doubles.
        /// </summary>
        [TestMethod]
        public void TestDoubleParse()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("A1", "-5");
            sheet.SetContentsOfCell("A2", "=A1*10");
            Assert.AreEqual(-50.0, sheet.GetCellValue("A2"));

        }

        // **************************************************************************************************************
        // Test for proper throwing of exceptions
        // **************************************************************************************************************

        /// <summary>
        /// Null parameters must generate exceptions from GetCellContents method.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestGetCellContents1()
        {
            // Instantiate delegates variables for testing
            Func<string, bool> ValidityDelegate = isValid;
            Func<string, string> NormalizeDelegate = normalize;
            string VersionString = "not_default_version";

            // Create empty spreadsheet
            AbstractSpreadsheet sheet1 = new Spreadsheet(ValidityDelegate, NormalizeDelegate, VersionString);

            // Create a null string and pass as parameter to generate expected exception
            string param = null;
            sheet1.GetCellContents(param);

            // Exception expected
        }

        /// <summary>
        /// Invalid parameter names must generate exceptions from GetCellContents method.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestGetCellContent2()
        {
            // Instantiate delegates variables for testing
            Func<string, bool> ValidityDelegate = isValid;
            Func<string, string> NormalizeDelegate = normalize;
            string VersionString = "not_default_version";

            // Create empty spreadsheet
            AbstractSpreadsheet sheet1 = new Spreadsheet(ValidityDelegate, NormalizeDelegate, VersionString);

            // Create an invalid string name and pass as parameter to generate expected exception
            string param = "_A1";
            sheet1.GetCellContents(param);

            // Exception expected
        }

        /// <summary>
        /// Invalid parameter names must generate exceptions from GetCellContents method.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestGetCellContents3()
        {
            // Instantiate delegates variables for testing
            Func<string, bool> ValidityDelegate = isValid;
            Func<string, string> NormalizeDelegate = normalize;
            string VersionString = "not_default_version";

            // Create empty spreadsheet
            AbstractSpreadsheet sheet1 = new Spreadsheet(ValidityDelegate, NormalizeDelegate, VersionString);

            // Create an invalid string name and pass as parameter to generate expected exception
            string param = "_";
            sheet1.GetCellContents(param);

            // Exception expected
        }

        /// <summary>
        /// Invalid parameter names must generate exceptions from GetCellContents method.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestGetCellContents4()
        {
            // Instantiate delegates variables for testing
            Func<string, bool> ValidityDelegate = isValid;
            Func<string, string> NormalizeDelegate = normalize;
            string VersionString = "not_default_version";

            // Create empty spreadsheet
            AbstractSpreadsheet sheet1 = new Spreadsheet(ValidityDelegate, NormalizeDelegate, VersionString);

            // Create an invalid string name and pass as parameter to generate expected exception
            string param = "A";
            sheet1.GetCellContents(param);

            // Exception expected
        }

        /// <summary>
        /// Invalid parameter names must generate exceptions from GetCellContents method.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestGetCellContents5()
        {
            // Instantiate delegates variables for testing
            Func<string, bool> ValidityDelegate = isValid;
            Func<string, string> NormalizeDelegate = normalize;
            string VersionString = "not_default_version";

            // Create empty spreadsheet
            AbstractSpreadsheet sheet1 = new Spreadsheet(ValidityDelegate, NormalizeDelegate, VersionString);

            // Create an invalid string name and pass as parameter to generate expected exception
            string param = "1";
            sheet1.GetCellContents(param);

            // Exception expected
        }
         
        /// <summary>
        /// Invalid parameter names must generate exceptions from GetCellContents method.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestGetCellContents6()
        {
            // Instantiate delegates variables for testing
            Func<string, bool> ValidityDelegate = isValid;
            Func<string, string> NormalizeDelegate = normalize;
            string VersionString = "not_default_version";

            // Create empty spreadsheet
            AbstractSpreadsheet sheet1 = new Spreadsheet(ValidityDelegate, NormalizeDelegate, VersionString);

            // Create an invalid string name and pass as parameter to generate expected exception
            string param = "#A1";
            sheet1.GetCellContents(param);
            // Exception expected
        }

        /// <summary>
        /// Parameter defined valid by delegate must only generate exceptions from GetCellContents method
        /// if parameter is does not meet valid cell name specifications.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestGetCellContents7()
        {
            // Create empty spreadsheet
            AbstractSpreadsheet sheet1 = new Spreadsheet();

            // Create an invalid string name and pass as parameter
            string param = "#1";
            sheet1.GetCellContents(param);
        }

        /// <summary>
        /// Parameter defined invalid by delegate must generate exceptions from GetCellContents method.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestGetCellContents8()
        {
            // Instantiate delegates variables for testing
            Func<string, string> NormalizeDelegate = normalize;
            string VersionString = "not_default_version";

            // Create empty spreadsheet
            AbstractSpreadsheet sheet1 = new Spreadsheet(s => false, NormalizeDelegate, VersionString);

            // Create an invalid string name and pass as parameter *** invalid defined by lambda expression to constructor ***
            string param = "A1";
            sheet1.GetCellContents(param);
        }

        /// <summary>
        /// Invalid parameter name must generate exceptions from GetCellContents method.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestGetCellContents9()
        {
            // Instantiate delegates variables for testing
            Func<string, bool> ValidityDelegate = isValid;
            Func<string, string> NormalizeDelegate = normalize;
            string VersionString = "not_default_version";

            // Create empty spreadsheet
            AbstractSpreadsheet sheet1 = new Spreadsheet(ValidityDelegate, NormalizeDelegate, VersionString);

            // Create an invalid string name and pass as parameter
            string param = "333A1333";
            sheet1.GetCellContents(param);
        }

        /// <summary>
        /// Null parameter must generate exceptions from SetContentsOfCell method.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSetContentsOfCell1()
        {
            // Create empty spreadsheet
            AbstractSpreadsheet sheet1 = new Spreadsheet();

            // Create an invalid string name and pass as parameter
            string param1 = "A1";
            string param2 = null;

            sheet1.SetContentsOfCell(param1, param2);
            // Exception expected
        }

        /// <summary>
        /// Null parameter must generate exceptions from SetContentsOfCell method.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSetContentsOfCell2()
        {
            // Create empty spreadsheet
            AbstractSpreadsheet sheet1 = new Spreadsheet();

            // Create an invalid string name and pass as parameter
            string param1 = null;
            string param2 = "some_content";

            sheet1.SetContentsOfCell(param1, param2);
            // Exception expected
        }

        /// <summary>
        /// Null parameter must generate exceptions from SetContentsOfCell method.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSetContentsOfCell3()
        {
            Func<string, bool> IsValid = isValid;
            
            // Create empty spreadsheet
            AbstractSpreadsheet sheet1 = new Spreadsheet(IsValid, N => N.ToUpper(), "not_default_version");

            // Create an invalid string name and pass as parameter
            string param1 = "in_valid_name";
            string param2 = "some_content";

            sheet1.SetContentsOfCell(param1, param2);
            // Exception expected
        }

        /// <summary>
        /// Invalid formula constructor must generate exceptions from SetContentsOfCell method.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestSetContentsOfCell4()
        {
            // Create empty spreadsheet
            AbstractSpreadsheet sheet1 = new Spreadsheet();

            // Create an invalid string for formula and pass as parameter
            string param1 = "A1";
            string param2 = "=";

            sheet1.SetContentsOfCell(param1, param2);
        }

        /// <summary>
        /// Invalid formula constructor must generate exceptions from SetContentsOfCell method.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestSetContentsOfCell5()
        {
            // Create empty spreadsheet
            AbstractSpreadsheet sheet1 = new Spreadsheet();

            // Create an invalid string for formula and pass as parameter
            string param1 = "A1";
            string param2 = "=+3+4";

            sheet1.SetContentsOfCell(param1, param2);
        }

        /// <summary>
        /// Invalid formula constructor must generate exceptions from SetContentsOfCell method.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestSetContentsOfCell6()
        {
            // Create empty spreadsheet
            AbstractSpreadsheet sheet1 = new Spreadsheet();

            // Create an invalid string for formula and pass as parameter
            string param1 = "A1";
            string param2 = "=A1*X";

            sheet1.SetContentsOfCell(param1, param2);
        }

        /// <summary>
        /// Invalid formula constructor must generate exceptions from SetContentsOfCell method.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestSetContentsOfCell7()
        {
            // Create empty spreadsheet
            AbstractSpreadsheet sheet1 = new Spreadsheet();

            // Create an invalid string for formula and pass as parameter
            string param1 = "A1";
            string param2 = "=1+2+3+4+5+6+7+8+9+A";

            sheet1.SetContentsOfCell(param1, param2);
        }

        /// <summary>
        /// Valid, non-formula, parameters must return emtpy sets.
        /// </summary>
        [TestMethod]
        public void TestSetContentsOfCell8()
        {
            // Create empty spreadsheet
            AbstractSpreadsheet sheet1 = new Spreadsheet();

            // Create a valid string for a double and pass as parameter
            string param1 = "A1";
            string param2 = "123";

            // Verify correct number of cells to recalculate is accurate
            Assert.AreEqual(1, sheet1.SetContentsOfCell(param1, param2).Count);
        }

        /// <summary>
        /// Valid, non-formula, parameters must return emtpy sets.
        /// </summary>
        [TestMethod]
        public void TestSetContentsOfCell9()
        {
            // Create empty spreadsheet
            AbstractSpreadsheet sheet1 = new Spreadsheet();

            // Create a valid string for a string and pass as parameter
            string param1 = "A1";
            string param2 = "not_a_double_not_a_formula";

            // Verify correct number of cells to recalculate is accurate
            Assert.AreEqual(1, sheet1.SetContentsOfCell(param1, param2).Count);
        }

        /// <summary>
        /// Valid, non-formula, parameters must return emtpy sets.
        /// </summary>
        [TestMethod]
        public void TestSetContentsOfCell10()
        {
            // Create empty spreadsheet
            AbstractSpreadsheet sheet1 = new Spreadsheet();

            // Create a valid string for a string and pass as parameter
            string param1 = "A1";
            string param2 = "A1";
            string param3 = "A2";
            string param4 = "A2";
            string param5 = "A3";
            string param6 = "A3";
            string param7 = "A4";
            string param8 = "A5";
            string param9 = "not_a_double_not_a_formula";

            // Verify correct number of cells to recalculate is accurate
            Assert.AreEqual(1, sheet1.SetContentsOfCell(param1, param9).Count);
            Assert.AreEqual(1, sheet1.SetContentsOfCell(param2, param9).Count);
            Assert.AreEqual(1, sheet1.SetContentsOfCell(param3, param9).Count);
            Assert.AreEqual(1, sheet1.SetContentsOfCell(param4, param9).Count);
            Assert.AreEqual(1, sheet1.SetContentsOfCell(param5, param9).Count);
            Assert.AreEqual(1, sheet1.SetContentsOfCell(param6, param9).Count);
            Assert.AreEqual(1, sheet1.SetContentsOfCell(param7, param9).Count);
            Assert.AreEqual(1, sheet1.SetContentsOfCell(param8, param9).Count);
        }

        /// <summary>
        /// Valid, non-formula, parameters must return emtpy sets.
        /// </summary>
        [TestMethod]
        public void TestSetContentsOfCell11()
        {
            // Create empty spreadsheet
            AbstractSpreadsheet sheet1 = new Spreadsheet();

            // Create a valid string for a string and pass as parameter
            string param1 = "A1";
            string param2 = "A2";
            string param3 = "A3";
            string param4 = "A4";
            string param5 = "A5";
            string param6 = "A6";
            string param7 = "A7";
            string param8 = "A8";
            string param9 = "=A2+A3";
            string paramA = "=A4+A5";
            string paramB = "=A6+A7";
            string paramC = "=3+4";
            string paramD = "=A6+7";
            string paramE = "=7+9";
            string paramF = "=A8+3";
            string paramG = "=3+4+7";

            // Verify correct number of cells to recalculate is accurate
            Assert.AreEqual(1, sheet1.SetContentsOfCell(param1, param9).Count);
            Assert.AreEqual(2, sheet1.SetContentsOfCell(param2, paramA).Count);
            Assert.AreEqual(2, sheet1.SetContentsOfCell(param3, paramB).Count);
            Assert.AreEqual(3, sheet1.SetContentsOfCell(param4, paramC).Count);
            Assert.AreEqual(3, sheet1.SetContentsOfCell(param5, paramD).Count);
            Assert.AreEqual(5, sheet1.SetContentsOfCell(param6, paramE).Count);
            Assert.AreEqual(3, sheet1.SetContentsOfCell(param7, paramF).Count);
            Assert.AreEqual(4, sheet1.SetContentsOfCell(param8, paramG).Count);
        }

        /// <summary>
        /// Verify "A0" as valid cell name per Jim's clarifications of valid variables..
        /// </summary>
        [TestMethod]
        public void TestSetContentsOfCell12()
        {
            // Create empty spreadsheet
            AbstractSpreadsheet sheet1 = new Spreadsheet();

            sheet1.SetContentsOfCell("A0", "=A2*(A3/A4)+A5");
            sheet1.SetContentsOfCell("A1", "=A0");
            sheet1.SetContentsOfCell("A2", "3");
            sheet1.SetContentsOfCell("A3", "10");
            sheet1.SetContentsOfCell("A4", "5");
            sheet1.SetContentsOfCell("A5", "1");

            double expected = 7;
            Assert.AreEqual(expected, sheet1.GetCellValue("A0"));
            Assert.AreEqual(expected, sheet1.GetCellValue("A1"));
        }

        // ****************************************************************************************************
        // Test XML Writing
        // ****************************************************************************************************

        /// <summary>
        /// Validates the proper functionality of the save method.
        /// </summary>
        [TestMethod]
        public void TestXmlWrite1()
        {
            // Instantiate delegate methods
            Func<string, bool> ValidityDelegate = isValid;
            Func<string, string> NormalizeDelegate = normalize;
            string VersionString = "not_default_version";

            // Construct spreadsheet and collections to keep track of cell names and contents
            AbstractSpreadsheet sheet = new Spreadsheet(ValidityDelegate, NormalizeDelegate, VersionString);
            List<string> names = new List<string>();
            List<int> contents = new List<int>();

            // Create cells that range from [A-Z][1-99]
            char n = 'a';
            for (int i = 1; i < 100; i++ )
            {
                // If character n goes beyond 'Z', reset the char back to 'A'
                if(n > 122)
                {
                    n = 'a';
                }

                string name = n.ToString() + i;
                string content = (i * 3).ToString();

                // Add current generation to a list for later assertion
                names.Add(name);
                contents.Add(i * 3);

                // Create a new cell
                sheet.SetContentsOfCell(name, content);

                n++;
            }

            // Save the spreadsheet to an XML file
            sheet.Save("test_XML.xml");

            // Now, read each element of the generated file and compare against expected saved values
            StreamReader stream = new StreamReader("test_XML.xml");
            string line;
            int index = 0;
            while ((line = stream.ReadLine()) != null)
            {
                try
                {
                    // Use a string builder to eliminate any potential white space due to indenting
                    StringBuilder sb = new StringBuilder();
                    for (int j = 0; j < line.Length; j++ )
                    {
                        if(!Char.IsWhiteSpace(line[j]))
                        {
                            sb.Append(line[j]);
                        }
                    }

                    line = sb.ToString();

                    // If the current line represents a name element, check its name
                    if (line.Substring(0, 6).Equals("<name>"))
                    {
                        // Name is only two characters; eg. "A1", "B2", "C3"
                        if (index < 9)
                        {
                            Assert.AreEqual(names[index].ToUpper(), line.Substring(6, 2));
                        }
                        
                        // Name is three characters; eg. "A10", "B11", "C12"
                        else
                        {
                            Assert.AreEqual(names[index].ToUpper(), line.Substring(6, 3));
                        }
                    }

                    // If the current line represents a contents element, check its contents
                    else if (line.Substring(0, 10).Equals("<contents>"))
                    {
                        // Content is only 1 character; eg. "3", "6", "9"
                        if (index < 3)
                        {
                            Assert.AreEqual(contents[index], int.Parse(line.Substring(10, 1)));
                        }

                        // Contents is two characters; eg. "12", "15", "18"
                        else if (index < 33)
                        {
                            Assert.AreEqual(contents[index], int.Parse(line.Substring(10, 2)));
                        }

                        // Contents is three characters; eg. "102", "105", "108"
                        else
                        {
                            Assert.AreEqual(contents[index], int.Parse(line.Substring(10, 3)));
                        }
                        
                        // Advance index counter
                        index++;
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    // Elements such as "cell" will through an exception in the above code because
                    //     it may be the only string on a row
                }
            }
        }

        // ****************************************************************************************************
        // Test XML Writing / Reading
        // ****************************************************************************************************

        /// <summary>
        /// Validates the proper functionality of the xml to spreadsheet generation.
        /// 
        /// Valid file path must not generate any exceptions.
        /// </summary>
        [TestMethod]
        public void TestXmlReader1()
        {
            // Instantiate delegates variables for testing
            Func<string, bool> ValidityDelegate = isValid;
            Func<string, string> NormalizeDelegate = normalize;
            string VersionString = "not_default_version";
            string PathToFile = "test_XML.xml";

            // Create empty spreadsheets
            AbstractSpreadsheet sheet1 = new Spreadsheet(PathToFile, ValidityDelegate, NormalizeDelegate, VersionString);
        }

        /// <summary>
        /// Validates the proper functionality of the xml to spreadsheet generation.
        /// 
        /// InValid file path must generate exceptions.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void TestXmlReader2()
        {
            // Instantiate delegates variables for testing
            Func<string, bool> ValidityDelegate = isValid;
            Func<string, string> NormalizeDelegate = normalize;
            string VersionString = "not_default_version";
            string PathToFile = "inValid_path";

            // Create empty spreadsheets
            AbstractSpreadsheet sheet1 = new Spreadsheet(PathToFile, ValidityDelegate, NormalizeDelegate, VersionString);
        }

        /// <summary>
        /// Validates the proper functionality of the xml to spreadsheet generation.
        /// 
        /// Non-matching version must generate exceptions.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void TestXmlReader3()
        {
            // Instantiate delegates variables for testing
            Func<string, bool> ValidityDelegate = isValid;
            Func<string, string> NormalizeDelegate = normalize;
            string VersionString = "non_matching_version";
            string PathToFile = "test_XML.xml";

            // Create empty spreadsheets
            AbstractSpreadsheet sheet1 = new Spreadsheet(PathToFile, ValidityDelegate, NormalizeDelegate, VersionString);
        }

        /// <summary>
        /// Validates proper functionality of the xml to spreadsheet generation.
        /// 
        /// </summary>
        [TestMethod]
        public void TestXmlReader4()
        {
            // Instantiate delegates variables for testing
            Func<string, bool> ValidityDelegate = isValid;
            Func<string, string> NormalizeDelegate = normalize;
            string VersionString = "not_default_version";
            string PathToFile = "test_XML.xml";

            // Create empty spreadsheets
            AbstractSpreadsheet sheet1 = new Spreadsheet(PathToFile, ValidityDelegate, NormalizeDelegate, VersionString);

            // Check cells that range from [A-Z][1-99]
            char n = 'a';
            for (int i = 1; i < 100; i++)
            {
                // If character n goes beyond 'Z', reset the char back to 'A'
                if (n > 122)
                {
                    n = 'a';
                }

                // These cells must exist in spreadsheet (previous tests guarantee this)
                // Verify that the contents of each cell are properly saved
                string name = n.ToString() + i;
                double content = i * 3;
                Assert.AreEqual(content, sheet1.GetCellContents(name));

                n++;
            }
        }

        ///// <summary>
        ///// Reading an XML with circularity must generate exception from Spreadsheet.
        ///// </summary>
        //[TestMethod]
        //[ExpectedException(typeof(SpreadsheetReadWriteException))]
        //public void TestXmlReader5()
        //{
        //    // Instantiate delegates variables for testing
        //    Func<string, bool> ValidityDelegate = isValid;
        //    Func<string, string> NormalizeDelegate = normalize;
        //    string VersionString = "not_default_version";
        //    string PathToFile = "test_CIRCULAR.xml";

        //    AbstractSpreadsheet sheet1 = new Spreadsheet(PathToFile, ValidityDelegate, NormalizeDelegate, VersionString);
        //}

        ///// <summary>
        ///// Reading an XML with missing tags must generate exception from Spreadsheet.
        ///// </summary>
        //[TestMethod]
        //[ExpectedException(typeof(SpreadsheetReadWriteException))]
        //public void TestXmlReader6()
        //{
        //    // Instantiate delegates variables for testing
        //    Func<string, bool> ValidityDelegate = isValid;
        //    Func<string, string> NormalizeDelegate = normalize;
        //    string VersionString = "not_default_version";
        //    string PathToFile = "test_INVALID.xml";

        //    // Test file is missing cell tag on final cell
        //    AbstractSpreadsheet sheet1 = new Spreadsheet(PathToFile, ValidityDelegate, NormalizeDelegate, VersionString);
        //}

        /// <summary>
        /// Validates proper functionality of the GetSavedVersion method.
        /// </summary>
        [TestMethod]
        public void TestGetSavedVersion1()
        {
            // Instantiate delegates variables for testing
            Func<string, bool> ValidityDelegate = isValid;
            Func<string, string> NormalizeDelegate = normalize;
            string VersionString = "not_default_version";
            string PathToFile = "test_XML.xml";

            AbstractSpreadsheet sheet1 = new Spreadsheet(PathToFile, ValidityDelegate, NormalizeDelegate, VersionString);

            string expected = "not_default_version";
            Assert.AreEqual(expected, sheet1.GetSavedVersion(PathToFile));
        }

        /// <summary>
        /// Validates proper functionality of the GetSavedVersion method.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void TestGetSavedVersion2()
        {
            // Instantiate delegates variables for testing
            Func<string, bool> ValidityDelegate = isValid;
            Func<string, string> NormalizeDelegate = normalize;
            string VersionString = "not_default_version";
            string PathToFile = "test_XML.xml";

            AbstractSpreadsheet sheet1 = new Spreadsheet(PathToFile, ValidityDelegate, NormalizeDelegate, VersionString);

            string expected = "not_default_version";
            Assert.AreEqual(expected, sheet1.GetSavedVersion("unknown_XML.xml"));
        }

        /// <summary>
        /// Validates proper functionality of the GetSavedVersion method.
        /// </summary>
        [TestMethod]
        public void TestGetSavedVersion3()
        {
            // Instantiate delegates variables for testing
            Func<string, bool> ValidityDelegate = isValid;
            Func<string, string> NormalizeDelegate = normalize;
            string VersionString = "not_default_version";
            string PathToFile = "test_XML.xml";

            AbstractSpreadsheet sheet1 = new Spreadsheet(PathToFile, ValidityDelegate, NormalizeDelegate, VersionString);

            string expected = "unknown_version";
            Assert.AreNotEqual(expected, sheet1.GetSavedVersion(PathToFile));
            Assert.AreEqual(VersionString, sheet1.GetSavedVersion(PathToFile));
        }

        // ****************************************************************************************************
        // Test Changed method
        // ****************************************************************************************************

        /// <summary>
        /// Validates proper functionality of the Changed property of the Spreadsheet.
        /// </summary>
        [TestMethod]
        public void TestChanged1()
        {
            // Changed must return false immediately upon construction
            AbstractSpreadsheet sheet1 = new Spreadsheet();
            Assert.AreEqual(false, sheet1.Changed);

            // Changed must return false if spreadsheet is not modified
            for (int i = 1; i < 10; i++ )
            {
                sheet1.GetCellContents("A" + i);
                Assert.AreEqual(false, sheet1.Changed);
            }

            // Changed must return true once spreadsheet has been modified
            for (int i = 1; i < 10; i++ )
            {
                sheet1.SetContentsOfCell("A" + i, "some_content" + i);
                Assert.AreEqual(true, sheet1.Changed);
            }
        }

        /// <summary>
        /// Validates proper functionality of the Changed property of the Spreadsheet.
        /// </summary>
        [TestMethod]
        public void TestChanged2()
        {
            // Changed must return false immediately upon construction
            AbstractSpreadsheet sheet1 = new Spreadsheet();

            // Changed must return false after saving the spreadsheet
            for (int i = 1; i < 100; i++)
            {
                sheet1.GetCellContents("A" + i);
                Assert.AreEqual(false, sheet1.Changed);

                sheet1.SetContentsOfCell("A" + i, "some_content" + i);
                Assert.AreEqual(true, sheet1.Changed);

                sheet1.Save("test_SAVE.xml");
                Assert.AreEqual(false, sheet1.Changed);
            }
        }

        /// <summary>
        /// Validates proper functionality of the Changed property of the Spreadsheet.
        /// </summary>
        [TestMethod]
        public void TestChanged3()
        {
            // Changed must return false immediately upon construction
            AbstractSpreadsheet sheet1 = new Spreadsheet();

            // Changed must return false after saving the spreadsheet
            for (int i = 1; i < 100; i++)
            {
                sheet1.GetCellContents("A" + i);
                Assert.AreEqual(false, sheet1.Changed);

                sheet1.SetContentsOfCell("A" + i, "some_content" + i);
                Assert.AreEqual(true, sheet1.Changed);

                sheet1.Save("test_SAVE.xml");
                Assert.AreEqual(false, sheet1.Changed);
            }

            // Instantiate delegates variables for testing
            Func<string, bool> ValidityDelegate = isValid;
            Func<string, string> NormalizeDelegate = normalize;
            string VersionString = "not_default_version";
            string PathToFile = "test_XML.xml";

            // Create empty spreadsheets
            sheet1 = new Spreadsheet(PathToFile, ValidityDelegate, NormalizeDelegate, VersionString);
            Assert.AreEqual(false, sheet1.Changed);

            // Changed must return false if spreadsheet is not modified
            for (int i = 1; i < 10; i++)
            {
                sheet1.GetCellContents("A" + i);
                Assert.AreEqual(false, sheet1.Changed);
            }

            // Changed must return true once spreadsheet has been modified
            for (int i = 1; i < 10; i++)
            {
                sheet1.SetContentsOfCell("A" + i, "some_content" + i);
                Assert.AreEqual(true, sheet1.Changed);
            }
        }

        // ****************************************************************************************************
        // Test GetCellValue method
        // ****************************************************************************************************

        /// <summary>
        /// Validates proper functionality of the GetCellValue method.
        /// </summary>
        [TestMethod]
        public void TestGetCellValue1()
        {
            AbstractSpreadsheet sheet1 = new Spreadsheet();
            sheet1.SetContentsOfCell("A1", "=A3+4");
            sheet1.SetContentsOfCell("A3", "7");

            double expected = 11;
            Assert.AreEqual(expected, sheet1.GetCellValue("A1"));
        }

        /// <summary>
        /// Validates proper functionality of the GetCellValue method.
        /// </summary>
        [TestMethod]
        public void TestGetCellValue2()
        {
            AbstractSpreadsheet sheet1 = new Spreadsheet();
            sheet1.SetContentsOfCell("A1", "=A3+4");
            sheet1.SetContentsOfCell("A3", "=A5+2");
            sheet1.SetContentsOfCell("A5", "5");

            double expected = 11;
            Assert.AreEqual(expected, sheet1.GetCellValue("A1"));
        }

        /// <summary>
        /// Validates proper functionality of the GetCellValue method.
        /// </summary>
        [TestMethod]
        public void TestGetCellValue3()
        {
            AbstractSpreadsheet sheet1 = new Spreadsheet();
            
            // Create a collection of cell dependencies
            sheet1.SetContentsOfCell("A1", "=A2+1");
            sheet1.SetContentsOfCell("A2", "=A3+1");
            sheet1.SetContentsOfCell("A3", "=A4+1");
            sheet1.SetContentsOfCell("A4", "=A5+1");
            sheet1.SetContentsOfCell("A5", "=A6+1");
            sheet1.SetContentsOfCell("A6", "=A7+1");
            sheet1.SetContentsOfCell("A7", "=A8+1");
            sheet1.SetContentsOfCell("A8", "=A9+1");
            sheet1.SetContentsOfCell("A9", "=A10+1");
            sheet1.SetContentsOfCell("A10", "=A11+1");
            sheet1.SetContentsOfCell("A11", "=A12+1");
            sheet1.SetContentsOfCell("A12", "=A13+1");
            sheet1.SetContentsOfCell("A13", "=A14+1");
            sheet1.SetContentsOfCell("A14", "=A15+1");
            sheet1.SetContentsOfCell("A15", "=A16+1");
            sheet1.SetContentsOfCell("A16", "=A17+1");
            sheet1.SetContentsOfCell("A17", "=A18+1");
            sheet1.SetContentsOfCell("A18", "=A19+1");
            sheet1.SetContentsOfCell("A19", "=A20+1");
            sheet1.SetContentsOfCell("A20", "1");

            // Collection represents a counter that will equal 20
            double expected = 20;
            Assert.AreEqual(expected, sheet1.GetCellValue("A1"));
            Assert.AreEqual(expected - 1, sheet1.GetCellValue("A2"));
            Assert.AreEqual(expected - 2, sheet1.GetCellValue("A3"));
            Assert.AreEqual(expected - 3, sheet1.GetCellValue("A4"));
            Assert.AreEqual(expected - 4, sheet1.GetCellValue("A5"));
            Assert.AreEqual(expected - 5, sheet1.GetCellValue("A6"));
            Assert.AreEqual(expected - 6, sheet1.GetCellValue("A7"));
            Assert.AreEqual(expected - 7, sheet1.GetCellValue("A8"));
            Assert.AreEqual(expected - 8, sheet1.GetCellValue("A9"));
            Assert.AreEqual(expected - 9, sheet1.GetCellValue("A10"));
            Assert.AreEqual(expected - 10, sheet1.GetCellValue("A11"));
            Assert.AreEqual(expected - 11, sheet1.GetCellValue("A12"));
            Assert.AreEqual(expected - 12, sheet1.GetCellValue("A13"));
            Assert.AreEqual(expected - 13, sheet1.GetCellValue("A14"));
            Assert.AreEqual(expected - 14, sheet1.GetCellValue("A15"));
            Assert.AreEqual(expected - 15, sheet1.GetCellValue("A16"));
            Assert.AreEqual(expected - 16, sheet1.GetCellValue("A17"));
            Assert.AreEqual(expected - 17, sheet1.GetCellValue("A18"));
            Assert.AreEqual(expected - 18, sheet1.GetCellValue("A19"));
            Assert.AreEqual(expected - 19, sheet1.GetCellValue("A20"));
        }

        /// <summary>
        /// Validates proper functionality of the GetCellValue method.
        /// </summary>
        [TestMethod]
        public void TestGetCellValue4()
        {
            AbstractSpreadsheet sheet1 = new Spreadsheet();

            sheet1.SetContentsOfCell("A1", "=A2+3");
            sheet1.SetContentsOfCell("A2", "cannot_add_string_to_double");

            if(!(sheet1.GetCellValue("A1") is FormulaError))
            {
                Assert.Fail();
            }

            double expected = 7;
            sheet1.SetContentsOfCell("A2", "4");
            Assert.AreEqual(expected, sheet1.GetCellValue("A1"));

            expected = 8;
            sheet1.SetContentsOfCell("A2", "=A3+4");
            sheet1.SetContentsOfCell("A3", "1");
            Assert.AreEqual(expected, sheet1.GetCellValue("A1"));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void TestGetCellValue5()
        {
            AbstractSpreadsheet sheet1 = new Spreadsheet();

            // Create a groupd cells all dependent on the same cell
            sheet1.SetContentsOfCell("A1", "=M5");
            sheet1.SetContentsOfCell("A2", "=M5");
            sheet1.SetContentsOfCell("A3", "=M5");
            sheet1.SetContentsOfCell("A4", "=M5");
            sheet1.SetContentsOfCell("A5", "=M5");
            sheet1.SetContentsOfCell("A6", "=M5");
            sheet1.SetContentsOfCell("A7", "=M5");
            sheet1.SetContentsOfCell("A8", "=M5");
            sheet1.SetContentsOfCell("A9", "=M5");

            // At this point all cell values must be FormulaErrors
            if (!(sheet1.GetCellValue("A1") is FormulaError) || !(sheet1.GetCellValue("A2") is FormulaError) ||
                !(sheet1.GetCellValue("A3") is FormulaError) || !(sheet1.GetCellValue("A4") is FormulaError) ||
                !(sheet1.GetCellValue("A5") is FormulaError) || !(sheet1.GetCellValue("A6") is FormulaError) ||
                !(sheet1.GetCellValue("A7") is FormulaError) || !(sheet1.GetCellValue("A8") is FormulaError) ||
                !(sheet1.GetCellValue("A9") is FormulaError))
            {
                Assert.Fail();
            }

            // Set dependee; all dependents must have the same value
            sheet1.SetContentsOfCell("M5", "20.345");

            // Now all cell values must be the double 20.345
            double expected = 20.345;
            Assert.AreEqual(expected, sheet1.GetCellValue("A1"));
            Assert.AreEqual(expected, sheet1.GetCellValue("A2"));
            Assert.AreEqual(expected, sheet1.GetCellValue("A3"));
            Assert.AreEqual(expected, sheet1.GetCellValue("A4"));
            Assert.AreEqual(expected, sheet1.GetCellValue("A5"));
            Assert.AreEqual(expected, sheet1.GetCellValue("A6"));
            Assert.AreEqual(expected, sheet1.GetCellValue("A7"));
            Assert.AreEqual(expected, sheet1.GetCellValue("A8"));
            Assert.AreEqual(expected, sheet1.GetCellValue("A9"));

            // Change the main dependee's contents which will affect all other cells
            sheet1.SetContentsOfCell("M5", "=M6");
            sheet1.SetContentsOfCell("M6", "0.987654321");

            expected = 0.987654321;
            Assert.AreEqual(expected, sheet1.GetCellValue("A1"));
            Assert.AreEqual(expected, sheet1.GetCellValue("A2"));
            Assert.AreEqual(expected, sheet1.GetCellValue("A3"));
            Assert.AreEqual(expected, sheet1.GetCellValue("A4"));
            Assert.AreEqual(expected, sheet1.GetCellValue("A5"));
            Assert.AreEqual(expected, sheet1.GetCellValue("A6"));
            Assert.AreEqual(expected, sheet1.GetCellValue("A7"));
            Assert.AreEqual(expected, sheet1.GetCellValue("A8"));
            Assert.AreEqual(expected, sheet1.GetCellValue("A9"));

        }

        // ****************************************************************************************************
        // Test Circular Exceptions
        // ****************************************************************************************************

        /// <summary>
        /// Validate proper throwing of circular exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void TestCircular1()
        {
            AbstractSpreadsheet sheet1 = new Spreadsheet();

            sheet1.SetContentsOfCell("A1", "=A2");
            sheet1.SetContentsOfCell("A2", "=A1");
        }

        /// <summary>
        /// Validate proper throwing of circular exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void TestCircular2()
        {
            AbstractSpreadsheet sheet1 = new Spreadsheet();

            // Create a collection of dependent cells
            int i = 0;
            for (i = 1; i < 10; i++ )
            {
                // Each cell is dependent on the next
                sheet1.SetContentsOfCell("A" + i, "=A" + (i + 1));
            }

            // Set the final cell value to a known value
            double expected = 77.777;
            sheet1.SetContentsOfCell("A" + i, "77.777");

            // At this point no circularity exists
            foreach(string name in sheet1.GetNamesOfAllNonemptyCells())
            {
                Assert.AreEqual(expected, sheet1.GetCellValue(name));
            }

            // Now set the final cell to refer to a dependent, creating circularity
            sheet1.SetContentsOfCell("A" + i, "=A1+3+4");

            // Exception expected
        }


        // ****************************************************************************************************
        // Test Various Exceptions
        // ****************************************************************************************************

        /// <summary>
        /// Null formula input must generate exception from spreadsheet.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestException1()
        {
            AbstractSpreadsheet sheet1 = new Spreadsheet();

            // A null string must generate an exception
            string contents = null;
            sheet1.SetContentsOfCell("A1", contents);
        }

        /// <summary>
        /// Invalid formula must generate exception from spreadsheet.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestException2()
        {
            AbstractSpreadsheet sheet1 = new Spreadsheet();

            sheet1.SetContentsOfCell("A1", "=");
        }

        /// <summary>
        /// Cell Formula that refer to Cells with contents type string must return an error.
        /// </summary>
        [TestMethod]
        public void TestException3()
        {
            AbstractSpreadsheet sheet1 = new Spreadsheet();

            sheet1.SetContentsOfCell("A1", "=A2+3");
            sheet1.SetContentsOfCell("A2", "not_a_double");

            if (!(sheet1.GetCellValue("A1") is FormulaError))
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Invalid cell name after normalization must generate exception from Spreadsheet.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestException4()
        {
            AbstractSpreadsheet sheet1 = new Spreadsheet(S => true, N => "###_invalid_name", "not_default_version");
            sheet1.GetCellContents("A1");
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void TestException5()
        {
            AbstractSpreadsheet sheet1 = new Spreadsheet();
            sheet1.SetContentsOfCell("A1", "some_content");
            sheet1.SetContentsOfCell("A1", "");
            Assert.IsFalse(sheet1.GetNamesOfAllNonemptyCells().GetEnumerator().MoveNext());
        }

        // ****************************************************************************************************
        // The previous tests generate XML files necessary for the following tests to execute.
        // ****************************************************************************************************

        /// <summary>
        /// Creation of empty spreadsheets must not generate any errors or exceptions.
        /// </summary>
        [TestMethod]
        public void TestConstructor1()
        {
            // Instantiate delegates variables for testing
            Func<string, bool> ValidityDelegate = isValid;
            Func<string, string> NormalizeDelegate = normalize;
            string VersionString = "not_default_version";
            string PathToFile = "test_XML.xml";

            // Create empty spreadsheets
            AbstractSpreadsheet sheet1 = new Spreadsheet();
            AbstractSpreadsheet sheet2 = new Spreadsheet(ValidityDelegate, NormalizeDelegate, VersionString);
            AbstractSpreadsheet sheet3 = new Spreadsheet(PathToFile, ValidityDelegate, NormalizeDelegate, VersionString);
        }

        /// <summary>
        /// Creation of empty spreadsheets must not generate any errors or exceptions.
        /// </summary>
        [TestMethod]
        public void TestConstructor2()
        {
            // Instantiate delegates variables for testing
            Func<string, bool> ValidityDelegate = isValid;
            Func<string, string> NormalizeDelegate = normalize;
            string VersionString = "not_default_version";
            string PathToFile = "test_XML.xml";

            // Create empty spreadsheets
            AbstractSpreadsheet sheet1 = new Spreadsheet();
            AbstractSpreadsheet sheet2 = new Spreadsheet(ValidityDelegate, NormalizeDelegate, VersionString);
            AbstractSpreadsheet sheet3 = new Spreadsheet(PathToFile, ValidityDelegate, NormalizeDelegate, VersionString);

            // Call GetContents should not generate error and should return an empty string
            Assert.AreEqual("", sheet1.GetCellContents("A1"));
            Assert.AreEqual("", sheet2.GetCellContents("A2"));
            Assert.AreEqual("", sheet3.GetCellContents("A3"));
        }


        // ****************************************************************************************************
        // Stress Test
        // ****************************************************************************************************

        /// <summary>
        /// Stress multiple functions of Spreadsheet.
        /// </summary>
        [TestMethod]
        public void StressTest()
        {
            // Create a value for size N
            double N = 400;

            // Instantiate delegates variables for testing
            Func<string, bool> ValidityDelegate = isValid;
            Func<string, string> NormalizeDelegate = normalize;
            string VersionString = "stress_version";
            string PathToFile = "stress_test_XML.xml";

            AbstractSpreadsheet sheet1 = new Spreadsheet(ValidityDelegate, NormalizeDelegate, VersionString);

            // Create a collection of dependencies
            for (int i = 1; i < N; i++ )
            {
                sheet1.SetContentsOfCell("A" + i, "=A" + (i+1) + "+1");
            }

            // All current cells must contain a FormulaError
            foreach(string name in sheet1.GetNamesOfAllNonemptyCells())
            {
                if (!(sheet1.GetCellValue("A1") is FormulaError))
                {
                    Assert.Fail();
                }
            }

            // Set the final cell 
            sheet1.SetContentsOfCell("A" + N, "1");

            // Each value should increment / decrement by 1 ranging 1 - N
            for (int i = 0; i < N; i++)
            {
                Assert.AreEqual(N - i ,sheet1.GetCellValue("A" + (i + 1)));
            }

            // Save the current spreadsheet
            sheet1.Save("stress_test_XML.xml");

            // Create another spreadsheet from the recently created XML; the two spreadsheets must be the same
            AbstractSpreadsheet sheet2 = new Spreadsheet(PathToFile, ValidityDelegate, NormalizeDelegate, VersionString);

            // Spreadsheets must have same version
            Assert.AreEqual(sheet1.Version, sheet2.Version);
            
            // Each value should increment / decrement by 1 ranging 1 - N
            for (int i = 0; i < N; i++)
            {
                Assert.AreEqual(N - i, sheet2.GetCellValue("A" + (i + 1)));
            }
            
            // Each cell must have the same value as the cell with the same name in the duplicate spreadsheet
            foreach(string name in sheet2.GetNamesOfAllNonemptyCells())
            {
                Assert.AreEqual(sheet1.GetCellValue(name), sheet2.GetCellValue(name));
            }
        }

        // ****************************************************************************************************
        // ****************************************************************************************************
        // ****************************************************************************************************
        // PS4 Grading Tests
        // ****************************************************************************************************
        // ****************************************************************************************************
        // ****************************************************************************************************

        //private TestContext testContextInstance;

        ///// <summary>
        /////Gets or sets the test context which provides
        /////information about and functionality for the current test run.
        /////</summary>
        //public TestContext TestContext
        //{
        //    get
        //    {
        //        return testContextInstance;
        //    }
        //    set
        //    {
        //        testContextInstance = value;
        //    }
        //}

        //#region Additional test attributes
        //// 
        ////You can use the following additional attributes as you write your tests:
        ////
        ////Use ClassInitialize to run code before running the first test in the class
        ////[ClassInitialize()]
        ////public static void MyClassInitialize(TestContext testContext)
        ////{
        ////}
        ////
        ////Use ClassCleanup to run code after all tests in a class have run
        ////[ClassCleanup()]
        ////public static void MyClassCleanup()
        ////{
        ////}
        ////
        ////Use TestInitialize to run code before running each test
        ////[TestInitialize()]
        ////public void MyTestInitialize()
        ////{
        ////}
        ////
        ////Use TestCleanup to run code after each test has run
        ////[TestCleanup()]
        ////public void MyTestCleanup()
        ////{
        ////}
        ////
        //#endregion

        //// EMPTY SPREADSHEETS
        //[TestMethod()]
        //[ExpectedException(typeof(InvalidNameException))]
        //public void Test1()
        //{
        //    Spreadsheet s = new Spreadsheet();
        //    s.GetCellContents(null);
        //}

        //[TestMethod()]
        //[ExpectedException(typeof(InvalidNameException))]
        //public void Test2()
        //{
        //    Spreadsheet s = new Spreadsheet();
        //    s.GetCellContents("1AA");
        //}

        //[TestMethod()]
        //public void Test3()
        //{
        //    Spreadsheet s = new Spreadsheet();
        //    Assert.AreEqual("", s.GetCellContents("A2"));
        //}

        //// SETTING CELL TO A DOUBLE
        //[TestMethod()]
        //[ExpectedException(typeof(InvalidNameException))]
        //public void Test4()
        //{
        //    Spreadsheet s = new Spreadsheet();
        //    s.SetContentsOfCell(null, "1.5");
        //}

        //[TestMethod()]
        //[ExpectedException(typeof(InvalidNameException))]
        //public void Test5()
        //{
        //    Spreadsheet s = new Spreadsheet();
        //    s.SetContentsOfCell("1A1A", "1.5");
        //}

        //[TestMethod()]
        //public void Test6()
        //{
        //    Spreadsheet s = new Spreadsheet();
        //    s.SetContentsOfCell("Z7", "1.5");
        //    Assert.AreEqual(1.5, (double)s.GetCellContents("Z7"), 1e-9);
        //}

        //// SETTING CELL TO A STRING
        //[TestMethod()]
        //[ExpectedException(typeof(ArgumentNullException))]
        //public void Test7()
        //{
        //    Spreadsheet s = new Spreadsheet();
        //    s.SetContentsOfCell("A8", (string)null);
        //}

        //[TestMethod()]
        //[ExpectedException(typeof(InvalidNameException))]
        //public void Test8()
        //{
        //    Spreadsheet s = new Spreadsheet();
        //    s.SetContentsOfCell(null, "hello");
        //}

        //[TestMethod()]
        //[ExpectedException(typeof(InvalidNameException))]
        //public void Test9()
        //{
        //    Spreadsheet s = new Spreadsheet();
        //    s.SetContentsOfCell("1AZ", "hello");
        //}

        //[TestMethod()]
        //public void Test10()
        //{
        //    Spreadsheet s = new Spreadsheet();
        //    s.SetContentsOfCell("Z7", "hello");
        //    Assert.AreEqual("hello", s.GetCellContents("Z7"));
        //}

        ////// SETTING CELL TO A FORMULA
        ////[TestMethod()]
        ////[ExpectedException(typeof(ArgumentNullException))]
        ////public void Test11()
        ////{
        ////    Spreadsheet s = new Spreadsheet();
        ////    s.SetContentsOfCell("A8", (Formula)null);
        ////}

        //[TestMethod()]
        //[ExpectedException(typeof(InvalidNameException))]
        //public void Test12()
        //{
        //    Spreadsheet s = new Spreadsheet();
        //    s.SetContentsOfCell(null, "=2");
        //}

        //[TestMethod()]
        //[ExpectedException(typeof(InvalidNameException))]
        //public void Test13()
        //{
        //    Spreadsheet s = new Spreadsheet();
        //    s.SetContentsOfCell("1AZ", "=2");
        //}

        //[TestMethod()]
        //public void Test14()
        //{
        //    Spreadsheet s = new Spreadsheet();
        //    s.SetContentsOfCell("Z7", "=3");
        //    Formula f = (Formula)s.GetCellContents("Z7");
        //    Assert.AreEqual(new Formula("3"), f);
        //    Assert.AreNotEqual(new Formula("2"), f);
        //}

        //// CIRCULAR FORMULA DETECTION
        //[TestMethod()]
        //[ExpectedException(typeof(CircularException))]
        //public void Test15()
        //{
        //    Spreadsheet s = new Spreadsheet();
        //    s.SetContentsOfCell("A1", "=A2");
        //    s.SetContentsOfCell("A2", "=A1");
        //}

        //[TestMethod()]
        //[ExpectedException(typeof(CircularException))]
        //public void Test16()
        //{
        //    Spreadsheet s = new Spreadsheet();
        //    s.SetContentsOfCell("A1", "=A2+A3");
        //    s.SetContentsOfCell("A3", "=A4+A5");
        //    s.SetContentsOfCell("A5", "=A6+A7");
        //    s.SetContentsOfCell("A7", "=A1+A1");
        //}

        //[TestMethod()]
        //[ExpectedException(typeof(CircularException))]
        //public void Test17()
        //{
        //    Spreadsheet s = new Spreadsheet();
        //    try
        //    {
        //        s.SetContentsOfCell("A1", "=A2+A3");
        //        s.SetContentsOfCell("A2", "15");
        //        s.SetContentsOfCell("A3", "30");
        //        s.SetContentsOfCell("A2", "=A3*A1");
        //    }
        //    catch (CircularException e)
        //    {
        //        Assert.AreEqual(15, (double)s.GetCellContents("A2"), 1e-9);
        //        throw e;
        //    }
        //}

        //// NONEMPTY CELLS
        //[TestMethod()]
        //public void Test18()
        //{
        //    Spreadsheet s = new Spreadsheet();
        //    Assert.IsFalse(s.GetNamesOfAllNonemptyCells().GetEnumerator().MoveNext());
        //}

        //[TestMethod()]
        //public void Test19()
        //{
        //    Spreadsheet s = new Spreadsheet();
        //    s.SetContentsOfCell("B1", "");
        //    Assert.IsFalse(s.GetNamesOfAllNonemptyCells().GetEnumerator().MoveNext());
        //}

        //[TestMethod()]
        //public void Test20()
        //{
        //    Spreadsheet s = new Spreadsheet();
        //    s.SetContentsOfCell("B1", "hello");
        //    Assert.IsTrue(new HashSet<string>(s.GetNamesOfAllNonemptyCells()).SetEquals(new HashSet<string>() { "B1" }));
        //}

        //[TestMethod()]
        //public void Test21()
        //{
        //    Spreadsheet s = new Spreadsheet();
        //    s.SetContentsOfCell("B1", "52.25");
        //    Assert.IsTrue(new HashSet<string>(s.GetNamesOfAllNonemptyCells()).SetEquals(new HashSet<string>() { "B1" }));
        //}

        //[TestMethod()]
        //public void Test22()
        //{
        //    Spreadsheet s = new Spreadsheet();
        //    s.SetContentsOfCell("B1", "=3.5");
        //    Assert.IsTrue(new HashSet<string>(s.GetNamesOfAllNonemptyCells()).SetEquals(new HashSet<string>() { "B1" }));
        //}

        //[TestMethod()]
        //public void Test23()
        //{
        //    Spreadsheet s = new Spreadsheet();
        //    s.SetContentsOfCell("A1", "17.2");
        //    s.SetContentsOfCell("C1", "hello");
        //    s.SetContentsOfCell("B1", "=3.5");
        //    Assert.IsTrue(new HashSet<string>(s.GetNamesOfAllNonemptyCells()).SetEquals(new HashSet<string>() { "A1", "B1", "C1" }));
        //}

        //// RETURN VALUE OF SET CELL CONTENTS
        //[TestMethod()]
        //public void Test24()
        //{
        //    Spreadsheet s = new Spreadsheet();
        //    s.SetContentsOfCell("B1", "hello");
        //    s.SetContentsOfCell("C1", "=5");
        //    Assert.IsTrue(s.SetContentsOfCell("A1", "17.2").SetEquals(new HashSet<string>() { "A1" }));
        //}

        //[TestMethod()]
        //public void Test25()
        //{
        //    Spreadsheet s = new Spreadsheet();
        //    s.SetContentsOfCell("A1", "17.2");
        //    s.SetContentsOfCell("C1", "=5");
        //    Assert.IsTrue(s.SetContentsOfCell("B1", "hello").SetEquals(new HashSet<string>() { "B1" }));
        //}

        //[TestMethod()]
        //public void Test26()
        //{
        //    Spreadsheet s = new Spreadsheet();
        //    s.SetContentsOfCell("A1", "17.2");
        //    s.SetContentsOfCell("B1", "hello");
        //    Assert.IsTrue(s.SetContentsOfCell("C1", "=5").SetEquals(new HashSet<string>() { "C1" }));
        //}

        //[TestMethod()]
        //public void Test27()
        //{
        //    Spreadsheet s = new Spreadsheet();
        //    s.SetContentsOfCell("A1", "=A2+A3");
        //    s.SetContentsOfCell("A2", "6");
        //    s.SetContentsOfCell("A3", "=A2+A4");
        //    s.SetContentsOfCell("A4", "=A2+A5");
        //    Assert.IsTrue(s.SetContentsOfCell("A5", "82.5").SetEquals(new HashSet<string>() { "A5", "A4", "A3", "A1" }));
        //}

        //// CHANGING CELLS
        //[TestMethod()]
        //public void Test28()
        //{
        //    Spreadsheet s = new Spreadsheet();
        //    s.SetContentsOfCell("A1", "=A2+A3");
        //    s.SetContentsOfCell("A1", "2.5");
        //    Assert.AreEqual(2.5, (double)s.GetCellContents("A1"), 1e-9);
        //}

        //[TestMethod()]
        //public void Test29()
        //{
        //    Spreadsheet s = new Spreadsheet();
        //    s.SetContentsOfCell("A1", "=A2+A3");
        //    s.SetContentsOfCell("A1", "Hello");
        //    Assert.AreEqual("Hello", (string)s.GetCellContents("A1"));
        //}

        //[TestMethod()]
        //public void Test30()
        //{
        //    Spreadsheet s = new Spreadsheet();
        //    s.SetContentsOfCell("A1", "Hello");
        //    s.SetContentsOfCell("A1", "=23");
        //    Assert.AreEqual(new Formula("23"), (Formula)s.GetCellContents("A1"));
        //    Assert.AreNotEqual(new Formula("24"), (Formula)s.GetCellContents("A1"));
        //}

        //// STRESS TESTS
        //[TestMethod()]
        //public void Test31()
        //{
        //    Spreadsheet s = new Spreadsheet();
        //    s.SetContentsOfCell("A1", "=B1+B2");
        //    s.SetContentsOfCell("B1", "=C1-C2");
        //    s.SetContentsOfCell("B2", "=C3*C4");
        //    s.SetContentsOfCell("C1", "=D1*D2");
        //    s.SetContentsOfCell("C2", "=D3*D4");
        //    s.SetContentsOfCell("C3", "=D5*D6");
        //    s.SetContentsOfCell("C4", "=D7*D8");
        //    s.SetContentsOfCell("D1", "=E1");
        //    s.SetContentsOfCell("D2", "=E1");
        //    s.SetContentsOfCell("D3", "=E1");
        //    s.SetContentsOfCell("D4", "=E1");
        //    s.SetContentsOfCell("D5", "=E1");
        //    s.SetContentsOfCell("D6", "=E1");
        //    s.SetContentsOfCell("D7", "=E1");
        //    s.SetContentsOfCell("D8", "=E1");
        //    ISet<String> cells = s.SetContentsOfCell("E1", "0");
        //    Assert.IsTrue(new HashSet<string>() { "A1", "B1", "B2", "C1", "C2", "C3", "C4", "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "E1" }.SetEquals(cells));
        //}
        //[TestMethod()]
        //public void Test32()
        //{
        //    Test31();
        //}
        //[TestMethod()]
        //public void Test33()
        //{
        //    Test31();
        //}
        //[TestMethod()]
        //public void Test34()
        //{
        //    Test31();
        //}

        //[TestMethod()]
        //public void Test35()
        //{
        //    Spreadsheet s = new Spreadsheet();
        //    ISet<String> cells = new HashSet<string>();
        //    for (int i = 1; i < 200; i++)
        //    {
        //        cells.Add("A" + i);
        //        Assert.IsTrue(cells.SetEquals(s.SetContentsOfCell("A" + i, "=A" + (i + 1))));
        //    }
        //}
        //[TestMethod()]
        //public void Test36()
        //{
        //    Test35();
        //}
        //[TestMethod()]
        //public void Test37()
        //{
        //    Test35();
        //}
        //[TestMethod()]
        //public void Test38()
        //{
        //    Test35();
        //}
        //[TestMethod()]
        //public void Test39()
        //{
        //    Spreadsheet s = new Spreadsheet();
        //    for (int i = 1; i < 200; i++)
        //    {
        //        s.SetContentsOfCell("A" + i, "=A" + (i + 1));
        //    }
        //    try
        //    {
        //        s.SetContentsOfCell("A150", "=A50");
        //        Assert.Fail();
        //    }
        //    catch (CircularException)
        //    {
        //    }
        //}
        //[TestMethod()]
        //public void Test40()
        //{
        //    Test39();
        //}
        //[TestMethod()]
        //public void Test41()
        //{
        //    Test39();
        //}
        //[TestMethod()]
        //public void Test42()
        //{
        //    Test39();
        //}

        //[TestMethod()]
        //public void Test43()
        //{
        //    Spreadsheet s = new Spreadsheet();
        //    for (int i = 0; i < 500; i++)
        //    {
        //        s.SetContentsOfCell("A1" + i, "=A1" + (i + 1));
        //    }
        //    HashSet<string> firstCells = new HashSet<string>();
        //    HashSet<string> lastCells = new HashSet<string>();
        //    for (int i = 0; i < 250; i++)
        //    {
        //        firstCells.Add("A1" + i);
        //        lastCells.Add("A1" + (i + 250));
        //    }
        //    Assert.IsTrue(s.SetContentsOfCell("A1249", "25.0").SetEquals(firstCells));
        //    Assert.IsTrue(s.SetContentsOfCell("A1499", "0").SetEquals(lastCells));
        //}
        //[TestMethod()]
        //public void Test44()
        //{
        //    Test43();
        //}
        //[TestMethod()]
        //public void Test45()
        //{
        //    Test43();
        //}
        //[TestMethod()]
        //public void Test46()
        //{
        //    Test43();
        //}

        //[TestMethod()]
        //public void Test47()
        //{
        //    RunRandomizedTest(47, 2519);
        //}
        //[TestMethod()]
        //public void Test48()
        //{
        //    RunRandomizedTest(48, 2521);
        //}
        //[TestMethod()]
        //public void Test49()
        //{
        //    RunRandomizedTest(49, 2526);
        //}
        //[TestMethod()]
        //public void Test50()
        //{
        //    RunRandomizedTest(50, 2521);
        //}

        //public void RunRandomizedTest(int seed, int size)
        //{
        //    Spreadsheet s = new Spreadsheet();
        //    Random rand = new Random(seed);
        //    for (int i = 0; i < 10000; i++)
        //    {
        //        try
        //        {
        //            switch (rand.Next(3))
        //            {
        //                case 0:
        //                    s.SetContentsOfCell(randomName(rand), "3.14");
        //                    break;
        //                case 1:
        //                    s.SetContentsOfCell(randomName(rand), "hello");
        //                    break;
        //                case 2:
        //                    s.SetContentsOfCell(randomName(rand), "" + randomFormula(rand));
        //                    break;
        //            }
        //        }
        //        catch (CircularException)
        //        {
        //        }
        //    }
        //    ISet<string> set = new HashSet<string>(s.GetNamesOfAllNonemptyCells());
        //    Assert.AreEqual(size, set.Count);
        //}

        //private String randomName(Random rand)
        //{
        //    return "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Substring(rand.Next(26), 1) + (rand.Next(99) + 1);
        //}

        //private String randomFormula(Random rand)
        //{
        //    String f = randomName(rand);
        //    for (int i = 0; i < 10; i++)
        //    {
        //        switch (rand.Next(4))
        //        {
        //            case 0:
        //                f += "+";
        //                break;
        //            case 1:
        //                f += "-";
        //                break;
        //            case 2:
        //                f += "*";
        //                break;
        //            case 3:
        //                f += "/";
        //                break;
        //        }
        //        switch (rand.Next(2))
        //        {
        //            case 0:
        //                f += 7.2;
        //                break;
        //            case 1:
        //                f += randomName(rand);
        //                break;
        //        }
        //    }
        //    return f;
        //}

        // ***************************************************************************
        // PS5 GRADING TESTS
        // ***************************************************************************

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        /// <summary>
        ///  Verifies cells and their values, which must alternate.
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="constraints"></param>
        public void VV(AbstractSpreadsheet sheet, params object[] constraints)
        {
            for (int i = 0; i < constraints.Length; i += 2)
            {
                if (constraints[i + 1] is double)
                {
                    Assert.AreEqual((double)constraints[i + 1], (double)sheet.GetCellValue((string)constraints[i]), 1e-9);
                }
                else
                {
                    Assert.AreEqual(constraints[i + 1], sheet.GetCellValue((string)constraints[i]));
                }
            }
        }


        /// <summary>
        /// For setting a spreadsheet cell.
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="name"></param>
        /// <param name="contents"></param>
        /// <returns></returns>
        public IEnumerable<string> Set(AbstractSpreadsheet sheet, string name, string contents)
        {
            List<string> result = new List<string>(sheet.SetContentsOfCell(name, contents));
            return result;
        }

        /// <summary>
        ///  Tests IsValid
        /// </summary>
        [TestMethod()]
        public void IsValidTest1()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "x");
        }
        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void IsValidTest2()
        {
            AbstractSpreadsheet ss = new Spreadsheet(s => s[0] != 'A', s => s, "");
            ss.SetContentsOfCell("A1", "x");
        }
        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void IsValidTest3()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("B1", "= A1 + C1");
        }
        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void IsValidTest4()
        {
            AbstractSpreadsheet ss = new Spreadsheet(s => s[0] != 'A', s => s, "");
            ss.SetContentsOfCell("B1", "= A1 + C1");
        }

        /// <summary>
        ///  Tests Normalize
        /// </summary>
        [TestMethod()]
        public void NormalizeTest1()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("B1", "hello");
            Assert.AreEqual("", s.GetCellContents("b1"));
        }
        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void NormalizeTest2()
        {
            AbstractSpreadsheet ss = new Spreadsheet(s => true, s => s.ToUpper(), "");
            ss.SetContentsOfCell("B1", "hello");
            Assert.AreEqual("hello", ss.GetCellContents("b1"));
        }
        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void NormalizeTest3()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("a1", "5");
            s.SetContentsOfCell("A1", "6");
            s.SetContentsOfCell("B1", "= a1");
            Assert.AreEqual(5.0, (double)s.GetCellValue("B1"), 1e-9);
        }
        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void NormalizeTest4()
        {
            AbstractSpreadsheet ss = new Spreadsheet(s => true, s => s.ToUpper(), "");
            ss.SetContentsOfCell("a1", "5");
            ss.SetContentsOfCell("A1", "6");
            ss.SetContentsOfCell("B1", "= a1");
            Assert.AreEqual(6.0, (double)ss.GetCellValue("B1"), 1e-9);
        }

        /// <summary>
        ///  Simple tests
        /// </summary>
        [TestMethod()]
        public void EmptySheet()
        {
            AbstractSpreadsheet ss = new Spreadsheet();
            VV(ss, "A1", "");
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void OneString()
        {
            AbstractSpreadsheet ss = new Spreadsheet();
            OneString(ss);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ss"></param>
        public void OneString(AbstractSpreadsheet ss)
        {
            Set(ss, "B1", "hello");
            VV(ss, "B1", "hello");
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void OneNumber()
        {
            AbstractSpreadsheet ss = new Spreadsheet();
            OneNumber(ss);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ss"></param>
        public void OneNumber(AbstractSpreadsheet ss)
        {
            Set(ss, "C1", "17.5");
            VV(ss, "C1", 17.5);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void OneFormula()
        {
            AbstractSpreadsheet ss = new Spreadsheet();
            OneFormula(ss);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ss"></param>
        public void OneFormula(AbstractSpreadsheet ss)
        {
            Set(ss, "A1", "4.1");
            Set(ss, "B1", "5.2");
            Set(ss, "C1", "= A1+B1");
            VV(ss, "A1", 4.1, "B1", 5.2, "C1", 9.3);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void Changed()
        {
            AbstractSpreadsheet ss = new Spreadsheet();
            Assert.IsFalse(ss.Changed);
            Set(ss, "C1", "17.5");
            Assert.IsTrue(ss.Changed);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void DivisionByZero1()
        {
            AbstractSpreadsheet ss = new Spreadsheet();
            DivisionByZero1(ss);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ss"></param>
        public void DivisionByZero1(AbstractSpreadsheet ss)
        {
            Set(ss, "A1", "4.1");
            Set(ss, "B1", "0.0");
            Set(ss, "C1", "= A1 / B1");
            Assert.IsInstanceOfType(ss.GetCellValue("C1"), typeof(FormulaError));
        }
        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void DivisionByZero2()
        {
            AbstractSpreadsheet ss = new Spreadsheet();
            DivisionByZero2(ss);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ss"></param>
        public void DivisionByZero2(AbstractSpreadsheet ss)
        {
            Set(ss, "A1", "5.0");
            Set(ss, "A3", "= A1 / 0.0");
            Assert.IsInstanceOfType(ss.GetCellValue("A3"), typeof(FormulaError));
        }


        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void EmptyArgument()
        {
            AbstractSpreadsheet ss = new Spreadsheet();
            EmptyArgument(ss);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ss"></param>
        public void EmptyArgument(AbstractSpreadsheet ss)
        {
            Set(ss, "A1", "4.1");
            Set(ss, "C1", "= A1 + B1");
            Assert.IsInstanceOfType(ss.GetCellValue("C1"), typeof(FormulaError));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void StringArgument()
        {
            AbstractSpreadsheet ss = new Spreadsheet();
            StringArgument(ss);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ss"></param>
        public void StringArgument(AbstractSpreadsheet ss)
        {
            Set(ss, "A1", "4.1");
            Set(ss, "B1", "hello");
            Set(ss, "C1", "= A1 + B1");
            Assert.IsInstanceOfType(ss.GetCellValue("C1"), typeof(FormulaError));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void ErrorArgument()
        {
            AbstractSpreadsheet ss = new Spreadsheet();
            ErrorArgument(ss);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ss"></param>
        public void ErrorArgument(AbstractSpreadsheet ss)
        {
            Set(ss, "A1", "4.1");
            Set(ss, "B1", "");
            Set(ss, "C1", "= A1 + B1");
            Set(ss, "D1", "= C1");
            Assert.IsInstanceOfType(ss.GetCellValue("D1"), typeof(FormulaError));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void NumberFormula1()
        {
            AbstractSpreadsheet ss = new Spreadsheet();
            NumberFormula1(ss);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ss"></param>
        public void NumberFormula1(AbstractSpreadsheet ss)
        {
            Set(ss, "A1", "4.1");
            Set(ss, "C1", "= A1 + 4.2");
            VV(ss, "C1", 8.3);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void NumberFormula2()
        {
            AbstractSpreadsheet ss = new Spreadsheet();
            NumberFormula2(ss);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ss"></param>
        public void NumberFormula2(AbstractSpreadsheet ss)
        {
            Set(ss, "A1", "= 4.6");
            VV(ss, "A1", 4.6);
        }


        /// <summary>
        ///  Repeats the simple tests all together
        /// </summary>
        [TestMethod()]
        public void RepeatSimpleTests()
        {
            AbstractSpreadsheet ss = new Spreadsheet();
            Set(ss, "A1", "17.32");
            Set(ss, "B1", "This is a test");
            Set(ss, "C1", "= A1+B1");
            OneString(ss);
            OneNumber(ss);
            OneFormula(ss);
            DivisionByZero1(ss);
            DivisionByZero2(ss);
            StringArgument(ss);
            ErrorArgument(ss);
            NumberFormula1(ss);
            NumberFormula2(ss);
        }

        /// <summary>
        ///  Four kinds of formulas
        /// </summary>
        [TestMethod()]
        public void Formulas()
        {
            AbstractSpreadsheet ss = new Spreadsheet();
            Formulas(ss);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ss"></param>
        public void Formulas(AbstractSpreadsheet ss)
        {
            Set(ss, "A1", "4.4");
            Set(ss, "B1", "2.2");
            Set(ss, "C1", "= A1 + B1");
            Set(ss, "D1", "= A1 - B1");
            Set(ss, "E1", "= A1 * B1");
            Set(ss, "F1", "= A1 / B1");
            VV(ss, "C1", 6.6, "D1", 2.2, "E1", 4.4 * 2.2, "F1", 2.0);
        }
        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void Formulasa()
        {
            Formulas();
        }
        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void Formulasb()
        {
            Formulas();
        }


        /// <summary>
        ///  Are multiple spreadsheets supported?
        /// </summary>
        [TestMethod()]
        public void Multiple()
        {
            AbstractSpreadsheet s1 = new Spreadsheet();
            AbstractSpreadsheet s2 = new Spreadsheet();
            Set(s1, "X1", "hello");
            Set(s2, "X1", "goodbye");
            VV(s1, "X1", "hello");
            VV(s2, "X1", "goodbye");
        }
        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void Multiplea()
        {
            Multiple();
        }
        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void Multipleb()
        {
            Multiple();
        }
        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void Multiplec()
        {
            Multiple();
        }

        /// <summary>
        ///  Reading/writing spreadsheets
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void SaveTest1()
        {
            AbstractSpreadsheet ss = new Spreadsheet();
            ss.Save("q:\\missing\\save.txt");
        }
        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void SaveTest2()
        {
            AbstractSpreadsheet ss = new Spreadsheet("q:\\missing\\save.txt", s => true, s => s, "");
        }
        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void SaveTest3()
        {
            AbstractSpreadsheet s1 = new Spreadsheet();
            Set(s1, "A1", "hello");
            s1.Save("save1.txt");
            s1 = new Spreadsheet("save1.txt", s => true, s => s, "default");
            Assert.AreEqual("hello", s1.GetCellContents("A1"));
        }
        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void SaveTest4()
        {
            using (StreamWriter writer = new StreamWriter("save2.txt"))
            {
                writer.WriteLine("This");
                writer.WriteLine("is");
                writer.WriteLine("a");
                writer.WriteLine("test!");
            }
            AbstractSpreadsheet ss = new Spreadsheet("save2.txt", s => true, s => s, "");
        }
        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void SaveTest5()
        {
            AbstractSpreadsheet ss = new Spreadsheet();
            ss.Save("save3.txt");
            ss = new Spreadsheet("save3.txt", s => true, s => s, "version");
        }
        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void SaveTest6()
        {
            AbstractSpreadsheet ss = new Spreadsheet(s => true, s => s, "hello");
            ss.Save("save4.txt");
            Assert.AreEqual("hello", new Spreadsheet().GetSavedVersion("save4.txt"));
        }
        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void SaveTest7()
        {
            using (XmlWriter writer = XmlWriter.Create("save5.txt"))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("spreadsheet");
                writer.WriteAttributeString("version", "");

                writer.WriteStartElement("cell");
                writer.WriteElementString("name", "A1");
                writer.WriteElementString("contents", "hello");
                writer.WriteEndElement();

                writer.WriteStartElement("cell");
                writer.WriteElementString("name", "A2");
                writer.WriteElementString("contents", "5.0");
                writer.WriteEndElement();

                writer.WriteStartElement("cell");
                writer.WriteElementString("name", "A3");
                writer.WriteElementString("contents", "4.0");
                writer.WriteEndElement();

                writer.WriteStartElement("cell");
                writer.WriteElementString("name", "A4");
                writer.WriteElementString("contents", "= A2 + A3");
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
            AbstractSpreadsheet ss = new Spreadsheet("save5.txt", s => true, s => s, "");
            VV(ss, "A1", "hello", "A2", 5.0, "A3", 4.0, "A4", 9.0);
        }
        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void SaveTest8()
        {
            AbstractSpreadsheet ss = new Spreadsheet();
            Set(ss, "A1", "hello");
            Set(ss, "A2", "5.0");
            Set(ss, "A3", "4.0");
            Set(ss, "A4", "= A2 + A3");
            ss.Save("save6.txt");
            using (XmlReader reader = XmlReader.Create("save6.txt"))
            {
                int spreadsheetCount = 0;
                int cellCount = 0;
                bool A1 = false;
                bool A2 = false;
                bool A3 = false;
                bool A4 = false;
                string name = null;
                string contents = null;

                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "spreadsheet":
                                Assert.AreEqual("default", reader["version"]);
                                spreadsheetCount++;
                                break;

                            case "cell":
                                cellCount++;
                                break;

                            case "name":
                                reader.Read();
                                name = reader.Value;
                                break;

                            case "contents":
                                reader.Read();
                                contents = reader.Value;
                                break;
                        }
                    }
                    else
                    {
                        switch (reader.Name)
                        {
                            case "cell":
                                if (name.Equals("A1")) { Assert.AreEqual("hello", contents); A1 = true; }
                                else if (name.Equals("A2")) { Assert.AreEqual(5.0, Double.Parse(contents), 1e-9); A2 = true; }
                                else if (name.Equals("A3")) { Assert.AreEqual(4.0, Double.Parse(contents), 1e-9); A3 = true; }
                                else if (name.Equals("A4")) { contents = contents.Replace(" ", ""); Assert.AreEqual("=A2+A3", contents); A4 = true; }
                                else Assert.Fail();
                                break;
                        }
                    }
                }
                Assert.AreEqual(1, spreadsheetCount);
                Assert.AreEqual(4, cellCount);
                Assert.IsTrue(A1);
                Assert.IsTrue(A2);
                Assert.IsTrue(A3);
                Assert.IsTrue(A4);
            }
        }


        /// <summary>
        ///  Fun with formulas
        /// </summary>

        [TestMethod()]
        public void Formula1()
        {
            Formula1(new Spreadsheet());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ss"></param>
        public void Formula1(AbstractSpreadsheet ss)
        {
            Set(ss, "a1", "= a2 + a3");
            Set(ss, "a2", "= b1 + b2");
            Assert.IsInstanceOfType(ss.GetCellValue("a1"), typeof(FormulaError));
            Assert.IsInstanceOfType(ss.GetCellValue("a2"), typeof(FormulaError));
            Set(ss, "a3", "5.0");
            Set(ss, "b1", "2.0");
            Set(ss, "b2", "3.0");
            VV(ss, "a1", 10.0, "a2", 5.0);
            Set(ss, "b2", "4.0");
            VV(ss, "a1", 11.0, "a2", 6.0);
        }
        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void Formula2()
        {
            Formula2(new Spreadsheet());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ss"></param>
        public void Formula2(AbstractSpreadsheet ss)
        {
            Set(ss, "a1", "= a2 + a3");
            Set(ss, "a2", "= a3");
            Set(ss, "a3", "6.0");
            VV(ss, "a1", 12.0, "a2", 6.0, "a3", 6.0);
            Set(ss, "a3", "5.0");
            VV(ss, "a1", 10.0, "a2", 5.0, "a3", 5.0);
        }
        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void Formula3()
        {
            Formula3(new Spreadsheet());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ss"></param>
        public void Formula3(AbstractSpreadsheet ss)
        {
            Set(ss, "a1", "= a3 + a5");
            Set(ss, "a2", "= a5 + a4");
            Set(ss, "a3", "= a5");
            Set(ss, "a4", "= a5");
            Set(ss, "a5", "9.0");
            VV(ss, "a1", 18.0);
            VV(ss, "a2", 18.0);
            Set(ss, "a5", "8.0");
            VV(ss, "a1", 16.0);
            VV(ss, "a2", 16.0);
        }
        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void Formula4()
        {
            AbstractSpreadsheet ss = new Spreadsheet();
            Formula1(ss);
            Formula2(ss);
            Formula3(ss);
        }
        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void Formula4a()
        {
            Formula4();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void MediumSheet()
        {
            AbstractSpreadsheet ss = new Spreadsheet();
            MediumSheet(ss);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ss"></param>
        public void MediumSheet(AbstractSpreadsheet ss)
        {
            Set(ss, "A1", "1.0");
            Set(ss, "A2", "2.0");
            Set(ss, "A3", "3.0");
            Set(ss, "A4", "4.0");
            Set(ss, "B1", "= A1 + A2");
            Set(ss, "B2", "= A3 * A4");
            Set(ss, "C1", "= B1 + B2");
            VV(ss, "A1", 1.0, "A2", 2.0, "A3", 3.0, "A4", 4.0, "B1", 3.0, "B2", 12.0, "C1", 15.0);
            Set(ss, "A1", "2.0");
            VV(ss, "A1", 2.0, "A2", 2.0, "A3", 3.0, "A4", 4.0, "B1", 4.0, "B2", 12.0, "C1", 16.0);
            Set(ss, "B1", "= A1 / A2");
            VV(ss, "A1", 2.0, "A2", 2.0, "A3", 3.0, "A4", 4.0, "B1", 1.0, "B2", 12.0, "C1", 13.0);
        }
        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void MediumSheeta()
        {
            MediumSheet();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void MediumSave()
        {
            AbstractSpreadsheet ss = new Spreadsheet();
            MediumSheet(ss);
            ss.Save("save7.txt");
            ss = new Spreadsheet("save7.txt", s => true, s => s, "default");
            VV(ss, "A1", 2.0, "A2", 2.0, "A3", 3.0, "A4", 4.0, "B1", 1.0, "B2", 12.0, "C1", 13.0);
        }
        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void MediumSavea()
        {
            MediumSave();
        }


        /// <summary>
        /// A long chained formula.  If this doesn't finish within 60 seconds, it fails.
        /// </summary>
        [TestMethod()]
        public void LongFormulaTest()
        {
            object result = "";
            Thread t = new Thread(() => LongFormulaHelper(out result));
            t.Start();
            t.Join(60 * 1000);
            if (t.IsAlive)
            {
                t.Abort();
                Assert.Fail("Computation took longer than 60 seconds");
            }
            Assert.AreEqual("ok", result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        public void LongFormulaHelper(out object result)
        {
            try
            {
                AbstractSpreadsheet s = new Spreadsheet();
                s.SetContentsOfCell("sum1", "= a1 + a2");
                int i;
                int depth = 100;
                for (i = 1; i <= depth * 2; i += 2)
                {
                    s.SetContentsOfCell("a" + i, "= a" + (i + 2) + " + a" + (i + 3));
                    s.SetContentsOfCell("a" + (i + 1), "= a" + (i + 2) + "+ a" + (i + 3));
                }
                s.SetContentsOfCell("a" + i, "1");
                s.SetContentsOfCell("a" + (i + 1), "1");
                Assert.AreEqual(Math.Pow(2, depth + 1), (double)s.GetCellValue("sum1"), 1.0);
                s.SetContentsOfCell("a" + i, "0");
                Assert.AreEqual(Math.Pow(2, depth), (double)s.GetCellValue("sum1"), 1.0);
                s.SetContentsOfCell("a" + (i + 1), "0");
                Assert.AreEqual(0.0, (double)s.GetCellValue("sum1"), 0.1);
                result = "ok";
            }
            catch (Exception e)
            {
                result = e;
            }
        }
    }
}
