// These tests require that a shortcut to the executable file SpreadsheetGUI.exe
//   be placed in the SpreadsheetGUI directory and that an Explorer window is open
//   to that directory. Therefore, create a shortcut of SpreadsheetGUI.exe and place it
//   in the directory   ..PS6/SpreadsheetGUI/
//   Make sure an Explorer window is open to this directory and that the shortcut exists
//     and is named SpreadsheetGUI.exe - Shortcut.  
//   During testing, three .ss files will be created in the PS6 directory. These files must
//     be deleted if the test cases are to be executed more than once. Otherwise, the test cases
//     will not know how to handle the SaveAs dialog and the tests will lose focus and fail.

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;


namespace ViewControllerUITest
{
    /// <summary>
    /// Tests the basic functionality of the Spreadsheet GUI.
    /// </summary>
    [CodedUITest]
    public class CodedUITest1
    {
        public CodedUITest1()
        {
        }
        
        /// <summary>
        /// Test initial launch of application.
        /// </summary>
        [TestMethod]
        public void UIOpen1()
        {
            // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
            // For more information on generated code, see http://go.microsoft.com/fwlink/?LinkId=179463

            // Beginning of Recording
            this.UIMap.AppLaunch();
            this.UIMap.InitialSelection();
            this.UIMap.Close1();
            // End of Recording
        }

        /// <summary>
        /// Test simple data input.
        /// </summary>
        [TestMethod]
        public void UIOpen2()
        {
            // Beginning of Recording
            this.UIMap.DataInput1();
            this.UIMap.CellSelection1();
            this.UIMap.CellSelection2();
            this.UIMap.CellSelection3();
            this.UIMap.CellSelection4();
            this.UIMap.Close2();
            // End of Recording
        }

        /// <summary>
        /// Test input resulting in FormulaErrors.
        /// </summary>
        [TestMethod]
        public void UIDependencies1()
        {
            // Beginning of Recording
            this.UIMap.OpenDataInput1();
            this.UIMap.CellSelection5();
            this.UIMap.FormulaError1();
            this.UIMap.DataInput2();
            this.UIMap.CellSelection6();
            this.UIMap.FormulaError2();
            this.UIMap.FormulaError3();
            this.UIMap.Close3();
            // End of Recording
        }

        /// <summary>
        /// Test input resulting in FormulaErrors, saving, and opening Forms.
        /// </summary>
        [TestMethod]
        public void UIDependencies2()
        {
            // Beginning of Recording
            this.UIMap.DataInput3();
            this.UIMap.FormulaError4();
            this.UIMap.FormulaError5();
            this.UIMap.CellSelection7();
            this.UIMap.DataInput4();
            this.UIMap.FormulaError6();
            this.UIMap.Save1();
            this.UIMap.CloseOpen1();
            this.UIMap.FormulaError7();
            this.UIMap.DataInput5();
            this.UIMap.FormulaError8();
            this.UIMap.SaveClose1();
            // End of Recording
        }

        /// <summary>
        /// Tests the saftey checks of saving over a file.
        /// </summary>
        [TestMethod]
        public void UISaveAs()
        {
            // Beginning of Recording
            this.UIMap.DataInput6();
            this.UIMap.FormulaError9();
            this.UIMap.DataInput7();
            this.UIMap.FormulaError10();
            this.UIMap.Close4();
            // End of Recording
        }

        /// <summary>
        /// Tests that the program only exists when the last Form is closed.
        /// </summary>
        [TestMethod]
        public void UINewForm()
        {
            // Beginning of Recording
            this.UIMap.OpenClose();
            // End of Recording
        }

        /// <summary>
        /// Tests the proper functionality of Option menu features.
        /// </summary>
        [TestMethod]
        public void UIFeatures()
        {
            // Beginning of Recording
            this.UIMap.DataInput8();
            this.UIMap.CellSelection8();
            this.UIMap.Features1();
            this.UIMap.CellSelection9();
            this.UIMap.CellSelection10();
            this.UIMap.CellSelection11();
            this.UIMap.Close6();
            // End of Recording
        }

        /// <summary>
        /// Tests for circular dependencies.
        /// </summary>
        [TestMethod]
        public void UIDependencies3()
        {
            // Beginning of Recording
            this.UIMap.DataInput9();
            this.UIMap.FormulaError11();
            this.UIMap.DataInput10();
            this.UIMap.Circular1();
            this.UIMap.Circular2();
            this.UIMap.Circular3();
            this.UIMap.Circular4();
            this.UIMap.Close7();
            // End of Recording
        }

        /// <summary>
        /// Tests that the help menu opens.
        /// </summary>
        [TestMethod]
        public void UIHelp()
        {
            this.UIMap.Help();
        }
        

        #region Additional test attributes

        // You can use the following additional attributes as you write your tests:

        ////Use TestInitialize to run code before running each test 
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //    // For more information on generated code, see http://go.microsoft.com/fwlink/?LinkId=179463
        //}

        ////Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //    // For more information on generated code, see http://go.microsoft.com/fwlink/?LinkId=179463
        //}

        #endregion

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
        private TestContext testContextInstance;

        public UIMap UIMap
        {
            get
            {
                if ((this.map == null))
                {
                    this.map = new UIMap();
                }

                return this.map;
            }
        }

        private UIMap map;
    }
}
