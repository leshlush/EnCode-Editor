import greenfoot.*;  // (World, Actor, GreenfootImage, Greenfoot and MouseInfo)

/**
 * Write a description of class SpaceShip here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class SpaceShip extends Actor
{
    private int speed;
    private boolean laserButton;
       
    public SpaceShip()
    {
        speed = 2;
        laserButton = false;
        
    }    
      
    public void act() 
    {
        keyCommands();
    }      

    public void moveRight()
    {
        setLocation(getX() + speed, getY());
    }
    
    public void fireLaser()
    {
        World world = getWorld();
        world.addObject(new Laser(), getX(), getY());
    }
    
        
    public void keyCommands()
    {
       
        if(Greenfoot.isKeyDown("right"))
        {
            moveRight();
        }
       
        if(Greenfoot.isKeyDown("space") && laserButton == false)
        {
            laserButton = true;
            fireLaser();
        }
        
        if(!Greenfoot.isKeyDown("space") && laserButton == true)
        {
            laserButton = false;
        }
    }
}
