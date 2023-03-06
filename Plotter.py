from matplotlib import pyplot as plt
import ast
#Code to plot the random walks using matplotlib
#The list of lists of position tuples is parsed, the tuples are unzipped (since matplotlib requires two separate lists for plotting) and then the plot is created.

file = open("./RandomWalks/bin/Debug/net6.0/results.txt","r")
randomWalkData = ast.literal_eval(file.readline()) 

splitUpCoordinates = list()

for bodyPositions in randomWalkData:
    splitUpCoordinates.append(list(zip(*bodyPositions))) #unzip 

colours = ["red", "orange", "yellow", "green", "cyan", "blue", "indigo", "violet"]

for bodyNumber in range(len(splitUpCoordinates)):
    plt.plot(splitUpCoordinates[bodyNumber][0], splitUpCoordinates[bodyNumber][1], color=colours[bodyNumber % len(colours)])

plt.title("Gravity simulation plots")
plt.show()