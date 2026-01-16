VAR YMorale = 0
VAR JPoints = 0

VAR medPoints = 0

VAR violations = 0
VAR commonalities = 0

#Yael
Look, in the bushes. A shrine. I should send a prayer to Elunia immediately.
+[Continue.]
#Jasper
A solar whetstone. Once I remove it from this rock, my tools will be sharp for weeks.
++[Continue]
**[Whetstone?]
#Yael
You can't possibly want to use this for sharpening tools. This is a sacred place.
+++[Contine]
#Jasper
You see the gleaning whetstone, yes? Dead gods can't help us, steel can. 
++++[Step In]  ->MEDIATIONSTART

=MEDIATIONSTART
#Edrick
{~What should I do?|How do I make them see eye to eye? | Another squabble.}
+[Talk to Yael] ->YAEL
+[Talk to Jasper] ->JASPER
+[Regroup] ->COMP
->DONE

=YAEL
#Yael
{~And what do you want, ghost?|Coming to bother me again? |Let's hope Jasper's ears are as sharp as her skull is thick.}
 +[Nevermind.] ->MEDIATIONSTART
 *[This shrine seems very important to you.]
 Of course it is. I just don't know when the next chance I'll get to pray will be. 
 **[Perhaps you should tell that to Jasper.]
~flagValue(1)
    She would never understand. The Sunblades have no desire to pray. 
    +++[Continue] -> MEDIATIONSTART
  **[I'm assuming that's why it has to be this shrine?]
  Exactly. If only the brute could see it that way.
  +++[Continue] -> MEDIATIONSTART
 *[Jasper is only worried that we could die without those whetstones.]
 ~flagValue(1)
  Who knows when I'll get another chance to pray?! The gods will forsake me. 
  **[Maybe Jasper is worried about the same thing.]
  ~flagCommon(2)
  ...Perhaps you are right.
  +++[Continue] ->MEDIATIONSTART
  ++[Alright, whatever.] -> MEDIATIONSTART

 
->DONE

=JASPER
#Jasper
If you're here to talk sense into me, I have plenty to spare.
 +[Nevermind.] ->MEDIATIONSTART
 *[You should respect the holy site. This is Yael's culture.]
 #Jasper
 ~flagValue(1)
 Who cares about culture right now? Can the gods reach you in this hellhole?
 ++[Continue] -> MEDIATIONSTART
 *[Can't Yael pray at the stone before you remove it?]

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
~flagCommon(3)
->DONE
++[Jasper, why don't we look for more whetstones somewhere else?]
~flagValue(1)
#Jasper
When we die in this maze, it will not be my fault. You'll have the Moonwalker to thank for that.
->DONE
++[You two figure it out on your own, this is stupid.]
#Yael
I will pray wherever I please. Do see to it that you don't interrupt me again, Jasper.
~flagValue(6)
You ended with {violations} flagged values.
->DONE

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