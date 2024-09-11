import sys
import Risk_engine
import Risk_drawmap
import pygame
pygame.init()

if __name__ == '__main__':
    Risk_engine.riskSetup()
    Risk_drawmap.Draw()
    while True:
        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                pygame.quit()
                sys.exit()
                
        pygame.display.flip()


        # Check for winner
        playerWins = Risk_engine.checkWinner()
        if playerWins == True:
            break
        else:
            print('Loop!')


        # Player moves
        Risk_engine.playerTurns()


