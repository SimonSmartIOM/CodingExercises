This solution contains three different approaches to solving the [Rocket Stages](https://open.kattis.com/problems/rocketstages) problem.

# RocketStages-ChatGPT
This is ChatGPT's failed attempt at solving the problem. It's just here to illustrate that ChatGPT isn't capable of 
solving complex problems like this one.

ChatGPT was not involved in the development of the other two solutions.

# RocketStages-ObjectOriented
This is an object-oriented solution to the problem, using clearly-defined Rocket and Stage classes.

While this is a good solution and will return the correct answer, it is lacking in efficiency as it tries all possible permutations of the stages.

# RocketStages-DynamicProgramming
This project contains a Dynamic Programming-based solution to the problem, which avoids the exponential increase in complexity
that the object-oriented solution suffers from when the number of stages increases.

This solution can return the correct answer much more quickly for larger inputs, but doesn't model the object as closely to 
the real world as the object-oriented solution.

This is probably the best solution to the problem, but the object-oriented solution has its merits as an example of structuring
objects to mirror real-world objects.