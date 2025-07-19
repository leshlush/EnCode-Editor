import greenfoot.*;  // (World, Actor, GreenfootImage, Greenfoot and MouseInfo)

/**
 * Write a description of class MyWorld here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class Space extends World
{
    private SpaceShip ship;
    private Enemy enemy;
    
    private Bar shipAmmoBar;
    private Bar shipShieldBar;
    public Space()
    {    
        // Create a new world with 600x400 cells with a cell size of 1x1 pixels.
        super(600, 400, 1);
        
        ship = new SpaceShip();
        addObject(ship, 300, 360);
        
        enemy = new Enemy();
        addObject(enemy, 20, 70);
        
        shipAmmoBar = new Bar("Ammo", 20, 20);
        shipAmmoBar.setTextColor(Color.WHITE);
        addObject(shipAmmoBar, 83, 355);
        
        
        shipShieldBar = new Bar("Shield", 100, 100);
        shipShieldBar.setTextColor(Color.WHITE);
        addObject(shipShieldBar, 85, 380);
    }
    
    public void act()
    {
       
        
    }
    
    
   
    private int getRandomFromRange(int low, int high)
    {
        return (int) (Math.random() * (high - low) + low + 1);
    }
    
    private boolean randomChance(int chance)
    {
        if(chance >= getRandomFromRange(0, 100) )
        {
            return true;
        }
        return false;
    }
}
