# RandomWalks
A C# project for computing random walks for multiple bodies. The computation is done in C# and then the results are passed over to a plotter written in Python.
## What is a random walk?
A random *step* is defined as a movement of distance one unit in a random direction. In the code, the angle of step is chosen randomly. Therefore, a random *walk* is the aggregation of these steps into a random-seeming path. This is what is output and plotted.
## Overview of the code
The C# program includes a Particle class which contains methods for walking randomly and getting the position. The rest of the python code manages the iteration of this process for a number of parallel Particles to create the plots.
The C# program then outputs this data to a file (which takes a long time for more particles and iterations), for the python program to read it in.
The python program then reads in the data, unzips the coordinates (since MatPlotLib requires the coordinates split) and passes them to a Plot function to plot them. A function also rotates the colour each time so the Particles' trails are coloured differently
## Future plans
* Possibly a CLI for the entire project - to make the computation (C#) and plotting (Python/MatPlotLib) happen sequentially without the user running the 2 currently separate programs manually. The CLI will also allow variables to be passed (number of Particles and number of iterations) and will plot this seamlessly.
* Improving the passing of data between the simulator and plotter - currently this done through a text file which is inefficient.
