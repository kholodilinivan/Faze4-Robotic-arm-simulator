clc;
clear all;
close all;
name = "Matlab";
Client = TCPInit('127.0.0.1',55001,name);

% gripping_point = 0.056;
gripping_point = 0.1978;

L(1) = Revolute('d',0.2358,'a',0,'alpha',-pi/2);
L(2) = Revolute('d',0,'a',0.3187,'alpha',-pi);
L(3) = Revolute('d',0,'a',0.0735,'alpha',-pi/2);
L(4) = Revolute('d',-0.25,'a',0,'alpha',pi/2);
L(5) = Revolute('d',0,'a',0,'alpha',-pi/2);
L(6) = Revolute('d',-gripping_point,'a',0,'alpha',pi);
robot = SerialLink(L);
joints = [0,-pi/2,0,0,-pi/2,0];
% robot.plot(joints);

for i = 1 : 3

grab = 2; % activate EE (0 - release the object, 1 - grab, 2 - do nothing)

t = [0:0.1:2];

    X0 = 0.427; %
    Y0 = 0.241; %
    Z0 = 0.2; %
    
    T = transl(X0,-Y0, Z0) * trotx(180, "deg");
    qi1 = robot.ikine(T);
    qf1 = [0 -pi/2 0 0 -pi/2 pi/2];
    q = jtraj(qf1,qi1,t);
    % robot.plot(q);
    
    b = 1;
    for a = 1 : length(q)
        func_data(Client, q, b);
        b=b+1;     
    end
    
    %% GRABING THE OBJECT
    
    % take object

    pause(0.5);
    grab = 1;
    func_grab(Client, grab);
    pause(4.5);
    
    % Generate Cube on the conveyor
    if (i == 1)
        init = 1;
        new_object(Client, init) % init: 1 - red, 2 - green, 3 - blue
    end
    if (i == 2)
        init = 3;
        new_object(Client, init) % init: 1 - red, 2 - green, 3 - blue
    end
    
    % back to initial pos
    b = 1;
    q = jtraj(qi1,qf1,t);
    % robot.plot(q);
    for a = 1 : length(q)
        func_data(Client, q, b); 
        b=b+1;
    end
    
    %% COLOR CHECK
    
    X1 = 0.303; % - cam
    Y1 = -0.064; % - cam
    Z1 = 0.2; % - cam
    
    T = transl(X1, -Y1, Z1) * trotx(180, "deg");
    qi1 = robot.ikine(T);
    qf1 = [0 -pi/2 0 0 -pi/2 pi/2];
    q = jtraj(qf1,qi1,t);
    % robot.plot(q);
    
    b = 1;
    for a = 1 : length(q)
        func_data(Client, q, b);
        b=b+1;     
    end
    
    color = color_check(Client); % function for detecting colors
    
    if color == 1 % Red sorting
        X2 = 0.44136;
        Y2 = -0.064;
        Z2 = 0.087+0.08;
    elseif color == 2 % Green sorting
        X2 = 0.44136;
        Y2 = -0.2083;
        Z2 = 0.087+0.12;
    else % Blue sorting
        X2 = 0.3027599;
        Y2 = -0.2083;
        Z2 = 0.087+0.2;
    end
    
    pause(1);
    %% PLACING THE CUBE TO THE SORTED PLACE
    
    T = transl(X2, -Y2, Z2) * trotx(180, "deg");
    qf1 = robot.ikine(T);
    q = jtraj(qi1,qf1,t);
    % robot.plot(q);
    
    b = 1;
    for a = 1 : length(q)
        func_data(Client, q, b);
        b=b+1;     
    end
    
    % release object
    grab = 0;
    func_grab(Client, grab);
    pause(2.5);
    
    %% Back to initial pos
    b = 1;
    qi1 = [0 -pi/2 0 0 -pi/2 pi/2];
    q = jtraj(qf1,qi1,t);
    % robot.plot(q);
    for a = 1 : length(q)
        func_data(Client, q, b); 
        b=b+1;
    end
end
%Close Gracefully
fprintf(1,"Disconnected from server\n");