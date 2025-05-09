import greenfoot.*;

public class PlayerControls  
{
    private Contestant player;
    private boolean jumpButton;
    
    public PlayerControls(Contestant player)
    {
        this.player = player;
        jumpButton = false;
    }
    
    public void keyCommands()
    {
        if(Greenfoot.isKeyDown("left"))
        {
            player.moveLeft();
        }
        
        if(Greenfoot.isKeyDown("right"))
        {
            player.moveRight();
        }
        
        if(Greenfoot.isKeyDown("space") && jumpButton == false)
        {
            player.jump();
            jumpButton = true;
        }
        
        if(!Greenfoot.isKeyDown("space") && jumpButton == true)
        {
            jumpButton = false;
        }
    }
    
}
