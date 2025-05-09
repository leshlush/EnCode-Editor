import greenfoot.*;  // (World, Actor, GreenfootImage, Greenfoot and MouseInfo)

/**
 * Write a description of class RivalEntity here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class RivalEntity extends Actor
{
   private Contestant rival;
   private Gravitator gravitator;
   private RivalIntelligence intelligence;
   private Label applesCaughtLabel;
   
   public RivalEntity()
   {
       getImage().setTransparency(0);
       
       rival = new Contestant();
       gravitator = new Gravitator();
       intelligence = new RivalIntelligence(rival);
       applesCaughtLabel = new Label(rival.getApplesCaught(), 25);
   }
   
   protected void addedToWorld(World world)
   {
       world.addObject(rival, getX(), getY());
       world.addObject(intelligence, getX(), getY());
       world.addObject(applesCaughtLabel, rival.getX(), rival.getY() - 50);
   }
   
   public void act()
   {
       gravitator.gravitate(rival);
       applesCaughtLabel.setLocation(rival.getX(), rival.getY() - 50);
       applesCaughtLabel.setValue(rival.getApplesCaught());
   }
   
   public void setRivalColorBlue()
   {
       rival.setColorBlue();
   }
   
   public void setRivalColorRed()
   {
       rival.setColorRed();
   }
   
   public void setRivalColorGreen()
   {
       rival.setColorGreen();
   }
}
