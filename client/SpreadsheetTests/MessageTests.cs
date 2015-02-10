using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using SS;

namespace SpreadsheetTests
{
    [TestClass]
    public class MessageTests
    {
        [TestMethod]
        public void CreateMessage()
        {
            // Assuming that \n will be stripped off by the string socket.
            Message incoming1 = new Message("FILELIST\u001Bsheet1.ss\u001Bsheet2.ss\u001Bsheet3.ss");
        }

        [TestMethod]
        public void ParseMessageType()
        {
            Message incoming1 = new Message("FILELIST\u001Bsheet1.ss\u001Bsheet2.ss\u001Bsheet3.ss");
            Assert.AreEqual(9, incoming1.GetMessageTypeCode());
            Assert.AreEqual("FILELIST", incoming1.GetMessageTypeName());
        }

        [TestMethod]
        public void ParseMessageParams()
        {
            Message incoming1 = new Message("FILELIST\u001Bsheet1.ss\u001Bsheet2.ss\u001Bsheet3.ss");
            List<String> parameters = new List<String>();
            List<String> expected_parameters = new List<String> {"sheet1.ss", "sheet2.ss", "sheet3.ss"};
            foreach (String s in incoming1.GetMessageParametersEnum())
            {
                parameters.Add(s);
            }
            //Confirm that the message type isn't the first item in Message's parameters enum.
            Assert.AreNotEqual(parameters[0], "FILELIST");

            for (int i = 0; i < 3; i++)
            {
                Assert.AreEqual(parameters[i], expected_parameters[i]);
            }
        }

        [TestMethod]
        public void TypeInvalid()
        {
            Message incoming1 = new Message("INVALID");
            Assert.AreEqual(10, incoming1.GetMessageTypeCode());
            Assert.AreEqual("INVALID", incoming1.GetMessageTypeName());
            Assert.AreEqual(0, incoming1.GetMessageParametersList().Count);
        }

        [TestMethod]
        public void TypeUpdateWithParams()
        {
            Message incoming1 = new Message("UPDATE\u001B99999999\u001BA1\u001B=(B1+C1+D1)/500");
            
            List<String> parameters = new List<String>();
            List<String> expected_parameters = new List<String> { "99999999", "A1", "=(B1+C1+D1)/500" };
            foreach (String s in incoming1.GetMessageParametersEnum())
            {
                parameters.Add(s);
            }

            for (int i = 0; i < 2; i++)
            {
                Assert.AreEqual(parameters[i], expected_parameters[i]);
            }

            Assert.AreEqual(11, incoming1.GetMessageTypeCode());
            Assert.AreEqual("UPDATE", incoming1.GetMessageTypeName());
        }

        [TestMethod]
        public void BuildPasswordMessage()
        {
            List<String> parameters = new List<string> { "PASSWORD", "0123456789" };
            Message outgoing1 = new Message(parameters);
            Assert.AreEqual("PASSWORD\u001B0123456789\n", outgoing1.GetMessage());
        }

        [TestMethod]
        public void BuildOpenMessage()
        {
            List<String> parameters = new List<string> { "OPEN", "sheet1.ss" };
            Message outgoing1 = new Message(parameters);
            Assert.AreEqual("OPEN\u001Bsheet1.ss\n", outgoing1.GetMessage());
        }

        [TestMethod]
        public void BuildCreateMessage()
        {
            List<String> parameters = new List<string> { "CREATE", "sheet1.ss" };
            Message outgoing1 = new Message(parameters);
            Assert.AreEqual("CREATE\u001Bsheet1.ss\n", outgoing1.GetMessage());
        }

        [TestMethod]
        public void BuildEnterMessage()
        {
            List<String> parameters = new List<string> { "ENTER", "A1", "=B1*C1", "sheet1.ss" };
            Message outgoing1 = new Message(parameters);
            Assert.AreEqual("ENTER\u001BA1\u001B=B1*C1\u001Bsheet1.ss\n", outgoing1.GetMessage());
        }

        [TestMethod]
        public void BuildUndoMessage()
        {
            List<String> parameters = new List<string> { "UNDO", "sheet1.ss" };
            Message outgoing1 = new Message(parameters);
            Assert.AreEqual("UNDO\u001Bsheet1.ss\n", outgoing1.GetMessage());
        }

        [TestMethod]
        public void BuildSaveMessage()
        {
            List<String> parameters = new List<string> { "SAVE", "sheet1.ss" };
            Message outgoing1 = new Message(parameters);
            Assert.AreEqual("SAVE\u001Bsheet1.ss\n", outgoing1.GetMessage());
        }
        [TestMethod]
        public void BuildDisconnectMessage()
        {
            List<String> parameters = new List<string> { "DISCONNECT" };
            Message outgoing1 = new Message(parameters);
            Assert.AreEqual("DISCONNECT\n", outgoing1.GetMessage());
        }

        [TestMethod]
        [ExpectedException(typeof(MessageException))]
        public void ExcTest_BuildUnsupportedMessage_TooManyParameters()
        {
            List<String> parameters = new List<string> { "ENTER", "A1", "=B1*C1", "sheet1.ss", "extra_param" };
            Message outgoing1 = new Message(parameters);
        }

        [TestMethod]
        [ExpectedException(typeof(MessageException))]
        public void ExcTest_BuildUnsupportedMessage_TooFewParameters()
        {
            List<String> parameters = new List<string> { "ENTER", "A1", "=B1*C1" };
            Message outgoing1 = new Message(parameters);
        }

        [TestMethod]
        [ExpectedException(typeof(MessageException))]
        public void ExcTest_BuildUnsupportedMessage_TypeNotProtocol()
        {
            List<String> parameters = new List<string> { "SKYNTAX", "A1", "=B1*C1", "sheet1.ss", "extra_param" };
            Message outgoing1 = new Message(parameters);
        }

        [TestMethod]
        [ExpectedException(typeof(MessageException))]
        public void ExcTest_ParseMessageParams_NoParameters()
        {
            Message incoming1 = new Message("FILELIST");
        }




    }
}
