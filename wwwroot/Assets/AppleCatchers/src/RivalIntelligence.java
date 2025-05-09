import greenfoot.*;
import java.util.List;
/**
 * Write a description of class RivalIntelligence here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class RivalIntelligence extends Actor
{
   private Contestant rival;
    
   public RivalIntelligence(Contestant rival)
   {
       this.rival = rival;
       
       getImage().setTransparency(0);
   }
   
   public void act()
   {
       move();
       jumpTowardApple();
   }
   
   private Apple findClosestApple()
   {
       List<Apple> apples = (List<Apple>) getWorld().getObjects(Apple.class);
       if(!apples.isEmpty())
       {
          Apple closest = apples.get(0);
          for(Apple a : apples)
          {
              if(Math.abs(a.getX() - rival.getX()) < Math.abs(closest.getX() - rival.getX()))
              {
                  closest = a;
               }
          }
          return closest;
       }
       return null;
   }
   
   private void move()
   {
       Apple closest = findClosestApple();
       if(closest != null)
       {
           if(closest.getX() - rival.getX() < -20)
           {
               rival.moveLeft();
           }
           
           else if(closest.getX() - rival.getX() > 20)
           {
               rival.moveRight();
           }
       }
   }
   
   private void jumpTowardApple()
   {
       Apple closest = findClosestApple();
       int jumpRange = Greenfoot.getRandomNumber(200) + 100;
       if( closest != null && Math.abs(closest.getX() - rival.getX()) < 30 && closest.getY() > rival.getY() - 150 )
       {
           rival.jump();
        }
   }
}
