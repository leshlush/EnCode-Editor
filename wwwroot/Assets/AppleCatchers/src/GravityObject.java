import greenfoot.*;

/**
 * Write a description of class GravityObject here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public interface GravityObject  
{
    public int getYSpeed();
    public void setYSpeed(int ySpeed);
    public void setLocation(int x, int y);
    public int getX(); 
    public int getY();
    public GreenfootImage getImage();   
    public Object getOneObjectAtOffset(int dx, int dy, java.lang.Class cls);
    
    
}
