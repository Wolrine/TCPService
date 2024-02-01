using ServerApp;

string host = "192.168.1.239";

// The TcpListener port
int port = 13000;

ServerCon server = new();
server.ExecuteServer(host, port);