import greenfoot.*;  // (World, Actor, GreenfootImage, Greenfoot and MouseInfo)


public class Player extends Actor
{
    private int speed;
    
    public Player()
    {
        speed = 3;
    }
    
    public void keyCommands()
    {
       
    }    
    
    public void act() 
    {
        keyCommands();
    }    
        
    public void getBigger()
    {
        GreenfootImage img = getImage();
        int width = img.getWidth() + (img.getWidth() / 5);
        int height = img.getHeight() + (img.getHeight() / 5);
        img.scale(width, height);
        setImage(img);
    }
    
    public void getSmaller()
    {
        GreenfootImage img = getImage();
        int width = img.getWidth() - (img.getWidth() / 5);
        int height = img.getHeight() - (img.getHeight() / 5);
        img.scale(width, height);
        setImage(img);
    }
    
    public void moveLeft()
    {
        setLocation(getX() - speed, getY());
    }
    
    public void moveRight()
    {
        setLocation(getX() + speed, getY());
    }
    
    public void moveUp()
    {
        setLocation(getX(), getY() - speed);
    }
    
    public void moveDown()
    {
        setLocation(getX(), getY() + speed);
    }
    
    
}
