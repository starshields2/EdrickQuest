VAR YMorale = 5
VAR JPoints = 5
VAR medPoints = 24
                                 
VAR avoidanceSP = 0
VAR competSP = 0
VAR acommoSP = 0
VAR collabSP = 0
VAR comproSP = 0

#Yael
What is that limping thing in the shadows?
+[Continue.]
#Jasper
It's just some dying animal. Let's keep moving. I'm hungry.
++[Continue]
#Yael
Are you insane? We have to get this creature back on its feet.
+++[Contine]
#Jasper
I'm not stopping for that mutt unless we're eating it.
++++[Step In]  ->MEDIATIONSTART

=MEDIATIONSTART
#Edrick
Another meaningless squabble.
+[Talk to Yael] ->YAEL
+[Talk to Jasper] ->JASPER
+[Regroup] ->COMP
->DONE

=YAEL
#Yael
You'd think that brute at least has compassion for animals.
 +[Nevermind.] ->MEDIATIONSTART
 *[]
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
~decreasemedPoints(10)
(You have {medPoints} points).
->DONE
++[Jasper, why don't we look for more whetstones somewhere else?]
~increasemedPoints(20)
#Jasper
When we die in this maze, it will not be my fault. You'll have the Moonwalker to thank for that.
(You have {medPoints} points).
->DONE
++[You two figure it out on your own, this is stupid.]
#Yael
I will pray wherever I please. Do see to it that you don't interrupt me again, Jasper.

(You have {medPoints} points).
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

==function increasemedPoints(amount)
~medPoints = medPoints + amount

==function decreasemedPoints(amount)
~medPoints = medPoints - amount