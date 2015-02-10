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

namespace SS
{
    /*
     * Message is a custom value type for Client-Server Spreadsheet messages. 
     * A Message represents a protocol message being sent or recieved by the client.
     * 
     * Messages contain fields for message type - represented as an integer to facilitate 
     * a switch statement - as well as a String for the complete message and a list of 
     * Strings for message parameters.
     * 
     * Message also supplies methods for parsing an incoming message and building 
     * an outgoing message.
     * 
     * Message type codes:
     * 
     * SENT FROM CLIENT TO SERVER
     *   1- PASSWORD[esc]password\n
     *   2- OPEN[esc]spreadsheet_name\n
     *   3- CREATE[esc]spreadsheet_name\n
     *   4- ENTER[esc]version_number[esc]cell_name[esc]cell_content[esc]\n
     *   5- RESYNC\n
     *   6- UNDO[esc]version_number\n
     *   7- SAVE[esc]version_number\n
     *   8- DISCONNECT\n
     *  
     * SENT FROM SERVER TO CLIENT
     *   9- FILELIST[esc]list_of_existing_filenames\n (delimited by [esc])
     *  10- INVALID\n
     *  11- UPDATE[esc]current_version[esc]cell_name1[esc]cell_content1...\n
     *          or UPDATE[esc]current_version\n
     *          or UPDATE[esc]current_version[esc]cell_name[esc]cell_content\n
     *  12- SAVED\n
     *  13- SYNC[esc]current_version[esc]cell_name[esc]cell_content...\n
     *  14- ERROR[esc]error_message\n
     */
    public class Message
    {
        // message_type is an integer code for the type of protocol message this represents
        // The code is included in the Message class header comment.
        private int message_type;

        // message is the complete protocol message, in the state it was received from the socket, 
        // or in a state that it could be passed to a socket.
        private String message;

        // message_params is a List of all of the parsed params that belong to this particular type of message.
        // The order and type of parameters in the List are described in the Message class header comment.
        private List<String> message_params;

        // Message type code for message types that don't match the protocol.
        private const int UNDEFINED = -1;

        /*
         * Two parameter constructor.
         * For outgoing messages, to be sent to the server.
         * The message field is built from this constructor's parameters.
         */
        public Message(List<String> parameters)
        {
            this.message_type = UNDEFINED;
            BuildMessage(parameters);
            if (message_type == UNDEFINED || message == null || message_params == null)
                throw new MessageException("The message was not built correctly.");
        }

        /*
         * Single parameter constructor
         * For incoming messages, received from the server, that need to be parsed.
         */
        public Message(String mess)
        {
            this.message = mess;
            this.message_type = UNDEFINED;
            this.message_params = new List<string>();
            ParseMessage(message);
            if (message_type == UNDEFINED)
                throw new MessageException("The message type does not match the protocol.");
            else if (message_params == null)
                throw new MessageException("The message's parameters were not correctly parsed.");
        }

        /*
         * Builds outgoing messages.
         * 
         * The message type is expected to be the first element in the parameter List. 
         * Message type is stored in the global int message_type according to the integer code
         * described in the Message class header.
         * 
         * If the outgoing message requires parameters, they are expected to have been passed
         * into the constructor. The parameters are assumed to be in the appropriate 
         * order within the List.
         * 
         * Message parameters are also stored in the message_params List, exactly as they were passed in.
         */
        private void BuildMessage(List<String> parameters)
        {
            this.message_params = parameters;
            String mess = parameters[0];

            if (mess == "PASSWORD") // PASSWORD[esc]password\n
            {
                message_type = 1;
                if (parameters.Count == 2)
                    this.message = mess + (char)0x001B + parameters[1] + "\n";
                else
                    throw new MessageException("PASSWORD expects 1 parameter.");
            }
            else if (mess == "OPEN") // OPEN[esc]spreadsheet_name\n
            {
                message_type = 2;
                if (parameters.Count == 2)
                    this.message = mess + (char)0x001B + parameters[1] + "\n";
                else
                    throw new MessageException("OPEN expects 1 parameter.");
            }
            else if (mess == "CREATE") // CREATE[esc]spreadsheet_name\n
            {
                message_type = 3;
                if (parameters.Count == 2) 
                    this.message = mess + (char)0x001B + parameters[1] + "\n";
                else
                    throw new MessageException("CREATE expects 1 parameter.");
            }
            else if (mess == "ENTER") // ENTER[esc]version_number[esc]cell_name[esc]cell_content[esc]\n
            {
                message_type = 4;
                if (parameters.Count == 4) 
                    this.message = mess + (char)0x001B + parameters[1] +
                        (char)0x001B + parameters[2] + (char)0x001B + parameters[3] + "\n";
                else
                    throw new MessageException("ENTER expects 3 parameters.");
            }
            else if (mess == "RESYNC") // RESYNC\n
            {
                message_type = 5;
                if (parameters.Count == 1)
                    this.message = mess + "\n";
                else
                    throw new MessageException("RESYNC expects 0 parameters.");
            }
            else if (mess == "UNDO") // UNDO[esc]version_number\n
            {
                message_type = 6;
                if (parameters.Count == 2)
                    this.message = mess + (char)0x001B + parameters[1] + "\n";
                else
                    throw new MessageException("UNDO expects 1 parameter.");
            }
            else if (mess == "SAVE") // SAVE[esc]version_number\n
            {
                message_type = 7;
                if (parameters.Count == 2)
                    this.message = mess + (char)0x001B + parameters[1] + "\n";
                else
                    throw new MessageException("SAVE expects 1 parameter.");
            }
            else if (mess == "DISCONNECT") // DISCONNECT\n
            {
                message_type = 8;
                if (parameters.Count == 1)
                    this.message = mess + "\n";
                else
                    throw new MessageException("DISCONNECT expects 1 parameter.");
            }
            else
            {
                throw new MessageException("The message type does not the protocol.");
            }
        }

