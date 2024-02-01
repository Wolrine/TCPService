using ClientApp;

string server = "192.168.1.17";
//Set the TcpListener on port 13000
int port = 13000;

ClientCon conServer = new();
conServer.ConnectServer(server, port);