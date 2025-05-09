public class Gravitator
{
    private int maxFallSpeed;
    private int fallDelay;
    private int fallCounter;
    
        
    public Gravitator()
    {
        maxFallSpeed = -50;
        fallDelay = 5;
        fallCounter = 0;
    }
    
    
    public void gravitate(GravityObject gravityObject)
    {
        fallMovement(gravityObject); 
    }
    
    protected void fallMovement(GravityObject gravityObject)
    {
        int unit = 1;
        if(gravityObject.getYSpeed() > 0)
        {
            unit = unit * -1;
        }          
        
        if(gravityObject.getYSpeed() > 0 || !isLanded(gravityObject))
        {
            for(int i = 0; i <= Math.abs(gravityObject.getYSpeed()); i = i + 1)
            {
                gravityObject.setLocation(gravityObject.getX(), gravityObject.getY() + unit);
                gravityAcceleration(gravityObject);
                if(isLanded(gravityObject))
                {
                    break;
                }
            }
        }        
    }    
    
     protected void gravityAcceleration(GravityObject gravityObject)
    {
        if(isLanded(gravityObject))
        {
            fallCounter = 0;
            gravityObject.setYSpeed(0);
        }
        
        else if(fallCounter % fallDelay == 0 && gravityObject.getYSpeed() >= maxFallSpeed)
        {
           gravityObject.setYSpeed(gravityObject.getYSpeed() - 1);
           fallCounter = fallCounter + 1;
        }
        
        else{
            fallCounter = fallCounter + 1;
        }
    }
     
       
    public boolean isLanded(GravityObject gravityObject)
    {
        if(getLandedOn(gravityObject) != null && gravityObject.getYSpeed() <= 0 )
        {
            return true;
        }
        return false;
    }    
  
    public Landable getLandedOn(GravityObject gravityObject)
    {
        int distance = getImageDistanceFromLandable(gravityObject);
        Landable landable = (Landable) gravityObject.getOneObjectAtOffset(0, distance, Landable.class);
        return landable;
    }
    
    public int getImageDistanceFromLandable(GravityObject gravityObject)
    {
        return gravityObject.getImage().getHeight()/2;
    }
}