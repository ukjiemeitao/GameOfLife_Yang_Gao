Objective
=========

This exercise tests whether a candidate can read and understand improve an existing code sample that is badly written and doesn't produce the correct output.

The aim is to refactor and/or rewrite the sample such that:

1. It produces the correct output for both the provided input, and for any other valid input. 
2. It is understandable to other people who may work on it in future and uses typical programming/formatting conventions.
3. It is engineered as you would expect production code to be.

The sample is available in both C# and F#, however a solution is F# is preferred if possible.

If you are limited in time, we prefer a solution that shows a good approach to one that merely gives the correct answer without improving the approach.
Given the time constraint you can assume that the input will be well formed.

What is the Game of Life
========================

Conway's Game of Life (https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life) is a simple 2-dimensional cellular automaton.

How does it work ?

1. We populate a 2-dimensional 'world' (a rectangular grid of 'cells') with a starting configuration of our choice. Each cell is given the either state 'alive' or 'dead'.
2. We evolve the world through one time-step using a rule based on the nearest-neighbours:

    a. each cell has at most 8 neighbours: above, below, left, right, above-left, above-right, below-left, below-right
    b. count the number of 'alive' neighbours.
    c. the new state of a cell at time T+1 is given by its state at time T and the number of alive neighbours at time T by using these rules:
        i. Any live cell with fewer than two live neighbours dies, as if by underpopulation.
        ii. Any live cell with two or three live neighbours lives on to the next generation.
        iii. Any live cell with more than three live neighbours dies, as if by overpopulation.
        iv. Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.

3. repeat stage 2. as many times as desired or until the state does not change.

What happens at the boundaries of the world ?

We would like you to apply boundaries such that the world is like the surface of a torus (or ring doughnut) - 
i.e. the left and right-hand edges of the world are connected and the top and bottom edges of the world are connected. 
All cells will therefore have exactly 8 neighbours.

Bonus marks
============

Bonus marks are available for considering how to represent this solution when the world becomes extremely large but very sparsely populated 
and you want to represent the state in as little memory as possible and execute the algorithm no worse than O(# alive cells).

