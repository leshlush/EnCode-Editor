import greenfoot.*;  // (World, Actor, GreenfootImage, Greenfoot and MouseInfo)

/**
 * Write a description of class Contestant here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class Contestant extends Actor implements GravityObject, Landable, Impassable, Comparable
{
    private int ySpeed;
    private int xSpeed;
    private int applesCaught;
    private GreenfootImage blue;
    private GreenfootImage red;
    private GreenfootImage green;
    private String contestantName;
    
    public Contestant()
    {
        ySpeed = 0;
        xSpeed = 3;
        applesCaught = 0;
        blue = new GreenfootImage("BlueContestant.png");
        red = new GreenfootImage("RedContestant.png");
        green = new GreenfootImage("GreenContestant.png");
        contestantName = "Blue Player";
    }
    
    public void moveLeft()
    {
        boolean alreadyStuck = intersectsImpassableOnRight();
        int x = getX();
        int y = getY();
        setLocation(getX() - xSpeed, getY());
        if(!alreadyStuck && hitImpassable())
        {
            setLocation(x,y);
        }
    }    
    
    public void moveRight()
    {
        boolean alreadyStuck = intersectsImpassableOnLeft();
        int x = getX();
        int y = getY();
        setLocation(getX() + xSpeed, getY());
        if(!alreadyStuck && hitImpassable())
        {
            setLocation(x,y);
        }
    }
    
    public void jump()
    {
        if(ySpeed == 0 && getOneObjectAtOffset(0, getImage().getHeight() / 2, Landable.class) != null)
        {
            ySpeed = 30;
        }
    }
    
    public boolean intersectsImpassableOnLeft()
    {
       Impassable intersects = (Impassable) getOneIntersectingObject(Impassable.class);
       if(intersects != null && intersects.getX() < getX())
       {
           return true;
       }
       
       return false;
    }
    
    public boolean intersectsImpassableOnRight()
    {
       Impassable intersects = (Impassable) getOneIntersectingObject(Impassable.class);
       if(intersects != null && intersects.getX() > getX())
       {
           return true;
       }
       
       return false;
    }
    
    public boolean hitImpassable()
    {
        if(isTouching(Impassable.class))
        {
            return true;
        }
        return false;
    }
    
    public int getApplesCaught()
    {
        return applesCaught;
    }
    
    public void setApplesCaught(int applesCaught)
    {
        this.applesCaught = applesCaught;
    }
    
    public void setColorBlue()
    {
        setImage(blue);
        contestantName = "Blue Player";
    }
    
    public void setColorGreen()
    {
        setImage(green);
        contestantName = "Green Player";
    }
    
    public void setColorRed()
    {
        setImage(red);
        contestantName = "Red Player";
    }
    
    public String getContestantName()
    {
        return contestantName;
    }
      
    @Override
    public int getYSpeed()
    {
        return ySpeed;
    }
    
    @Override 
    public void setYSpeed(int ySpeed)
    {
        this.ySpeed = ySpeed;   
    }       
    
    @Override
    public Actor getOneObjectAtOffset(int dx, int dy, java.lang.Class cls)
    {
        return super.getOneObjectAtOffset(dx, dy, cls);
    }
    
    @Override
    public int compareTo(Object other)
    {
        Contestant c = (Contestant) other;
        return c.getApplesCaught() - applesCaught;
    }
}
    
    

