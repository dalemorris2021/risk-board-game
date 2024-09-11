import random, terr_objects

# Gets number of players, checks for proper input
def getHow_Many_Players():
    while True:
        # Check that input is a number
        try:
            players = int(input('Enter number of players: '))
        except ValueError:
            print('Input was not a number!')
            print('Try again!')
            continue
        else:
            print('{} player game.'.format(players))
            if players < 2:
                print('Not enough players!')
                print('Try again!')
                continue
            elif players > 6:
                print('Too many players!')
                print('Try again!')
            else:
                return players

        

# Player object class
class Player(object):
    def __init__(self, name, color):
        self.name = name
        self.cards = [] # will add later
        self.Armies = 0
        self.terrsOwned = 0 # Number of territories owned
        self.color = None


    def terrs_Conquered(self):
        return [x for x in territories.values() if x.player == self]


    def place_army(self, terr, number = 1):
        if terr.Player == None:
            terr.Player = self
        if self.Armies >= number:
            terr.Armies += number
            self.Armies -= number

    def place_armyFortify(self, fromTerr, toTerr):
        if fromTerr.Armies == 1:
            print('You only have one army at {}'.format(fromTerr))
            return
        print('There are {} armies availible to move.'.format(fromTerr.Armies - 1))
        try:
            num = int(input('How many armies would you like to place: '))
        except ValueError:
            print('Input was not a number!')
            print('Try again!')
            place_armyFortify(self, fromTerr, toTerr)
        else:
            toTerr.Armies += num
            fromTerr.Armies -= num
            


# Creates player objects and stores in list
def createPlayers(num_of_players):
    RED = (255, 0 , 0)
    BLUE = (0, 0, 255)
    GREEN = (0, 128 , 0)
    BLACK = (0, 0, 0)
    YELLOW = (255, 255, 0)
    SILVER = (192, 192, 192)
    colors = [RED, BLUE, GREEN, BLACK, YELLOW, SILVER]
    players = []
    for p in range(num_of_players):
        players.append(Player(input('Enter player {0} name: '.format(p+1)), colors[p]))
    if len(players) == 2: players.append(Player('Neutral'))
    return players


# Set starting number of armies for each player
def setNum_of_Armies(playerList):
    x, y, z, t = 35, 30, 25, 20
    if len(playerList) == 2:
        print('Add 2 player later!')
    elif len(playerList) == 3:
        playerList[0].Armies = x
        playerList[1].Armies = x
        playerList[2].Armies = x
        return x
    elif len(playerList) == 4:
        playerList[0].Armies = y
        playerList[1].Armies = y
        playerList[2].Armies = y
        playerList[3].Armies = y
        return y
    elif len(playerList) == 5:
        playerList[0].Armies = z
        playerList[1].Armies = z
        playerList[2].Armies = z
        playerList[3].Armies = z
        playerList[4].Armies = z
        return z
    elif len(playerList) == 6:
        playerList[0].Armies = t
        playerList[1].Armies = t
        playerList[2].Armies = t
        playerList[3].Armies = t
        playerList[4].Armies = t
        playerList[5].Armies = t
        return t


# Roll die list of players
def rollDie():
    r = random.randint(1, 6)
    return r

# Sets player order by die roll returns player order in list
def playerOrder(playerList):
    rolls = []
    for i in range(len(playerList)):
        input('Player {} press enter to roll die: '.format(i+1))
        r = rollDie()
        print(r)
        rolls.append(r)
        
    sortedPlayers = [0] * len(playerList)
    for i in range(len(playerList)):
        sortedPlayers[i] = playerList[rolls.index(max(rolls))]
        playerList.remove(playerList[rolls.index(max(rolls))])
        rolls.remove(max(rolls))

    playerList = sortedPlayers
    return playerList

# Creates a dictionary of territory objects, keys are strings of territory names
def dict_Terrs():
    terr_Dict = terr_objects.makeTerrs()
    return terr_Dict



def claimTerrs(players, terrs):
    # Players place armies
    i = 0
    currentPlayer = 0
    while i < 42:
        # Check if dictionary of territories is empty
#        if bool(terrs) == False:
#            break
        
        print('Player {} claim a territory: '.format(players[currentPlayer].name))
        userinput = input()
        # Capitalizes 1st letter of each word
        selection = userinput.title()

        # Check if key is in dictionary
        if selection not in terrs:
            print('Invalid Try again!')

        elif selection in terrs:
            # place_army adds territory owned to player object
            # also updates territory object, for which player claimed
            players[currentPlayer].place_army(terrs[selection])
            players[currentPlayer].terrsOwned += 1
            i += 1
            # Change current player
            currentPlayer = (currentPlayer + 1) % len(players)
            


def place_Initial_Armies(players, terrs):
    currentPlayer = 0
#    i = 0
    while (players[currentPlayer].Armies) > 0:
        print('Player {} place an army on a owned territory: '.format(players[currentPlayer].name))
        userinput = input()
        # Capitalizes 1st letter of each word
        selection = userinput.title()

        if selection not in terrs:
            print('Invalid Try again!')
            
        # check if player owns territory
        elif players[currentPlayer] != terrs[selection].Player:
            print('You do not own this territory!')
            
        else:
            # place_army adds territory owned to player object
            # also updates territory object, for which player claimed
            players[currentPlayer].place_army(terrs[selection])
            players[currentPlayer].terrsOwned += 1
            currentPlayer = (currentPlayer + 1) % len(players)
            

# Checks for winner which ends game            
def checkWinner(players):
   # playerList[2].terrsOwned = 42
    for i in range(len(players)):
        if players[i].terrsOwned == 42:
            return True

    return False            
            
