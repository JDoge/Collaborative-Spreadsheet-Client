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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using SS;
using SpreadsheetUtilities;
using System.IO;
using System.Text.RegularExpressions;
using CustomNetworking;
using System.Net.Sockets;

namespace SpreadsheetGUI
{
    public partial class Form1 : Form
    {
        // Field for Spreadsheet Object
        private Spreadsheet sheet;
        private TextBox help;

        private bool connected_to_server;
        private String lastMessageFromServer;

        /// <summary>
        /// Constructor creates a new Spreadsheet Form.
        /// </summary>
        public Form1() : this(null, "untitled") { }

        /// <summary>
        /// Overloaded Constructor creates a new Spreadsheet Form based on parameter.
        /// </summary>
        /// <param name="_sheet">The spreadsheet data needed to construct representative form.</param>
        public Form1(Spreadsheet _sheet, string fileName)
        {
            socket = null;

            InitializeComponent();

            if (ReferenceEquals(_sheet, null)) // User wants to create a new Spreadsheet
            {
                sheet = new Spreadsheet(s => Regex.IsMatch(s, @"^[a-zA-Z][1-9][0-9]?$"),
                                        s => s.ToUpper(), "ps6");
            }
            else // User wants to open an existing Spreadsheet from file
            {
                sheet = _sheet;
                foreach (string name in sheet.GetNamesOfAllNonemptyCells())
                {
                    int col, row;
                    string cell_value;
                    delimitCellName(name, out col, out row, out cell_value);
                    spreadsheetPanel1.SetValue(col, row, cell_value);
                    toolStripStatusLabel1.Text = fileName;

                    setFormText(fileName);
                }
            }

            // Notify Panel that an event has taken place
            spreadsheetPanel1.SetSelection(2, 3);
            spreadsheetPanel1.SelectionChanged += displaySelection;
        }

        private void IncomingEvent(String line)
        {

        }

        /// <summary>
        /// Updates the Spreadsheet Panel and signals Panel to ReDraw.
        /// </summary>
        /// <param name="ss">The Panel to ReDraw</param>
        private void displaySelection(SpreadsheetPanel ss)
        {
            // Get the currently selected cell
            int row, col;
            String value;
            ss.GetSelection(out col, out row);
            ss.GetValue(col, row, out value);

            // Update cell name labels in layout panel
            string cell = getPanelCellName(col, row);
            LabelName1.Text = cell;
            LabelName2.Text = cell;

            // Update cell value and contents
            double result;
            if (double.TryParse(sheet.GetCellValue(cell).ToString(), out result))
            {
                if (result < 0)
                {
                    LabelValue2.ForeColor = Color.Red;   // Set color to Red for negative numbers
                }
                else
                {
                    LabelValue2.ForeColor = Color.Black; // Set color to Black for negative numbers
                }
            }

            // Update the Value
            LabelValue2.Text = sheet.GetCellValue(cell).ToString();

            // Formulas must retain equals sign in contents box
            if (sheet.GetCellContents(cell) is Formula)
            {
                TextContents.Text = "=" + sheet.GetCellContents(cell).ToString(); // Re-Prepend the Equals sign for formulas
            }
            else
            {
                TextContents.Text = sheet.GetCellContents(cell).ToString(); // Update the Contents Editor
            }

            // Use the status bar to print out all messages from server
            if(connected_to_server)
            {
                toolStripStatusLabel1.Text = lastMessageFromServer;
            }
        }

        /// <summary>
        /// Closes the current form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Performs safety check during closure of form.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (safetyCheckOnClose().Equals(System.Windows.Forms.DialogResult.Cancel))
            {
                e.Cancel = true; // If user selected cancel then do not close form
            }
        }

