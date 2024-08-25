VAR JPoints = 0
VAR YPoints = 0
VAR medPoints = 100


What do you mean, Edrick is dead. He's our guide.# Yael

+[Continue] -> OPENER
-> DONE

=OPENER
# Jasper
It was your fault for trying to float our wagon. Why did he listen to you of all people!
+[Continue] ->continue1
=continue1
# Yael
Why did you take us to this gods forsaken river in the first place?
+[Continue]->continue2
->DONE

=continue2
# Jasper
Because we would have frozen to death in the mountains! We would have DIED.
+[Continue] ->continue3
->DONE

=continue3
#Yael
Well look who did die, thanks to you.
+[Continue] ->continue4
->DONE

=continue4
#Edrick
...
+[Continue] ->continue5

=continue5
# Jasper
...Edrick. 
+[Continue] ->continue6
->DONE
=continue6
#Yael
...Don't you think it's a little rude to float so close? Don't you have a purgatory appointment or something?
+[Continue] ->JaspContine

=JaspContine
What the fuck Yael, don't be rude.
#Jasper
+[Stop Listening] ->START
->DONE

=START
#Edrick
+[Mediate] Remember, the Artifact needs both of you. ->Jasper
+[Convince Jasper]
{JPoints >= 3: I understand. #Jasper}

{JPoints < 3: I'm going alone. End of story. #Jasper}
++[Continue] ->EndOfConvo


+[Convince Yael]
{YPoints < 3: I'm not going anywhere with her... #Yael}

{YPoints >= 3: ...fine. I suppose this is the time to break the rules. #Yael}
++[Continue] ->EndOfConvo
*[So, When's My Funeral?]
#Yael
Oh, sorry Edrick, I wasn't aware ghosts could be stowaways.
++[Continue]
~increaseYPoints(1)
~increaseJPoints(1)
->funeral

=funeral
#Jasper
I don't know what to tell you, don't have a heart attack next time.
**[Tough crowd.] ->START

->DONE

=Jasper
++[Let me talk to Yael.] ->Yael
++[I need some time.] ->START
    ++[Ask about the journey]
    #Edrick
    Have you ever made this journey alone before? 
    +++[Continue]
    #Jasper
    Should I be afraid of some snow and ice? It's a mountain, for the gods' sakes.
    ++++[Continue] ->next
    =next
    I'd be more concerned about that princess there, she could break a nail. I don't think she's ever been outside the city walls. 
    #Jasper
    ****[You need Yael.] You are well aware that you need Yael to complete the Artifact.
    ~decreaseJPoints(1)
    What I need is some peace and quiet, and someone who doesn't insult me every fifteen minutes.
    ->Jasper
    ****[Everyone would die without you both.] 
    #Edrick
    Oh yes Jasper, go ahead. Go alone, and let everyone share my fate.
    ~increaseJPoints(5)
    +++++[Continue]
    #Jasper
    ...no. I understand what you're trying to tell me.
    ->Jasper
      ->DONE
    ***[Yael thinks you're dirty.] 
    #Edrick
    Yael keeps saying you're dirty. I know you have your own methods, but maybe if you meet her halfway she'll be a little more reasonable?
    Yael doesn't know what she's talking about. This scent is hard earned from so many hours sunbathing. It means I go outside and get fresh air instead of keeping my nose in a book.
    *****[Do you think you could help her clean her staff?]  
    If it'll get her to stop nagging at me, sure.
    ~increaseJPoints(5)
    ->Jasper
    ->DONE
    
    =Yael
#Yael
I'm not going anywhere with her, she'll put her grimy hands all over my stuff!
   ++[Let me Talk to Jasper.] ->Jasper
   ++[Let me Think.] ->START
  *[Be Direct] 
  #Edrick
  You can't let some hygiene issues keep you from making this journey. 
  ~increaseYPoints(2)
  **[Continue]
  #Yael
  I'm just not used to people so... uncaring of their presence, Edrick.
  ***[Jasper isn't uncaring.]

  #Edrick
  Jasper cares very much. it's why she gets so upset when you scold her.

  ****[Continue]
  #Yael
  Well. It's not charming, I'll tell you that.
*****[I'm sure.] ->START

 *[Be Reasonable]
  ~decreaseYPoints(1)
 #Edrick
 Your hands are just as dirty, and they're going to get dirtier on the journey.

 **[Continue]
 #Yael
 Nonsense, I have very clean hands, and I never go anywhere without my sanitizer.
  ***[Honesty]
 #Edrick
 You can't refuse this journey because of a few germs.
 ****[Continue]
 #Yael
 I suppose you're correct. As long as she doesn't touch my staff again.
 
 ~increaseYPoints(8)
 *****[Let's regroup.] ->Yael
 ***[Compromise] 
 #Edrick 
 What if Jasper washes her hands every thirty minutes, under your supervision? 
 ~increaseYPoints(4)
 ****[Continue]
 #Yael
 You can't be serious, but I'll humor you. It WOULD make me feel better. 
 *****[Continue] -> START

 *[City Walls]
 #Edrick 
 You may need someone to do the punching and kicking when you encounter a wild animal or demon.
 **[Continue]
 #Yael
 Don't be ridiculous! You of all people should know Moonwalkers are well versed in survival, and we don't need to shed blood to do it.
 ~decreaseYPoints(2)
 ***[Continue]
 ->Yael
 
    ->DONE
    
    
=EndOfConvo
#Edrick
A shaky truce will have to do. We can keep moving, at least.

->DONE
    
    
    ==function increaseYPoints(amount)
~YPoints = YPoints + amount

==function decreaseMedPoints(amount)
~medPoints = medPoints - amount

==function decreaseYPoints(amount)
~YPoints = YPoints - amount

==function increaseJPoints(amount)
~JPoints = JPoints + amount

== function decreaseJPoints(amount)
~JPoints = JPoints - amount