import greenfoot.*;  // (World, Actor, GreenfootImage, Greenfoot and MouseInfo)


public class Bar extends Actor
{
    private final int height = 20;
    private final int width = 80; 
    private GreenfootImage barImg;
	private GreenfootImage label;
	private GreenfootImage valueText;
    private int maxValue;
    private int value;
    private String text; 
    private Color barColor;
    private Color textColor;
    
     public Bar(String text, int maxValue, int value)
    {
        this.text = text;
        this.maxValue = maxValue;
        this.value = value;            
        barColor = Color.BLUE;
        textColor = Color.WHITE;
        drawBar(); 
        combineComponents();
        
    }    
   
    public void act() 
    {
        combineComponents();
    }  
    
     private void drawFrame()
    {
        barImg.setColor(Color.WHITE);
        barImg.drawRect(0, 0, width - 1, height -1 );
        barImg.drawRect(1, 1, width - 2, height -2 );
        barImg.drawRect(2, 2, width - 4, height -4 );
    }
    
    private void drawBar()
    {
        int full = width - 6;
        int empty = 0;
        int barStop = (value * full) / maxValue;
        
        barImg = new GreenfootImage(width, height);
        drawFrame();
        barImg.setColor(barColor);
        barImg.fillRect(3, 3, barStop, height - 6);   
        
        
    }
    
    private void combineComponents()
    {
        drawBar();
		drawLabelText();
		drawValueText();

		int width = label.getWidth() + barImg.getWidth() + valueText.getWidth();
        GreenfootImage bar = new GreenfootImage(width, height);
        bar.drawImage(label, 0, 0);
        bar.drawImage(barImg, label.getWidth(), 0);
        bar.drawImage(valueText, barImg.getWidth() + label.getWidth(), 0);
        setImage(bar);
    }
    
    private void drawLabelText()
    {
        label = new GreenfootImage(text + " ", 20, textColor, null);
    }
    
    private void drawValueText()
    {
        valueText = new GreenfootImage(" " + Integer.toString(value), 20, textColor, null);
    }
    
    public void setValue(int value)
    {
        this.value = value;
    }
    
    public void setBarColor(Color barColor)
    {
        this.barColor = barColor;
    }
    
    public void setTextColor(Color textColor)
    {
        this.textColor = textColor;
    }
}
