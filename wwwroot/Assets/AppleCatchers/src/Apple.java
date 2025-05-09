import greenfoot.*;  // (World, Actor, GreenfootImage, Greenfoot and MouseInfo)

/**
 * Write a description of class Apple here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class Apple extends Actor
{
    private int speed;
    
    public Apple()
    {
        speed = Greenfoot.getRandomNumber(2) + 1;
    }
   
    
    public void act() 
    {
        moveDown();
        caught();
        disappear();
    }  
    
    public void moveDown()
    {
        setLocation(getX(), getY() + speed);
    }
    
    public void disappear()
    {
        if(getWorld() != null && isTouching(Ground.class))
        {
            getWorld().removeObject(this);
        }
    }
    
    public void caught()
    {
        Contestant catcher = (Contestant) getOneIntersectingObject(Contestant.class);
        if(catcher != null && catcher.getY() - getY() >= 25)
        {
            getWorld().removeObject(this);
            int applesCaught = catcher.getApplesCaught() + 1;
            catcher.setApplesCaught(applesCaught);
        }
    }
}
