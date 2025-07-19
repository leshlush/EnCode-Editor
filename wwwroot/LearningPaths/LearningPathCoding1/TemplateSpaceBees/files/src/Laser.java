import greenfoot.*;  // (World, Actor, GreenfootImage, Greenfoot and MouseInfo)

/**
 * Write a description of class Laser here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class Laser extends Actor
{
   
    private int speed;
    
    public Laser()
    {
        speed = 4;
    }
    
    public void act() 
    {
       setLocation(getX(), getY() - speed);
       disappear();
    }    
    
    
    public void disappear()
    {
        if(getY() <= 10 )
        {
            World world = getWorld();
            world.removeObject(this);
        }
    }
}