# Call methods for deploy, attack and fortify        
def playerTurns(players, terrs):
    print('Player Turns!')
    i = 0
    currentPlayer = 0
    while i < len(players):
        # Deploy armies to territories
        if players[i].Armies > 0:
            print('Deploy armies!')
            deployArmies(players[currentPlayer], terrs)

        userinput = input('Player {} will you attack (Y/N): '.format(players[currentPlayer].name))
        answer = userinput.title()
        if answer == 'Y':
            Attack(players[currentPlayer], terrs)

        elif answer == 'N':
            userinput = input('Player {} will you fortify (Y/N): '.format(players[currentPlayer].name))
            answer = userinput.title()
            if answer == 'Y':
                print('Fortify!')
                # Fortify a territory
                Fortify(players[currentPlayer], terrs)
                currentPlayer = (currentPlayer + 1) % len(players)
                i += 1
            elif answer == 'N':
                currentPlayer = (currentPlayer + 1) % len(players)
                i += 1
            else:
                print('Invalid!')

        else:
            print('Invalid!')


# Player turn deplao availible armies
def deployArmies(player, terrs):
    while (player.Armies) != 0:
        print('Player {} select territory to place army'.format(player.name))
        userinput = input()
        selected_Terr = userinput.title()
        if selected_Terr not in terrs:
            print('Invalid Try again!')
        
        elif player != terrs[selected_Terr].Player:
            print('Not your Territory!')

        else:
            print('You have {} armies to deploy!'.format(player.Armies))
            # Check that input is a number
            try:
                num = int(input('How many armies would you like to place: '))
            except ValueError:
                print('Input was not a number!')
                print('Try again!')
            else:
                player.place_army(terrs[selected_Terr], num)
                print('{} armies have been moved to {}!'.format(num, terrs[selected_Terr].name))


# Player turn fortify a territory
def Fortify(player, terrs):
    userinput = input('Player {} select territory to move armies from: '.format(player.name))
    fromTerr = userinput.title()
    userinput = input('Select territory to place armies: ')
    toTerr = userinput.title()
    #Check if key is in dictionary
    if fromTerr not in terrs or toTerr not in terrs:
            print('Invalid Try again!')
            Fortify(player, terrs)
    elif (player == terrs[fromTerr].Player and player == terrs[toTerr].Player):
        player.place_armyFortify(terrs[fromTerr], terrs[toTerr])
    else:
        print('You must select territories you own!')
        Fortify(player, terrs)
    

# Get information needed for attack
def Attack(player, terrs):
    userinput = input('Player {} what territory will you attack: '.format(player.name))
    defendTerr = userinput.title()
    if defendTerr not in terrs:
        print('Not in dictionary!')
        return
    elif player == terrs[defendTerr].Player:
        print('You own this territory!')
        return
    userinput = input('Player {} what territory will you attack from: '.format(player.name))
    attackTerr = userinput.title()
    if attackTerr not in terrs:
        return
    if terrs[attackTerr].Armies <= 1:
        print('Not enough armies to attack!')
        return
    neighbor = terrs[defendTerr].isNeighbor(terrs[attackTerr])
    terrs[attackTerr].print_Neigh
    if neighbor == False:
        print('These territories are not neighbors!')
        return
    else:
        startAttack(terrs[attackTerr], terrs[defendTerr], player, terrs[defendTerr].Player)


# Actual attack, determines outcome
def startAttack(attackTerr, defendTerr, attackPlayer, defendPlayer):
    while attackTerr.Armies >= 2:
        # Set number of dice attacker rolls
        if attackTerr.Armies >= 4:
            dice1 = [random.randint(1, 6) for i in range(3)]
        elif attackTerr.Armies < 4:
            dice1 = [random.randint(1, 6) for i in range(2)]
        dice1.sort()
        dice1.reverse()
        # Set number of dice defender rolls
        if defendTerr.Armies >= 2:
            dice2 = [random.randint(1, 6) for i in range(2)]
            dice2.sort()
            dice2.reverse()
        elif defendTerr.Armies < 2:
            dice2 = [random.randint(1, 6)]
        # Compare top two dice from attacker and defender
        # dice1 is attack dice, dice2 is defend dice
        if len(dice2) == 2:
            if dice1[0] > dice2[0] and dice1[1] > dice2[1]:
                defendTerr.Armies -= 2
                print('Defending territory lost 2 army!')
            elif dice1[0] < dice2[0] and dice1[1] < dice2[1]:
                attackTerr.Armies -= 2
                print('Attacking territory lost 2 army!')
            elif dice1[0] > dice2[0] and dice1[1] < dice2[1]:
                attackTerr.Armies -= 1
                defendTerr.Armies -= 1
                print('Both territories lost 1 army!')
            elif dice1[0] < dice2[0] and dice1[1] > dice2[1]:
                attackTerr.Armies -= 1
                defendTerr.Armies -= 1
                print('Both territories lost 1 army!')
        elif len(dice2) == 1:
            if dice1[0] > dice2[0]:
                defendTerr.Armies -= 1
                print('Defending territory lost 1 army!')
            elif dice1[0] < dice2[0]:
                attackTerr.Armies -= 1

        if defendTerr.Armies == 0:
            place_armyWinner(attackPlayer, defendTerr, attackTerr.Armies - 1)
            print('Attacking player has won the territory, your armies now occupy it!')
            break
        elif attackTerr.Armies == 1:
            print('Defending player has retain the territory!')
            break
        
# Move armies into territory that has been won
def place_armyWinner(player, terr, number):
    print('{} armies have been moved to {}!'.format(number, terr.name))
    if terr.Player != player:
        terr.Player = player
    terr.Armies += number


      
if __name__ == '__main__':
    print('\nEnd of Risk_setup.py')
