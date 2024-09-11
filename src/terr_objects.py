import sys
import Risk_drawmap
import pygame
pygame.init()

def makeTerrs():
    class Terr(object):
        def __init__(self, name, cont, neighbors):
            self.name = name
            self.cont = cont
            self.Armies = 0
            self.neighbors = neighbors
            self.Player = None # Should be a player object
            self.coordinates = [0,0]
            self.color = None
            self.terrNum = 0

        def string_to_Object(self):
            # Change list of neighbors from string to objects
            for i in range(len(self.neighbors)):
                self.neighbors[i] = territories[self.neighbors[i]]

        def isNeighbor(self, terr):
            # Check list of neighbors, returns True or False
            return terr in self.neighbors
        
        def print_Neigh(self):
            # For testing
            print(self.name, self.neighbors)


    # Create all territory objects
    alaska = Terr('Alaska', 'N.America', ['Alberta', 'Northwest Territory', 'Kamchatka'])
    alberta = Terr('Alberta', 'N.America', ['Alaska', 'Northwest Territory', 'Ontario', 'West U.S.'])
    greenland = Terr('Greenland', 'N.America', ['Northwest Territory', 'Ontario', 'Iceland', 'Quebec'])
    northwest_Territory = Terr('Northwest Territory', 'N.America', ['Alaska', 'Alberta', 'Ontario', 'Greenland'])
    ontario = Terr('Ontario', 'N.America', ['Greenland', 'Alberta', 'Northwest Territory', 'West U.S.', 'East U.S.', 'Quebec'])
    quebec = Terr('Quebec', 'N.America', ['Greenland', 'East U.S.', 'Ontario'])
    west_us = Terr('West U.S.', 'N.America', ['Alberta', 'Ontario', 'East U.S.', 'Central America'])
    east_us = Terr('East U.S.', 'N.America', ['Ontario', 'West U.S.', 'Central America', 'Quebec'])
    central_America = Terr('Central America', 'N.America', ['West U.S.', 'East U.S.', 'Venezuela'])
    venezuela = Terr('Venezuela', 'S.America', ['Central America', 'Peru', 'Brazil'])
    brazil = Terr('Brazil', 'S.America', ['Venezuela', 'Peru', 'Argentina', 'North Africa'])
    peru = Terr('Peru', 'S.America', ['Venezuela', 'Brazil', 'Argentina'])
    argentina = Terr('Argentina', 'S.America', ['Peru', 'Brazil'])
    south_Africa = Terr('South Africa', 'Africa', ['Congo', 'East Africa'])
    congo = Terr('Congo', 'Africa', ['North Africa', 'East Africa', 'South Africa'])
    east_Africa = Terr('East Africa', 'Africa', ['Egypt', 'North Africa', 'Congo', 'South Africa', 'Madagascar'])
    north_Africa = Terr('North Africa', 'Africa', ['Brazil', 'Congo', 'East Africa', 'Egypt', 'West Europe'])
    egypt = Terr('Egypt', 'Africa', ['North Africa', 'East Africa', 'Middle East', 'South Europe'])
    madagascar = Terr('Madagascar', 'Africa', ['East Africa'])
    west_Europe = Terr('West Europe', 'Europe', ['North Africa', 'South Europe', 'North Europe', 'Great Britain'])
    great_Brit = Terr('Great Britain', 'Europe', ['West Europe', 'North Europe', 'Scandinavia', 'Iceland'])
    iceland = Terr('Iceland', 'Europe', ['Greenland', 'Great Britain', 'Scandinavia'])
    scandinavia = Terr('Scandinavia', 'Europe', ['Iceland', 'Great Britain', 'North Europe', 'Ukraine'])
    north_Europe = Terr('North Europe', 'Europe', ['Scandinavia', 'Great Britain', 'West Europe', 'South Europe', 'Ukraine'])
    south_Europe = Terr('South Europe', 'Europe', ['West Europe', 'North Europe', 'Ukraine', 'Middle East', 'Egypt'])
    ukraine = Terr('Ukraine', 'Europe', ['Scandinavia', 'North Europe', 'South Europe', 'Middle East', 'Afghanistan', 'Ural'])
    middle_East = Terr('Middle East', 'Asia', ['Egypt', 'South Europe', 'Ukraine', 'Afghanistan', 'India'])
    afghanistan = Terr('Afghanistan', 'Asia', ['Middle East', 'Ukraine', 'India', 'China', 'Ural'])
    ural = Terr('Ural', 'Asia', ['Ukraine', 'Afghanistan', 'China', 'Siberia'])
    india = Terr('India', 'Asia', ['Middle East', 'Afghanistan', 'China', 'Siam'])
    siam = Terr('Siam', 'Asia', ['India', 'China', 'Indonesia'])
    china = Terr('China', 'Asia', ['Siam', 'India', 'Afghanistan', 'Siberia', 'Mongolia', 'Ural'])
    mongolia = Terr('Mongolia', 'Asia', ['China', 'Siberia', 'Irkutsk', 'Kamchatka', 'Japan'])
    japan = Terr('Japan', 'Asia', ['Kamchatka', 'Mongolia'])
    siberia = Terr('Siberia', 'Asia', ['China', 'Mongolia', 'Irkutsk', 'Yakutsk', 'Ural'])
    irkutsk = Terr('Irkutsk', 'Asia', ['Siberia', 'Mongolia', 'Kamchatka', 'Yakutsk'])
    yakutsk = Terr('Yakutsk', 'Asia', ['Siberia', 'Irkutsk', 'Kamchatka'])
    kamchatka = Terr('Kamchatka', 'Asia', ['Yakutsk', 'Irkutsk', 'Mongolia', 'Japan', 'Alaska'])
    indonesia = Terr('Indonesia', 'Australia', ['Siam', 'New Guinea', 'West Australia'])
    new_Guinea = Terr('New Guinea', 'Australia', ['Indonesia', 'West Australia', 'East Australia'])
    west_Aust = Terr('West Australia', 'Australia', ['Indonesia', 'New Guinea', 'East Australia'])
    east_Aust = Terr('East Australia', 'Australia', ['West Australia', 'New Guinea'])

    # List of all Territory objects
    terr_Obj = [alaska, northwest_Territory, alberta, ontario, west_us, east_us, central_America, quebec, greenland, iceland, great_Brit
                , north_Europe, west_Europe, south_Europe, scandinavia, ukraine, afghanistan, ural, siberia, yakutsk, kamchatka, irkutsk
                , mongolia, china, middle_East, india, siam, japan, venezuela, peru, brazil, argentina, north_Africa, egypt, east_Africa
                , congo, south_Africa, madagascar, indonesia, new_Guinea, west_Aust, east_Aust]

    # Territory coordinates
    Locations = [[50, 75], [125, 75], [125, 150], [200, 150], [125, 225], [200, 225], [125, 300], [275, 150], [325, 75], [375, 175]
                 , [375, 250], [450, 250], [375, 325], [450, 325], [450, 175], [525, 250], [600, 250], [600, 175], [675, 175], [775, 75]
                 , [850, 75], [775, 150], [775, 225], [750, 300], [600, 325], [675, 350], [750, 375], [850, 225], [200, 375], [200, 450]
                 , [275, 450], [200, 525], [425, 425], [500, 425], [500, 500], [425, 500], [500, 575], [575, 575], [825, 450], [900, 450]
                 , [825, 525], [900, 525]]

    for i in range(len(terr_Obj)):
        terr_Obj[i].coordinates = Locations[i]
        terr_Obj[i].terrNum = (i+1)

    territories = {}
    def make_dict(terr_Obj, territories):
        # Puts Terr(object) into dictionary with keys equal to 'Names'
        i = 0
        for terr in terr_Obj:
            territories[terr.name] = terr_Obj[i]
            i += 1

    make_dict(terr_Obj, territories) # Make dictionary of Terr(objects)

    # Changes list of neighbors from strings into Terr objects
    for a_terr in territories.values():
        a_terr.string_to_Object()

    return territories




if __name__ == '__main__':
    makeTerrs()
    while True:
        Risk_drawmap.Draw()
        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                pygame.quit()
                sys.exit()
            pygame.display.update()

    print('\nEnd of terr_objects.py')
