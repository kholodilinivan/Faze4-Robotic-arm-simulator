function writeTCP(TCP_Handle,message)
message = uint8(char(message));
message_size = size(message,2);
write(TCP_Handle,[message_size,message]);
end