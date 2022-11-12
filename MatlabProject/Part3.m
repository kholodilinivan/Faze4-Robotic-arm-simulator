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

X1 = 0.414; %
Y1 = 0.196; %
% Z1 = 0.1974; % - GREEN
% Z1 = 0.118; % - RED
% Z1 = 0.039; % - BLUE

T = transl(X1,-Y1, Z1) * trotx(180, "deg");
qi1 = robot.ikine(T);
qf1 = [0 -pi/2 0 0 -pi/2 pi/2];
q = jtraj(qf1,qi1,t);
% robot.plot(q);

b = 1;
for a = 1 : length(q)
    func_data(Client, q, b);
    b=b+1;     
end

%%
color = color_check(Client); % function for detecting colors

if color == 1 % Red sorting
    X2 = 0.44136;
    Y2 = -0.064;
    Z2 = 0+0.04;
elseif color == 2 % Green sorting
    X2 = 0.44136;
    Y2 = -0.2083;
    Z2 = 0+0.08;
else % Blue sorting
    X2 = 0.3027599;
    Y2 = -0.2083;
    Z2 = 0+0.2;
end

% first loop

%% GRABING THE OBJECT

% take object
grab = 1;
func_grab(Client, grab);
pause(4.5);

% back to initial pos
b = 1;
q = jtraj(qi1,qf1,t);
% robot.plot(q);
for a = 1 : length(q)
    func_data(Client, q, b); 
    b=b+1;
end

%% PLACING THE CUBE TO THE SORTED PLACE

T = transl(X2, -Y2, Z2) * trotx(180, "deg");
qi1 = robot.ikine(T);
qf1 = [0 -pi/2 0 0 -pi/2 pi/2];
q = jtraj(qf1,qi1,t);
% robot.plot(q);

b = 1;
for a = 1 : length(q)
    func_data(Client, q, b);
    b=b+1;     
end

% release object
grab = 0;
func_grab(Client, grab);
pause(0.5);

% back to initial pos
b = 1;
q = jtraj(qi1,qf1,t);
% robot.plot(q);
for a = 1 : length(q)
    func_data(Client, q, b); 
    b=b+1;
end

%Close Gracefully
fprintf(1,"Disconnected from server\n");