        /*
         * Parses incoming messages.
         * 
         * The message type is expected to be the first string in the parameter message m. 
         * Message type is stored in the global int message_type according to the integer code
         * described in the Message class header.
         * 
         * If the message type is followed by additional parameters, the <esc> character (char)27 
         * will be expected as the delimitor at the end of the message type and between all seperate 
         * parameters. 
         * 
         * Message parameters will be stored individually in the global List message_params.
         */
        private void ParseMessage(string m)
        {
            char[] c = new char[] { (char)0x001B }; // (char)0x001B is the Unicode [esc] char
            String[] type_param_array = m.Split(c);
            String type = type_param_array[0];

            if (type == "FILELIST") // FILELIST[esc]list_of_existing_filenames\n (delimited by [esc])
            {
                message_type = 9;
                // if (type_param_array.Length > 1) // The type is still included in the array at index 0.
                    for (int i = 1; i < type_param_array.Length; i++)
                        message_params.Add(type_param_array[i]);
                //else
                //    throw new MessageException("FILELIST expects at least 1 parameter.");
            }
            else if (type == "INVALID") // INVALID\n
            {
                message_type = 10;
                if (type_param_array.Length > 1)
                    throw new MessageException("INVALID expects 0 parameters.");
            }
            else if (type == "UPDATE") // UPDATE[esc]current_version[esc]cell_name1[esc]cell_content1...\n
                                        // or UPDATE[esc]current_version\n
                                        // or UPDATE[esc]current_version[esc]cell_name[esc]cell_content\n   
            {
                message_type = 11;
                if (type_param_array.Length > 1) // UPDATE messages should have at least 1 parameter
                {
                    for (int i = 1; i < type_param_array.Length; i++)
                        message_params.Add(type_param_array[i]);
                }
                else
                {
                    throw new MessageException("UPDATE expects at least 1 parameter.");
                }
            }
            else if (type == "SAVED") // SAVED\n
            {
                message_type = 12;
                if (type_param_array.Length > 1)
                    throw new MessageException("SAVED expects 0 parameters.");
            }
            else if (type == "SYNC") // SYNC[esc]current_version[esc]cell_name[esc]cell_content...\n
            {
                message_type = 13;
                if (type_param_array.Length > 1) // SYNC messages should have at least 1 parameter
                {
                    for (int i = 1; i < type_param_array.Length; i++)
                        message_params.Add(type_param_array[i]);
                }
                else
                {
                    throw new MessageException("SYNC expects at least 1 parameter.");
                }
            }
            else if (type == "ERROR") // ERROR[esc]error_message\n
            {
                message_type = 14;
                if (type_param_array.Length == 2)
                    for (int i = 1; i < type_param_array.Length; i++)
                        message_params.Add(type_param_array[i]);
                else
                    throw new MessageException("ERROR expects 1 parameter.");
            }
            else
            {
                throw new MessageException("The message type does not match the protocol.");
            }
        }

        /*
         * Returns the entire message, including all delimiters and parameters
         * as one string.
         * For incoming messages, the complete message string should be unaltered
         * from the string that was received from the server.
         * For outgoing messages, the message should always be in a form that
         * can be sent to the server.
         * The full message string must always follow the protocol.
         */
        public String GetMessage()
        {
            return this.message;
        }

        /*
         * Returns the Message's integer type code.
         */
        public int GetMessageTypeCode()
        {
            return this.message_type;
        }

        /*
         * Returns the Message's type as a String that matches the protocol exactly.
         * The type name will always be capitalized.
         */
        public String GetMessageTypeName()
        {
            switch (this.message_type)
            {
                case 1:
                    return "PASSWORD";
                case 2:
                    return "OPEN";
                case 3:
                    return "CREATE";
                case 4:
                    return "ENTER";
                case 5:
                    return "RESYNC";
                case 6:
                    return "UNDO";
                case 7:
                    return "SAVE";
                case 8:
                    return "DISCONNECT";
                case 9:
                    return "FILELIST";
                case 10:
                    return "INVALID";
                case 11:
                    return "UPDATE";
                case 12:
                    return "SAVED";
                case 13:
                    return "SYNC";
                case 14:
                    return "ERROR";
                default:
                    return "Does not contain a valid message type.";
            }
        }

        /*
         * Returns an IEnumerable of the Message's parameters.
         * Each parameter is an individual string.
         */
        public IEnumerable<String> GetMessageParametersEnum()
        {
            foreach (String param in message_params)
                yield return param;
        }

        /*
         * Returns a List of the Message's parameters.
         * Each parameter is an individual string.
         */
        public List<String> GetMessageParametersList()
        {
            return message_params;
        }
    } // End of Message Class

    public class MessageException : Exception
    {
        public MessageException()
        {
            Console.WriteLine("Message has thrown an exception without an explanation.");
        }

        public MessageException(string message)
            : base(message)
        {
            Console.WriteLine(message);
        }
    } // End of MessageException class
} // End of namespace