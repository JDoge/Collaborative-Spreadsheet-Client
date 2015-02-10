# Collaborative-Spreadsheet-Client
Client-Server Spreadsheet
=======================================================================================================================

University of Utah
CS3505 - Software Practice II
Spring 2014

Group:    Skyntax

Members:  Zach Lobato, Jared Potter, and Taylor Wilson

=======================================================================================================================

Please note that this is only the client part of this project. If requested by a legitimate source, we can provide the rest. 

Pictures of this client can be found at:
http://imgur.com/a/5iOpR

Thank you for looking at our README file. This solution uses C++98 libraries as well as Boost and MySQL Connector 
  libraries. To run the spreadsheet server, first compile the executable with the provided Makefile. The Makefile
  is programmed to create an executable named 'run'. The executable 'run' requires a single argument to main which
  identifies the port the server should used. Example, './run 2500' will run the server on port 2500. If a port
  is not specified the server will exit rudely. Once the server is running, it can be shutdown by typing 'EXIT'.
  The administrator will then be asked to confirm the shutdown. To confirm, type 'Y'. These commands are case-
  sensitive. The solution for our spreadsheet server requires a MySQL server. Without the MySQL server
  the spreadsheet server will not be able to save the contents of cells. Due to the how the MySQL is setup, it is
  expected that the server source code and executable reside in the following CADE machine path: 
  
  ~/cs3505_spreadsheet/server_main/ 

=======================================================================================================================
