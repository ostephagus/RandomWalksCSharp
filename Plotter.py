from matplotlib import pyplot as plt
import ast
file = open("SimulateOutput.txt","r")
verletSimulationData = ast.literal_eval(file.readline()) #Parses in a list of lists of tuples of tuples (vectors). Tree structure: iteration, body, position-velocity, xcoord-ycoord (vector)
#Create a list of positions through unzipping multiple times
#for each highest-level list (different iterations):
#   unzip the next-highest-level list (creating 2 lists of positions and velocities)
positions = [] #positions and velocites have form: iteration, body, xcoord-ycoord
velocities = []

for iterationData in verletSimulationData: #Looping through the main list to access each iteration and extract body data
    positionsVelocities = list(zip(*iterationData))
    positions.append(positionsVelocities[0])
    velocities.append(positionsVelocities[1])

bodyCoordinates = list(zip(*positions))

splitUpCoordinates = list()
for bodyCoordinate in bodyCoordinates:
    splitUpCoordinates.append(list(zip(*bodyCoordinate)))

print(splitUpCoordinates)

colours = ["r","g","b","y"]

for bodyNumber in range(len(splitUpCoordinates)):
    plt.plot(splitUpCoordinates[bodyNumber][0], splitUpCoordinates[bodyNumber][1], color=colours[bodyNumber % len(colours)])

plt.title("Gravity simulation plots")
plt.show()

    
#Final format for data:
#Separate lists for each body
#Separate lists inside each body for each coordinate (calls will be made to plot() for each body, passing in its coordinates as 2 separate lists)
#Coordinate lists ordered by iteration (but all in the same list)
#Final list needs to be in the form: position-velocity, body, xcoord-ycoord, iteration
