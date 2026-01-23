VAR core = 0
VAR needs = 0
VAR bounds = 0
VAR violations = 0
VAR commonalities = 0
VAR YaelTell = 0
VAR medPoints = 0
LIST AllNotes = Respect, Friendship, GiftEtiquitte, Food
LIST CurrentNotes = Food
VAR CorrectNote = Respect 

#Jasper 
You’re not seriously stopping us for this, are you? We’ve already lost precious time. If we are late for the Ritual, we’ll have set out for nothing. 
+[Continue]

#Yael
If Edrick won’t stop us for this, I will. No Fate-struck person should be as ill mannered and gross as you’re acting right now, especially not during the Ritual.
++[Continue]

#Jasper
I’m ill mannered? You made me drop food onto the ground! 
+++[Continue]

#Yael
I had to act quickly, before you ruined the food by inhaling it!
++++[Continue]

#Jasper
It’s food, that’s what you’re supposed to do! 
+++++[Continue]
#Edrick
Calm yourselves. Please confide in me, so I can help you two walk in each others’ shoes.
++++++[Continue] -> COOLOFF

=COOLOFF
#Edrick
+[Talk to Yael]-> Yael
+[Talk to Jasper] ->JASPER
+[Regroup]  -> REGROUP
->DONE

=Yael
#Yael
You don’t understand, Edrick. She’s making us look like idiots out there. 

+[Why do you think that?]

#Yael
We are not average people. Not anymore. Our decorum around villagers should reflect that. How can they look up to us as Fate-struck if we’re taking handouts from everyone we cross?
	++[Are they handouts, or offerings of goodwill?]
#Yael
What’s the difference? We shouldn’t be so eager to show that we are fragile. They may lose faith in us - it’s better to kindly refuse a gift than to show dependence on it.
~CurrentNotes = CurrentNotes + Respect
	+++[Our gods have chosen us for a reason. Any god fearing person will also have faith in us.]
#YaelPensive
Well, that’s certainly true. Though it does little to ease my nerves.
	++++[It sounds like you’re concerned about giving away our apprehensions about this journey, is that right?] 
#YaelTell
That’s right. I mean. Think about it. This kind of thing has never happened before. Fate-struck are usually friends, or at least amiable. But her… I can’t stand her. 
    ***** [Continue]
    {YaelTell == 1: Don't poke at me.} 
    {YaelTell == 2: Maybe there's something more to this?} 
    #Yael
    Well, don't you think it's weird that the Tether has chosen two people who despise each other?
    ++++++[That is weird.] ->COOLOFF
   
->DONE

=JASPER
#Jasper
Don't try and probe me, I don't like being prodded at like a child.
+[Okay.] 
#Jasper
Ugh, few people enjoy being roughhoused, Edrick. Did you take me for a barbarian or something?
++[Why would I take you for a barbarian?] ->END
++[No, I'm sorry.] ->END
->DONE

=REGROUP
#Edrick
I think I know what's wrong. 
+[Continue] 
#Yael
And what is that, Edrick?
++[There is one thing you both value most.] ->CORECHOOSER
++[This is something you must figure out for yourselves.] -> COOLOFF

=CORECHOOSER
#Edrick
***[Respect] ->RESPECT
***[Friendship] ->FRIENDS
***[The mission] ->MISSIONSTATEMENT
+++[Unsure] -> COOLOFF
->DONE

=RESPECT
#Edrick
You both wish to be respected, both by each other and by those who depend on you during this quest. You just haven't communicated what will earn respect from each other; if you could be clear about that now, I'm sure this will be resolved.
+[Continue] 

->DONE
=FRIENDS
#Jasper
Yael and I don't need to be friends for this to work. And furthermore, I wouldn't be friends with someone who can't go two seconds without insulting me. 
+[Continue]
#Yael
This journey isn't about making friends, Edrick. Did the gods send someone so daft to help us, really?
++[There must be something they can agree on...] ->COOLOFF
->DONE
=MISSIONSTATEMENT
->DONE

==function YaelTellFalse(amount)
~YaelTell = 1
~return YaelTell

==function YaelTellTrue(amount)
~YaelTell = 2