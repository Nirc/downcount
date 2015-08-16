A C\# implementation.

The algorithm uses recursion and brute force to iterate through all possible arithmitic combinations of the numbers
looking for all solutions to the problem e.g.
The set of solutions to the game, y from (x1, x2, x3...xn), is equivalent to the set of solutions to
set of soltions[(y - x1) from (x2, x3, ..xn)] ADD x1  UNION
set of soltions[(y + x1) from (x2, x3, ..xn)] MINUS x1 UNION
set of soltions[(y / x1) from (x2, x3, ..xn)] MULTIPLY x1 (provided y/x1 is an integer) UNION
set of soltions[(y * x1) from (x2, x3, ..xn)] DIVIDE x1 UNION
etc etc  recursivley
  

It includes a simple command line application that allows you to play the game

![Screenshot](https://raw.githubusercontent.com/shiningdragon/Downcount/master/CSharp/Downcount%20console%20tester.jpg "Screenshot")



