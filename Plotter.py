import matplotlib.colors as matcolours
from matplotlib import pyplot as plt
import struct
#Code to plot the random walks using matplotlib
#The list of lists of position tuples is parsed, the tuples are unzipped (since matplotlib requires two separate lists for plotting) and then the plot is created.

def getDoubleList(filename): 
    """
    Function to parse in a binary file in the format:
    first dimension length (4 bytes - int)
    second dimension length (4 bytes - int)
    contents of doube array (8 bytes repeated - doubles)
    """
    INTLENGTH = 4 #Length of an integer (bytes)
    DOUBLELENGTH = 8 #Length of a double (bytes)
    f = open(filename, "rb")
    dim1Length = struct.unpack("i", f.read(INTLENGTH))[0]
    dim2Length = struct.unpack("i", f.read(INTLENGTH))[0]
    doubleList = list()
    for i in range(dim1Length):
        doubleList.append(list())
        for j in range(dim2Length):
            doubleList[i].append(struct.unpack("dd", f.read(DOUBLELENGTH*2))) #Read double tuple and put into list
    f.close()
    return doubleList

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

randomWalkData = getDoubleList("C:/Users/Owner/OneDrive/Documents/Coding/C#/RandomWalks/RandomWalks/bin/Debug/net6.0/intermediateresults.bin")

splitUpCoordinates = SplitCoordinates(randomWalkData)

colours = GetColourList(list(matcolours.CSS4_COLORS.keys()), len(splitUpCoordinates))

for bodyNumber in range(len(splitUpCoordinates)):
    plt.plot(splitUpCoordinates[bodyNumber][0], splitUpCoordinates[bodyNumber][1], color=colours[bodyNumber % len(colours)])

plt.title("Random Walks plot")
plt.show()