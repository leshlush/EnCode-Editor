import greenfoot.*;  // (World, Actor, GreenfootImage, Greenfoot and MouseInfo)

public class MyWorld extends World
{

    private Player player;
    private Bottle rOne;
    private Bottle rTwo;
    private Bottle rThree;
    private Bottle rFour;
    
    private Cake gOne;
    private Cake gTwo;
    private Cake gThree;
    private Cake gFour;
    
    
    public MyWorld()
    {    
        // Create a new world with 600x400 cells with a cell size of 1x1 pixels.
        super(600, 400, 1); 
        
        player = new Player();
        addObject(player, 300, 300);
        
        rOne = new Bottle();
        addObject(rOne, 100, 100);
        
        rTwo = new Bottle();
        addObject(rTwo, 500, 100);
        
        rThree = new Bottle();
        addObject(rThree, 100, 200);
        
        rFour = new Bottle();
        addObject(rFour, 500, 200);
        
        gOne = new Cake();
        addObject(gOne, 200, 100);
        
        gTwo = new Cake();
        addObject(gTwo, 400, 100);
        
        gThree = new Cake();
        addObject(gThree, 200, 300);
        
        gFour = new Cake();
        addObject(gFour, 500, 300);
    }
}
