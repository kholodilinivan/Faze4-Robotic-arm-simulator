function new_object(ClientHandle, init)

writeTCP(ClientHandle,sprintf(sprintf("NewObj:%d",init)));
% pause(0.005);