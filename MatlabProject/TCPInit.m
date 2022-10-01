function TCP_Handle = TCPInit(IP_Address, Port, Name)

%% TCPInit is a helper function to set up a TCP connection
%
% IP_Address is the server address you want to connect to
% Port is the server port you want to connect to
% Name is the Name you want to give your client
%

%%Main code
TCP_Handle = tcpclient(IP_Address,Port);
fprintf(1,"Connected to server\n");

%Setting Name
SetName = sprintf("Name:%s",Name)
writeTCP(TCP_Handle,SetName);

