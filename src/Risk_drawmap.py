import sys
from math import *
import pygame
#from pygame.locals import *
pygame.init()


def Draw():
        # Pygame set screen size and caption
        width = 1024
        height = 620
        size = (width, height)

        display_Surf = pygame.display.set_mode(size)
        pygame.display.set_caption('Risk pygame')

        # Make display background white
        WHITE = (255, 255, 255)
        display_Surf.fill(WHITE)

        # Colors for graphics
        BLACK = (0, 0, 0)
        GREY = (128, 128, 128)


        class Terrs:
                # Territories by number (0 thru 41)
                Alaska = 0
                NW_territory = 1
                Alberta = 2
                Ontario = 3
                West_US = 4
                East_US = 5
                central_America = 6
                Quebec = 7
                Greenland = 8
                Iceland = 9
                Great_Britain = 10
                N_Europe = 11
                W_Europe = 12
                S_Europe = 13
                Scandinavia = 14
                Ukraine = 15
                Afghanistan = 16
                Ural = 17
                Siberia = 18
                Yakutsk = 19
                Kamchatka = 20
                Irkutsk = 21
                Mongolia = 22
                China = 23
                Middle_East = 24
                India = 25
                Siam = 26
                Japan = 27
                Venezuela = 28
                Peru = 29
                Brazil = 30
                Argentina = 31
                N_Africa = 32
                Egypt = 33
                E_Africa = 34
                Congo = 35
                S_Africa = 36
                Madagascar = 37
                Indonesia = 38
                New_Guinea = 39
                W_Australia = 40
                E_Australia = 41

           
        # Locations of Terrs
        Loc = [[0,0], [0,0], [0,0], [0,0], [0,0], [0,0], [0,0], [0,0], [0,0], [0,0]
                , [0,0], [0,0], [0,0], [0,0], [0,0], [0,0], [0,0], [0,0], [0,0], [0,0]
                , [0,0], [0,0], [0,0], [0,0], [0,0], [0,0], [0,0], [0,0], [0,0], [0,0]
                , [0,0], [0,0], [0,0], [0,0], [0,0], [0,0], [0,0], [0,0], [0,0], [0,0]
                , [0,0], [0,0]]
        
        
        def set_Loc():
                set_Alaska(Terrs.Alaska, 50, 75)
                set_TerrsLoc(Terrs.NW_territory, Terrs.Alaska, 75, 0)
                set_TerrsLoc(Terrs.Alberta, Terrs.NW_territory, 0, 75)
                set_TerrsLoc(Terrs.Ontario, Terrs.Alberta, 75, 0)
                set_TerrsLoc(Terrs.West_US, Terrs.Alberta, 0, 75)
                set_TerrsLoc(Terrs.East_US, Terrs.West_US, 75, 0)
                set_TerrsLoc(Terrs.central_America, Terrs.West_US, 0, 75)
                set_TerrsLoc(Terrs.Quebec, Terrs.Ontario, 75, 0)
                set_TerrsLoc(Terrs.Greenland, Terrs.NW_territory, 200, 0)
                set_TerrsLoc(Terrs.Iceland, Terrs.Greenland, 50, 100)
                set_TerrsLoc(Terrs.Great_Britain, Terrs.Iceland, 0, 75)
                set_TerrsLoc(Terrs.W_Europe, Terrs.Great_Britain, 0, 75)
                set_TerrsLoc(Terrs.Scandinavia, Terrs.Iceland, 75, 0)
                set_TerrsLoc(Terrs.N_Europe, Terrs.Scandinavia, 0, 75)
                set_TerrsLoc(Terrs.S_Europe, Terrs.N_Europe, 0, 75)
                set_TerrsLoc(Terrs.Ukraine, Terrs.N_Europe, 75, 0)
                set_TerrsLoc(Terrs.Venezuela, Terrs.central_America, 75, 75)
                set_TerrsLoc(Terrs.Peru, Terrs.Venezuela, 0, 75)
                set_TerrsLoc(Terrs.Brazil, Terrs.Venezuela, 75, 75)
                set_TerrsLoc(Terrs.Argentina, Terrs.Peru, 0, 75)
                set_TerrsLoc(Terrs.N_Africa, Terrs.W_Europe, 50, 100)
                set_TerrsLoc(Terrs.Egypt, Terrs.N_Africa, 75, 0)
                set_TerrsLoc(Terrs.Congo, Terrs.N_Africa, 0, 75)
                set_TerrsLoc(Terrs.E_Africa, Terrs.Egypt, 0, 75)
                set_TerrsLoc(Terrs.S_Africa, Terrs.Congo, 75, 75)
                set_TerrsLoc(Terrs.Madagascar, Terrs.S_Africa, 75, 0)
                set_TerrsLoc(Terrs.Ural, Terrs.Ukraine, 75, -75)
                set_TerrsLoc(Terrs.Afghanistan, Terrs.Ural, 0, 75)
                set_TerrsLoc(Terrs.Middle_East, Terrs.Afghanistan, 0, 75)
                set_TerrsLoc(Terrs.Siberia, Terrs.Ural, 75, 0)
                set_TerrsLoc(Terrs.Yakutsk, Terrs.Siberia, 100, -100)
                set_TerrsLoc(Terrs.Irkutsk, Terrs.Yakutsk, 0, 75)
                set_TerrsLoc(Terrs.Mongolia, Terrs.Irkutsk, 0, 75)
                set_TerrsLoc(Terrs.India, Terrs.Middle_East, 75, 25)
                set_TerrsLoc(Terrs.China, Terrs.Mongolia, -25, 75)
                set_TerrsLoc(Terrs.Siam, Terrs.China, 0, 75)
                set_TerrsLoc(Terrs.Kamchatka, Terrs.Irkutsk, 75, -75)
                set_TerrsLoc(Terrs.Japan, Terrs.Mongolia, 75, 0)
                set_TerrsLoc(Terrs.Indonesia, Terrs.Siam, 75, 75)
                set_TerrsLoc(Terrs.W_Australia, Terrs.Indonesia, 0, 75)
                set_TerrsLoc(Terrs.E_Australia, Terrs.W_Australia, 75, 0)
                set_TerrsLoc(Terrs.New_Guinea, Terrs.Indonesia, 75, 0)


        #Set first Territory location, Alaska
        def set_Alaska(Alaska, x, y):
                Loc[Alaska][0] = x
                Loc[Alaska][1] = y


        #Set remaining locations
        def set_TerrsLoc(curr_Terr, prev_Terr, x, y):
                Loc[curr_Terr][0] = Loc[prev_Terr][0] + x
                Loc[curr_Terr][1] = Loc[prev_Terr][1] + y



        #Draw territories to surface
        def draw_Circle(Loc):
                for i in range(len(Loc)):
                        # Draw Shape
                        pygame.draw.circle(display_Surf, GREY, Loc[i], 25, 0)
                        draw_Font(Loc, i)

        def draw_Font(Loc, i):
                fontObj = pygame.font.SysFont('timesnewroman', 25)
                textSurfaceObj = fontObj.render(str(i+1), True, WHITE)
                textRectObj = textSurfaceObj.get_rect()
                textRectObj.center = (Loc[i])
                display_Surf.blit(textSurfaceObj, textRectObj)


        # List of Connections                
        def set_Connect(Loc):
                # There is a total of 80 connections
                draw_Connect((0, 75), Loc[Terrs.Alaska]) #1
                draw_Connect(Loc[Terrs.Alaska], Loc[Terrs.NW_territory]) #2
                draw_Connect(Loc[Terrs.Alaska], Loc[Terrs.Alberta]) #3
                draw_Connect(Loc[Terrs.NW_territory], Loc[Terrs.Greenland]) #4
                draw_Connect(Loc[Terrs.NW_territory], Loc[Terrs.Alberta]) #5
                draw_Connect(Loc[Terrs.NW_territory], Loc[Terrs.Ontario]) #6
                draw_Connect(Loc[Terrs.Alberta], Loc[Terrs.Ontario]) #7
                draw_Connect(Loc[Terrs.Ontario], Loc[Terrs.Greenland]) #8
                draw_Connect(Loc[Terrs.Quebec], Loc[Terrs.Greenland]) #9
                draw_Connect(Loc[Terrs.Iceland], Loc[Terrs.Greenland]) #10
                draw_Connect(Loc[Terrs.East_US], Loc[Terrs.Quebec]) #11
                draw_Connect(Loc[Terrs.Ontario], Loc[Terrs.Quebec]) #12
                draw_Connect(Loc[Terrs.Ontario], Loc[Terrs.West_US]) #13
                draw_Connect(Loc[Terrs.Ontario], Loc[Terrs.East_US]) #14
                draw_Connect(Loc[Terrs.West_US], Loc[Terrs.central_America]) #15
                draw_Connect(Loc[Terrs.West_US], Loc[Terrs.Alberta]) #16
                draw_Connect(Loc[Terrs.West_US], Loc[Terrs.East_US]) #17
                draw_Connect(Loc[Terrs.central_America], Loc[Terrs.East_US]) #18
                draw_Connect(Loc[Terrs.central_America], Loc[Terrs.Venezuela]) #19
                draw_Connect(Loc[Terrs.Venezuela], Loc[Terrs.Peru]) #20
                draw_Connect(Loc[Terrs.Venezuela], Loc[Terrs.Brazil]) #21
                draw_Connect(Loc[Terrs.Peru], Loc[Terrs.Brazil]) #22
                draw_Connect(Loc[Terrs.Argentina], Loc[Terrs.Brazil]) #23
                draw_Connect(Loc[Terrs.N_Africa], Loc[Terrs.Brazil]) #24
                draw_Connect(Loc[Terrs.Peru], Loc[Terrs.Argentina]) #25
                draw_Connect(Loc[Terrs.N_Africa], Loc[Terrs.Egypt]) #26
                draw_Connect(Loc[Terrs.N_Africa], Loc[Terrs.E_Africa]) #27
                draw_Connect(Loc[Terrs.N_Africa], Loc[Terrs.Congo]) #28
                draw_Connect(Loc[Terrs.Egypt], Loc[Terrs.E_Africa]) #29
                draw_Connect(Loc[Terrs.Congo], Loc[Terrs.E_Africa]) #30
                draw_Connect(Loc[Terrs.Congo], Loc[Terrs.S_Africa]) #31
                draw_Connect(Loc[Terrs.Madagascar], Loc[Terrs.E_Africa]) #32
                draw_Connect(Loc[Terrs.S_Africa], Loc[Terrs.E_Africa]) #33
                draw_Connect(Loc[Terrs.Egypt], Loc[Terrs.S_Europe]) #34
                draw_Connect(Loc[Terrs.Egypt], Loc[Terrs.Middle_East]) #35
                draw_Connect(Loc[Terrs.N_Africa], Loc[Terrs.W_Europe]) #35
                draw_Connect(Loc[Terrs.Iceland], Loc[Terrs.Great_Britain]) #36
                draw_Connect(Loc[Terrs.Iceland], Loc[Terrs.Scandinavia]) #37
                draw_Connect(Loc[Terrs.Great_Britain], Loc[Terrs.N_Europe]) #38
                draw_Connect(Loc[Terrs.Great_Britain], Loc[Terrs.W_Europe]) #39
                draw_Connect(Loc[Terrs.W_Europe], Loc[Terrs.S_Europe]) #40
                draw_Connect(Loc[Terrs.N_Europe], Loc[Terrs.S_Europe]) #41
                draw_Connect(Loc[Terrs.Scandinavia], Loc[Terrs.N_Europe]) #42
                draw_Connect(Loc[Terrs.Ukraine], Loc[Terrs.Scandinavia]) #43
                draw_Connect(Loc[Terrs.Ukraine], Loc[Terrs.N_Europe]) #44
                draw_Connect(Loc[Terrs.Ukraine], Loc[Terrs.S_Europe]) #45
                draw_Connect(Loc[Terrs.Ukraine], Loc[Terrs.Middle_East]) #46
                draw_Connect(Loc[Terrs.Ukraine], Loc[Terrs.Afghanistan]) #47
                draw_Connect(Loc[Terrs.Ukraine], Loc[Terrs.N_Europe]) #48
                draw_Connect(Loc[Terrs.S_Europe], Loc[Terrs.Middle_East]) #49
                draw_Connect(Loc[Terrs.Ukraine], Loc[Terrs.Ural]) #50
                draw_Connect(Loc[Terrs.Afghanistan], Loc[Terrs.Ural]) #51
                draw_Connect(Loc[Terrs.Siberia], Loc[Terrs.Ural]) #52
                draw_Connect(Loc[Terrs.Afghanistan], Loc[Terrs.Middle_East]) #53
                draw_Connect(Loc[Terrs.India], Loc[Terrs.Middle_East]) #54
                draw_Connect(Loc[Terrs.India], Loc[Terrs.Afghanistan]) #55
                draw_Connect(Loc[Terrs.India], Loc[Terrs.China]) #56
                draw_Connect(Loc[Terrs.India], Loc[Terrs.Siam]) #57
                draw_Connect(Loc[Terrs.Indonesia], Loc[Terrs.Siam]) #58
                draw_Connect(Loc[Terrs.Indonesia], Loc[Terrs.New_Guinea]) #59
                draw_Connect(Loc[Terrs.Indonesia], Loc[Terrs.W_Australia]) #60
                draw_Connect(Loc[Terrs.E_Australia], Loc[Terrs.New_Guinea]) #61
                draw_Connect(Loc[Terrs.W_Australia], Loc[Terrs.E_Australia]) #62
                draw_Connect(Loc[Terrs.W_Australia], Loc[Terrs.New_Guinea]) #63
                draw_Connect(Loc[Terrs.China], Loc[Terrs.Siam]) #64
                draw_Connect(Loc[Terrs.China], Loc[Terrs.Afghanistan]) #65
                draw_Connect(Loc[Terrs.China], Loc[Terrs.Ural]) #66
                draw_Connect(Loc[Terrs.China], Loc[Terrs.Siberia]) #67
                draw_Connect(Loc[Terrs.China], Loc[Terrs.Mongolia]) #68
                draw_Connect(Loc[Terrs.Siberia], Loc[Terrs.Yakutsk]) #69
                draw_Connect(Loc[Terrs.Siberia], Loc[Terrs.Irkutsk]) #70
                draw_Connect(Loc[Terrs.Mongolia], Loc[Terrs.Irkutsk]) #71
                draw_Connect(Loc[Terrs.Kamchatka], Loc[Terrs.Irkutsk]) #72
                draw_Connect(Loc[Terrs.Mongolia], Loc[Terrs.Siberia]) #73
                draw_Connect(Loc[Terrs.Kamchatka], Loc[Terrs.Yakutsk]) #74
                draw_Connect(Loc[Terrs.Kamchatka], Loc[Terrs.Mongolia]) #75
                draw_Connect(Loc[Terrs.Kamchatka], Loc[Terrs.Japan]) #76
                draw_Connect(Loc[Terrs.Mongolia], Loc[Terrs.Japan]) #77
                draw_Connect(Loc[Terrs.Great_Britain], Loc[Terrs.Scandinavia]) #78
                draw_Connect(Loc[Terrs.W_Europe], Loc[Terrs.N_Europe]) #79
                draw_Connect(Loc[Terrs.Kamchatka], (1024, 75)) #80
        


        # Draw connections        
        def draw_Connect(p1, p2):
                pygame.draw.line(display_Surf, BLACK, p1, p2, 3)


        set_Loc()
        set_Connect(Loc) # Needs to be done before territories (Circles)
        draw_Circle(Loc) 



# Changes color by redrawing territory
def changeColor(Loc, terr):
        pygame.draw.circle(display_Surf, BLUE, Loc[terr], 25, 0)
        draw_Font(Loc, terr)


def draw_Font(Loc, i):
        fontObj = pygame.font.SysFont('timesnewroman', 25)
        textSurfaceObj = fontObj.render(str(i+1), True, WHITE)
        textRectObj = textSurfaceObj.get_rect()
        textRectObj.center = (Loc[i])
        display_Surf.blit(textSurfaceObj, textRectObj)


# Only runs if executed as main
if __name__ == '__main__':
        Draw()
        while True:
                for event in pygame.event.get():
                        if event.type == pygame.QUIT:
                                pygame.quit()
                                sys.exit()
                
                pygame.display.flip()

        print('End of Risk_drawmap.py')
