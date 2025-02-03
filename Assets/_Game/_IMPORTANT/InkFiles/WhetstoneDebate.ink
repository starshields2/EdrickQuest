VAR YMorale = 5
VAR JPoints = 5
VAR medPoints = 100

VAR avoidanceSP = 0
VAR competSP = 0
VAR acommoSP = 0
VAR collabSP = 0
VAR comproSP = 0

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
What should I do?
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
 *[We'll die without those whetstones.]
 ~decreaseYMorale(3)
  Who knows when I'll get another chance to pray? The gods will forsake me. 
  ++[Continue] -> MEDIATIONSTART

 
->DONE

=JASPER
#Jasper
If you're here to talk sense into me, I have plenty to spare.
 +[Nevermind.] ->MEDIATIONSTART
 *[You should respect the holy site. This is Yael's culture.]
 #Jasper
 ~decreaseJPoints(1)
 Who cares about culture right now? Can the gods reach you in this hellhole?
 ++[Continue] -> MEDIATIONSTART
 *[Can't Yael pray at the stone before you remove it?]
 ~increaseJPoints(1)
 {JPoints < 3: We don't have time for that. #Jasper}

{JPoints >= 3: ...fine. Make it quick. #Jasper}
 ++[Continue] -> MEDIATIONSTART
 
 
->DONE  

=COMP
 {JPoints < 3: I don't see why this needed to be a conversation. #Jasper}
{JPoints >= 3: Well, as long as it's over with quickly. #Jasper}
+[Continue]
#Yael
 {YMorale < 3: You were going to destroy it anyway. #Yael}
{YMorale >= 3:Let's just talk this out. #Yael}
++[How about Yael just prays before the shrine is destroyed?]
~compromise(1)
->DONE
++[Jasper, why don't we look for more whetstones somewhere else?]
~accomodate(1)
#Jasper
When we die in this maze, it will not be my fault. You'll have the Moonwalker to thank for that.
->DONE
++[You two figure it out on your own, this is stupid.]
~avoid(1)
#Yael
I will pray wherever I please. Do see to it that you don't interrupt me again, Jasper.
->DONE

->END

==function increaseYMorale(amount)
~YMorale = YMorale + amount
==function decreaseYMorale(amount)
~YMorale = YMorale - amount

==function increaseJPoints(amount)
~JPoints = JPoints + amount
==function decreaseJPoints(amount)
~JPoints = JPoints - amount

==function accomodate(amount)
~acommoSP = acommoSP + amount

==function compete(amount)
~competSP = competSP + amount

== function avoid(amount)
~avoidanceSP = avoidanceSP + amount

==function collab(amount)
~collabSP = collabSP + amount

==function compromise(amount)
~comproSP = comproSP + amount