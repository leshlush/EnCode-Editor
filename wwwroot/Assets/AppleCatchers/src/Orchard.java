import greenfoot.*;  // (World, Actor, GreenfootImage, Greenfoot and MouseInfo)

import java.util.List;
import java.util.Collections;
/**
 * Write a description of class MyWorld here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class Orchard extends World
{
    private Ground ground;
    private PlayerEntity player;
    private RivalEntity rivalOne;
    private RivalEntity rivalTwo;
    private int appleTimer;
    private int gameTime;
    private Bar gameTimeBar;
        
    public Orchard()
    {    
        // Create a new world with 600x400 cells with a cell size of 1x1 pixels.
        super(600, 400, 1); 
        
        ground = new Ground();
        addObject(ground, 300, 400);
        
        player = new PlayerEntity();
        player.setPlayerColorBlue();
        addObject(player, 300, 200);
        
       
        
        rivalTwo = new RivalEntity();
        rivalTwo.setRivalColorRed();
        //addObject(rivalTwo, 500, 200);
        
        appleTimer = 20;
        
        gameTime = 2000;
        gameTimeBar = new Bar("Time Left: ", gameTime, gameTime);
        addObject(gameTimeBar, 100, 30);
    }
    
    public void act()
    {
        addAppleOnTimer();
        gameTimeBar.setValue(gameTime);
        countDown();
        end();
    }
    
    public void countDown()
    {
        gameTime--;
    }
    
    public void end()
    {
        if(gameTime < 0)
        {
            declareWinner();
            Greenfoot.stop();
        }
    }
    
    public void addApple()
    {
        int x = Greenfoot.getRandomNumber(600);
        addObject(new Apple(), x, 0);
    }
    
    public void addAppleOnTimer()
    {
        if(appleTimer > 0)
        {
            appleTimer = appleTimer - 1;
        }
        
        else
        {
            appleTimer = Greenfoot.getRandomNumber(300) + 50;
            addApple();
            addApple();
        }
    }
    
    public List<Contestant> getContestantsInOrder()
    {
        List<Contestant> contestants = (List<Contestant>) getObjects(Contestant.class);
        Collections.sort(contestants);
        return contestants;
    }
    
    public void declareWinner()
    {
        List<Contestant> contestants = getContestantsInOrder();
        if(contestants.get(0).getApplesCaught() == contestants.get(1).getApplesCaught())
        {
            showText("DRAW!!!!", 300, 200);
        }
        else
        {
            Contestant winner = contestants.get(0);
            showText(winner.getContestantName() + " wins!!", 300, 200);
        }
    }
    
    
    
}