        /// <summary>
        /// Checks if form is safe to close without loss of data.
        /// </summary>
        /// <returns>True if no data will be lost if form is closed; else false.</returns>
        private System.Windows.Forms.DialogResult safetyCheckOnClose()
        {
            // Check if spreadsheet has been changed before closing
            if (!sheet.Changed)
            {
                return System.Windows.Forms.DialogResult.OK;
            }

            // Prompt user that unsaved changes will be lost.
            else
            {
                // Set up message box
                string message = "Would you like to save changes?";
                string caption = "Warning! Unsaved changes will be lost.";
                MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
                DialogResult result;

                // Display message box and Save dialog if user selects Yes
                result = MessageBox.Show(message, caption, buttons);
                switch (result)
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        // if Online Spreadsheet flag is set, send "DISCONNECT\n" to server
                        if (!ReferenceEquals(socket, null) && socket.isConnected())
                        {
                            // Build a Message object for the disconnect message.
                            SS.Message disconnect_message = new SS.Message(new List<string>() { "DISCONNECT" });
                            // Send the disconnect message to the server.
                            socket.BeginSend(disconnect_message.GetMessage(), (a, b) => { }, null);
                        }
                        saveToolStripMenuItem_Click(this, new EventArgs());
                        return System.Windows.Forms.DialogResult.Yes;

                    case System.Windows.Forms.DialogResult.No:
                        // if Online Spreadsheet flag is set, send "DISCONNECT\n" to server
                        if (!ReferenceEquals(socket, null) && socket.isConnected())
                        {
                            // Build a Message object for the disconnect message.
                            SS.Message disconnect_message = new SS.Message(new List<string>() { "DISCONNECT" });
                            // Send the disconnect message to the server.
                            socket.BeginSend(disconnect_message.GetMessage(), (a, b) => { }, null);
                        }
                        return System.Windows.Forms.DialogResult.No;

                    case System.Windows.Forms.DialogResult.Cancel:
                        return System.Windows.Forms.DialogResult.Cancel;

                    default:
                        return System.Windows.Forms.DialogResult.OK;
                }
            }
        }

        /// <summary>
        /// Creates a new form window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SpreadsheetApplicationContext.getAppContext().RunForm(new Form1());
        }

        /// <summary>
        /// Displays the help contents for the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void viewHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            help = new TextBox();
            Help.ShowHelp(help, "../../../HelpContents.html");
        }

        /// <summary>
        /// Provides functionality to Menu ToolStrip.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create an open dialog that filters *.ss files from *.* files
            Stream file;
            OpenFileDialog openDialog = new OpenFileDialog();

            openDialog.Filter = "ss files (*.ss)|*.ss|All files (*.*)|*.*";
            openDialog.FilterIndex = 1;
            openDialog.DefaultExt = "ss";

            // Show dialog and check validity of user input
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((file = openDialog.OpenFile()) != null)
                    {
                        Spreadsheet toOpen = new Spreadsheet(openDialog.FileName,
                            s => Regex.IsMatch(s, @"^[a-zA-Z]+[1-9]+\d*$"),
                            s => s.ToUpper(), "ps6");
                        SpreadsheetApplicationContext.getAppContext().RunForm(new Form1(toOpen, openDialog.FileName));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error. Could not open file from disk. Original error: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Saves the current Form to disk.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // if Online Spreadsheet flag is set, send "SAVE\n" to server
            if (!ReferenceEquals(socket, null) && socket.isConnected())
            {
                // Build a Message object for the save message.
                SS.Message save_message = new SS.Message(new List<string>() { "SAVE", current_version_number.ToString() });
                // Send the save message to the server.
                socket.BeginSend(save_message.GetMessage(), (a, b) => { }, null);
            }
            else // else, regular save
            {
                // Create an save dialog that filters *.ss files from *.* files
                Stream file;
                SaveFileDialog saveDialog = new SaveFileDialog();

                saveDialog.Filter = "ss files (*.ss)|*.ss|All files (*.*)|*.*";
                saveDialog.FilterIndex = 1;
                saveDialog.RestoreDirectory = true;
                saveDialog.DefaultExt = "ss";

                // Show dialog and check validity of user input
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    if ((file = saveDialog.OpenFile()) != null)
                    {
                        file.Close();  // Close stream before saving or Spreadsheet.Save will throw an exception
                        sheet.Save(saveDialog.FileName);
                        toolStripStatusLabel1.Text = saveDialog.FileName;

                        setFormText(saveDialog.FileName);
                    }
                }
            }
        }

        /// <summary>
        /// Updates Panel and Spreadsheet.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextContents_KeyDown(object sender, KeyEventArgs e)
        {
            int col, row;
            spreadsheetPanel1.GetSelection(out col, out row);
            keyPressed(col, row, e);
        }

        /// <summary>
        /// Sets the contents of cell identified by the received parameters.
        /// </summary>
        /// <param name="col">Column of cell.</param>
        /// <param name="row">Row of cell.</param>
        private void setCell(int col, int row)
        {
            string cell = getPanelCellName(col, row);

            // If connected to the server, send cell and contents to the server.
            // Else, update cell as usual.
            if (connected_to_server)
            {
                String contents;
                if (TextContents.Text.StartsWith("="))
                {
                    contents = TextContents.Text.ToUpper();
                }
                else
                {
                    contents = TextContents.Text;
                }
                SS.Message update_message = new SS.Message(new List<String>() { "ENTER", current_version_number.ToString(), cell, contents });
                socket.BeginSend(update_message.GetMessage(), (a, b) => { }, null);
            }
            else
            {
                // Re-Calculate all cells that have changed as a result of setting this Cell
                try
                {
                    int newCol, newRow;
                    ISet<string> cellToRecalculate = sheet.SetContentsOfCell(cell, TextContents.Text);
                    foreach (string name in cellToRecalculate)
                    {
                        delimitCellName(name, out newCol, out newRow);
                        spreadsheetPanel1.SetValue(newCol, newRow, sheet.GetCellValue(name).ToString());
                    }
                }
                // If a circular dependency exists do not allow the user to continue
                catch (CircularException c)
                {
                    string message = "Cells cannot equal themselves.";
                    string caption = "Error! " + c.Message;
                    MessageBox.Show(message, caption);
                }
                // If Formula throws an exception do not allow the user to continue
                catch (FormulaFormatException f)
                {
                    string message = f.Message;
                    string caption = "Error! Input Is Not A Valid Formula.";
                    MessageBox.Show(message, caption);
                }
            }
        }

        /// <summary>
        /// Event handlers for keys pressed within contents text box editor.
        /// </summary>
        /// <param name="col">Column of cell</param>
        /// <param name="row">Row of cell</param>
        /// <param name="e"></param>
        private void keyPressed(int col, int row, KeyEventArgs e)
        {
            string cell = getPanelCellName(col, row);
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    setCell(col, row);
                    spreadsheetPanel1.SetValue(col, row, sheet.GetCellValue(cell).ToString());
                    spreadsheetPanel1.SetSelection(col, row + 1);
                    displaySelection(spreadsheetPanel1);
                    break;

                case Keys.Up:
                    setCell(col, row);
                    spreadsheetPanel1.SetValue(col, row, sheet.GetCellValue(cell).ToString());
                    spreadsheetPanel1.SetSelection(col, row - 1);
                    displaySelection(spreadsheetPanel1);
                    break;

                case Keys.Down:
                    setCell(col, row);
                    spreadsheetPanel1.SetValue(col, row, sheet.GetCellValue(cell).ToString());
                    spreadsheetPanel1.SetSelection(col, row + 1);
                    displaySelection(spreadsheetPanel1);
                    break;

                case Keys.Tab:
                    setCell(col, row);
                    spreadsheetPanel1.SetValue(col, row, sheet.GetCellValue(cell).ToString());
                    spreadsheetPanel1.SetSelection(col + 1, row);
                    displaySelection(spreadsheetPanel1);
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Provides navigational functionality to Panel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spreadsheetPanel1_KeyDown(object sender, KeyEventArgs e)
        {
            int col, row;
            spreadsheetPanel1.GetSelection(out col, out row);

            switch (e.KeyCode)
            {
                case Keys.N:
                    spreadsheetPanel1.SetSelection(col - 1, row);
                    displaySelection(spreadsheetPanel1);
                    break;

                case Keys.M:
                    spreadsheetPanel1.SetSelection(col + 1, row);
                    displaySelection(spreadsheetPanel1);
                    break;

                case Keys.P:
                    spreadsheetPanel1.SetSelection(col, row - 1);
                    displaySelection(spreadsheetPanel1);
                    break;

                case Keys.L:
                    spreadsheetPanel1.SetSelection(col, row + 1);
                    displaySelection(spreadsheetPanel1);
                    break;

                case Keys.Tab:
                    spreadsheetPanel1.SetSelection(col + 1, row);
                    displaySelection(spreadsheetPanel1);
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Separates column from row in a given cell name.
        /// </summary>
        /// <param name="name">The name of the cell.</param>
        /// <param name="col">An integer to represent the column.</param>
        /// <param name="row">An integer to represent the row.</param>
        private void delimitCellName(string name, out int col, out int row, out string cell_value)
        {
            col = name.ElementAt(0) - 65;
            row = int.Parse(name.Substring(1, name.Length - 1)) - 1;
            cell_value = sheet.GetCellValue(name).ToString();
        }

        /// <summary>
        /// Separates column from row in a given cell name.
        /// </summary>
        /// <param name="name">The name of the cell.</param>
        /// <param name="col">An integer to represent the column.</param>
        private void delimitCellName(string name, out int col, out int row)
        {
            col = name.ElementAt(0) - 65;
            row = int.Parse(name.Substring(1, name.Length - 1)) - 1;
        }

        /// <summary>
        /// Returns the string value of a cell based on column and row.
        /// </summary>
        /// <param name="col">The Panel's Column.</param>
        /// <param name="row">The Panel's Row.</param>
        /// <returns>A string representation of the selected cell in the Panel.</returns>
        private string getPanelCellName(int col, int row)
        {
            char c = (char)(65 + col);
            return "" + c + (row + 1);
        }

        /// <summary>
        /// Sets the Form text to display the current file name.
        /// </summary>
        /// <param name="fileName">The file name of this form.</param>
        private void setFormText(string fileName)
        {
            StringBuilder sb = new StringBuilder();
            char[] path = fileName.ToCharArray();

            // Extract the portion of absolute path (from right to left) until the first slash is found
            for (int i = path.Count() - 1; i >= 0; i--)
            {
                // 92 is ASCII for /
                if (path[i] != 92)
                {
                    sb.Insert(0, path[i]);
                }
                else
                {
                    break;
                }
            }

            this.Text = "Spreadsheet - " + sb.ToString();
        }

        /// <summary>
        /// Displays the dependents of the current cell.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void displayDependentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Get the current cell that is selected
            int col, row;
            spreadsheetPanel1.GetSelection(out col, out row);
            string cell = getPanelCellName(col, row);

            // Capture the value of the current cell
            object cellContents = sheet.GetCellContents(cell);
            string contents;

            // Prepend equals sign to string
            if (cellContents is Formula)
            {
                contents = "=" + cellContents.ToString();
            }
            else
            {
                contents = cellContents.ToString();
            }

            // Get collection of cell affected by changing current cell
            StringBuilder sb = new StringBuilder();
            foreach (string name in sheet.SetContentsOfCell(cell, contents))
            {
                sb.Append(name + "\n");
            }

            // Remove current cell from output
            sb.Remove(0, cell.Length + 1);

            // Output dependent cells
            string message = sb.ToString();
            string caption = cell + "'s Dependents Are The Following Cells:";
            MessageBox.Show(message, caption);
        }

        /// <summary>
        /// Displays all cells whose value is a negative number.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showAllNegativesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Get collection of cell affected by changing current cell
            StringBuilder sb = new StringBuilder();
            Object cellValue;
            foreach (string name in sheet.GetNamesOfAllNonemptyCells())
            {
                cellValue = sheet.GetCellValue(name);
                if (cellValue is double && (double)cellValue < 0)
                {
                    sb.Append(name + " = " + cellValue.ToString() + "\n");
                }
            }

            // Output dependent cells
            string message = sb.ToString();
            string caption = "The Following Cells Have Negative Values:";
            MessageBox.Show(message, caption);
        }


        //-----------------------------Server Spreadsheet Code------------------------------------//

        // A TCP socket for connecting to the spreadsheet server.
        StringSocket socket;
        // When opening or creating a SS from the server, set the local version number to the update version number.
        bool first_update;
        // The version number is used to recognize if the local SS is out of sync with the server's version.
        int current_version_number;
        // The hostname of the server.
        String host;
        // The server's port number;
        int port;

        /*
         * When "Connect to Server" is selected from the File menu, connect to the server
         * and display the password textbox.
         */
        private void connectToServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ReferenceEquals(socket, null) || !socket.isConnected())
            {
                // Display the flow layout panel, which contains both the password and file list panels.
                flowLayoutPanel1.Visible = true;
                hostnameTextbox.Visible = true;
                portTextbox.Visible = true;
                hostSubmitButton.Visible = true;
                hostnameLabel.Visible = true;
                portLabel.Visible = true;
                hostConnectPanel.Visible = true;
                // Place the cursor in the hostname text box.
                hostnameTextbox.Focus();
            }
            else
            {
                MessageBox.Show("You are already connected to the server.");
            }
        }

        /*
         * 
         * 
         */
        private void hostSubmitButton_Click(object sender, EventArgs e)
        {
            host = hostnameTextbox.Text;
            bool port_parse_result = Int32.TryParse(portTextbox.Text, out port);
            if (host.Length == 0)
            {
                MessageBox.Show("A hostname is required, in the format lab#-#.eng.utah.edu.");
                hostnameTextbox.Invoke(new Action(() => { hostnameTextbox.Text = ""; }));
            }
            else
            {
                if (!port_parse_result)
                {
                    MessageBox.Show("Not a valid port number.");
                    portTextbox.Invoke(new Action(() => { portTextbox.Text = ""; }));
                }
                else
                {
                    // Connect to the server.
                    bool successful_connection = ConnectSocket(host, port);

                    if (successful_connection)
                    {
                        connected_to_server = true;

                        // Hide the hostname and socket elements and display the password elements.
                        hostnameTextbox.Visible = false;
                        portTextbox.Visible = false;
                        hostSubmitButton.Visible = false;
                        hostnameLabel.Visible = false;
                        portLabel.Visible = false;
                        passwordTextbox.Visible = true;
                        submitButton.Visible = true;
                        passwordLabel.Visible = true;

                        // Include the undo button in the file menu.
                        undoOnServerToolStripMenuItem.Visible = true;

                        // Sets the cursor focus on the password TextBox
                        passwordTextbox.Focus();
                    }
                    else // If the server is not found, return to the unchanged spreadsheet. 
                    {
                        // flowLayoutPanel1.Visible = false;
                        // passwordPanel.Visible = false;
                        MessageBox.Show("The server was not found.");

                    }
                }
            }
        }

        /*
         * When the "Submit" button is clicked in the password panel, send a password message to the server
         * in the form PASSWORD[esc]password\n .
         * Begin listening for a response from the server.
         */
        private void submitButton_Click(object sender, EventArgs e)
        {
            // Build a Message object for the password message.
            SS.Message password_message = new SS.Message(new List<string>() { "PASSWORD", passwordTextbox.Text });
            // Send the password message to the server.
            socket.BeginSend(password_message.GetMessage(), (a, b) => { }, null);
            // Listen for either a file list or invalid message from the server.
            socket.BeginReceive(LineReceived, null);
        }

        /*
         * Connects to the spreadsheet server given by the hostname and port parameters.
         * 
         * Returns true if the connection was successful, otherwise returns false.
         */
        private bool ConnectSocket(String hostname, int port)
        {
            try
            {
                // Try to connect to server
                TcpClient client = new TcpClient(hostname, port);
                // Create a new socket from the TCP connection.
                socket = new StringSocket(client.Client, UTF8Encoding.Default);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /*
         * A CallBack that's passed as an argument to the socket.BeginReceive method.
         * When the socket has received a message, LineReceived is called.
         * The appropriate action is taken, based on the type of incoming message.
         */
        public void LineReceived(string s, Exception e, object payload)
        {
            // If the incoming message isn't null, parse the message and take the appropriate action.
            if (!ReferenceEquals(s, null))
            {
                #region receive callback
                // Create a Message object from the incoming message. 
                SS.Message incoming_message = new SS.Message(s);
                lastMessageFromServer = incoming_message.GetMessage();

                if (incoming_message.GetMessageTypeName().Equals("INVALID"))
                {
                    MessageBox.Show("Invalid Password");
                }
                //invalidPasswordLabel.Invoke(new Action(() => { invalidPasswordLabel.Visible = true; }));
                else if (incoming_message.GetMessageTypeName().Equals("FILELIST"))
                {
                    filelistBox.Invoke(new Action(() => { filelistBox.DataSource = incoming_message.GetMessageParametersList(); }));
                    filelistPanel.Invoke(new Action(() => { filelistPanel.Visible = true; }));
                }
                else if (incoming_message.GetMessageTypeName().Equals("SAVED"))
                {
                    // Currently doesn't do anything
                    toolStripStatusLabel1.Text = "Remote spreadsheet saved.";
                }
                else if (incoming_message.GetMessageTypeName().Equals("ERROR"))
                {
                    MessageBox.Show(incoming_message.GetMessageParametersList()[0]);
                }
                else if (incoming_message.GetMessageTypeName().Equals("UPDATE"))
                {
                    // If this is the first update from the server, set the local version number to the server's version number.
                    if (first_update)
                    {
                        current_version_number = Int32.Parse(incoming_message.GetMessageParametersList()[0]);
                        first_update = false;
                    }
                    // If the local version number doesn't match the server's version number, request a RESYNC.
                    if (Int32.Parse(incoming_message.GetMessageParametersList()[0]) != current_version_number)
                    {
                        // Just for testing. Do not include in final project.
                        MessageBox.Show("Local version number: " + current_version_number + "\nIncoming version number: " +
                            (incoming_message.GetMessageParametersList()[0]));

                        SS.Message resync_message = new SS.Message(new List<String>() { "RESYNC" });
                        socket.BeginSend(resync_message.GetMessage(), (a, b) => { }, null);
                    }
                    // If the version numbers do match, loop through all of the cells to be updated and change their contents and value.
                    else
                    {
                        for (int i = 1; i < incoming_message.GetMessageParametersList().Count; i = i + 2)
                        {
                            setCellFromServer(incoming_message.GetMessageParametersList()[i], incoming_message.GetMessageParametersList()[i + 1]);
                        }
                        current_version_number++;
                    }
                }
                else if (incoming_message.GetMessageTypeName().Equals("SYNC"))
                {
                    // The current version number is set to the version number of the SYNC message + 1.
                    current_version_number = Int32.Parse(incoming_message.GetMessageParametersList()[0]) + 1;
                    // Clean the spreadsheet to prepare it for synchronizing with the latest version from the server.
                    clearSpreadsheet();
                    // Fill the spreadsheet with the cell contents received in the SYNC message.
                    for (int i = 1; i < incoming_message.GetMessageParametersList().Count; i = i + 2)
                    {
                        //Thread.Sleep(5);
                        setCellFromServer(incoming_message.GetMessageParametersList()[i], incoming_message.GetMessageParametersList()[i + 1]);
                    }
                }
                else
                {
                    throw new SS.MessageException("The message received from the server can not be understood.");
                }

                #endregion

                // Listen for the next message from the server.
                socket.BeginReceive(LineReceived, null);
            }
            // If the line received is null, the server connection has been lost. Inform the user and close the socket. 
            else
            {
                socket.Close();
                connected_to_server = false;
                skyntaxServerName.Invoke(new Action(() => { skyntaxServerName.Visible = false; }));
                MessageBox.Show("The server has been disconnected.");
            }
        }

        /*
         * This method is identical to the standard setCell method, except that it is called when UPDATE messages are
         * received from the server. 
         * 
         * Circular dependencies are checked and formulas are eveluated as normal.
         * 
         * The server has already performed its own circular dependency check. Therefore, no error 
         * messages need to be sent back to the server. It is assumed that updates causing a CD will never be 
         * sent.
         * 
         * Formula errors are displayed in the cell, as usual. The cell's contents continue to hold the formula,
         * allowing a cell with an error value to be revaluated when its dependees change.
         */
        private void setCellFromServer(String cell, String contents)
        {
            // Re-Calculate all cells that have changed as a result of setting this Cell
            try
            {
                int newCol, newRow;

                ISet<string> cellToRecalculate = sheet.SetContentsOfCell(cell, contents);

                foreach (string name in cellToRecalculate)
                {
                    delimitCellName(name, out newCol, out newRow);
                    spreadsheetPanel1.SetValue(newCol, newRow, sheet.GetCellValue(name).ToString());

                }
            }
            // If a circular dependency exists do not allow the user to continue
            catch (CircularException c)
            {
                string message = "Cells cannot equal themselves.";
                string caption = "Error! " + c.Message;
                MessageBox.Show(message, caption);
            }
            // If Formula throws an exception do not allow the user to continue
            catch (FormulaFormatException f)
            {
                string message = f.Message;
                string caption = "Error! Input Is Not A Valid Formula.";
                MessageBox.Show(message, caption);
            }
        }

        /*
         * Requests the selected file from the server.
         */
        private void openFileButton_Click(object sender, EventArgs e)
        {
            // Hide the password and file selection panel, revealing the spreadsheet.
            flowLayoutPanel1.Visible = false;
            hostnameTextbox.Visible = false;
            portTextbox.Visible = false;
            hostSubmitButton.Visible = false;
            hostnameLabel.Visible = false;
            portLabel.Visible = false;
            passwordTextbox.Visible = false;
            submitButton.Visible = false;
            passwordLabel.Visible = false;
            filelistPanel.Visible = false;
            skyntaxServerName.Visible = true;

            // The next update message will be the first. Set the current_version number to the update version number.
            first_update = true;

            SS.Message open_message = new SS.Message(new List<String>() { "OPEN", (string)filelistBox.SelectedItem });
            // Clear any contents from the spreadsheet in preparation for building the spreadsheet from the opened file.
            clearSpreadsheet();
            socket.BeginSend(open_message.GetMessage(), (a, b) => { }, null);

            // Change the filename in the tool strip and at the bottom of the window.
            toolStripStatusLabel1.Text = (string)filelistBox.SelectedItem;
            setFormText((string)filelistBox.SelectedItem);
        }

        /*
         * Creates a new, blank spreadsheet on the server and opens it.
         */
        private void createFileButton_Click(object sender, EventArgs e)
        {
            // Warn the user if a name hasn't been entered in the textbox.
            if (newFileBox.Text.Length == 0)
            {
                MessageBox.Show("Spreadsheet name required.");
            }
            else
            {
                // Hide the password and file selection panel, revealing the spreadsheet.
                flowLayoutPanel1.Visible = false;
                hostnameTextbox.Visible = false;
                portTextbox.Visible = false;
                hostSubmitButton.Visible = false;
                hostnameLabel.Visible = false;
                portLabel.Visible = false;
                passwordTextbox.Visible = false;
                submitButton.Visible = false;
                passwordLabel.Visible = false;
                filelistPanel.Visible = false;
                skyntaxServerName.Visible = true;

                // The next update message will be the first. Set the current_version number to the upadte version number.
                first_update = true;

                SS.Message create_message = new SS.Message(new List<String>() { "CREATE", (string)newFileBox.Text });
                // Clear any contents from the spreadsheet in preparation for building the spreadsheet from the opened file.
                clearSpreadsheet();
                socket.BeginSend(create_message.GetMessage(), (a, b) => { }, null);

                // Change the filename in the tool strip and at the bottom of the window.
                toolStripStatusLabel1.Text = (string)filelistBox.SelectedItem;
                setFormText((string)filelistBox.SelectedItem);
            }
        }

        /*
         * Set the contents of every cell to "". 
         * The spreadsheet will then remove every cell from its data structures.
         */
        private void clearSpreadsheet()
        {
            for (int col = 1; col <= 26; col++)
            {
                for (int row = 1; row <= 99; row++)
                {
                    setCellFromServer((char)(col + 64) + row.ToString(), "");
                }
            }
        }

        /*
         * Send an UNDO request to the server.
         * The server will push an UPDATE to all connected clients that reverts the spreadsheet 
         * to the state it was in prior to the most recent change.
         */
        private void undoOnServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Undo has no effect if the client isn't connected to a server.
            // The undo button should only be visible when connected.
            if (!ReferenceEquals(socket, null) && socket.isConnected())
            {
                SS.Message undo_message = new SS.Message(new List<String>() { "UNDO", current_version_number.ToString() });
                socket.BeginSend(undo_message.GetMessage(), (a, b) => { }, null);
            }
        }

        /*
         * Check if the client is connected to the server when the File menu item is clicked.
         * If the connection has been lost, hide the the undo button.
         * Undo is not a valid activity when the spreadsheet isn't connected to the server.
         */
        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!connected_to_server)
            {
                undoOnServerToolStripMenuItem.Visible = false;
            }
        }
    }// End of Form1 Class
}// End of namespace
