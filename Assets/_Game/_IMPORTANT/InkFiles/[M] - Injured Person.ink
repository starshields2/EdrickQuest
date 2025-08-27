VAR YMorale = 0
VAR JPoints = 0

VAR medPoints = 0

VAR violations = 0
VAR commonalities = 0

#Edrick
A man lies in the shadows, breaths grasping at the walls like clenched fingers. He will not last much longer. The equinox already consumes him, speckled stars choking his thin throat. Across his neck lies the signature pendant of the Moonwalker Clerics.
+[Continue.]
#Edrick
Please put me out of my misery.
++[Continue]
#Yael
Impossible. You swore an oath, and you will keep it, brother.
+++[Contine]
#Jasper
His life is over, there's no need to stay the course with such cruel rituals! I'll do it. Turn away, princess.
++++[Step In]  ->MEDIATIONSTART

=MEDIATIONSTART
#Edrick
{~What should I do?|Do I stop them? | Should I intervene?}
+[Yael, let him go. He is suffering.] ->YAEL
+[Jasper, reconsider this.] ->JASPER
+[(Do nothing.)] ->COMP
->DONE

=YAEL
#Yael
There are some things you are still to understand, travleler. He swore an oath. Now he will burn in Huthshank.
  +[This is different.] -> DONE
->DONE

=JASPER
#Jasper
Jasper raises her knife, sweat beading at her brow as she kneels in front of the dying man. 
 +[He swore an oath, Jasper.]
  #Jasper
An oath to uncaring gods. He's asking for freedom from this.
 ++[Your ignorance of the gods only makes them invisible to you.]
 Japser puts her knife away.
 +++[End] ->DONE
 +[You chose to do this. So do it.]
#Jasper
Jasper plunges her knife into the man's throat. Nothing but tarry, glittering blood seeps out. She must harden her gaze. That was no longer mage or Moonwalker. 
 ++[We have to keep going.] ->DONE
 
 
 
 
->DONE  

=COMP
[They argue]

->END

==function flagValue(amount)
~violations = violations + amount

==function flagCommon(amount)
~commonalities = commonalities + amount

==function decreaseJPoints(amount)
~JPoints = JPoints - amount

==function increasemedPoints(amount)
~medPoints = medPoints + amount

==function decreasemedPoints(amount)
~medPoints = medPoints - amount