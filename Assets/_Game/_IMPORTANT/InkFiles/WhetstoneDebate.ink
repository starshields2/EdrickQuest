VAR YMorale = 5
VAR JPoints = 5
VAR medPoints = 100

#Yael
Look, in the bushes. A shrine. I should send a prayer to Elunia immediately.
+[Continue.]
#Jasper
A solar whetstone! Once I remove it from this rock, my tools will be sharp for weeks.
++[Continue]
#Yael
You can't possibly want to use this for sharpening tools. This is a sacred place.
+++[Contine]
#Jasper
You see the gleaning whetstone, yes? Dead gods can't help us, steel can. 
++++[Step In]  ->MEDIATIONSTART

=MEDIATIONSTART
#Edrick
+[Talk to Yael] ->YAEL
+[Talk to Jasper] ->JASPER
+[Regroup] ->COMP
->DONE

=YAEL
#Yael
And what do you want, ghost?
 +[Nevermind.] ->MEDIATIONSTART
 *[This shrine seems very important to you.]
   ~increaseYMorale(2)
 Of course it is. I just don't know when the next chance I'll get to pray will be. 
 **[I'm sure it's not the only shrine on this path.]
~decreaseYMorale(1)
    You say that so that Jasper can take the whetstone and destroy the shrine. 
    +++[Continue] -> MEDIATIONSTART
  **[I'm assuming that's why it has to be this shrine?]
  Exactly. 
  ~increaseYMorale(2)
  +++[Continue] -> MEDIATIONSTART
 *[Preparedness for the journey ahead is more important than the shrine.]
 ~decreaseYMorale(3)
  Who knows when I'll get another chance to pray? The gods will forsake me. 
  ++[Continue] -> MEDIATIONSTART

 
->DONE

=JASPER
#Jasper
If you're here to talk sense into me, I have plenty to spare.
 +[Nevermind.] ->MEDIATIONSTART
 *[You should respect the holy site. This is Yael's culture.]
 ++[Continue] -> MEDIATIONSTART
 *[Can't Yael pray at the stone before you remove it?]
 ++[Continue] -> MEDIATIONSTART
 
->DONE  

=COMP
->DONE

->END

==function increaseYMorale(amount)
~YMorale = YMorale + amount
==function decreaseYMorale(amount)
~YMorale = YMorale - amount