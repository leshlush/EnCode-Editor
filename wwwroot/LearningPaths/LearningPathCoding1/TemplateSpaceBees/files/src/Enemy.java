import greenfoot.*;  // (World, Actor, GreenfootImage, Greenfoot and MouseInfo)

/**
 * Write a description of class Enemy here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class Enemy extends Actor
{
    private int speed;
    private long timer;
    private long duration;
    
    public Enemy()
    {
        speed = 2;
        timer = System.currentTimeMillis();
        duration = 2000;
        
       
    }
    
    
    public void act() 
    {
        move();
        shootTimer();
    }    
    
    public void move()
    {
        setLocation(getX() + speed, getY());
        if(isAtEdge())
        {
            speed = speed * -1;
        }
    }
    
    public void shootTimer()
    {
        if (System.currentTimeMillis() - duration > timer)
        {
            shoot();
            timer = System.currentTimeMillis();
        }
    }
    
    public void shoot()
    {
        EnemyLaser left = new EnemyLaser();
        EnemyLaser right = new EnemyLaser();
        EnemyLaser center = new EnemyLaser();
        left.angleLeft();
        right.angleRight();
        center.angleDown();
        
        World world = getWorld();
        world.addObject(left, getX(), getY());
        world.addObject(center, getX(), getY());
        world.addObject(right, getX(), getY());
    }
}
