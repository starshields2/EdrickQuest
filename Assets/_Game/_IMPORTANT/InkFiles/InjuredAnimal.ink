VAR violations = 0
VAR commonalities = 0
VAR medPoints = 70
VAR experience = 0
VAR hound = 0
VAR prep = 0
VAR overhear = 0
VAR misconception = 0
                                 
VAR avoidanceSP = 0
VAR competSP = 0
VAR acommoSP = 0
VAR collabSP = 0
VAR comproSP = 0

//Exposure
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
Another meaningless squabble...
+[Talk to Yael] ->YAEL
+[Talk to Jasper] ->JASPER
+[Regroup] ->COMP
->DONE

=YAEL
#Yael
You'd think that brute at least has compassion for animals.
 +[Nevermind.] ->MEDIATIONSTART
 *[Maybe she's never had a pet.]
 The Sunblades are too busy bonking each other over the head with the blunt end of their greatswords to care for animals they don't have a use for. They don't even name their sled dogs.
 **[You're very passionate about this.]
    Of course I am! If I leave this silverpaw to die, we may as well throw ourselves into the nearest ditch. There is no coming back from turning the other cheek to Elunia's sacred pack. 
    ***[Sacred pack?]
    One of our own, Barley, left a silverpaw to die out in the cold. His right hand became hard and frozen the next day, and never recovered. I refuse to suffer a similar curse.
    ++++[Continue] -> MEDIATIONSTART
  **[Would Jasper like to overhear that?]
  Maybe hearing it would expose her to some sense. 
  ~overheard(1)
  +++[I'll pass the message on.] -> MEDIATIONSTART
 *[How long will it take to heal the dog?] 
 //prep 1
 #Yael
 A ritual like this should only take about an hour, give or take.
 ~enablePrep(1)
 ++[Continue] ->MEDIATIONSTART
// * {JPoints > 3} [Jasper is extremely nervous.]
  She should have said that... 
  ++[Continue] -> MEDIATIONSTART

 
->DONE

=JASPER
#Jasper
So are we eating that animal or what? ( {overhear} points)
 +[Nevermind.] ->MEDIATIONSTART

 *{overhear > 0} [Yael said the Sunblades don't even name their sled dogs?] 
Well that's just false, makes sense coming from a cleric though. All sled dogs share a common name, from which pack they come from. 
++[Ah. A misconception.] ->MEDIATIONSTART
+[What's bothering you?]
If we spend time and resources on this mutt, we'll never make it. We should just leave it.
 ++[Continue] -> MEDIATIONSTART
 
 
->DONE  

=COMP
 {violations < 3: I don't see why this needed to be a conversation. #Jasper}
{violations >= 3: Well, as long as it's over with quickly. #Jasper}
+[Continue]
#Yael
 {commonalities < 3: You were going to destroy it anyway. #Yael}
{commonalities >= 3:Let's just talk this out. #Yael}
 ++{misconception > 0} [Jasper does respect these animals. She is concerned for our own safety in this moment.] 

(You have {medPoints} points).
->DONE
++[I'm making an executive decision. We leave the animal.]

#Jasper
When we die in this maze, it will not be my fault. You'll have the Moonwalker to thank for that.
(You have {medPoints} points).
->DONE
++[You two figure it out on your own, this is stupid.]
#Yael
I will pray wherever I please. Do see to it that you don't interrupt me again, Jasper.

->DONE

->END



==function enablePrep(amount)
~prep = prep + amount

==function overheard(amount)
~overhear = overhear + amount

==function misconceptionIncrease(amount)
~misconception = misconception + amount