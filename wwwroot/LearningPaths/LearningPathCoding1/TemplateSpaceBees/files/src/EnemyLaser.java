import greenfoot.*;  // (World, Actor, GreenfootImage, Greenfoot and MouseInfo)

/**
 * Write a description of class EnemyLaser here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class EnemyLaser extends Actor
{
    private int speed;
    
    public EnemyLaser()
    {
        speed = 5;
    }
    
    public void act() 
    {
        move(speed);
        disappear();
    }    
    
    public void angleLeft()
    {
        turn(135);
    }
    
    public void angleRight()
    {
        turn(45);
    }
    
    public void angleDown()
    {
        turn(90);
    }
    
    public void disappear()
    {
        World world = getWorld();
        if(isAtEdge())
        {
            world.removeObject(this);
        }
    }
}
