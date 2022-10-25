function color = color_check(Client)

test = ImageReadTCP_One(Client,'Center');
imshow(test);
R = test(:,:,1);
G = test(:,:,2);
B = test(:,:,3);
if R > G & R > B
    color = 1; % Red Color
elseif G > R & G > B
    color = 2; % Green Color
else
    color = 3; % Blue Color
end