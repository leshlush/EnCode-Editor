import greenfoot.*;  // (World, Actor, GreenfootImage, Greenfoot and MouseInfo)

/**
 * Write a description of class PlayerEntity here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class PlayerEntity extends Actor
{
    private Contestant player;
    private Gravitator gravitator;
    private PlayerControls controls;
    private Label applesCaughtLabel;   
    
    public PlayerEntity()
    {
        getImage().setTransparency(0);
        
        player = new Contestant();
        gravitator = new Gravitator();
        controls = new PlayerControls(player);
        applesCaughtLabel = new Label(player.getApplesCaught(), 25);
    }
    
    protected void addedToWorld(World world)
    {
        world.addObject(player, getX(), getY());
        world.addObject(applesCaughtLabel, player.getX(), player.getY() - 50);
    }
    
    public void act()
    {
        gravitator.gravitate(player);
        controls.keyCommands();
        applesCaughtLabel.setLocation(player.getX(), player.getY() - 50);
        applesCaughtLabel.setValue(player.getApplesCaught());
        
    }
    
    public void setPlayerColorBlue()
    {
        player.setColorBlue();
    }
    
    public void setPlayerColorRed()
    {
        player.setColorRed();
    }
    
    public void setPlayerColorGreen()
    {
        player.setColorGreen();
    }
}
