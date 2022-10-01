function func_data(ClientHandle, Q, Grab, b)
writeTCP(ClientHandle,sprintf("ModRobot:%f,%f,%f,%f,%f,%f,%d",Q(b,1)*180/pi,(Q(b,2)+pi/2)*180/pi,Q(b,3)*180/pi,Q(b,4)*180/pi,Q(b,5)*180/pi,Q(b,6)*180/pi,Grab));
pause(0.054);


