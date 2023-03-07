import matplotlib.colors as matcolours
from matplotlib import pyplot as plt
import ast
#Code to plot the random walks using matplotlib
#The list of lists of position tuples is parsed, the tuples are unzipped (since matplotlib requires two separate lists for plotting) and then the plot is created.
def SplitCoordinates(data):
    splitUpCoordinates = list()
    for bodyPositions in data:
        splitUpCoordinates.append(list(zip(*bodyPositions))) #unzip 
    return splitUpCoordinates

def GetColourList(originalColourList, numOfColours):
    originalListLength = len(originalColourList)
    if numOfColours > originalListLength:
        return originalColourList
    
    colourSpacing = originalListLength // numOfColours
    return originalColourList[0:numOfColours*colourSpacing:colourSpacing]

file = open("./RandomWalks/bin/Debug/net6.0/results.txt","r")
randomWalkData = ast.literal_eval(file.readline()) 

splitUpCoordinates = SplitCoordinates(randomWalkData)

colours = GetColourList(list(matcolours.CSS4_COLORS.keys()), len(splitUpCoordinates))

for bodyNumber in range(len(splitUpCoordinates)):
    plt.plot(splitUpCoordinates[bodyNumber][0], splitUpCoordinates[bodyNumber][1], color=colours[bodyNumber % len(colours)])

plt.title("Random Walks plot")
plt.show()