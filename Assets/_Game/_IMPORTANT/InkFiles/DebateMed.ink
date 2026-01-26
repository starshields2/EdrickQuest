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
    
    +[Reacting rashly didn’t help our image either.]
#Yael 
It was a rash decision, I won’t lie. But I was only trying to help. We can’t invite any bad omens on our journey with unwashed hands.
++[What motivated you to go so far as to lay hands on her?]
#Yael
Well, I told her to put the hotcakes down and clean her hands, and it was as if she had cotton stuck in her ears. So I just. Acted on it.
	+++[Regardless of how put off you feel, Jasper is your colleague. It would be just as unfair of you to disrespect her space.]
#Yael
You do know I despise it when you start making sense, right Edrick?
++++[I think I can tell.] ->COOLOFF
	+++[I think you should apologize for making her drop the food.] 
Yael: And I think you should stop telling me what to do.
++++[Right, sorry.] -> COOLOFF
++++[Unfortunately my friend, that is my job.] ->COOLOFF
++ [I understand Moonwalkers have rituals they must perform before eating. Jasper shouldn’t have to abide by those.]
#Yael
Well, I shouldn’t have to witness her sacrilege at every other moment, either.
+++[Maybe you should look away next time?] ->COOLOFF
+[I think Jasper is a little frustrated by your vocabulary.]
#Yael
It’s certainly not my fault. We do have state of the art education in the city. 

   
->DONE

=JASPER
#Jasper
Jasper: Why’s Yael so concerned with how prim and proper we look anyway? We’re trudging through dirt. 
+[Can you share a bit more about this argument from your perspective, Jasper?]
#Jasper: 
There’s not much to share. I was offered food, I said “thank you,” and I tried to eat it, before Yael decided I should starve.
++[In Yael’s sect, hand washing is a ritual that cannot be forgone before eating. Doing so could invite chaos and ill-intended spirits from the hands into the body.]
#Jasper
I don’t really care what Moonwalkers do before eating. We don’t do that. 
+++[ You don’t have to do that. I’m just letting you know.] 
#Jasper
Well, why don’t you tell her that I’m not going to abide by her rules. So she can shove it.
++++[How about we loop back around to this?] ->COOLOFF
+++[Could you at least wipe or wash your hands before eating next time?]
#Jasper
Why are you asking me to compromise first? She’s literally insulting me left and right. I deserve some respect! 
++++[I understand. I'll ask her about this.] ->COOLOFF
++[You seem quite irritated about this. May I ask why?]
Jasper: Few people enjoy being roughhoused, Edrick. And besides that. She keeps saying things she knows I’m not going to understand.
+++[Like what? Elaborate?]
#Jasper
Can you believe she asked me to have a proper “decorum?” What does decorating anything have to do with this?
++++[…I see. Did she tell you what that actually means?]
#Jasper
No! And she’s been doing that on purpose - using fancy language just to complicate everything. 
+++++[How about we bring that up when we regroup?] ->COOLOFF

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