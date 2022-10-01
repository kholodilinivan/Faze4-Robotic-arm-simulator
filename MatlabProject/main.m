%% Pick up object (1st level)
clc;
clear all;
close all;
name = "Matlab";
Client = TCPInit('127.0.0.1',55001,name);

% gripping_point = 0.056;
gripping_point = 0.1978;

L(1) = Revolute('d',0.2358,'a',0,'alpha',-pi/2);
L(2) = Revolute('d',0,'a',0.32,'alpha',-pi);
L(3) = Revolute('d',0,'a',0.0735,'alpha',-pi/2);
L(4) = Revolute('d',-0.25,'a',0,'alpha',pi/2);
L(5) = Revolute('d',0,'a',0,'alpha',-pi/2);
L(6) = Revolute('d',-gripping_point,'a',0,'alpha',pi);
robot = SerialLink(L);
joints = [0,-pi/2,0,0,-pi/2,0];
% robot.plot(joints);

grab = 2; % activate EE (0 - release the object, 1 - grab, 2 - do nothing)
t = [0:0.1:2];

X1 = 0.414; % - X
Y1 = -0.196; % - Z 
Z1 = 0.079; % - Y

T = transl(X1, Y1, Z1) * trotx(180, "deg");
qi1 = robot.ikine(T);
qf1 = [0 -pi/2 0 0 -pi/2 pi/2];
q = jtraj(qf1,qi1,t);
% robot.plot(q);

b = 1;
for a = 1 : length(q)
    func_data(Client, q, grab, b);
    b=b+1;     
end

% take object
grab = 1;
func_data(Client, q, grab, b-1);
pause(4.5);

% back to initial pos
grab = 2;
b = 1;
q = jtraj(qi1,qf1,t);
% robot.plot(q);
for a = 1 : length(q)
    func_data(Client, q, grab, b); 
    b=b+1;
end

% release object
grab = 0;
func_data(Client, q, grab, b-1);

%Close Gracefully
fprintf(1,"Disconnected from server\n");