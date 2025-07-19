import greenfoot.*;  // (World, Actor, GreenfootImage, Greenfoot and MouseInfo)

/**
 * Write a description of class MyWorld here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class Forest extends World
{
    private Helicopter helicopter;
       
    public Forest()
    {    
        // Create a new world with 600x400 cells with a cell size of 1x1 pixels.
        super(600, 400, 1); 
     
        
        helicopter = new Helicopter();
        helicopter.setRotation(270);
        addObject(helicopter, 300, 300);
        
    }
    
    public void act()
    {
       
    }
    
        
   
}
