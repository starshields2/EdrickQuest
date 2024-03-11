VAR JMorale = 0
VAR YMorale = 0
VAR EdrickPoints = 100


->Jasper 

==Jasper

+[If you don't start defending Yael, she could die.] 
{JMorale < 3: I don't care. I'll do this myself.}
{JMorale >= 3: I understand. This is a special occasion!}

*{EdrickPoints > 1} [All you have to do is like, stand in front of her.]
She should stand in front of herself! Wait. 
~decreaseJMorale(1)
~decreaseEdrickPoints(1)
**[Hey, now you're just grasping at straws.]
No, I didn't get to say what I wanted to! 
**[Okay, try again.]
...no, don't worry about it.
~decreaseEdrickPoints(1)
->Jasper

*{EdrickPoints >1} [If Yael dies, you can't make fun of her stupid bag!]
~decreaseEdrickPoints(1)
~increaseJMorale(1)
SO TRUE. I'll give it some thought! 

-Enough talk, I'm fighting here! Bother me later!
->DONE

==function increaseJMorale(amount)
~JMorale = JMorale + amount

==function decreaseEdrickPoints(amount)
~EdrickPoints = EdrickPoints - amount

==function decreaseJMorale(amount)
~JMorale = JMorale -1