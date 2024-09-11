import sys
import Risk_setup
import Risk_drawmap
import pygame
pygame.init()

global playerList
global Terr_dictionary        

def riskSetup():
    global playerList
    global Terr_dictionary
    print('Welcome to Risk in python!')
    print('--------------------------')
    print('Need 3 to 6 players to start.')
    # Get number of players
    print()
    players = Risk_setup.getHow_Many_Players()
    
    # Createa list of player objects
    playerList = Risk_setup.createPlayers(players)

    # Prints 1st player
    # print(playerList[0].name)

    # Prints list of players
    print('Players: ')
    for x in playerList:
        print(x.name)

    print()

    # Method for starting number of Armies
    startingArmies = Risk_setup.setNum_of_Armies(playerList)
    print('Each player will start with {} armies.'.format(startingArmies))

    # Method to determine which player goes first
    playerList = Risk_setup.playerOrder(playerList)

    print('Players: ')
    for x in playerList:
        print(x.name)

    # Method to create dictionary of Territory objects
    Terr_dictionary = Risk_setup.dict_Terrs()

    # Method to place Armies
    Risk_setup.claimTerrs(playerList, Terr_dictionary)

    # Method for players to place initial armies
    Risk_setup.place_Initial_Armies(playerList, Terr_dictionary)



def checkWinner():
    return Risk_setup.checkWinner(playerList)

def playerTurns():
    Risk_setup.playerTurns(playerList, Terr_dictionary)



# Testing game, randomly gives players territories
# and assigns 10 armies to each
if __name__ == '__main__':
    riskSetup()
    Risk_drawmap.Draw()
    while True:
        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                pygame.quit()
                sys.exit()
                
        pygame.display.update()
        
        test = checkWinner()
        if test == True:
            print('true')
            break
        else:
            print('false')

        playerTurns()

    print('End of Risk_engine.py')
